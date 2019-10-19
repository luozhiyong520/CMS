<?php 
/*-----------------------------------
 * @version: OEcms v2.0 Free(免费版)
 * @website: www.phpoe.com,www.phpcoo.com
 * @email  : phpzac@foxmail.com
 * @qq     : 944811833,16100225
 * @update : 2011.02.20
 * @说明   ：前端页面函数，请以template_开头，可二次开发
-----------------------------------*/
/**
	@ 单页导航
	@ 返回值 array()
	@ date 2011.05.20
*/
function template_pagenav(){
	$query = "SELECT pageid,title FROM ".DB_PREFIX."page WHERE flag=1 AND showstatus=1 ORDER BY orders ASC";
	$array = $GLOBALS['db']->getall($query);
	foreach($array as $key=>$value){
		//URL路由处理
		if($GLOBALS['config']['htmltype']=="php"){
			$array[$key]['url'] = "page.php?id=".$value['pageid'];
		}else{
			$array[$key]['url'] = "page-".$value['pageid'].".html";
		}
	}
	return $array;
}
/**
	@ 推荐单页导航
	@ 返回值 array()
	@ date 2011.05.20
*/
function template_elitepagenav(){
	$query = "SELECT pageid,title FROM ".DB_PREFIX."page WHERE flag=1 AND showstatus=1 ORDER BY orders ASC";
	$array = $GLOBALS['db']->getall($query);
	foreach($array as $key=>$value){
		//URL路由处理
		if($GLOBALS['config']['htmltype']=="php"){
			$array[$key]['url'] = "page.php?id=".$value['pageid'];
		}else{
			$array[$key]['url'] = "page-".$value['pageid'].".html";
		}
	}
	return $array;
}





/**
	@ 友情连接
	@ param int $type -- 类型 0--全部 1--文字，2--LOGO
	@ 返回值 array
	@ date 2011.05.20
*/
function template_link($type=0){
	$query  = "SELECT linkid,linktitle,linkurl,logourl FROM ".DB_PREFIX."links WHERE flag=1";
	if($type==1){
		$query .= " AND linktype=1";
	}else if($type==2){
		$query .= " AND linktype=2";
	}
	$query .= " ORDER BY orders ASC";
	$array  = $GLOBALS['db']->getall($query);
	$i      = 1;
	foreach($array as $key=>$value){
		$array[$key]['i'] = $i;
		$i = ($i+1);
	}
	return $array;
}


/**
	@ 在线客服
	@ 返回值 array
	@ date 2011.05.20
*/
function template_onlinechat(){
	$query  = "SELECT ontype,title,number,sitetitle FROM ".DB_PREFIX."onlinechat WHERE flag=1";
	$query .= " ORDER BY ontype ASC,orders ASC";
	return $GLOBALS['db']->getall($query);
}

/**
	@ 新闻分类
	@ param int $num -- 显示数量 0-不限制
	@ 返回值 array
	@ date 2011.05.20
*/
function template_newscate($num=0){
	$query  = "SELECT cateid,catename FROM ".DB_PREFIX."newscate WHERE flag=1";
	$query .= " ORDER BY orders ASC";
	if($num>0){
		$query .= " LIMIT ".$num."";
	}
	$array  = $GLOBALS['db']->getall($query);
	foreach($array as $key=>$value){
		//URL路由处理
		if($GLOBALS['config']['htmltype']=="php"){
			$array[$key]['url'] = "newslist.php?cid=".$value['cateid'];
		}else{
			$array[$key]['url'] = "news-cat-".$value['cateid'].".html";
		}
	}
	return $array;
}



/**
	@ 产品分类 (只含一级)
	@ param int $num   -- 显示数量 0-不限制
	@ param int $elite -- 是否推荐 1--Y，0--N
	@ 返回值 array()
	@ date 2011.05.20
*/
function template_productcate($num=0,$elite=0){
	$query  = "SELECT cateid,catename FROM ".DB_PREFIX."productcate WHERE flag=1 AND parentid=0";
	if($elite>0){
		$query .= " AND elite=1";
	}
	$query .= " ORDER BY orders ASC";
	if($num>0){
		$query .= " LIMIT ".$num."";
	}
	$array  = $GLOBALS['db']->getall($query);
	foreach($array as $key=>$value){
		//URL路由处理
		if($GLOBALS['config']['htmltype']=="php"){
			$array[$key]['url'] = "productlist.php?cid=".$value['cateid'];
		}else{
			$array[$key]['url'] = "product-cat-".$value['cateid'].".html";
		}
	}
	return $array;
}

