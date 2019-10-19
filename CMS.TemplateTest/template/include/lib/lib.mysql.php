<?php

class lib_mysql
{

		public $link_id = NULL;
		public $queryCount = 0;
		public $queryTime = "";
		public $queryLog = array( );
		public $max_cache_time = 300;
		public $cache_data_dir = "data/_caches/";
		public $root_path = "";
		public $error_message = array( );
		public $platform = "";
		public $version = "";
		public $dbhash = "";
		public $starttime = 0;
		public $timeline = 0;
		public $timezone = 0;
		public $mysql_config_cache_file_time = 0;
		public $mysql_disable_cache_tables = array( );

		public function __construct( $dbhost, $dbuser, $dbpw, $dbname = "", $charset = "utf8", $pconnect = 0 )
		{
				if ( defined( "PHPOE_ROOT" ) && !$this->root_path )
				{
						$this->root_path = PHPOE_ROOT;
				}
				if ( $pconnect )
				{
						if ( !( $this->link_id = mysql_pconnect( $dbhost, $dbuser, $dbpw ) ) )
						{
								$this->errormsg( "Can't Connect MySQL Server(".$dbhost.")!" );
								return FALSE;
						}
				}
				else
				{
						if ( "4.2" <= PHP_VERSION )
						{
								$this->link_id = mysql_connect( $dbhost, $dbuser, $dbpw, TRUE );
						}
						else
						{
								$this->link_id = mysql_connect( $dbhost, $dbuser, $dbpw );
								mt_srand( ( double )microtime( ) * 1000000 );
						}
						if ( !$this->link_id )
						{
								$this->errormsg( "Can't Connect MySQL Server(".$dbhost.")!" );
								return FALSE;
						}
				}
				$this->dbhash = md5( $this->root_path.$dbhost.$dbuser.$dbpw.$dbname );
				$this->version = mysql_get_server_info( $this->link_id );
				if ( "4.1" < $this->version )
				{
						if ( $charset != "latin1" )
						{
								mysql_query( "SET character_set_connection=".$charset.", character_set_results={$charset}, character_set_client=binary", $this->link_id );
								//echo "SET character_set_connection=".$charset;				
						}
						if ( "5.0.1" < $this->version )
						{
								mysql_query( "SET sql_mode=''", $this->link_id );
						}
				}
				$sqlcache_config_file = $this->root_path.$this->cache_data_dir."sqlcache_config_file_".$this->dbhash.".php";
				@include( $sqlcache_config_file );
				$this->starttime = time( );
				if ( $this->max_cache_time && $this->mysql_config_cache_file_time + $this->max_cache_time < $this->starttime )
				{
						if ( $dbhost != "." )
						{
								$result = mysql_query( "SHOW VARIABLES LIKE 'basedir'", $this->link_id );
								$row = mysql_fetch_assoc( $result );
								if ( !empty( $row['Value'][1] ) || $row['Value'][1] == ":" && !empty( $row['Value'][2] ) || $row['Value'][2] == "\\" )
								{
										$this->platform = "WINDOWS";
								}
								else
								{
										$this->platform = "OTHER";
								}
						}
						else
						{
								$this->platform = "WINDOWS";
						}
						if ( !( $this->platform == "OTHER" ) && !( $dbhost != "." ) && !( strtolower( $dbhost ) != "localhost:3306" ) && $dbhost != "127.0.0.1:3306" || "5.1" <= PHP_VERSION && date_default_timezone_get( ) == "UTC" )
						{
								$result = mysql_query( "SELECT UNIX_TIMESTAMP() AS timeline, UNIX_TIMESTAMP('".date( "Y-m-d H:i:s", $this->starttime )."') AS timezone", $this->link_id );
								$row = mysql_fetch_assoc( $result );
								if ( $dbhost != "." && strtolower( $dbhost ) != "localhost:3306" && $dbhost != "127.0.0.1:3306" )
								{
										$this->timeline = $this->starttime - $row['timeline'];
								}
								if ( "5.1" <= PHP_VERSION && date_default_timezone_get( ) == "UTC" )
								{
										$this->timezone = $this->starttime - $row['timezone'];
								}
						}
						$content = "<?php\r\n\$this->mysql_config_cache_file_time = ".$this->starttime.";\r\n\$this->timeline = ".$this->timeline.";\r\n\$this->timezone = ".$this->timezone.";\r\n\$this->platform = '".$this->platform."';\r\n?>";
						@file_put_contents( $sqlcache_config_file, $content );
				}
				if ( $dbname )
				{
						if ( mysql_select_db( $dbname, $this->link_id ) === FALSE )
						{
								$this->errormsg( "Can't select MySQL database(".$dbname.")!" );
								return FALSE;
						}
						return TRUE;
				}
				return TRUE;
		}

