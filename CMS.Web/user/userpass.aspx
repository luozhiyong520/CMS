<%@ Page Language="C#" Inherits="PageView<UserManagePageModel>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>密码设置</title>
    <!-- #include file="/controls/header.htm" -->  
    <script type="text/javascript" src="/js/usermanage.js"></script>
</head>

<body>
<div class="con_p" style="padding-bottom:100px;">
    
   

   <div class="cz_bk">
      <div class="lm_div">企业全网平台查询端口密码设置</div>
  <div style="padding:20px;">
     
     <input type="text" value="<%=Model.password%>" id="updatepass" /> 
    <input type="button" id="setpass" value="修  改" />

  </div>
  </div>

</div>


     <script language="javascript">
   


         
         $("#setpass").click(function () {       
                 $.ajax({
                     type: "GET",
                     url: "/AjaxUserManage/updatepass.cspx?pass=" + $.trim($("#updatepass").val()),
                     success: function (responseText) {
                         if (responseText == "000000") {
                             alert("设置成功");
                         }
                     }
                 });
           
         });
       
       

    </script>
 

</body>