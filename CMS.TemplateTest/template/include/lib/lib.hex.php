<?php
/*
   软件传过来的值进行解密
*/
$secret=$_GET['secret'];
$customerId=$_GET['customerId'];
$productId=$_GET['productId'];
$moduleId=$_GET['moduleId'];
$campaignId=$_GET['campaignId'];
$md='1';
$level = "product";





$decret = hex2Bins($secret);//十六进制转为二进制
$decret = base64_decode($decret);//进行base64解码
$str1 = str_replace("upchina2012","",$decret);//将密钥进行替换
$username = substr($str1,0,-32);// 从后向前取32位长度
$password = str_replace($username,"",$str1); //剩余部分为用户名

$_SESSION['username']=$username;
$_SESSION['password']=$password;

function hex2Bins($hex_string) {
    return pack('H*', $hex_string);
}


?>
