<?php
/*�к��ַ�����*/
function replacebadchar( $_string )
{
		if ( empty( $_string ) )
		{
				return;
		}
		if ( $_string == "" )
		{
				return;
		}
		$_string = trim( $_string );
		$_string = str_replace( "'", "", $_string );
		$_string = str_replace( "=", "", $_string );
		$_string = str_replace( "#", "", $_string );
		$_string = str_replace( "\$", "", $_string );
		$_string = str_replace( ">", "", $_string );
		$_string = str_replace( "<", "", $_string );
		$_string = str_replace( "//", "", $_string );
		$_string = str_replace( "/", "", $_string );
		$_string = str_replace( "\\", "", $_string );
		$_string = str_replace( "*", "", $_string );
		return $_string;
}
/*����ַ�����ʽ*/
function ischar( $_string )
{
		if ( empty( $_string ) || $_string == "" )
		{
				return FALSE;
		}
		return TRUE;
}
/*������ָ�ʽ*/
function isnumber( $_string )
{
		if ( is_numeric( $_string ) )
		{
				return TRUE;
		}
		return FALSE;
}
/*���ʱ���ʽ*/
function isdatetime( $_date )
{
		if ( preg_match( "/[\\d]{4}-[\\d]{2}-[\\d]{2}/iU ", $_date ) )
		{
				return TRUE;
		}
		return FALSE;
}

/* ��ȡIP*/
function getip( )
{
		if ( $_SERVER['HTTP_X_FORWARDED_FOR'] )
		{
				$ip = $_SERVER['HTTP_X_FORWARDED_FOR'];
				return $ip;
		}
		if ( $_SERVER['HTTP_CLIENT_IP'] )
		{
				$ip = $_SERVER['HTTP_CLIENT_IP'];
				return $ip;
		}
		if ( $_SERVER['REMOTE_ADDR'] )
		{
				$ip = $_SERVER['REMOTE_ADDR'];
				return $ip;
		}
		if ( getenv( "HTTP_X_FORWARDED_FOR" ) )
		{
				$ip = getenv( "HTTP_X_FORWARDED_FOR" );
				return $ip;
		}
		if ( getenv( "HTTP_CLIENT_IP" ) )
		{
				$ip = getenv( "HTTP_CLIENT_IP" );
				return $ip;
		}
		if ( getenv( "REMOTE_ADDR" ) )
		{
				$ip = getenv( "REMOTE_ADDR" );
				return $ip;
		}
		$ip = "unknown";
		return $ip;
}


/*
��ȡָ���ַ�������ַ�
*/
function getrndchar( $length )
{
		$hash = "";
		$chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
		$max = strlen( $chars ) - 1;
		mt_srand( ( double )microtime( ) * 1000000 );
		$i = 0;
		for ( $i = 0;	$i < $length;	$i++	)
		{
				$hash .= $chars[mt_rand( 0, $max )];
		}
		return $hash;
}
/*
��ȡָ���ַ��������
*/
function getrndnum( $length )
{
		$hash = "";
		$chars = "0123456789";
		$max = strlen( $chars ) - 1;
		mt_srand( ( double )microtime( ) * 1000000 );
		
		for ( $i = 0;	$i < $length;	$i++	)
		{
				$hash .= $chars[mt_rand( 0, $max )];
		}
		return $hash;
}
/*��ת*/
function mb( $_string, $_comurl, $_gotype )
{
		echo "<meta http-equiv='Content-Type' content='text/html; charset=".PHPOE_CHARSET."' />";
		echo "<script language=javascript>alert('".$_string."');";
		if ( $_gotype == 1 )
		{
				echo "window.history.go(-1);";
		}
		else
		{
				echo "window.location.href='".$_comurl."';";
		}
		echo "</script>";
		exit( );
}
/*��ת*/
function html_mb( $_string, $_comurl, $_gotype )
{
		echo "<meta http-equiv='Content-Type' content='text/html; charset=".PHPOE_CHARSET."' />";
		echo "<style type='text/css'>body { background:#fcfcf3; font: 12px Arial, Helvetica, sans-serif;}.content{ width:400px;height:190px;position:absolute;top:50%;left:45%;margin:-95px 0 0 -200px;}.main { width:394px; height:184px; padding:6px 0px 0px 6px; position: relative;}.maintop { width:392px; height:180px; position:absolute; top:0px; left:0px; background: #fff; border-color:#3366cc; border-style:solid;border-width:3px 1px 1px 1px; }.mainbottom { width:394px; height:184px; background:#e3e3e3; }.txttop { width:392px; height:75px; margin:10px 0px; color:#ff6600; font-weight:bold; line-height:90px; font-size:40px; font-family:Arial, Helvetica, sans-serif; text-indent: 17px;}.txtbottom { width:377px; height:48px; line-height:24px; padding-left: 15px; }.txtbottom a { color: #0099FF; font-weight:bold; text-decoration:underline; }.txtbottom a:hover { text-decoration:none; }</style><div class='content'>  <div class='main'>    <div class='mainbottom'></div>\t  <div class='maintop'>\t    <div class='txttop'>��Ϣ��ʾ��</div>";
		switch ( $_gotype )
		{
		case 1 :
				echo "    <div class='txtbottom'>".$_string."&nbsp;&nbsp;<br /><a href='' onClick=\"window.history.go(-1); return false;\">������һҳ</a>";
				break;
		case 0 :
				echo "    <div class='txtbottom'>".$_string."&nbsp;&nbsp;<br /><a href='".$_comurl."'>������һҳ</a>";
				break;
		case 2 :
				echo "    <div class='txtbottom'>".$_string."&nbsp;&nbsp;<br /><a href='/index.php'> �� ҳ </a>";
		}
		echo "    </div>    </div>  </div></div>";
		exit( );
}
/*�رյ������Ӵ��ڣ���ˢ�¸�����*/
function openner( $_string )
{
		if ( !ischar( $_string ) )
		{
				echo "<script language='javascript'>opener.location.reload();window.close();</script>";
				exit( );
		}
		echo "<script language='javascript'>alert('".$_string."');opener.location.reload();window.close();</script>";
		exit( );
}



