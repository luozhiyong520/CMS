<?php
	/* $Id    db select 下拉选 
	 @params  $inputvalue   : 传入当前值
	 @params  $selectname   : SELECT 名称
	 @params  $dbtable      : 表名
	 @params  $text         : text提示
	 */
	function db_select($inputvalue,$selectname,$dbtable,$text="==请选择=="){
		$obj=NULL;
		$obj = $GLOBALS['db'];
		if($dbtable=="authgroup"){
			$fieldopvalue = "groupid";
			$fieldoptext  = "groupname";
			$where        = " WHERE flag=1 ORDER BY orders ASC";
		} elseif($dbtable=="user_authgroup"){
			$fieldopvalue = "groupid";
			$fieldoptext  = "groupname";
			$where        = " WHERE flag=1 ORDER BY orders ASC";
		} elseif($dbtable=='skin'){
			$fieldopvalue = "skinid";
			$fieldoptext  = "skinname";
			$where        = " ORDER BY skinid ASC";
		} elseif($dbtable=='adszone'){
			$fieldopvalue = "zoneid";
			$fieldoptext  = "zonename";
			$where        = " WHERE flag=1 ORDER BY orders ASC";
		} else{
			$fieldopvalue = "cateid";
			$fieldoptext  = "catename";
			$where        = " WHERE flag=1 ORDER BY orders ASC";
		}
		$temp  = "<select name='$selectname' id='$selectname'>";
		$temp .= "<option value=''>$text</option>";
		$sel_sql	= "SELECT $fieldopvalue,$fieldoptext FROM ".DB_PREFIX.$dbtable.$where;
		$sel		= $obj->getall($sel_sql);
		$loop		= "";
		foreach($sel as $key=>$value){
			$loop .= "<option value=\"".$value[$fieldopvalue]."\"";
			if(trim($inputvalue)==trim($value[$fieldopvalue])){
				$loop .= " selected";
			}
			$loop .= ">".$value[$fieldoptext]."</option>";
		}
		$temp .= $loop."</select>";
		return $temp;
	}



function common_radio( $inputvalue, $radioname )
{
		$temp = "<input type='radio' id='".$radioname."' name='{$radioname}' value='1'";
		if ( $inputvalue == "1" )
		{
				$temp .= " checked";
		}
		$temp .= "> 是，";
		$temp .= "<input type='radio' id='".$radioname."' name='{$radioname}' value='0'";
		if ( $inputvalue == "0" )
		{
				$temp .= " checked";
		}
		$temp .= "> 否";
		return $temp;
}

function common_checkbox( $inputvalue, $checkboxname, $boxtext )
{
		$temp = "<input type='checkbox' id='".$checkboxname."' name='{$checkboxname}' value='1'";
		if ( $inputvalue == "1" )
		{
				$temp .= " checked";
		}
		$temp .= ">".$boxtext;
		return $temp;
}

function news_cate_select( $inputvalue, $selectname )
{
		$temp = "<select name='".$selectname."' id='{$selectname}'>";
		$temp .= "<option value=''>==选择==</option>";
		$temp .= $GLOBALS['db']->fetch_select( $inputvalue, "SELECT cateid,catename FROM ".DB_PREFIX."newscate WHERE flag=1 ORDER BY orders ASC" );
		$temp .= "</select>";
		return $temp;
}

function linktype_select( $inputvalue, $selectname )
{
		$temp = "<select name='".$selectname."' id='{$selectname}'>";
		$temp .= "<option value=''>==选择==</option>";
		$temp .= "<option value='1'";
		if ( $inputvalue == "1" )
		{
				$temp .= " selected";
		}
		$temp .= ">文字链接</option>";
		$temp .= "<option value='2'";
		if ( $inputvalue == "2" )
		{
				$temp .= " selected";
		}
		$temp .= ">LOGO链接</option>";
		$temp .= "</select>";
		return $temp;
}

function logtype_select( $inputvalue, $selectname )
{
		$temp = "<select name='".$selectname."' id='{$selectname}'>";
		$temp .= "<option value=''>==选择==</option>";
		$temp .= "<option value='1'";
		if ( $inputvalue == "1" )
		{
				$temp .= " selected";
		}
		$temp .= ">管理员</option>";
		$temp .= "<option value='2'";
		if ( $inputvalue == "2" )
		{
				$temp .= " selected";
		}
		$temp .= ">会员</option>";
		$temp .= "</select>";
		return $temp;
}