		public function select_database( $dbname )
		{
				return mysql_select_db( $dbname, $this->link_id );
		}

		public function set_mysql_charset( $charset )
		{
				if ( "4.1" < $this->version )
				{
						if ( in_array( strtolower( $charset ), array( "gb2312", "gbk", "big5", "utf-8", "utf8" ) ) )
						{
								$charset = str_replace( "-", "", $charset );
						}
						if ( $charset != "latin1" )
						{
								mysql_query( "SET character_set_connection=".$charset.", character_set_results={$charset}, character_set_client=binary", $this->link_id );
						}
				}
		}

		public function fetch_array( $query, $result_type = MYSQL_ASSOC )
		{
				return mysql_fetch_array( $query, $result_type );
		}

		public function query( $sql, $type = "" )
		{
				$this->queryCount++;
				$this->queryLog[] = $sql;
				if ( $this->queryTime == "" )
				{
						$this->queryTime = microtime( );
				}
				if ( "4.3" <= PHP_VERSION && $this->starttime + 1 < time( ) )
				{
						mysql_ping( $this->link_id );
				}
				if ( !( $query = mysql_query( $sql, $this->link_id ) ))
				{
						$this->error_message[]['message'] = "MySQL Query Error";
						$this->error_message[]['sql'] = $sql;
						$this->error_message[]['error'] = mysql_error( $this->link_id );
						$this->error_message[]['errno'] = mysql_errno( $this->link_id );
						$this->errormsg( );
						return FALSE;
				}
				if ( defined( "DEBUG_MODE" ) && ( DEBUG_MODE & 8 ) == 8 )
				{
						$logfilename = $this->PHPOE_ROOT."data/mysql_query_".$this->dbhash."_".date( "Y_m_d" ).".log";
						$str = $sql."\n\n";
						if ( "5.0" <= PHP_VERSION )
						{
								file_put_contents( $logfilename, $str, FILE_APPEND );
								return $query;
						}
						$fp = @fopen( $logfilename, "ab+" );
						if ( $fp )
						{
								fwrite( $fp, $str );
								fclose( $fp );
						}
				}
				return $query;
		}

		public function affected_rows( )
		{
				return mysql_affected_rows( $this->link_id );
		}

		public function error( )
		{
				return mysql_error( $this->link_id );
		}

		public function errno( )
		{
				return mysql_errno( $this->link_id );
		}

		public function result( $result, $row )
		{
				return mysql_result( $result, $row );
		}

		public function num_rows( $data )
		{
				return mysql_num_rows( $data );
		}

		public function num_fields( $data )
		{
				return mysql_num_fields( $data );
		}

		public function free_result( $data )
		{
				return mysql_free_result( $query );
		}

		public function insert_id( )
		{
				return mysql_insert_id( $this->link_id );
		}

		public function fetchrow( $data )
		{
				return mysql_fetch_assoc( $data );
		}

		public function fetch_fields( $data )
		{
				return mysql_fetch_field( $data );
		}

		public function version( )
		{
				return $this->version;
		}

		public function ping( )
		{
				if ( "4.3" <= PHP_VERSION )
				{
						return mysql_ping( $this->link_id );
				}
				return FALSE;
		}