/**
	@ 产品分类（含二级分类）
	@ param int $rootnum  -- 一级分类数量 0表示不限制
	@ param int $childnum -- 二级分类数量 0表示不限制
	@ 返回值 array()
	@ date 2011.05.20
*/
function template_producttreecate($rootnum=0,$childnum=0){
	if(!isnumber($rootnum)){
		$rootnum = 10;
	}
	if(!isnumber($childnum)){
		$childnum = 10;
	}
	$category   = array();
	$parent_sql = "SELECT cateid,catename FROM ".DB_PREFIX."productcate WHERE parentid=0 AND depth=0 AND flag=1 ORDER BY orders ASC";
	if(intval($rootnum)>0){
		$parent_sql.=" LIMIT ".$rootnum."";
	}
	$parent_cate = $GLOBALS['db']->getall($parent_sql);
	foreach($parent_cate as $parent_key => $parent_value){

		//URL路由处理
		if($GLOBALS['config']['htmltype']=="php"){
			$parent_url = "productlist.php?cid=".$parent_value['cateid'];
		}else{
			$parent_url = "product-cat-".$parent_value['cateid'].".html";
		}

		$category[] = array('cateid'=>$parent_value['cateid'],'catename'=>$parent_value['catename'],'url'=>$parent_url,'childcategory'=>array());
		//读取子类
		$child_sql = "SELECT cateid,catename FROM ".DB_PREFIX."productcate WHERE parentid=".$parent_value['cateid']." AND flag=1 ORDER BY orders ASC";
		if($childnum>0){
			$child_sql.=" LIMIT $childnum";
		}
		$child_cate = $GLOBALS['db']->getall($child_sql);
		foreach($child_cate as $child_key => $child_value){
			//URL路由处理
			if($GLOBALS['config']['htmltype']=="php"){
				$child_url = "productlist.php?cid=".$child_value['cateid'];
			}else{
				$child_url = "product-cat-".$child_value['cateid'].".html";
			}
			$category[count($category)-1]['childcategory'][] = array('cateid'=>$child_value['cateid'],'catename'=>$child_value['catename'],'url'=>$child_url);
		}
	}
	return $category;
}


/**
	@ 最新产品
	@ param int $cateid -- 分类 0不限制
	@ param int $num    -- 显示个数 0--表示默认
	@ param int $elite  -- 是否推荐 1--Y，0--N
	@ 返回值 array()
	@ date 2011.05.20
*/
function template_newproduct($cateid=0,$num=0,$elite=0){
	$searchsql = " WHERE p.flag=1";
    if((int)$cateid>0){
		$childs_sql = common_unlimite_cate_childsql("productcate","p",$cateid,""); //所有子类
		if(ischar($childs_sql)){
			$searchsql .= " AND (p.cateid=$cateid".$childs_sql.")";
		}else{
			$searchsql .= " AND p.cateid=$cateid";
		}
	}
	if((int)$elite>0){
		$searchsql .= " AND p.elite=1";
	}
 	$query  = "SELECT p.*,c.catename AS catename FROM ".DB_PREFIX."product AS p".
             " LEFT JOIN ".DB_PREFIX."productcate AS c ON p.cateid=c.cateid".
		       $searchsql." ORDER BY p.productid DESC";

	if((int)$num>0){
		$query .= " LIMIT ".$num."";
	}else{
	    $query .= " LIMIT ".$GLOBALS['config']['productnum']."";
	}
	$array  = $GLOBALS['db']->getall($query);
	$i      = 1;
	foreach($array as $key=>$value){
		if($GLOBLAS['config']['productlen']>0){
			if(strtolower(PHPOE_CHARSET)=="utf-8"){
				$array[$key]['sort_productname'] = $GLOBLAS['config']['productlen'];
			}else{
				$array[$key]['sort_productname'] = $GLOBLAS['config']['productlen'];
			}
		}else{
			$array[$key]['sort_productname'] = $value['productname'];
		}
        $array[$key]['filter_content'] = $value['content']; //过滤html标签

		$array[$key]['i'] = $i;
		$i = ($i+1);

		//URL路由处理
		if($GLOBALS['config']['htmltype']=="php"){
			$array[$key]['url'] = "product.php?id=".$value['productid'];
			$array[$key]['caturl'] = "productlist.php?cid=".$value['cateid'];
		}else{
			$array[$key]['url'] = "product-".$value['productid'].".html";
			$array[$key]['caturl'] = "product-cat-".$value['cateid'].".html";
		}

	}
	return $array;
}

