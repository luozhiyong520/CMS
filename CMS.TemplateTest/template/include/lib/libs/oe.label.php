<?php
/*-----------------------------------
 * @version: OEcms v2.0 Free(��Ѱ�)
 * @website: www.phpoe.com,www.phpcoo.com
 * @email  : phpzac@foxmail.com
 * @qq     : 944811833,16100225
 * @update : 2011.02.20
 * @˵��   ��������ǩ�����ִ�Сд �ɶ��ο���
-----------------------------------*/



/**
	@ ��ǩ˵������������_����_ϵͳĬ��
	@ ��ǩ���ƣ�li_link
	@ ��ǩ�÷���{foreach $li_link as ...}{/foreach}
	@ date 2011.04.20
*/
$li_link = template_link(1);
$tpl->assign("li_link",$li_link);
/**
	@ ��ǩ˵������������_LOGO_ϵͳĬ��
	@ ��ǩ���ƣ�li_linklogo
	@ ��ǩ�÷���{foreach $li_linklogo as ...}{/foreach}
	@ date 2011.04.20
*/
$li_linklogo = template_link(2);
$tpl->assign("li_linklogo",$li_linklogo);



/**
	@ ��ǩ˵������ҳ����_ϵͳĬ��
	@ ��ǩ���ƣ�li_pagenav
	@ ��ǩ�÷���{foreach $li_pagenav as ...}{/foreach}
	@ date 2011.04.20
*/
$li_pagenav = template_pagenav();
$tpl->assign("li_pagenav",$li_pagenav);
/**
	@ ��ǩ˵�����Ƽ���ʾ�ĵ���_ϵͳĬ��
	@ ��ǩ���ƣ�li_elitepagenav
	@ ��ǩ�÷���{foreach $li_elitepagenav as ...}{/foreach}
	@ date 2011.04.20
*/
$li_elitepagenav = template_elitepagenav();
$tpl->assign("li_elitepagenav",$li_elitepagenav);



/**
	@ ��ǩ˵�������߿ͷ�_ϵͳĬ��
	@ ��ǩ���ƣ�li_onlinechat
	@ ��ǩ�÷���{foreach $li_onlinechat as ...}{/foreach}
	@ date 2011.04.20
*/
$li_onlinechat = template_onlinechat();
$tpl->assign("li_onlinechat",$li_onlinechat);


/**
	@ ��ǩ˵������Ѷ�����б�_ϵͳĬ��
	@ ��ǩ���ƣ�li_newscate
	@ ��ǩ�÷���{foreach $li_newscate as ...}{/foreach}
	@ date 2011.04.20
*/
$li_newscate = template_newscate();
$tpl->assign("li_newscate",$li_newscate);




/**
	@ ��ǩ˵������Ʒ����_һ������ _ϵͳĬ��
	@ ��ǩ���ƣ�li_productcate
	@ ��ǩ�÷���{foreach $li_productcate as ...}{/foreach}
	@ date 2011.04.20
*/
$li_productcate = template_productcate();
$tpl->assign("li_productcate",$li_productcate);
/**
	@ ��ǩ˵������Ʒ����_�������� _ϵͳĬ��
	@ ��ǩ���ƣ�li_productcatetree
	@ ��ǩ�÷���{foreach $li_productcatetree as ...}{/foreach}
	@ date 2011.04.20
*/
$li_productcatetree = template_producttreecate();
$tpl->assign("li_productcatetree",$li_productcatetree);
/**
	@ ��ǩ˵�������²�Ʒ_ϵͳĬ��
	@ ��ǩ���ƣ�li_newproduct
	@ ��ǩ�÷���{foreach $li_newproduct as ...}{/foreach}
	@ date 2011.04.20
*/
$li_newproduct = template_newproduct();
$tpl->assign("li_newproduct",$li_newproduct);
/**
	@ ��ǩ˵�����Ƽ���Ʒ_ϵͳĬ��
	@ ��ǩ���ƣ�li_eliteproduct
	@ ��ǩ�÷���{foreach $li_eliteproduct as ...}{/foreach}
	@ date 2011.04.20
*/
$li_eliteproduct = template_eliteproduct();
$tpl->assign("li_eliteproduct",$li_eliteproduct);



/**
	@ ��ǩ˵�����������һ������_ϵͳĬ��
	@ ��ǩ���ƣ�li_projectcate
	@ ��ǩ�÷���{foreach $li_projectcate as ...}{/foreach}
	@ date 2011.04.20
*/
$li_projectcate = template_projectcate();
$tpl->assign("li_projectcate",$li_projectcate);
/**
	@ ��ǩ˵�������������������_ϵͳĬ��
	@ ��ǩ���ƣ�li_projectcatetree
	@ ��ǩ�÷���{foreach $li_projectcatetree as ...}{/foreach}
	@ date 2011.04.20
*/
$li_projectcatetree = template_projecttreecate();
$tpl->assign("li_projectcatetree",$li_projectcatetree);
/**
	@ ��ǩ˵�������½������_ϵͳĬ��
	@ ��ǩ���ƣ�li_newproject
	@ ��ǩ�÷���{foreach $li_newproject as ...}{/foreach}
	@ date 2011.04.20
*/
$li_newproject = template_newproject();
$tpl->assign("li_newproject",$li_newproject);
/**
	@ ��ǩ˵�����Ƽ��������_ϵͳĬ��
	@ ��ǩ���ƣ�li_eliteproject
	@ ��ǩ�÷���{foreach $li_eliteproject as ...}{/foreach}
	@ date 2011.04.20
*/
$li_eliteproject = template_newproject(0,0,1);
$tpl->assign("li_eliteproject",$li_eliteproject);
?>