		public function escape_string( $unescaped_string )
		{
				if ( "4.3" <= PHP_VERSION )
				{
						return mysql_real_escape_string( $unescaped_string );
				}
				return mysql_escape_string( $unescaped_string );
		}

		public function close( )
		{
				return mysql_close( $this->link_id );
		}

		public function errormsg( $message = "", $sql = "" )
		{
				if ( $message )
				{
						echo "<b>boleme info</b>: ";
						echo $message;
						echo "\n\n";
						exit( );
				}
				echo "<b>MySQL server error report:";
				print_r( $this->error_message );
				exit( );
		}

		public function selectlimit( $sql, $num = 0, $start = 0, $iscache = FALSE )
		{
				if ( $num != 0 )
				{
						if ( $start == 0 )
						{
								$sql .= " LIMIT ".$num;
						}
						else
						{
								$sql .= " LIMIT ".$start.", ".$num;
						}
				}
				return $this->query( $sql );
		}

		public function getone( $sql, $limited = FALSE )
		{
				if ( $limited )
				{
						$sql = trim( $sql." LIMIT 1" );
				}
				$res = $this->query( $sql );
				if ( $res !== FALSE )
				{
						$row = mysql_fetch_row( $res );
						return $row[0];
				}
				return FALSE;
		}

		public function getonecached( $sql, $cached = "FILEFIRST" )
		{
				$sql = trim( $sql." LIMIT 1" );
				$cachefirst = ( $cached == "FILEFIRST" || $this->platform != "WINDOWS" ) && $this->max_cache_time;
				if ( !$cachefirst )
				{
						return $this->getOne( $sql, TRUE );
				}
				$result = $this->getSqlCacheData( $sql, $cached );
				if ( empty( $result['storecache'] ) )
				{
						return $result['data'];
				}
				$arr = $this->getOne( $sql, TRUE );
				if ( $arr !== FALSE && $cachefirst )
				{
						$this->setSqlCacheData( $result, $arr );
				}
				return $arr;
		}

		public function getall( $sql, $iscache = TRUE )
		{
				$res = $this->query( $sql );
				if ( $res !== FALSE )
				{
						$arr = array( );
						while ( $row = mysql_fetch_assoc( $res ) )
						{
								$arr[] = $row;
						}
				}
				else
				{
						return FALSE;
				}
				return $arr;
		}

		public function getallcached( $sql, $cached = "FILEFIRST" )
		{
				$cachefirst = ( $cached == "FILEFIRST" || $this->platform != "WINDOWS" ) && $this->max_cache_time;
				if ( !$cachefirst )
				{
						return $this->getAll( $sql );
				}
				$result = $this->getSqlCacheData( $sql, $cached );
				if ( empty( $result['storecache'] ) )
				{
						return $result['data'];
				}
				$arr = $this->getAll( $sql );
				if ( $arr !== FALSE && $cachefirst )
				{
						$this->setSqlCacheData( $result, $arr );
				}
				return $arr;
		}

		public function getrow( $sql, $limited = FALSE, $iscache = TRUE )
		{
				if ( $limited )
				{
						$sql = trim( $sql." LIMIT 1" );
				}
				$res = $this->query( $sql );
				if ( $res !== FALSE )
				{
						$arr = mysql_fetch_assoc( $res );
						return $arr;
				}
				return FALSE;
		}

		public function getrowcached( $sql, $cached = "FILEFIRST" )
		{
				$sql = trim( $sql." LIMIT 1" );
				$cachefirst = ( $cached == "FILEFIRST" || $this->platform != "WINDOWS" ) && $this->max_cache_time;
				if ( !$cachefirst )
				{
						return $this->getRow( $sql, TRUE );
				}
				$result = $this->getSqlCacheData( $sql, $cached );
				if ( empty( $result['storecache'] ) )
				{
						return $result['data'];
				}
				$arr = $this->getRow( $sql, TRUE );
				if ( $arr !== FALSE && $cachefirst )
				{
						$this->setSqlCacheData( $result, $arr );
				}
				return $arr;
		}

