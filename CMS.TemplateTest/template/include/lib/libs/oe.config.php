<?php
/*-----------------------------------
 * @version: OEcms v2.0 Free(免费版)
 * @website: www.phpoe.com,www.phpcoo.com
 * @email  : phpzac@foxmail.com
 * @qq     : 944811833,16100225
 * @update : 2011.02.20
-----------------------------------*/
/** 页面编码 */
define('PHPOE_CHARSET','gbk');
/** 默认时间 中国上海 */
date_default_timezone_set('Asia/Shanghai');

/** 后台关于我们设置 **/
$_LANG = array(
    'a1' => '标签一',
	'a2' => '标签二',
	'a3' => '标签三',
	'a4' => '标签四',
	'a5' => '标签五',
	'a6' => '标签六',
	'a7' => '标签七',
	'a8' => '标签八',
	'a9' => '标签九',
	'a10' => '标签十',
);

/**
	@ 参数配置
	@ param tagchannel -- Tag频道
*/
$_VALUE_PARAMS = array(
	'tagchannel'=>"news#新闻频道|product#产品频道|project#解决方案频道|page#单页频道",
);
?>