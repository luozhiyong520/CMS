<?php
/*
   �����������ֵ���н���
*/
$secret=$_GET['secret'];
$customerId=$_GET['customerId'];
$productId=$_GET['productId'];
$moduleId=$_GET['moduleId'];
$campaignId=$_GET['campaignId'];
$md='1';
$level = "product";





$decret = hex2Bins($secret);//ʮ������תΪ������
$decret = base64_decode($decret);//����base64����
$str1 = str_replace("upchina2012","",$decret);//����Կ�����滻
$username = substr($str1,0,-32);// �Ӻ���ǰȡ32λ����
$password = str_replace($username,"",$str1); //ʣ�ಿ��Ϊ�û���

$_SESSION['username']=$username;
$_SESSION['password']=$password;

function hex2Bins($hex_string) {
    return pack('H*', $hex_string);
}


?>