		public function getcol( $sql, $iscache = TRUE )
		{
				$res = $this->query( $sql );
				if ( $res !== FALSE )
				{
						$arr = array( );
						while ( $row = mysql_fetch_row( $res ) )
						{
								$arr[] = $row[0];
						}
				}
				else
				{
						return FALSE;
				}
				return $arr;
		}

		public function getcolcached( $sql, $cached = "FILEFIRST" )
		{
				$cachefirst = ( $cached == "FILEFIRST" || $this->platform != "WINDOWS" ) && $this->max_cache_time;
				if ( !$cachefirst )
				{
						return $this->getCol( $sql );
				}
				$result = $this->getSqlCacheData( $sql, $cached );
				if ( empty( $result['storecache'] ) )
				{
						return $result['data'];
				}
				$arr = $this->getCol( $sql );
				if ( $arr !== FALSE && $cachefirst )
				{
						$this->setSqlCacheData( $result, $arr );
				}
				return $arr;
		}

		public function autoexecute( $table, $field_values, $mode = "INSERT", $where = "", $querymode = "" )
		{
				$field_names = $this->getcol( "DESC ".$table );
				$sql = "";
				if ( $mode == "INSERT" )
				{
						$fields = $values = array( );
						foreach ( $field_names as $value )
						{
								if ( array_key_exists( $value, $field_values ) )
								{
										$fields[] = $value;
										$values[] = "'".$field_values[$value]."'";
								}
						}
						if ( !empty( $fields ) )
						{
								$sql = "INSERT INTO ".$table." (".implode( ", ", $fields ).") VALUES (".implode( ", ", $values ).")";
						}
				}
				else
				{
						$sets = array( );
						foreach ( $field_names as $value )
						{
								if ( array_key_exists( $value, $field_values ) )
								{
										$sets[] = $value." = '".$field_values[$value]."'";
								}
						}
						if ( !empty( $sets ) )
						{
								$sql = "UPDATE ".$table." SET ".implode( ", ", $sets )." WHERE ".$where;
						}
				}
				if ( $sql )
				{
						return $this->query( $sql, $querymode );
				}
				return FALSE;
		}

		public function setmaxcachetime( $second )
		{
				$this->max_cache_time = $second;
		}

		public function getmaxcachetime( )
		{
				return $this->max_cache_time;
		}

		public function getsqlcachedata( $sql, $cached = "" )
		{
				$sql = trim( $sql );
				$result = array( );
				$result['filename'] = $this->root_path.$this->cache_data_dir."sqlcache_".abs( crc32( $this->dbhash.$sql ) )."_".md5( $this->dbhash.$sql ).".php";
				$data = @file_get_contents( $result['filename'] );
				if ( isset( $data[23] ) )
				{
						$filetime = substr( $data, 13, 10 );
						$data = substr( $data, 23 );
						if ( !( $cached == "FILEFIRST" ) && $filetime + $this->max_cache_time < time( ) || $cached == "MYSQLFIRST" && $filetime < $this->table_lastupdate( $this->get_table_name( $sql ) ) )
						{
								$result['storecache'] = TRUE;
								return $result;
						}
						$result['data'] = unserialize( $data );
						if ( $result['data'] === FALSE )
						{
								$result['storecache'] = TRUE;
								return $result;
						}
						$result['storecache'] = FALSE;
						return $result;
				}
				$result['storecache'] = TRUE;
				return $result;
		}

		public function setsqlcachedata( $result, $data )
		{
				if ( $result['storecache'] === TRUE && $result['filename'] )
				{
						@file_put_contents( $result['filename'], "<?php exit;?>".@time( ).@serialize( $data ) );
						clearstatcache( );
				}
		}