function check_strpos( $s_str, $s_needlechar )
{
		if ( !ischar( $s_str ) )
		{
				return;
		}
		if ( !ischar( $s_needlechar ) )
		{
				return;
		}
		$s_temparray = explode( $s_needlechar, $s_str );
		if ( 0 < count( $s_temparray ) )
		{
				return TRUE;
		}
		return FALSE;
}
/*��ʽ��ʱ��*/
function formattime( $_datetime, $_type )
{
		switch ( $_type )
		{
		case 1 :
				$_newtime = date( "Y-m-d", strtotime( $_datetime ) );
				return $_newtime;
		case 2 :
				$_newtime = substr( $_datetime, 5, 5 );
				$_newtime = str_replace( "-", "/", $_newtime );
				return $_newtime;
		}
		$_newtime = date( "Y-m-d H:i:s", strtotime( $_datetime ) );
		return $_newtime;
}
/*��ȡʱ��*/
function getdatetime( $_timer, $_type )
{
		if ( $_type == 1 )
		{
				$_newtime = date( "Y-m-d", $_timer );
				return $_newtime;
		}
		$_newtime = date( "Y-m-d H:i:s", $_timer );
		return $_newtime;
}
/*��ȡ����*/
function get_newdate( $_timer, $_days )
{
		$a_date = date( "Y-m-d", $_timer );
		$new_date = strtotime( $a_date ) + 86400 * $_days;
		$new_date = date( "Y-m-d", $new_date );
		return $new_date;
}

function checkbox( $_value, $_invalue )
{
		$_returnvalue = "";
		if ( $_value == "" )
		{
				$_returnvalue = "";
				return $_returnvalue;
		}
		if ( trim( $_value ) == trim( $_invalue ) )
		{
				$_returnvalue = " checked";
		}
		return $_returnvalue;
}
/*�����˵�ѡ��*/
function selectbox( $_value, $_invalue )
{
		$_returnvalue = "";
		if ( $_value == "" )
		{
				$_returnvalue = "";
				return $_returnvalue;
		}
		if ( trim( $_value ) == trim( $_invalue ) )
		{
				$_returnvalue = " selected";
		}
		return $_returnvalue;
}

function getmicrotime( )
{
		list( $usec, $sec ) = explode( " ", microtime( ) );
		return ( double )$usec + ( double )$sec;
}

function createhtml( $s_content, $s_filename )
{
		if ( !ischar( $s_filename ) )
		{
				return;
		}
		if ( !ischar( $s_content ) )
		{
				return;
		}
		$s_fso = fopen( $s_filename, "w" );
		if ( $s_fso )
		{
				fwrite( $s_fso, $s_content );
		}
		fclose( $s_fso );
}

function deletefile( $s_filename )
{
		if ( !ischar( $s_filename ) )
		{
				return;
		}
		@unlink( $s_filename );
}
/*���email*/
function is_mail( $m )
{
		if ( substr_count( $m, "@" ) != 1 )
		{
				return FALSE;
		}
		if ( $m[0] == "@" )
		{
				return FALSE;
		}
		if ( $m[0] == "." )
		{
				return FALSE;
		}
		if ( $m[strlen( $m ) - 1] == "@" )
		{
				return FALSE;
		}
		if ( $m[strlen( $m ) - 1] == "." )
		{
				return FALSE;
		}
		if ( substr_count( $m, ".@" ) != 0 )
		{
				return FALSE;
		}
		if ( substr_count( $m, "@." ) != 0 )
		{
				return FALSE;
		}
		if ( substr_count( $m, ".." ) != 0 )
		{
				return FALSE;
		}
		if ( substr_count( substr( $m, strpos( $m, "@" ) ), "." ) == 0 )
		{
				return FALSE;
		}
		return TRUE;
}

/*�滻html��ǩ*/
function replacebr( $s_content )
{
		$s_content = str_replace( "\n", "<br />", $s_content );
		return $s_content;
}




