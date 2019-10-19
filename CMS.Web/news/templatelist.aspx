
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>模板管理</title>
    <!-- #include file="/controls/header.htm" -->
    <script src="../js/jsontimeconvertor.js" type="text/javascript"></script>
    <script src="../js/pagetemplate.js" type="text/javascript"></script>
</head>
<body>


     <div class="con_p"> 
        <div class="cz_bk">
            <div class="lm_div">
                管理选项</div>
            <div class="cz_xx">
              <a href="javascript:void(0)" id="btnAddtemplate" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add' >添加模板</a>               
            </div>          
        </div>
        <div class="cz_bk">
            <div class="lm_div">搜索信息</div>
                 <div class="cz_xx">
                    <span>模板名称：</span><input type="text" id="txtlikeTemplateName"  maxlength="50" />
                     <span>文件名：</span><input type="text" id="txtlikeFileName" maxlength="50" />
                     <select id="slctlikeTempleteType">
                         <option value="0">全部</option>
                          <option value="1">列表页</option>
                           <option value="2">终极页面</option>                         
                     </select>
                     <a href="#" id="btnSearch" class="easyui-linkbutton" iconCls="icon-find">搜索信息</a>
                  </div>
            </div>
            
        <div class="cz_bk">
         <table id="pgTemp"></table>
        </div>


    </div>
    <div id="divbody" title="模板" style="padding: 8px; display:none;">
        <table cellpadding="4" border="0px">
            <tr>
                <td style="width: 70px">
                    模板名称:
                </td>
                <td>
                     <input id="txtTemplateName" name="templateName" class="easyui-validatebox" style="width:280px;"
                        type="text" /><label style="color:Red;">*</label>
                </td>
                 <td style="width: 70px;">
                    模板文件路径:
                </td>
                <td>
                    <input id="txtTemplateFileName" name="templateFileName" class="easyui-validatebox" style="width:280px;"
                        type="text" /><label style="color:Red;">*</label>
                </td>
            </tr>
            <tr>
                <td style="width: 70px;">
                    排序值:
                </td>
                <td>
                    <input id="txtOrderNum" name="orderNum" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" class="easyui-validatebox" style="width:280px;"
                        type="text" /><label style="color:Red;">*</label>
                </td>
                 <td style="width: 100px">
                     模板生成文件路径:
                </td>
                <td>
                 <input name="htmlPath" style=" width:280px;" type="text"  id="txtHtmlPath" class="easyui-validatebox"
                            value="0" /><label style="color:Red;">*</label>
                </td>
            </tr>
             <tr>
                <td style="width: 70px;">
                    站点名:
                </td>
                <td colspan="3">
                    <input id="SiteName" name="SiteName"  class="easyui-validatebox" style="width:280px;" type="text" /> 
                </td>
               
            </tr>
            <tr>
                <td style="width:70px;">
                    简介:
                </td>
                <td colspan="3">
                     <textarea id="txtRemark" name="remark" class="easyui-validatebox" cols="20" rows="2"
                        style="width:680px; height: 300px;"></textarea>
                </td>
            </tr>
            <tr>
                <td style="width: 50px;">
                    状态:
                </td>
                <td>
                    <input id="yes" type="radio" value="true" name="Status" checked="checked"/>开启
                    <input id="no" type="radio" value="false" name="Status" />停用
                </td>
                 <td style="width: 50px;">
                    模板类型:
                </td>
                <td>
                   <select id="slctTempleteType">
                          <option value="1">列表页</option>
                           <option value="2">终极页面</option>                          
                     </select>
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     模板编码:
                     &nbsp;&nbsp;&nbsp;&nbsp;
                     <select id="slctEncoding">
                          <option value="UTF-8">UTF-8</option>
                           <option value="GBK">GBK</option>
                          <option value="GB2312">GB2312</option>
                          <option value="utf-8-1">UTF-8(不带签名)</option>
                     </select>
                     <input id="hdCreatedUser" type="hidden" />
                      <input id="hdCreatedTime" type="hidden" />
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
