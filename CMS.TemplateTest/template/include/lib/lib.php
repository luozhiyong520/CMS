<?php



function phpcoo_copyright( )
{
		echo "<div align='center'><font color='#999999'>Powered by <a href='http://www.upchina.com/' target=_blank>upcms</a> &copy;2012-2013 <a href='http://www.upchina.com/' target='_blank'>www.upchina.com</a></font></div>";
}

error_reporting( E_ALL & ~E_NOTICE );
if ( __FILE__ == "" )
{
		exit( "Fatal error code: 0" );
}
define( "PATH_ROOT", str_replace( "include/lib/lib.php", "", str_replace( "\\", "/", __FILE__ ) ) );

if ( DIRECTORY_SEPARATOR == "\\" )
{
		@ini_set( "include_path", ".;".ROOT_PATH );
}
else
{
		@ini_set( "include_path", ".:".ROOT_PATH );
}

require( PATH_ROOT."include/lib/lib.timer.php" );
$libtimer = new lib_timer( );
$libtimer->start( );
require( PATH_ROOT."include/lib/config.php" );
require( PATH_ROOT."include/lib/libs/oe.config.php" );
require( PATH_ROOT."include/lib/libs/var.inc.php" );
require( PATH_ROOT."include/lib/libs/oe.page.php" );
require( PATH_ROOT."include/lib/libs/oe.data.php" );
require( PATH_ROOT."include/lib/libs/oe.function.php" );
require( PATH_ROOT."include/lib/lib.mysql.php" );

//¿ÉÐ´ÕÊºÅ
$db = new lib_mysql( $upchina_dbhost_w, $upchina_dbuser_w, $upchina_dbpasswd_w, $upchina_dbname_w, $upchina_dbcharset_w );
//Ö»¶ÁÕÊºÅ
$dbRead = new lib_mysql( $upchina_dbhost_r, $upchina_dbuser_r, $upchina_dbpasswd_r, $upchina_dbname_r, $upchina_dbcharset_r );

$config = array( );

$urlsuffix = "php";
if ( $config['htmltype'] == "html" || $config['htmltype'] == "rewrite" )
{
		$urlsuffix = "html";
}
require( PATH_ROOT."include/lib/lib.page.php" );
require( PATH_ROOT."include/lib/lib.main.php" );

require( PATH_ROOT."include/lib/lib.function.php" );
require( PATH_ROOT."include/lib/lib.command.php" );

if ( !file_exists( PATH_ROOT."data/_caches" ) )
{
		@mkdir( PATH_ROOT."data/_caches", 511 );
		@chmod( PATH_ROOT."data/_caches", 511 );
}
if ( !file_exists( PATH_ROOT."data/_compiled" ) )
{
		@mkdir( PATH_ROOT."data/_compiled", 511 );
		@chmod( PATH_ROOT."data/_compiled", 511 );
}
clearstatcache( );

require(PATH_ROOT."include/lib/smarty/Smarty.class.php" );
$tpl = new Smarty( );
$tpl->setTemplateDir( PATH_ROOT );
$tpl->setCacheDir( PATH_ROOT."data/_caches" );
$tpl->setCompileDir( PATH_ROOT."data/_compiled" );
$tpl->setCaching( FALSE );
$tpl->allow_php_tag = TRUE;
$tpl->allow_php_templates = TRUE;
$tpl->compile_check = TRUE;
$tpl->force_compile = FALSE;
$tpl->debugging = FALSE;
$tpl->registerResource( "db", array( "db_get_template", "db_get_timestamp", "db_get_secure", "db_get_trusted" ) );
$tpl->registerResource( "str", array( "str_get_template", "str_get_timestamp", "str_get_secure", "str_get_trusted" ) );
$tpl->assign( "label_sitename", $config['sitename'] );
$tpl->assign( "label_sitetitle", $config['sitetitle'] );
$tpl->assign( "label_siteurl", $config['siteurl'] );
$tpl->assign( "label_logo", $config['logoimg'] );
$tpl->assign( "label_logowidth", $config['logowidth'] );
$tpl->assign( "label_logoheight", $config['logoheight'] );
$tpl->assign( "label_banner", $config['bannerimg'] );
$tpl->assign( "label_bannerwidth", $config['bannerwidth'] );
$tpl->assign( "label_bannerheight", $config['bannerheight'] );
$tpl->assign( "label_qqstatus", $config['qqstatus'] );
$tpl->assign( "label_metadescription", $config['metadescription'] );
$tpl->assign( "label_metakeyword", $config['metakeyword'] );
$tpl->assign( "label_editor", $config['editor'] );
$tpl->assign( "label_sitecopyright", $config['sitecopyright'] );
$tpl->assign( "label_item1", $config['itemcontent1'] );
$tpl->assign( "label_item2", $config['itemcontent2'] );
$tpl->assign( "label_item3", $config['itemcontent3'] );
$tpl->assign( "label_item4", $config['itemcontent4'] );
$tpl->assign( "label_item5", $config['itemcontent5'] );
$tpl->assign( "label_item6", $config['itemcontent6'] );
$tpl->assign( "label_item7", $config['itemcontent7'] );
$tpl->assign( "label_item8", $config['itemcontent8'] );
$tpl->assign( "label_item9", $config['itemcontent9'] );
$tpl->assign( "label_item10", $config['itemcontent10'] );
$tpl->assign( "label_htmltype", $config['htmltype'] );
$tpl->assign( "label_htmlext", $config['htmlext'] );
$tpl->assign( "label_googleseo", $config['googleseo'] );
$tpl->assign( "label_yahooseo", $config['yahooseo'] );
$tpl->assign( "label_bingseo", $config['bingseo'] );
$tpl->assign( "skinpath", INDEX_TEMPLATE );
$tpl->assign( "urlsuffix", $urlsuffix );
$tpl->assign( "urltype", $config['htmltype'] );
$tpl->assign( "tagrange", $config['tagrange'] );
$tpl->assign( "tagurlnum", $config['tagurlnum'] );
$tpl->assign( "page_charset", PHPOE_CHARSET );
$tpl->assign( "INDEX_TEMPLATE", INDEX_TEMPLATE );
$tpl->assign( "ADMIN_TEMPLATE", ADMIN_TEMPLATE );
?>