function foundinarr( $s_strarr, $s_tofind, $s_strsplit )
{
		$s_flag = FALSE;
		if ( ischar( $s_strarr ) )
		{
		}
		if ( !ischar( $s_tofind ) )
		{
				$s_flag = FALSE;
				return $s_flag;
		}
		$arrtemp = explode( $s_strsplit, $s_strarr );
		
		for ( $s_i = 0;	$s_i < sizeof( $arrtemp );	$s_i++	)
		{
				$s_value = trim( $arrtemp[$s_i] );
				if ( !( $s_value == $s_tofind ) )
				{
						continue;
				}
				$s_flag = TRUE;
				break;
		}
		return $s_flag;
}
/*ʱ����ʾ*/
function getdatestring( $_timer, $_type )
{
		$y = substr( $_timer, 0, 4 );
		$m = substr( $_timer, 5, 2 );
		$d = substr( $_timer, 8, 2 );
		$h = substr( $_timer, 11, 2 );
		$i = substr( $_timer, 14, 2 );
		$s = substr( $_timer, 17, 2 );
		$mk = mktime( $h, $i, $s, $m, $d, $y );
		$w = date( "w", $mk );
		if ( $_type == 1 )
		{
				$newtime = $y."-".$m."-".$d;
				return $newtime;
		}
		if ( $_type == 2 )
		{
				$newtime = $y."��".$m."��".$d."��";
				return $newtime;
		}
		if ( $_type == 3 )
		{
				$newtime = $h.":".$i.":".$s;
				return $newtime;
		}
		if ( $_type == 4 )
		{
				$newtime = $h.":".$i;
				return $newtime;
		}
		if ( $_type == 5 )
		{
				$newtime = $h."ʱ".$i."��".$s."��";
				return $newtime;
		}
		if ( $_type == 6 )
		{
				$newtime = $y."��".$m."��".$d."�� ".$h."ʱ".$i."��".$s."��";
				return $newtime;
		}
		if ( $_type == 7 )
		{
				$newtime = $y."/".$m."/".$d;
				return $newtime;
		}
		if ( $_type == 8 )
		{
				$arrweek = array( "��", "һ", "��", "��", "��", "��", "��" );
				$newtime = "����".$arrweek[$w];
				return $newtime;
		}
		if ( $_type == 9 )
		{
				$newtime = $y."��".$m."��".$d."�� ".$h."ʱ".$i."��";
				return $newtime;
		}
		if ( $_type == 10 )
		{
				$newtime = $m."-".$d;
		}
		return $newtime;
}

/*�����*/
function get_yearinterval( $s_year )
{
		$n_year = date( "Y", time( ) );
		$b_year = substr( $s_year, 0, 4 );
		return $n_year - $b_year;
}
/*���email*/
function isemail( $s_email )
{
		$pattern = "/^([\\w\\.-]+)@([a-zA-Z0-9-]+)(\\.[a-zA-Z\\.]+)\$/i";
		if ( preg_match( $pattern, $s_email, $matches ) )
		{
				return TRUE;
		}
		return FALSE;
}
/*����û���*/
function check_userstr( $s_str )
{
		$s_str = gbktoutf( $s_str );
		if ( preg_match( "/^[0-9a-zA-Z_\\x{4e00}-\\x{9fa5}]+\$/u", $s_str ) )
		{
				return TRUE;
		}
		return FALSE;
}
/*����û�������*/
function check_userstrlen( $str )
{
		$str = strtolower( $str );
		$name_len = strlen( $str );
		$temp_len = 0;
		$i = 0;
		while ( $i < $name_len )
		{
				if ( !strpos( "abcdefghijklmnopqrstvuwxyz0123456789_", $str[$i] ) )
				{
						$i += 2;
						$temp_len += 2;
				}
				else
				{
						$i += 1;
						$temp_len += 1;
				}
		}
		return $temp_len;
}

function filter_text( $s_str )
{
		$minlen = 5;
		$maxlen = 11;
		$num = "0123456789";
		$a = "";
		
		for ( $j = 0;	$j < strlen( $s_str );	$j++	)
		{
				if ( 0 < substr_count( $num, $s_str[$j] ) && $minlen <= strlen( substr( $s_str, $j, $minlen ) ) )
				{
						
						for ( $i = $maxlen;	$minlen <= $i;	$i--	)
						{
								if ( is_numeric( substr( $s_str, $j, $i ) ) )
								{
										$a .= preg_replace( "/\\d{1,}\\s{0,1}/", "xxxx", substr( $s_str, $j, $i ) );
										$j += $i;
								}
						}
				}
				$a .= $s_str[$j];
		}
		return $a;
}

function utftogbk( $value )
{
		return iconv( "UTF-8", "gbk", $value );
}

function gbktoutf( $value )
{
		return iconv( "gbk", "UTF-8", $value );
}


function strip_array( &$_data )
{
		if ( is_array( $_data ) )
		{
				foreach ( $_data as $_key => $_value )
				{
						$_data[$_key] = trim( strip_array( $_value ) );
				}
				return $_data;
		}
		return stripslashes( trim( $_data ) );
}

echo " ";
?>
