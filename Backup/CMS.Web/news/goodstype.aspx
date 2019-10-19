<%@ Page Language="C#" AutoEventWireup="true" Inherits="PageView<XH_GoodsTypeModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>现货类型</title>
    <!-- #include file="/controls/header.htm" -->
    <script src="../js/goodsType.js" type="text/javascript"></script>
</head>
<body>
    <div class="con_p">
        <div class="cz_bk">
            <div class="lm_div">管理选项</div>
            <div class="cz_xx">
                <a href="goodstype.aspx" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-edit' >现货列表</a> | <a class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add'  href="javascript:void(0)" id="addActualsType">添加现货类型</a>
            </div>
        </div>
        <div class="cz_bk">
            <div class="lm_div">搜索信息</div>
                 <div class="cz_xx" id="actualsassociateinfo">
                    <span>渤海现货代码：</span><input type="text" id="txtserachActualsName"  maxlength="20" />
                     <span>品种名称：</span><input type="text" id="txtserachStockName" maxlength="20" />
                     <span>品种类型:</span><input type="text" id="txtserachTypeName" maxlength="20" />
                     <a href="#" id="btnSearch" class="easyui-linkbutton" iconCls="icon-find">搜索信息</a>
                  </div>
            </div>
        <div class="cz_bk">
         <table id="pgActualsGoods"></table>
        </div>
    </div>

     <div id="divbody" title="现货关联" style="padding: 8px; display: none">
        <table cellpadding="4" border="0px">
            <tr>
                <td style="width: 100px">
                    渤海现货代码:
                </td>
                <td>
                    <input name="ActualsCode" type="text" maxlength="8" id="txtActualsCode" class="w300" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    渤海现货名称:
                </td>
                <td>
                    <input name="ActualsName" type="text" maxlength="20" id="txtActualsName" class="w300" />
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
