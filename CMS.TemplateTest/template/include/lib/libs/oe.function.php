<?php 
/*-----------------------------------
 * @version: OEcms v2.0 Free(��Ѱ�)
 * @website: www.phpoe.com,www.phpcoo.com
 * @email  : phpzac@foxmail.com
 * @qq     : 944811833,16100225
 * @update : 2011.02.20
 * @˵��   ��ǰ��ҳ�溯��������template_��ͷ���ɶ��ο���
-----------------------------------*/
/**
	@ ��ҳ����
	@ ����ֵ array()
	@ date 2011.05.20
*/
function template_pagenav(){
	$query = "SELECT pageid,title FROM ".DB_PREFIX."page WHERE flag=1 AND showstatus=1 ORDER BY orders ASC";
	$array = $GLOBALS['db']->getall($query);
	foreach($array as $key=>$value){
		//URL·�ɴ���
		if($GLOBALS['config']['htmltype']=="php"){
			$array[$key]['url'] = "page.php?id=".$value['pageid'];
		}else{
			$array[$key]['url'] = "page-".$value['pageid'].".html";
		}
	}
	return $array;
}
/**
	@ �Ƽ���ҳ����
	@ ����ֵ array()
	@ date 2011.05.20
*/
function template_elitepagenav(){
	$query = "SELECT pageid,title FROM ".DB_PREFIX."page WHERE flag=1 AND showstatus=1 ORDER BY orders ASC";
	$array = $GLOBALS['db']->getall($query);
	foreach($array as $key=>$value){
		//URL·�ɴ���
		if($GLOBALS['config']['htmltype']=="php"){
			$array[$key]['url'] = "page.php?id=".$value['pageid'];
		}else{
			$array[$key]['url'] = "page-".$value['pageid'].".html";
		}
	}
	return $array;
}





/**
	@ ��������
	@ param int $type -- ���� 0--ȫ�� 1--���֣�2--LOGO
	@ ����ֵ array
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
	@ ���߿ͷ�
	@ ����ֵ array
	@ date 2011.05.20
*/
function template_onlinechat(){
	$query  = "SELECT ontype,title,number,sitetitle FROM ".DB_PREFIX."onlinechat WHERE flag=1";
	$query .= " ORDER BY ontype ASC,orders ASC";
	return $GLOBALS['db']->getall($query);
}