/**
	@ 推荐产品
	@ param int $cateid -- 分类 0不限制
	@ param int $num    -- 显示个数 0--表示默认
	@ 返回值 array()
	@ date 2011.05.20
*/
function template_eliteproduct($cateid=0,$num=0){
	$searchsql = " WHERE p.flag=1 and p.elite=1";
    if((int)$cateid>0){
		$childs_sql = common_unlimite_cate_childsql("productcate","p",$cateid,""); //所有子类
		if(ischar($childs_sql)){
			$searchsql .= " AND (p.cateid=$cateid".$childs_sql.")";
		}else{
			$searchsql .= " AND p.cateid=$cateid";
		}
	}
 	$query  = "SELECT p.*,c.catename AS catename FROM ".DB_PREFIX."product AS p".
             " LEFT JOIN ".DB_PREFIX."productcate AS c ON p.cateid=c.cateid".
		       $searchsql." ORDER BY p.productid DESC";

	if((int)$num>0){
		$query .= " LIMIT ".$num."";
	}else{
	    $query .= " LIMIT ".$GLOBALS['config']['eliteproductnum']."";
	}
	$array  = $GLOBALS['db']->getall($query);
	$i      = 1;
	foreach($array as $key=>$value){
		if($GLOBLAS['config']['eliteproductlen']>0){
			if(strtolower(PHPOE_CHARSET)=="utf-8"){
				$array[$key]['sort_productname'] = $GLOBLAS['config']['eliteproductlen'];
			}else{
				$array[$key]['sort_productname'] = $GLOBLAS['config']['eliteproductlen'];
			}
		}else{
			$array[$key]['sort_productname'] = $value['productname'];
		}

		$array[$key]['filter_content'] = $value['content']; //过滤html标签

		$array[$key]['i'] = $i;
		$i = ($i+1);
		//URL路由处理
		if($GLOBALS['config']['htmltype']=="php"){
			$array[$key]['url'] = "product.php?id=".$value['productid'];
			$array[$key]['caturl'] = "productlist.php?cid=".$value['cateid'];
		}else{
			$array[$key]['url'] = "product-".$value['productid'].".html";
			$array[$key]['caturl'] = "product-cat-".$value['cateid'].".html";
		}

	}
	return $array;
}

/**
	@ 解决方案分类 (只含一级)
	@ param int $num   -- 显示数量 0-不限制
	@ param int $elite -- 是否推荐 1--Y,0--N
	@ 返回值 array()
	@ date 2011.05.20
*/
function template_projectcate($num=0,$elite=0){
	$query  = "SELECT cateid,catename FROM ".DB_PREFIX."projectcate WHERE flag=1 AND parentid=0";
	if($elite>0){
		$query .= " AND elite=1";
	}
	$query .= " ORDER BY orders ASC";
	if($num>0){
		$query .= " LIMIT ".$num."";
	}
	$array  = $GLOBALS['db']->getall($query);
	foreach($array as $key=>$value){
		//URL路由处理
		if($GLOBALS['config']['htmltype']=="php"){
			$array[$key]['url'] = "projectlist.php?cid=".$value['cateid'];
		}else{
			$array[$key]['url'] = "project-cat-".$value['cateid'].".html";
		}
	}
	return $array;
}

