<%@ Page Language="C#" AutoEventWireup="true" Inherits="PageView<ArticlePageModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>标题前标签</title>
    <!-- #include file="/controls/header.htm" -->
    <script src="../js/actualsassociate.js" type="text/javascript"></script>
</head>
<body>
    <div class="con_p">
        <div class="cz_bk">
            <div class="lm_div">管理选项</div>
            <div class="cz_xx">
                <a href="actualsassociatelist.aspx" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-edit' >现货关联列表</a> | <a class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add'  href="javascript:void(0)" id="addActualsAssociate">添加现货关联</a>
            </div>
        </div>
        <div class="cz_bk">
            <div class="lm_div">搜索信息</div>
                 <div class="cz_xx" id="actualsassociateinfo">
                    <span>渤海现货名称：</span><input type="text" id="txtserachActualsName"  maxlength="20" />
                     <span>关联品种名称：</span><input type="text" id="txtserachStockName" maxlength="20" />
                     <span>关联品种类型:</span>
                     <select id="drpTypeId">
                        <option value="0">--请选择--</option>
                         <option value="1">A股</option>
                          <option value="2">国内期货</option>
                           <option value="3">外盘</option>                         
                     </select>
                     <a href="#" id="btnSearch" class="easyui-linkbutton" iconCls="icon-find">搜索信息</a>
                  </div>
            </div>
        <div class="cz_bk">
         <table id="pgActualsAssociate"></table>
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
            <tr>
                <td style="width: 100px">
                    关联品种类型:
                </td>
                <td>
                   <select id="sltTypeId" style=" width:304px;">
                         <option value="">--请选择--</option>
                         <option value="1">A股</option>
                          <option value="2">国内期货</option>
                           <option value="3">外盘</option>                         
                     </select>
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    关联交易所:
                </td>
                <td>
                    <input name="Exchange" type="text" maxlength="50" id="txtExchange" class="w300" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    关联品种代码:
                </td>
                <td>
                    <input name="StockCode" type="text" maxlength="8" id="txtStockCode" class="w300" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    关联品种名称:
                </td>
                <td>
                    <input name="StockName" type="text" maxlength="20" id="txtStockName" class="w300" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                   <div style="color:Red; line-height:20px;">
                        <span >说明：</span>
                        <p>渤海现货代码:系统默认添加BH,如:BHBAPYT</p>
                         <p>A股:股票号以60开头系统默认添加SH,如:SH600192；以00或者30开头系统默认添加SZ,如:SZ300175</p>
                         <p>国内期货:交易所是郑州系统默认添加ZC,如:ZCCF401；是大连系统默认添加DC，如:DCZCCF311;是上海系统默认添加SC，如:SCCU1401</p>
                         <p>外盘:暂时没特殊规则</p>
                         <p>以上系统默认添加均可手工输入</p>
                   </div>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