		public function table_lastupdate( $tables )
		{
				$lastupdatetime = "0000-00-00 00:00:00";
				$tables = str_replace( "`", "", $tables );
				$this->mysql_disable_cache_tables = str_replace( "`", "", $this->mysql_disable_cache_tables );
				foreach ( $tables as $table )
				{
						if ( in_array( $table, $this->mysql_disable_cache_tables ) )
						{
								$lastupdatetime = "2037-12-31 23:59:59";
						}
						else
						{
								if ( strstr( $table, "." ) != NULL )
								{
										$tmp = explode( ".", $table );
										$sql = "SHOW TABLE STATUS FROM `".trim( $tmp[0] )."` LIKE '".trim( $tmp[1] )."'";
								}
								else
								{
										$sql = "SHOW TABLE STATUS LIKE '".trim( $table )."'";
								}
								$result = mysql_query( $sql, $this->link_id );
								$row = mysql_fetch_assoc( $result );
								if ( $lastupdatetime < $row['Update_time'] )
								{
										$lastupdatetime = $row['Update_time'];
								}
						}
				}
				$lastupdatetime = strtotime( $lastupdatetime ) - $this->timezone + $this->timeline;
				return $lastupdatetime;
		}

		public function get_table_name( $query_item )
		{
				$query_item = trim( $query_item );
				$table_names = array( );
				if ( stristr( $query_item, " JOIN " ) == "" )
				{
						if ( preg_match( "/^SELECT.*?FROM\\s*((?:`?\\w+`?\\s*\\.\\s*)?`?\\w+`?(?:(?:\\s*AS)?\\s*`?\\w+`?)?(?:\\s*,\\s*(?:`?\\w+`?\\s*\\.\\s*)?`?\\w+`?(?:(?:\\s*AS)?\\s*`?\\w+`?)?)*)/is", $query_item, $table_names ) )
						{
								$table_names = preg_replace( "/((?:`?\\w+`?\\s*\\.\\s*)?`?\\w+`?)[^,]*/", "\\1", $table_names[1] );
								return preg_split( "/\\s*,\\s*/", $table_names );
						}
				}
				else if ( preg_match( "/^SELECT.*?FROM\\s*((?:`?\\w+`?\\s*\\.\\s*)?`?\\w+`?)(?:(?:\\s*AS)?\\s*`?\\w+`?)?.*?JOIN.*\$/is", $query_item, $table_names ) )
				{
						$other_table_names = array( );
						preg_match_all( "/JOIN\\s*((?:`?\\w+`?\\s*\\.\\s*)?`?\\w+`?)\\s*/i", $query_item, $other_table_names );
						return array_merge( array(
								$table_names[1]
						), $other_table_names[1] );
				}
				return $table_names;
		}

		public function set_disable_cache_tables( $tables )
		{
				if ( !is_array( $tables ) )
				{
						$tables = explode( ",", $tables );
				}
				foreach ( $tables as $table )
				{
						$this->mysql_disable_cache_tables[] = $table;
				}
				array_unique( $this->mysql_disable_cache_tables );
		}

		public function insert( $table, $array, $is = 0 )
		{
				$fields = $values = array( );
				$table = str_replace( "%s", DB_PRE, $table );
				if ( empty( $array ) )
				{
						return FALSE;
				}
				$sql_array = array( );
				if ( $is )
				{
						foreach ( $array[0] as $key => $val )
						{
								$fields[] = $key;
						}
						foreach ( $array as $arr )
						{
								foreach ( $arr as $key => $val )
								{
										$values[] = $this->encode( $val );
								}
								$sql_array[] = "('".implode( "','", $values )."')";
								$values = array( );
						}
				}
				else
				{
						foreach ( $array as $key => $val )
						{
								$fields[] = $key;
								$values[] = $this->encode( $val );
						}
						$sql_array[] = "('".implode( "','", $values )."')";
				}
				$sql = "INSERT INTO ".$table." (".implode( ",", $fields ).") VALUES ";
				$sql .= implode( ",", $sql_array ).";";
				if ( $this->query( $sql ) )
				{
						return TRUE;
				}
				return FALSE;
		}

