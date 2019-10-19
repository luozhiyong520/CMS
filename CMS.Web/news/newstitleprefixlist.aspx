<%@ Page Language="C#" AutoEventWireup="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>标题前标签</title>
    <!-- #include file="/controls/header.htm" -->
    <script src="../js/newstitleprefix.js" type="text/javascript"></script>
</head>
<body>
    <div class="con_p">
        <div class="cz_bk">
            <div class="lm_div">管理选项</div>
            <div class="cz_xx">
                <a href="newstitleprefixlist.aspx" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-edit' >标题前标签列表</a> | <a class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add'  href="javascript:void(0)" id="addNewsTitlePrefix">添加标题前标签</a>
            </div>
        </div>
        <div class="cz_bk">
         <table id="pgnewstitleprefix"></table>
        </div>
    </div>

     <div id="divbody" title="标题前标签" style="padding: 8px; display: none">
        <table cellpadding="4" border="0px">
            <tr>
                <td style="width: 100px">
                    标题前标签名称:
                </td>
                <td>
                    <input name="Prefix" type="text" maxlength="50" id="txtPrefix" class="w300" />
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