function ontype_select( $inputvalue, $selectname )
{
		$temp = "<select name='".$selectname."' id='{$selectname}'><option value=''>==选择==</option>";
		$temp .= "<option value='1'";
		if ( $inputvalue == "1" )
		{
				$temp .= " selected";
		}
		$temp .= ">QQ</option>";
		$temp .= "<option value='2'";
		if ( $inputvalue == "2" )
		{
				$temp .= " selected";
		}
		$temp .= ">淘宝旺旺</option>";
		$temp .= "<option value='3'";
		if ( $inputvalue == "3" )
		{
				$temp .= " selected";
		}
		$temp .= ">MSN</option>";
		$temp .= "<option value='4'";
		if ( $inputvalue == "4" )
		{
				$temp .= " selected";
		}
		$temp .= ">Skype</option>";
		$temp .= "</select>";
		return $temp;
}

function common_unlimite_cate_select( $table, $inputvalue, $selectname )
{
		$temp = "<select name='".$selectname."' id='{$selectname}'>";
		$temp .= "<option value=''>==选择==</option>";
		$parent_sql = "SELECT cateid,catename FROM ".DB_PREFIX.$table." WHERE flag=1 AND parentid=0 ORDER BY orders ASC";
		$parent_query = $GLOBALS['db']->query( $parent_sql );
		while ( $parent_rows = $GLOBALS['db']->fetch_array( $parent_query ) )
		{
				$temp .= "<option value='".$parent_rows['cateid']."'";
				if ( isnumber( $inputvalue ) && $inputvalue == $parent_rows['cateid'] )
				{
						$temp .= " selected";
				}
				$temp .= ">".$parent_rows['catename']."</option>";
				$temp .= common_unlimite_catechild_select( $table, $parent_rows['cateid'], $inputvalue );
		}
		$temp .= "</select>";
		return $temp;
}

function common_unlimite_catechild_select( $s_table, $s_rootid, $s_inputvalue, $s_temp = "" )
{
		$s_temp = $s_temp;
		if ( isnumber( $s_rootid ) )
		{
				$s_child_sql = "SELECT cateid,catename,depth FROM ".DB_PREFIX.$s_table.( " WHERE parentid=".$s_rootid." AND flag=1 ORDER BY orders ASC" );
				$s_child_query = $GLOBALS['db']->query( $s_child_sql );
				while ( $s_child_rows = $GLOBALS['db']->fetch_array( $s_child_query ) )
				{
						$s_temp .= "<option value='".$s_child_rows['cateid']."'";
						if ( isnumber( $s_inputvalue ) && $s_inputvalue == $s_child_rows['cateid'] )
						{
								$s_temp .= " selected";
						}
						$s_tree = "";
						if ( $s_child_rows['depth'] == 1 )
						{
								$s_tree = "&nbsp;&nbsp;├ ";
						}
						else
						{
								
								for ( $s_ii = 2;	$s_ii <= $s_child_rows['depth'];	$s_ii++	)
								{
										$s_tree .= "&nbsp;&nbsp;│";
								}
								$s_tree .= "&nbsp;&nbsp;├ ";
						}
						$s_temp = $s_temp.">".$s_tree.$s_child_rows['catename']."</option>";
						$s_temp = common_unlimite_catechild_select( $s_table, $s_child_rows['cateid'], $s_inputvalue, $s_temp );
				}
		}
		return $s_temp;
}

