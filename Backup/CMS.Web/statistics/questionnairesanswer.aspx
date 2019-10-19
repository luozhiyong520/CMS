<%@ Page Language="C#" AutoEventWireup="true" Inherits="PageView<QuestionnairesAnswerPageModel>" %>
<%@ Import Namespace="CMS.Model" %>
<%@ Import Namespace="CMS.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>问卷调查统计</title>
    <style type="text/css">
        .style1
        {
            width: 105px;
        }
        .style2
        {
            width: 109px;
        }
        .style4
        {
            width: 128px;
        }
        .style7
        {
            width: 44px;
        }
        .style8
        {
            width: 46px;
        }
        .style9
        {
            width: 45px;
        }
        .style10
        {
            width: 51px;
        }
        .style11
        {
            width: 15px;
        }
        .style12
        {
            width: 57px;
        }
        
        .ble {border: 1px solid #B1CDE3;  border-collapse: collapse; }  
        .ble td {border: 1px solid #B1CDE3;  background: #fff;  padding: 3px 3px 3px 8px;  }  
         body{font-size:12px;  color: #4f6b72;  }
         
        .btb{border: 1px solid red ;}
         
        #questionnai
        {
            width: 149px;
        }
         
    </style>
  <!-- #include file="/controls/header.htm" -->  
    <script type="text/javascript">
        $(function () {
            LoadStyle();
            var mydate = new Date();
            var year = mydate.getFullYear();
            var month = mydate.getMonth() + 1;
            var day = mydate.getDate();

            $("#Inquiry").click(function () {

                var dateRange = GetDateRange2("StartTime", "EndTiem");
                var txtStart = dateRange.StartDate == "" ? "" : dateRange.StartDate + " 00:00:00";
                var txtEnd = dateRange.EndDate == "" ? "" : dateRange.EndDate + " 23:59:59";
                GoInquiry(txtStart, txtEnd + "&t=6");
            });
            
            $("#questionnai").val('<%=Model.CurrentQid %>');
            $("#Today").click(function () {
                GoInquiry(year + "-" + month + "-" + day + " 00:00:00", year + "-" + month + "-" + day + " 23:59:59&t=1");
            });

            $("#qt").click(function () {
                GoInquiry(year + "-" + month + "-" + (day - 1) + " 00:00:00", year + "-" + month + "-" + (day - 1) + " 23:59:59&t=2");
            });

            $("#Week").click(function () {
                GoInquiry(year + "-" + month + "-" + (day - mydate.getDay() + 1) + " 00:00:00", year + "-" + month + "-" + day + " 23:59:59&t=3");
            });

            $("#Mouths").click(function () {
                GoInquiry(year + "-" + month + "-01 00:00:00", year + "-" + month + "-30 23:59:59&t=4");
            });

            $("#All").click(function () {
                GoInquiry("", "&t=5");
            });

        })

        function GoInquiry(strat, end) {
            window.location.href = "questionnairesanswer.aspx?QId=" + $("#questionnai").val() + "&Start=" + strat + "&End=" + end;
        }
        
        // 根据二个字符串，返回一个日期范围。
        function GetDateRange2(txtStart, txtEnd) {
            var _date1 = $("#" + txtStart).datebox("getValue");
            var _date2 = $("#" + txtEnd).datebox("getValue");

            var _d1 = parseDate(_date1);
            var _d2 = parseDate(_date2);
            if (_date1.length > 0 && _d1 == null) {
                alert("日期格式输入无效。"); $("#" + txtStart).focus(); return null;
            }
            if (_date2.length > 0 && _d2 == null) {
                alert("日期格式输入无效。"); $("#" + txtEnd).focus(); return null;
            }
            if (_date1.length > 0 && _date2.length > 0 && _d1 > _d2) {
                alert("日期范围输入无效。"); $("#" + txtEnd).focus(); return null;
            }
            var obj = { StartDate: _date1, EndDate: _date2 };
            return obj;
        }

        // 解析一个字符串中的日期
        function parseDate(str) {
            if (typeof (str) == 'string') {
                var results = str.match(/^\s*0*(\d{4})-0?(\d{1,2})-0?(\d{1,2})\s*$/);
                if (results && results.length > 3)
                    return new Date(parseInt(results[1]), parseInt(results[2]) - 1, parseInt(results[3]));
            }
            return null;
        }
       
        function LoadStyle() {
            var str = "  至  ";
            switch (getUrlParam("t")) {
                case "1":
                    //$("#Today").attr("class", "btb");
                    $("#Today");
                    break;
                case "2":
                    //$("#qt").attr("class", "btb");
                    $("#qt");
                    break;
                case "3":
                    //$("#Week").attr("class", "btb");
                    $("#Week");
                    break;
                case "4":
                    //$("#Mouths").attr("class", "btb");
                    $("#Mouths");
                    break;
                case "5":
                    //$("#All").attr("class", "btb");
                    $("#All");
                    str = "";
                    break;
                case "6":
                    break;
                case null:
                    //$("#All").attr("class", "btb");
                    $("#All");
                    str = "";
                    break;
            }
            if (str != "")
                $("#selTime,#selTime2").html(getUrlParam("Start").split(' ')[0] + str + getUrlParam("End").split(' ')[0]);
        }

        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }
    </script>
</head>
<body>
        <div class="con_p">
        <div class="cz_bk" >
            <div class="lm_div">
                搜索信息</div>
                 <div class="cz_xx">
                    问卷信息:
                    <select id="questionnai" class="easyui-validatebox" name="D1">
                    <%foreach (var item in Model.QuestionnairesList)
                        {%>
                        <option value="<%=item.QId %>"><%=item.Title %></option>
                        <%} %>
                    </select>
                        
                            <%--<input type="button" id="Today" value="今天" />--%>
                            <a href="#" id="Today" class="easyui-linkbutton" iconCls="icon-find">今天</a>
                        
                        
                            <%--<input type="button" id="qt" value="昨日" />--%>
                            <a href="#" id="qt" class="easyui-linkbutton" iconCls="icon-find">昨日</a>
                        
                            <%--<input type="button" id="Week" value="本周" />--%>
                            <a href="#" id="Week" class="easyui-linkbutton" iconCls="icon-find">本周</a>
                        
                        
                            <%--<input type="button" id="Mouths" value="本月" />--%>
                            <a href="#" id="Mouths" class="easyui-linkbutton" iconCls="icon-find">本月</a>
                        
                            <%--<input type="button" id="All" value="所有" />--%>
                             <a href="#" id="All" class="easyui-linkbutton" iconCls="icon-find">所有</a>
                        
                        指定时间
                        
                            <input type="text" id="StartTime" class="myTextbox easyui-datebox" style="width: 120px" />
                        
                        至
                        
                            <input type="text" id="EndTiem" class="myTextbox easyui-datebox" style="width: 120px" />
                        
                        
                            <a href="#" id="Inquiry" class="easyui-linkbutton" iconCls="icon-find">查询</a>
                        
                </div>
            </div>

        <div class="cz_bk"  style="overflow: scroll; height: 688px;">
            
                    <div class="lm_div">
                        投票统计列表</div>
                         <%--<lable id="selTime"></lable>--%>
                         <div style=" padding:10px;">
                        <%if (Model.QuestionnairesAndOptionsList == null)
                          { %>
                          <div style="font-size:18px; ">
                              <label id="selTime2"></label><br />
                              <label>没有提交的问卷调查</label>
                          </div>
                          <%}else
                          { %>
                        <table>
                            <tr style="font-size:18px; ">
                            <td id="selTime"></td>
                                <td>&nbsp;
                                参与调查人数:
                                </td>
                                <td>
                                <%=Model.UserNum %>
                                </td>
                            </tr>
                        </table>    
                           <%
                            for (int i = 0; i < Model.QuestionnairesAndOptionsList.Count; i++)
                            {%>
                            <table class="ble" width="600">
                            <tr><td width="535">
                                    <strong>项目</strong>
                                </td>
                                 <td width="30">
                                    <strong>人数</strong>
                                 </td>
                                 <td width="35">
                                    <strong>比例</strong>
                                 </td>
                            </tr>
                            <tr>
                                <td>
                                <strong><%=Model.QuestionnairesAndOptionsList[i].Questionnaires.Title %></strong>
                                </td>
                                 <td></td> 
                                      <td></td>
                            </tr>
                                <%foreach (var item in Model.QuestionnairesAndOptionsList[i].ListOptions)
                                {%><tr><td>
                                    <%=item.Info%>
                                      </td>
                                      <td><%=item.OptionsNum %></td> 
                                      <td><%=item.OptionsRate.ToString("0.00") %>%</td>

                 
                            </tr>
                                <%}%>
                                </table>
                                <br />
                            <%} %>
                            <table class="ble" width="600">
                                <tr align="center">
                                    <td width="60">
                                        <strong>用户名</strong>
                                    </td>
                                    <td width="510">
                                        <strong>问题</strong>
                                    </td>
                                    <td width="30">
                                        <strong>时间</strong>
                                    </td>
                                </tr>
                                <%if (Model.QuestionnairesanswerList != null)
                                foreach (var items in Model.QuestionnairesanswerList)
                                {%>
                                    <tr style=" height:20px"><td><%=items.UserName%></td>
                                    <td><%=items.Info%></td>
                                    <td><%=items.CreateTime %></td>
                                    </tr>       
                                <%} %>
                            </table>
                        <%} %>
                </div>
            </div>
        </div>
</body>
</html>