		public function update( $table, $array, $where = NULL )
		{
				$fields = $values = array( );
				foreach ( $array as $key => $val )
				{
						$values[] = preg_match( "/^\\[\\[.+\\]\\]\$/", $val ) ? $key."=".substr( $val, 2, -2 ) : $key."='".$this->encode( $val )."'";
				}
				$sql = "UPDATE ".$table." SET ".implode( ",", $values ).( empty( $where ) ? ";" : " WHERE ".$where.";" );
				if ( $this->query( $sql ) )
				{
						return TRUE;
				}
				return FALSE;
		}

		public function encode( $s )
		{
				return addslashes( $s );
		}

		public function createtable( $tableName, $sql, $kid = NULL, $auto_increment = 0 )
		{
				$kidSql = isset( $kid[0] ) ? "{$kid} int not null AUTO_INCREMENT primary key," : "";
				$autoSql = 0 < $auto_increment ? " AUTO_INCREMENT=".$auto_increment : "";
				$this->query( "create table IF NOT EXISTS ".$tableName." ({$kidSql}{$sql}) ENGINE=MyISAM  CHARSET=".BETUO_DB_CHARSET."{$autoSql};" );
		}

		public function altertable( $tableName, $sql, $is = 0 )
		{
				$alteration = $is ? " RENAME TO " : "ADD";
				$this->query( "ALTER TABLE ".$tableName." {$alteration} ({$sql}) ;" );
		}

		public function createdb( )
		{
				@mysql_query( "CREATE DATABASE IF NOT EXISTS ".BETUO_DB_DATA." DEFAULT CHARACTER SET utf8 COLLATE utf8_unicode_ci;", @mysql_connect( BETUO_DB_HOST, BETUO_DB_USER, BETUO_DB_PASS ) );
		}

		public function fetch_text( $sql )
		{
				$text = "";
				$result = mysql_query( $sql );
				if ( $this->num_rows( $result ) == 0 )
				{
						$text = "";
						return $text;
				}
				$rows = mysql_fetch_array( $result );
				if ( !rows )
				{
						$text = "";
						return $text;
				}
				$text = $rows[0];
				return $text;
		}

		public function fetch_select( $inputvale, $sql )
		{
				$selects = "";
				$query = mysql_query( $sql );
				if ( !$query )
				{
						$selects = "";
						return $selects;
				}
				while ( $rows = mysql_fetch_array( $query ) )
				{
						$selects .= "<option value='".$rows[0]."'";
						if ( $inputvale != "" && trim( $inputvale ) == trim( $rows[0] ) )
						{
								$selects .= " selected";
						}
						$selects .= ">".$rows[1]."</option>";
				}
				return $selects;
		}

		public function fetch_select_t( $inputvale, $sql )
		{
				$selects = "";
				$query = mysql_query( $sql );
				if ( !$query )
				{
						$selects = "";
						return $selects;
				}
				while ( $rows = mysql_fetch_array( $query ) )
				{
						$selects .= "<option value='".$rows[0]."'";
						if ( $inputvale != "" && trim( $inputvale ) == trim( $rows[0] ) )
						{
								$selects .= " selected";
						}
						$selects .= ">".$rows[1]." ".$rows[2]."</option>";
				}
				return $selects;
		}

		public function fetch_newid( $sql, $startid )
		{
				$n_id = 0;
				$result = $this->query( $sql );
				$ns = $this->result( $result, 0 );
				if ( empty( $ns ) || $ns == "" )
				{
						$n_id = $startid;
						return $n_id;
				}
				$n_id = $ns + 1;
				return $n_id;
		}

		public function fetch_count( $sql )
		{
				return $this->result( $this->query( $sql ), 0 );
		}

		public function fetch_row( $sql )
		{
				return $this->fetch_array( $this->query( $sql ) );
		}

		public function checkdata( $sql )
		{
				$returnflag = FALSE;
				$result = $this->query( $sql );
				if ( $this->num_rows( $result ) == 0 )
				{
						$returnflag = FALSE;
						return $returnflag;
				}
				$returnflag = TRUE;
				return $returnflag;
		}

}

?>
