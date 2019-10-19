<?php
/*-----------------------------------
 * @version: OEcms v2.0 Free(免费版)
 * @website: www.phpoe.com,www.phpcoo.com
 * @email  : phpzac@foxmail.com
 * @qq     : 944811833,16100225
 * @update : 2011.02.20
 * @说明   ：公共标签，区分大小写 可二次开发
-----------------------------------*/



/**
	@ 标签说明：友情连接_文字_系统默认
	@ 标签名称：li_link
	@ 标签用法：{foreach $li_link as ...}{/foreach}
	@ date 2011.04.20
*/
$li_link = template_link(1);
$tpl->assign("li_link",$li_link);
/**
	@ 标签说明：友情连接_LOGO_系统默认
	@ 标签名称：li_linklogo
	@ 标签用法：{foreach $li_linklogo as ...}{/foreach}
	@ date 2011.04.20
*/
$li_linklogo = template_link(2);
$tpl->assign("li_linklogo",$li_linklogo);



/**
	@ 标签说明：单页导航_系统默认
	@ 标签名称：li_pagenav
	@ 标签用法：{foreach $li_pagenav as ...}{/foreach}
	@ date 2011.04.20
*/
$li_pagenav = template_pagenav();
$tpl->assign("li_pagenav",$li_pagenav);
/**
	@ 标签说明：推荐显示的导航_系统默认
	@ 标签名称：li_elitepagenav
	@ 标签用法：{foreach $li_elitepagenav as ...}{/foreach}
	@ date 2011.04.20
*/
$li_elitepagenav = template_elitepagenav();
$tpl->assign("li_elitepagenav",$li_elitepagenav);



/**
	@ 标签说明：在线客服_系统默认
	@ 标签名称：li_onlinechat
	@ 标签用法：{foreach $li_onlinechat as ...}{/foreach}
	@ date 2011.04.20
*/
$li_onlinechat = template_onlinechat();
$tpl->assign("li_onlinechat",$li_onlinechat);


/**
	@ 标签说明：资讯分类列表_系统默认
	@ 标签名称：li_newscate
	@ 标签用法：{foreach $li_newscate as ...}{/foreach}
	@ date 2011.04.20
*/
$li_newscate = template_newscate();
$tpl->assign("li_newscate",$li_newscate);




/**
	@ 标签说明：产品分类_一级分类 _系统默认
	@ 标签名称：li_productcate
	@ 标签用法：{foreach $li_productcate as ...}{/foreach}
	@ date 2011.04.20
*/
$li_productcate = template_productcate();
$tpl->assign("li_productcate",$li_productcate);
/**
	@ 标签说明：产品分类_二级分类 _系统默认
	@ 标签名称：li_productcatetree
	@ 标签用法：{foreach $li_productcatetree as ...}{/foreach}
	@ date 2011.04.20
*/
$li_productcatetree = template_producttreecate();
$tpl->assign("li_productcatetree",$li_productcatetree);
/**
	@ 标签说明：最新产品_系统默认
	@ 标签名称：li_newproduct
	@ 标签用法：{foreach $li_newproduct as ...}{/foreach}
	@ date 2011.04.20
*/
$li_newproduct = template_newproduct();
$tpl->assign("li_newproduct",$li_newproduct);
/**
	@ 标签说明：推荐产品_系统默认
	@ 标签名称：li_eliteproduct
	@ 标签用法：{foreach $li_eliteproduct as ...}{/foreach}
	@ date 2011.04.20
*/
$li_eliteproduct = template_eliteproduct();
$tpl->assign("li_eliteproduct",$li_eliteproduct);



/**
	@ 标签说明：解决方案一级分类_系统默认
	@ 标签名称：li_projectcate
	@ 标签用法：{foreach $li_projectcate as ...}{/foreach}
	@ date 2011.04.20
*/
$li_projectcate = template_projectcate();
$tpl->assign("li_projectcate",$li_projectcate);
/**
	@ 标签说明：解决方案二级分类_系统默认
	@ 标签名称：li_projectcatetree
	@ 标签用法：{foreach $li_projectcatetree as ...}{/foreach}
	@ date 2011.04.20
*/
$li_projectcatetree = template_projecttreecate();
$tpl->assign("li_projectcatetree",$li_projectcatetree);
/**
	@ 标签说明：最新解决方案_系统默认
	@ 标签名称：li_newproject
	@ 标签用法：{foreach $li_newproject as ...}{/foreach}
	@ date 2011.04.20
*/
$li_newproject = template_newproject();
$tpl->assign("li_newproject",$li_newproject);
/**
	@ 标签说明：推荐解决方案_系统默认
	@ 标签名称：li_eliteproject
	@ 标签用法：{foreach $li_eliteproject as ...}{/foreach}
	@ date 2011.04.20
*/
$li_eliteproject = template_newproject(0,0,1);
$tpl->assign("li_eliteproject",$li_eliteproject);
?>