 <!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>后台管理系统</title>
 <!-- #include file="/controls/header.htm" -->  
    <script type="text/javascript" src="/js/sitemaster.js"></script>
     
<script language="javascript">
    function ShowLinks(obj) {
        if ($(obj).attr("class") != "links") {
            $("#left_menu ul li a").removeClass("links");
            $(obj).addClass("links");
        }
    }
    function selMenu(obj, menuid) {
        if ($("#" + menuid).css("display", "none")) {
            $("#left_menu ul").css("display", "none");
            $("#" + menuid).css("display", "");
            if ($(obj).parent().attr("class") != "selected") {
                $("#menu ul li").removeClass("selected");
                $(obj).parent().addClass("selected");
                $("#left_menu ul li a").removeClass("links");
                $("#" + menuid).children().first().children().addClass("links");
                var url = $("#" + menuid).children().first().children().attr("href");
                if (url == undefined) {
                    url = "/";
                }
                window.open(url, target = 'main');
            }
        }
    }
</script>
   
</head>
<body>

<div id="pageheader"></div> 
 <table width="100%" height="100%" cellspacing="0" cellpadding="0" border="0">
  <tbody>
   <tr>
    <td  align="left" width="10%">
        <div id="left_menu"></div>
    </td>
	<td colspan="2" align="left" width="90%" height="100%" >
            <iframe frameborder="0" scrolling="yes"  src="" id="main" name="main" style="width: 100%; height: 100%;"></iframe>
    </td>
  </tr>
</tbody></table> 

</body>
</html>
