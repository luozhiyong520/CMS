<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
 <!-- #include file="/controls/header.htm" -->  
<head>
    <title>资讯统计</title>
    <script src="/js/statistics.js" type="text/javascript"></script>
    <script language="javascript">
        $(document).ready(function () {
            $.ajax({
                url: '/AjaxChannel/GetChannelNodeInit.cspx?typeID=1',
                success: function (shtml) {
                    $("#ChannelId").html('');
                    $("#ChannelId").append("<option value='0'>-请选择类别-</option>" + shtml);
                }
            });

            $.ajax({
                url: '/AjaxUserManage/Getadminstor.cspx',
                success: function (shtml) {
                    var dropdownStr = "";
                    for (var i = 0; i < shtml.length; i++) {
                        var str = shtml[i].AdminName;
                        dropdownStr += "<option value='" + str + "'>" + str + "</option>";
                    }
                    $("#AdminId").html('');
                    $("#AdminId").append("<option value='0'>-请选择类别-</option>" + dropdownStr);
                }
            });
        })
    </script>
</head>
<body>
<div class="con_p" style="padding-bottom:100px;">
         <div class="cz_bk">
            <div class="lm_div">搜索信息</div>
   	        <div class="cz_xx">
            <table cellspacing="0" width="100%">
                    <tr>
                    <td><table><tr>
                    <td>
                    栏目名称：
                          <select id="ChannelId" class="inTextbox" name="ChannelId"  style="width:160px">    
                            <option value='0'>-请选择类别-</option>                        
                          </select>
                   </td>

                     <td>编辑人员：
                         <select id="AdminId" class="inTextbox" name="AdminId"  style="width:160px">       
                            <option value='0'>-请选择类别-</option>                        
                          </select>
                     </td>
                    <td class="p10">信息日期 从 </td>
                        <td><input type="text" id="StartTime" class="myTextbox easyui-datebox" style="width: 120px" /></td>
                        <td style="width: 35px; text-align: right"> 到 </td>
                        <td><input type="text" id="EndTiem" class="myTextbox easyui-datebox" style="width: 120px" /></td>
                        <td><a href="#" id="btnQuery" class="easyui-linkbutton" iconCls="icon-find">查找信息</a></td>
                    </tr></table></td>

                    </tr>
                </table>
            </div>
            </div>
            <div class="cz_bk">
                <div class="lm_div">列表</div>
                <div id="tblQueryResult"></div>
            </div>
        </div>
        </div>
</body>
</html>