/**
	@ 解决方案分类（含二级分类）
	@ param int $rootnum  -- 一级分类数量 0表示不限制
	@ param int $childnum -- 二级分类数量 0表示不限制
	@ 返回值 array()
	@ date 2011.05.20
*/
function template_projecttreecate($rootnum=0,$childnum=0){
	if(!isnumber($rootnum)){
		$rootnum = 10;
	}
	if(!isnumber($childnum)){
		$childnum = 10;
	}
	$category = array();
	$parent_sql = "SELECT cateid,catename FROM ".DB_PREFIX."projectcate WHERE parentid=0 AND depth=0 AND flag=1 ORDER BY orders ASC";
	if(intval($rootnum)>0){
		$parent_sql.=" LIMIT ".$rootnum."";
	}
	$parent_cate = $GLOBALS['db']->getall($parent_sql);
	foreach($parent_cate as $parent_key => $parent_value){
		//URL路由处理
		if($GLOBALS['config']['htmltype']=="php"){
			$parent_url = "projectlist.php?cid=".$parent_value['cateid'];
		}else{
			$parent_url = "project-cat-".$parent_value['cateid'].".html";
		}
		$category[] = array('cateid'=>$parent_value['cateid'],'catename'=>$parent_value['catename'],'url'=>$parent_url,'childcategory'=>array());
		//读取子类
		$child_sql = "SELECT cateid,catename FROM ".DB_PREFIX."projectcate WHERE parentid=".$parent_value['cateid']." AND flag=1 ORDER BY orders ASC";
		if($childnum>0){
			$child_sql.=" LIMIT $childnum";
		}
		$child_cate = $GLOBALS['db']->getall($child_sql);
		foreach($child_cate as $child_key => $child_value){
			//URL路由处理
			if($GLOBALS['config']['htmltype']=="php"){
				$child_url = "projectlist.php?cid=".$child_value['cateid'];
			}else{
				$child_url = "project-cat-".$child_value['cateid'].".html";
			}
			$category[count($category)-1]['childcategory'][] = array('cateid'=>$child_value['cateid'],'catename'=>$child_value['catename'],'url'=>$child_url);
		}
	}
	return $category;
}

/**
	@ 最新解决方案
	@ param int $cateid -- 分类 0不限制
	@ param int $num    -- 显示数量 0-不限制
	@ param int $elite  -- 是否推荐 1--Y，0--N
	@ 返回值 array
	@ date 2011.05.20
*/
function template_newproject($cateid=0,$num=0,$elite=0){
	$searchsql = " WHERE p.flag=1";
    if((int)$cateid>0){
		$childs_sql = common_unlimite_cate_childsql("projectcate","p",$cateid,""); //所有子类
		if(ischar($childs_sql)){
			$searchsql .= " AND (p.cateid=$cateid".$childs_sql.")";
		}else{
			$searchsql .= " AND p.cateid=$cateid";
		}
	}
	if((int)$elite>0){
		$searchsql .= " AND p.elite=1";
	}
 	$query  = "SELECT p.*,c.catename AS catename FROM ".DB_PREFIX."project AS p".
             " LEFT JOIN ".DB_PREFIX."projectcate AS c ON p.cateid=c.cateid".
		       $searchsql." ORDER BY p.projectid DESC";

	if((int)$num>0){
		$query .= " LIMIT ".$num."";
	}else{
	    $query .= " LIMIT ".$GLOBALS['config']['projectnum']."";
	}
	$array  = $GLOBALS['db']->getall($query);
	$i      = 1;
	foreach($array as $key=>$value){
		if($GLOBALS['config']['projectlen']>0){
			if(strtolower(PHPOE_CHARSET)=="utf-8"){
				$array[$key]['sort_title'] = $GLOBALS['config']['projectlen'];
			}else{
				$array[$key]['sort_title'] = $GLOBALS['config']['projectlen'];
			}
		}else{
			$array[$key]['sort_title'] = $value['title'];
		}

		//过滤html_摘要
		if($value['summary']!=""){
			$intro  = $value['summary'];
		}else{
			$intro  = $value['content'];
		}
		//$intro = cut_str($intro,300,0,"gbk");
		$array[$key]['filter_content'] = $intro; //过滤html的content

		$array[$key]['i'] = $i;
		$i = ($i+1);

		//URL路由处理
		if($GLOBALS['config']['htmltype']=="php"){
			$array[$key]['url'] = "project.php?id=".$value['projectid'];
			$array[$key]['caturl'] = "projectlist.php?cid=".$value['cateid'];
		}else{
			$array[$key]['url'] = "project-".$value['projectid'].".html";
			$array[$key]['caturl'] = "project-cat-".$value['cateid'].".html";
		}
	}
	return $array;
}
?>