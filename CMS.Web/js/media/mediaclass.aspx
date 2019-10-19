<%@ Page Language="C#" Inherits="PageView<MediaPageModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>菜单管理</title>
    <!-- #include file="/controls/header.htm" -->
    <script type="text/javascript" src="/js/mediaclass.js"></script>
</head>
<body>
   

    <div class="con_p">
        <div class="cz_bk">
            <div class="lm_div">管理选项</div>
            <div class="cz_xx">
                <a href="mediaclass.aspx" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-edit' >文件分类列表</a> | <a class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add'  href="javascript:void(0)" id="addMediaClass">添加一级类别</a>
            </div>
        </div>
        <div class="cz_bk">
            <div class="lm_div">文件分类列表</div>
            <div class="lm_name">
                <table cellpadding="0" cellspacing="0" class="c_table">
                    <tr class="tr_bg">
                        <td width="50%">
                            文件分类名
                        </td>
                        <td width="50%">
                            操作
                        </td>
                    </tr>
                </table>
            </div>
            <table width="100%" cellspacing="0" cellpadding="0" class="c_table">
                <tbody>

                  <%foreach (MediaClass mediaclass in Model.MediaClassList)
                    { %>
                    <tr bgcolor="#F1F6F9">
                        <td width="20%" nowrap="" align="left" class="tr_line">
                            <img width="16" height="16" align="left" border="0" src="/themes/default/images/admin/16-member-add.png">&nbsp;<%=mediaclass.MediaClassName%>
                        </td>
                  
                        <td width="20%" nowrap="" align="center" class="tr_line">
                            <a href="javascript:void(0);" onclick="editMediaClass(<%=mediaclass.MediaClassId%>,'<%=mediaclass.MediaClassName%>',<%=mediaclass.SortId %>)">
                                <img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Update.gif"></a>
                            <a href="javascript:void(0);" onclick="delMediaClass(<%=mediaclass.MediaClassId%>,'<%=mediaclass.MediaClassName%>')">
                                <img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Del.gif"></a>
                        </td>
                    </tr>
                   <%} %>
                </tbody>
            </table>
        </div>
    </div>

     <div id="MediaClass" title="文件类别" style="padding: 8px; display: none">
        <table cellpadding="4" border="0px">
            <tr>
                <td style="width: 80px">
                    分类名称:
                </td>
                <td>
                    <input name="MediaClassName" type="text" maxlength="40" id="MediaClassName" class="w300" />
                </td>
            </tr>
            <tr>
                <td style="width: 80px">
                    排序:
                </td>
                <td>
                    <input name="Sort" type="text" maxlength="20" id="Sort" style="width:40px" value="0" />
                </td>
            </tr>
        </table>
    </div>

</body>
</html>