function common_unlimite_cate_array( $spec_cat_id, $arr )
{
		static $cat_options = array();
		if (isset($cat_options[$spec_cat_id])){
			return $cat_options[$spec_cat_id];
		}
		if (!isset($cat_options[0])){
			$level = $last_cat_id = 0;
			$options = $cat_id_array = $level_array = array();
			while (!empty($arr)){
				foreach ($arr AS $key => $value){
					$cat_id = $value['cateid'];
					if ($level == 0 && $last_cat_id == 0){
						if ($value['parentid'] > 0){
							break;
						}
						$options[$cat_id]              = $value;
						$options[$cat_id]['depth']     = $level;
						$options[$cat_id]['cateid']    = $cat_id;
						$options[$cat_id]['catename']  = $value['catename'];
						unset($arr[$key]);
						if ($value['has_children'] == 0){
							continue;
						}
						$last_cat_id  = $cat_id;
						$cat_id_array = array($cat_id);
						$level_array[$last_cat_id] = ++$level;
						continue;
					}
					if ($value['parentid'] == $last_cat_id){
						$options[$cat_id]              = $value;
						$options[$cat_id]['depth']     = $level;
						$options[$cat_id]['cateid']    = $cat_id;
						$options[$cat_id]['catename']  = $value['catename'];
						unset($arr[$key]);
						if ($value['has_children'] > 0){
							if (end($cat_id_array) != $last_cat_id){
								$cat_id_array[] = $last_cat_id;
							}
							$last_cat_id    = $cat_id;
							$cat_id_array[] = $cat_id;
							$level_array[$last_cat_id] = ++$level;
						}
					}
					elseif ($value['parentid'] > $last_cat_id){
						break;
					}
				}
				$count = count($cat_id_array);
				if ($count > 1){
					$last_cat_id = array_pop($cat_id_array);
				}elseif ($count == 1){
					if ($last_cat_id != end($cat_id_array)){
						$last_cat_id = end($cat_id_array);
					}else{
						$level = 0;
						$last_cat_id = 0;
						$cat_id_array = array();
						continue;
					}
				}
				if ($last_cat_id && isset($level_array[$last_cat_id])){
					$level = $level_array[$last_cat_id];
				}else{
					$level = 0;
				}
			}
			$cat_options[0] = $options;

		}else{
			$options = $cat_options[0];
		}
		if (!$spec_cat_id){
			return $options;
		}else{
			if (empty($options[$spec_cat_id])){
				return array();
			}
			$spec_cat_id_level = $options[$spec_cat_id]['level'];
			foreach ($options AS $key => $value){
				if ($key != $spec_cat_id){
					unset($options[$key]);
				}else{
					break;
				}
			}
			$spec_cat_id_array = array();
			foreach ($options AS $key => $value){
				if (($spec_cat_id_level == $value['depth'] && $value['cateid'] != $spec_cat_id) ||
					($spec_cat_id_level > $value['depth'])){
					break;
				}else{
					$spec_cat_id_array[$key] = $value;
				}
			}
			$cat_options[$spec_cat_id] = $spec_cat_id_array;
			return $spec_cat_id_array;
		}
}

function common_unlimite_cate_childsql( $s_table, $s_as = "", $s_rootid, $s_sqlstr = "" )
{
		$s_sqlstr = $s_sqlstr;
		if ( isnumber( $s_rootid ) )
		{
				$child_sql = "SELECT cateid FROM ".DB_PREFIX.$s_table.( " WHERE parentid=".$s_rootid );
				$child_rows = $GLOBALS['db']->getall( $child_sql );
				foreach ( $child_rows as $s_key => $s_value )
				{
						if ( $s_as != "" )
						{
								$s_sqlstr = $s_sqlstr." or ".$s_as.".cateid=".$s_value['cateid']."";
						}
						else
						{
								$s_sqlstr = $s_sqlstr." or cateid=".$s_value['cateid']."";
						}
						$s_sqlstr = common_unlimite_cate_childsql( $s_table, $s_as, $s_value['cateid'], $s_sqlstr );
				}
		}
		return $s_sqlstr;
}

function common_getpicname( $splitvar, $picurl )
{
		if ( ischar( $picurl ) )
		{
				$arr_t = explode( "/", $picurl );
				return $arr_t[max( array_flip( $arr_t ) )];
		}
		return "";
}

function common_params_select( $paramtype, $inputvalue, $selectname = "" )
{
		$s_temp = "<select name='".$selectname."' id='{$selectname}'>";
		$s_temp .= "<option value=''>  </option>";
		$s_values = $GLOBALS[_VALUE_PARAMS][$paramtype];
		$s_arrvalue = explode( "|", $s_values );
		$i = 0;
		for ( ;	$i < sizeof( $s_arrvalue );	++$i	)
		{
				$s_vs = trim( $s_arrvalue[$i] );
				$s_arrvs = explode( "#", $s_vs );
				$s_temp = $s_temp."<option value='".$s_arrvs[0]."'";
				if ( $inputvalue == $s_arrvs[0] )
				{
						$s_temp .= " selected";
				}
				$s_temp = $s_temp.">".$s_arrvs[1]."</option>";
		}
		$s_temp .= "</select>";
		return $s_temp;
}

function common_params_text( $paramtype, $inputvalue )
{
		$s_temp = "";
		$s_values = $GLOBALS[_VALUE_PARAMS][$paramtype];
		$s_arrvalue = explode( "|", $s_values );
		$i = 0;
		for ( ;	$i < sizeof( $s_arrvalue );	++$i	)
		{
				$s_vs = trim( $s_arrvalue[$i] );
				$s_arrvs = explode( "#", $s_vs );
				if ( $inputvalue == $s_arrvs[0] )
				{
						$s_temp = $s_arrvs[1];
				}
		}
		return $s_temp;
}

?>
