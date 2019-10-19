<%@ Control Language="C#" Inherits="UserControlView<AdminControlModel>" %>
<script language="javascript" type="text/javascript">
    function loginout() {
        $.ajax({
            type: "GET",
            url: "/AjaxLogin/Logout.cspx",
            success: function (responseText) {
                window.location = 'login.html';
            }
        });
    }
   function editpass(adminId){
            $("#EditPass :text").val("");

            ShowEditItemDialog("0", 'EditPass', 500, 220, function (j_dialog) {
               // var j_waitDialog = ShowWaitMessageDialog();
                var oldpassword = $("#oldpassword").val();
                var password = $("#password").val();
                var repassword = $("#repassword").val();
                if (password == "") {
                    $.messager.alert(g_MsgBoxTitle, "密码不能为空!", "info", function () {
                        // 直接修改页面中的文字
                        return false;
                    });
                }else if (password != repassword) {
                    $.messager.alert(g_MsgBoxTitle, "两次输入的密码不一致。", "info", function () {
                        // 直接修改页面中的文字
                        return false;
                    });
                } else {
                    $.ajax({
                        type: "GET",
                        url: "/AjaxUserManage/UpdatePassWord.cspx",
                        data: $.param({ oldpassword: oldpassword }) + "&" + $.param({ password: password }) + "&" + $.param({ repassword: repassword }) + "&" + $.param({ adminId: adminId }),
                        complete: function () { HideWaitMessageDialog(j_waitDialog); },
                        success: function (responseText) {
                            if (responseText == "000001") {
                                $.messager.alert(g_MsgBoxTitle, "原始密码输入错误。", "info", function () {
                                    // 直接修改页面中的文字
                                    return false;
                                });
                            } else if (responseText == "000002") {
                                $.messager.alert(g_MsgBoxTitle, "两次输入的密码不一致。", "info", function () {
                                    // 直接修改页面中的文字
                                    return false;
                                });
                            } else {
                                $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function () {
                                    // 直接修改页面中的文字
                                    j_dialog.hide().dialog('close');
                                });
                            }
                        }
                    });
                }
            });
}

</script>
<table width="100%" height="59" cellspacing="0" cellpadding="0" border="0">
  <tbody>
  <tr id="header">
    <td height="59" align="center" width="10%">
 		 <img src="/themes/default/images/admin/logo.png"> 
     </td>
     <td height="59" valign="middle" width="90%">
    <div id="menu">
        <ul>
            <%
                int i=0;
                foreach(AdminMenuModule adminOneMenu in Model.AdminOneMenuModuleList)
                {
                  if (i == 0)
                  { %>
                 <li class="selected"><a target="left" onclick="selMenu(this,'menu<%=i%>');" class="nav_product"><%=adminOneMenu.ModuleName%></a></li>
            <%}
                  else
                  { %>
            <li><a target="left" onclick="selMenu(this,'menu<%=i%>');" class="nav_service"><%=adminOneMenu.ModuleName%></a></li>
            <%} %>
            
            <%
                i++;
                } %>
         </ul>
    </div>
   <div class="menuright">
   <%=CMS.BLL.UserCookies.AdminName %>  |  <a href="/" target="_blank">网站首页</a> | <a href="javascript:void(0);"  onclick="editpass(<%=CMS.BLL.UserCookies.AdminId %>)"> 修改密码 </a> | <a href="javascript:void(0)"  onclick="loginout()">退出</a>
    
    </div>
     </td>
  </tr>
 </tbody></table>


 <div id="EditPass" title="用户" style="padding: 8px; display: none">
<table cellpadding="4" border="0px">
    <tr>
        <td style="width:66px">原始密码:</td>
	    <td><input name="oldpassword" type="text" maxlength="20" id="oldpassword" class="w300 inTextbox" /> </td>
    </tr>
    <tr>
        <td style="width:66px">新密码:</td>
	    <td><input name="password" type="text" maxlength="20" id="password" class="w300 inTextbox" /></td>
    </tr>
    <tr>
        <td style="width:66px">确认新密码:</td>
	    <td><input name="repassword" type="text" maxlength="20" id="repassword" class="w300 inTextbox" /></td>
    </tr>
   
</table>
</div>