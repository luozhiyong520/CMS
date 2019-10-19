<%@ Page Language="C#" Inherits="PageView<UserManagePageModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>菜单管理</title>
    <!-- #include file="/controls/header.htm" -->
    <script type="text/javascript" src="/js/menumodule.js"></script>
</head>
<body>
    <div class="con_p">
        <div class="cz_bk">
            <div class="lm_div">管理选项</div>
            <div class="cz_xx">
                <a href="menumodule.aspx" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-edit' >菜单列表</a> | <a class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add'  href="javascript:void(0)" id="addMenuModule">添加一级菜单</a>
            </div>
        </div>
        <div class="cz_bk" style="height:650px; overflow:auto; margin-bottom:20px;">
            <div class="lm_div">菜单列表</div>
          
            <table width="100%" cellspacing="0" cellpadding="0" class="c_table" style="margin-bottom:10px;" >
                <tbody>
                    <TR>
                        <TH width="20%" nowrap>菜单名</TH>
                        <TH width="20%" nowrap>菜单链接地址</TH>
                        <TH width="20%" nowrap>添加二级菜单</TH>
                        <TH width="20%" nowrap>状态</TH>
                        <TH width="20%" nowrap>操作</TH>
                    </TR>
                    <% foreach (AdminMenuModule adminMenu in Model.AdminMenuList)
                       { %>
                    <tr bgcolor="#F1F6F9">
                        <td width="20%" nowrap="" align="left" class="tr_line">
                            <img width="16" height="16" align="left" border="0" src="/themes/default/images/admin/16-member-add.png"><span
                                name="ModuleName"><%=adminMenu.ModuleName%></span>
                        </td>
                        <td width="20%" nowrap="" align="center" class="tr_line">
                            <%=adminMenu.TargetUrl %>
                        </td>
                        <td width="20%" nowrap="" align="center" class="tr_line">
                            <img width="16" height="16" align="middle" border="0" src="/themes/default/images/admin/Item.Doc.Insert.gif"
                                onclick="addTwoMenuModule(<%=adminMenu.ModuleId%>)" style="cursor: pointer">
                        </td>
                        <td width="20%" nowrap="" align="center" class="tr_line">
                            <%if (adminMenu.Status == 1)
                              { %>启用
                            <%  }
                              else
                              {%>
                            停用
                            <% } %>
                        </td>
                        <td width="20%" nowrap="" align="center" class="tr_line">
                            <a href="javascript:void(0);" onclick="editMenuModule(<%=adminMenu.ModuleId %>,'<%=adminMenu.ModuleName%>')">
                                <img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Update.gif"></a>
                            <a href="javascript:void(0);" onclick="delOneModule(<%=adminMenu.ModuleId%>,'<%=adminMenu.ModuleName%>')">
                                <img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Del.gif"></a>
                        </td>
                    </tr>
                    <%if (Model.Dic[adminMenu.ModuleId] != null)
                      { %>
                    <tr>
                        <td colspan="5" style="border: 0">
                            <table width="100%" cellspacing="0" cellpadding="0" class="c_table">
                                <tbody>
                                    <% foreach (AdminMenuModule adminTwoMenu in Model.Dic[adminMenu.ModuleId])
                                       { %>
                                    <tr height="16">
                                        <td width="20%" nowrap="" align="left" class="tr_line">
                                            <img width="16" height="16" align="middle" border="0" src="/themes/default/images/admin/T.gif"><img
                                                width="16" height="16" align="middle" border="0" src="/themes/default/images/admin/16-member.png"><%=adminTwoMenu.ModuleName%>
                                        </td>
                                        <td width="20%" nowrap="" align="center" class="tr_line">
                                            <%=adminTwoMenu.TargetUrl %>
                                        </td>
                                        <td width="20%" nowrap="" align="center" class="tr_line">
                                        </td>
                                        <td width="20%" nowrap="" align="center" class="tr_line">
                                            <%if (adminTwoMenu.Status == 1)
                                              { %>启用
                                            <%  }
                                              else
                                              {%>
                                            停用
                                            <% } %>
                                        </td>
                                        <td width="20%" nowrap="" align="center" class="tr_line">
                                            <a href="javascript:void(0);" onclick="editTwoMenuModule(<%=adminTwoMenu.ModuleId%>)">
                                                <img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Update.gif"></a>
                                            <a href="javascript:void(0);" onclick="delTwoModule(<%=adminTwoMenu.ModuleId%>,'<%=adminTwoMenu.ModuleName%>')">
                                                <img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Del.gif"></a>
                                        </td>
                                    </tr>
                                    <%} %>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <%} %>
                    <%} %>
                </tbody>
            </table>
        </div>
    </div>
    <div id="OneMenuModuleItem" title="一级菜单" style="padding: 8px; display: none">
        <table cellpadding="4" border="0px">
            <tr>
                <td style="width: 80px">
                    菜单名称:
                </td>
                <td>
                    <input name="modulename" type="text" maxlength="40" id="modulename" class="w300 inTextbox" />
                </td>
            </tr>
        </table>
    </div>
    <div id="TwoMenuModuleItem" title="二级菜单" style="padding: 8px; display: none">
        <table cellpadding="4" border="0px">
            <tr>
                <td style="width: 80px">
                    二级菜单名:
                </td>
                <td>
                    <input name="moduletwoname" type="text" maxlength="40" id="moduletwoname" class="w300 inTextbox" />
                </td>
            </tr>
            <tr>
                <td style="width: 80px">
                    菜单链接地址:
                </td>
                <td>
                    <input name="targeturl" type="text" maxlength="80" id="targeturl" class="w300 inTextbox" />
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