/**
	@ ���ŷ���
	@ param int $num -- ��ʾ���� 0-������
	@ ����ֵ array
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
		//URL·�ɴ���
		if($GLOBALS['config']['htmltype']=="php"){
			$array[$key]['url'] = "newslist.php?cid=".$value['cateid'];
		}else{
			$array[$key]['url'] = "news-cat-".$value['cateid'].".html";
		}
	}
	return $array;
}



/**
	@ ��Ʒ���� (ֻ��һ��)
	@ param int $num   -- ��ʾ���� 0-������
	@ param int $elite -- �Ƿ��Ƽ� 1--Y��0--N
	@ ����ֵ array()
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
		//URL·�ɴ���
		if($GLOBALS['config']['htmltype']=="php"){
			$array[$key]['url'] = "productlist.php?cid=".$value['cateid'];
		}else{
			$array[$key]['url'] = "product-cat-".$value['cateid'].".html";
		}
	}
	return $array;
}

/**
	@ ��Ʒ���ࣨ���������ࣩ
	@ param int $rootnum  -- һ���������� 0��ʾ������
	@ param int $childnum -- ������������ 0��ʾ������
	@ ����ֵ array()
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

		//URL·�ɴ���
		if($GLOBALS['config']['htmltype']=="php"){
			$parent_url = "productlist.php?cid=".$parent_value['cateid'];
		}else{
			$parent_url = "product-cat-".$parent_value['cateid'].".html";
		}

		$category[] = array('cateid'=>$parent_value['cateid'],'catename'=>$parent_value['catename'],'url'=>$parent_url,'childcategory'=>array());
		//��ȡ����
		$child_sql = "SELECT cateid,catename FROM ".DB_PREFIX."productcate WHERE parentid=".$parent_value['cateid']." AND flag=1 ORDER BY orders ASC";
		if($childnum>0){
			$child_sql.=" LIMIT $childnum";
		}
		$child_cate = $GLOBALS['db']->getall($child_sql);
		foreach($child_cate as $child_key => $child_value){
			//URL·�ɴ���
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
	@ ���²�Ʒ
	@ param int $cateid -- ���� 0������
	@ param int $num    -- ��ʾ���� 0--��ʾĬ��
	@ param int $elite  -- �Ƿ��Ƽ� 1--Y��0--N
	@ ����ֵ array()
	@ date 2011.05.20
*/
function template_newproduct($cateid=0,$num=0,$elite=0){
	$searchsql = " WHERE p.flag=1";
    if((int)$cateid>0){
		$childs_sql = common_unlimite_cate_childsql("productcate","p",$cateid,""); //��������
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
        $array[$key]['filter_content'] = $value['content']; //����html��ǩ

		$array[$key]['i'] = $i;
		$i = ($i+1);

		//URL·�ɴ���
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
	@ �Ƽ���Ʒ
	@ param int $cateid -- ���� 0������
	@ param int $num    -- ��ʾ���� 0--��ʾĬ��
	@ ����ֵ array()
	@ date 2011.05.20
*/
function template_eliteproduct($cateid=0,$num=0){
	$searchsql = " WHERE p.flag=1 and p.elite=1";
    if((int)$cateid>0){
		$childs_sql = common_unlimite_cate_childsql("productcate","p",$cateid,""); //��������
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

		$array[$key]['filter_content'] = $value['content']; //����html��ǩ

		$array[$key]['i'] = $i;
		$i = ($i+1);
		//URL·�ɴ���
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
	@ ����������� (ֻ��һ��)
	@ param int $num   -- ��ʾ���� 0-������
	@ param int $elite -- �Ƿ��Ƽ� 1--Y,0--N
	@ ����ֵ array()
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
		//URL·�ɴ���
		if($GLOBALS['config']['htmltype']=="php"){
			$array[$key]['url'] = "projectlist.php?cid=".$value['cateid'];
		}else{
			$array[$key]['url'] = "project-cat-".$value['cateid'].".html";
		}
	}
	return $array;
}

/**
	@ ����������ࣨ���������ࣩ
	@ param int $rootnum  -- һ���������� 0��ʾ������
	@ param int $childnum -- ������������ 0��ʾ������
	@ ����ֵ array()
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
		//URL·�ɴ���
		if($GLOBALS['config']['htmltype']=="php"){
			$parent_url = "projectlist.php?cid=".$parent_value['cateid'];
		}else{
			$parent_url = "project-cat-".$parent_value['cateid'].".html";
		}
		$category[] = array('cateid'=>$parent_value['cateid'],'catename'=>$parent_value['catename'],'url'=>$parent_url,'childcategory'=>array());
		//��ȡ����
		$child_sql = "SELECT cateid,catename FROM ".DB_PREFIX."projectcate WHERE parentid=".$parent_value['cateid']." AND flag=1 ORDER BY orders ASC";
		if($childnum>0){
			$child_sql.=" LIMIT $childnum";
		}
		$child_cate = $GLOBALS['db']->getall($child_sql);
		foreach($child_cate as $child_key => $child_value){
			//URL·�ɴ���
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
	@ ���½������
	@ param int $cateid -- ���� 0������
	@ param int $num    -- ��ʾ���� 0-������
	@ param int $elite  -- �Ƿ��Ƽ� 1--Y��0--N
	@ ����ֵ array
	@ date 2011.05.20
*/
function template_newproject($cateid=0,$num=0,$elite=0){
	$searchsql = " WHERE p.flag=1";
    if((int)$cateid>0){
		$childs_sql = common_unlimite_cate_childsql("projectcate","p",$cateid,""); //��������
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

		//����html_ժҪ
		if($value['summary']!=""){
			$intro  = $value['summary'];
		}else{
			$intro  = $value['content'];
		}
		//$intro = cut_str($intro,300,0,"gbk");
		$array[$key]['filter_content'] = $intro; //����html��content

		$array[$key]['i'] = $i;
		$i = ($i+1);

		//URL·�ɴ���
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