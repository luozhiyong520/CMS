<%@ Page Language="C#" Inherits="PageView<DirectionPageModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>UP风向标</title>
    <!-- #include file="/controls/header.htm" -->
<script language="javascript">
    $(function () {
        $("#stockbutton").click(function () {
            var stockMarketQuotations = encodeURIComponent($("#stockMarketQuotations").val());
            var stockPopularity = $("#stockPopularity").val();
            $.ajax({
                type: "GET",
                url: "/AjaxDirection/EditDirection.cspx?MarketQuotations=" + stockMarketQuotations + "&Popularity=" + stockPopularity + "&TypeId=1",
                success: function (responseText) {
                    if (responseText == "000001") {
                        alert("请填写人气指标");
                    } else {
                        alert("更新成功");
                        window.location = window.location;
                    }
                }
            });

        });
        $("#actualsbutton").click(function () {
            var actualsMarketQuotations = encodeURIComponent($("#actualsMarketQuotations").val());
            var actualsPopularity = $("#actualsPopularity").val();
            $.ajax({
                type: "GET",
                url: "/AjaxDirection/EditDirection.cspx?MarketQuotations=" + actualsMarketQuotations + "&Popularity=" + actualsPopularity + "&TypeId=2",
                success: function (responseText) {
                    if (responseText == "000001") {
                        alert("请填写人气指标");
                    } else {
                        alert("更新成功");
                        window.location = window.location;
                    }
                }
            });

        });
        $("#futuresbutton").click(function () {
            var futuresMarketQuotations = encodeURIComponent($("#futuresMarketQuotations").val());
            var futuresPopularity = $("#futuresPopularity").val();
            $.ajax({
                type: "GET",
                url: "/AjaxDirection/EditDirection.cspx?MarketQuotations=" + futuresMarketQuotations + "&Popularity=" + futuresPopularity + "&TypeId=3",
                success: function (responseText) {
                    if (responseText == "000001") {
                        alert("请填写人气指标");
                    } else {
                        alert("更新成功");
                        window.location = window.location;
                    }
                }
            });

        });

        $("#gjsbutton").click(function () {
            var gjsMarketQuotations = encodeURIComponent($("#gjsMarketQuotations").val());
            var gjsPopularity = $("#gjsPopularity").val();
            $.ajax({
                type: "GET",
                url: "/AjaxDirection/EditDirection.cspx?MarketQuotations=" + gjsMarketQuotations + "&Popularity=" + gjsPopularity + "&TypeId=4",
                success: function (responseText) {
                    if (responseText == "000001") {
                        alert("请填写人气指标");
                    } else {
                        alert("更新成功");
                        window.location = window.location;
                    }
                }
            });

        });

    })
    function changestockimg(op) {
       if(op=="空头强势")
           $(".stockimg img").attr("src", "/themes/default/images/admin/d1.png");
       else if (op == "空头占优")
           $(".stockimg img").attr("src", "/themes/default/images/admin/d2.png");
       else if (op == "多空平衡")
           $(".stockimg img").attr("src", "/themes/default/images/admin/d3.png");
       else if (op == "多头占优")
           $(".stockimg img").attr("src", "/themes/default/images/admin/d4.png");
       else if (op == "多头强势")
           $(".stockimg img").attr("src", "/themes/default/images/admin/d5.png");
   }
   function changeactualsimg(op) {
       if (op == "空头强势")
           $(".actualsimg img").attr("src", "/themes/default/images/admin/d1.png");
       else if (op == "空头占优")
           $(".actualsimg img").attr("src", "/themes/default/images/admin/d2.png");
       else if (op == "多空平衡")
           $(".actualsimg img").attr("src", "/themes/default/images/admin/d3.png");
       else if (op == "多头占优")
           $(".actualsimg img").attr("src", "/themes/default/images/admin/d4.png");
       else if (op == "多头强势")
           $(".actualsimg img").attr("src", "/themes/default/images/admin/d5.png");
   }
   function changefuturesimg(op) {
       if (op == "空头强势")
           $(".futuresimg img").attr("src", "/themes/default/images/admin/d1.png");
       else if (op == "空头占优")
           $(".futuresimg img").attr("src", "/themes/default/images/admin/d2.png");
       else if (op == "多空平衡")
           $(".futuresimg img").attr("src", "/themes/default/images/admin/d3.png");
       else if (op == "多头占优")
           $(".futuresimg img").attr("src", "/themes/default/images/admin/d4.png");
       else if (op == "多头强势")
           $(".futuresimg img").attr("src", "/themes/default/images/admin/d5.png");
   }
   function changegjsimg(op) {
       if (op == "空头强势")
           $(".gjsimg img").attr("src", "/themes/default/images/admin/d1.png");
       else if (op == "空头占优")
           $(".gjsimg img").attr("src", "/themes/default/images/admin/d2.png");
       else if (op == "多空平衡")
           $(".gjsimg img").attr("src", "/themes/default/images/admin/d3.png");
       else if (op == "多头占优")
           $(".gjsimg img").attr("src", "/themes/default/images/admin/d4.png");
       else if (op == "多头强势")
           $(".gjsimg img").attr("src", "/themes/default/images/admin/d5.png");
   } 
</script>
</head>

<body>
    <div class="con_p">
        <div class="cz_bk">
            <div class="lm_div">管理选项</div>
            <div class="cz_xx">
                <a href="javascript:void(0)" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-edit' >UP方向标</a></div>
        </div>
  

        <div class="cz_bk">
           <div class="cz_xx">
               <span style="font-size:14px; font-weight:bold;color:#ff0000; margin:0 20px;">股票市场</span>
 
              <select id="stockMarketQuotations" onchange="changestockimg(this.options[this.selectedIndex].value);" >
                <option value="空头强势" <%if(Model.StockList.MarketQuotations=="空头强势"){%> selected <%} %>>空头强势</option>
                <option value="空头占优" <%if(Model.StockList.MarketQuotations=="空头占优"){%> selected <%} %>>空头占优</option>
                <option value="多空平衡" <%if(Model.StockList.MarketQuotations=="多空平衡"){%> selected <%} %>>多空平衡</option>
                <option value="多头占优" <%if(Model.StockList.MarketQuotations=="多头占优"){%> selected <%} %>>多头占优</option>
                <option value="多头强势" <%if(Model.StockList.MarketQuotations=="多头强势"){%> selected <%} %>>多头强势</option>
              </select>
              
              人气指标：<input type="text" id="stockPopularity" value="<%=Model.StockList.Popularity%>" />
              <input type="button" value="修改" id="stockbutton" class="inButton" />
               <%if (Model.StockList.MarketQuotations == "空头强势")
                 {%>
               <div style="margin-left:100px;" class="stockimg"><img src="/themes/default/images/admin/d1.png" /></div>
               <%} %>
                <%if (Model.StockList.MarketQuotations == "空头占优")
                 {%>
               <div style="margin-left:100px;" class="stockimg"><img src="/themes/default/images/admin/d2.png" /></div>
               <%} %>
                <%if (Model.StockList.MarketQuotations == "多空平衡")
                 {%>
               <div style="margin-left:100px;"class="stockimg"><img src="/themes/default/images/admin/d3.png" /></div>
               <%} %>
                <%if (Model.StockList.MarketQuotations == "多头占优")
                 {%>
               <div style="margin-left:100px;" class="stockimg"><img src="/themes/default/images/admin/d4.png" /></div>
               <%} %>
                <%if (Model.StockList.MarketQuotations == "多头强势")
                 {%>
               <div style="margin-left:100px;" class="stockimg"><img src="/themes/default/images/admin/d5.png" /></div>
               <%} %>

              
              
             
           </div>

            <div class="cz_xx">
                <span style="font-size:14px; font-weight:bold; color:#ff0000; margin:0 20px;">现货市场</span>
        
              <select id="actualsMarketQuotations" onchange="changeactualsimg(this.options[this.selectedIndex].value);">
                <option value="空头强势" <%if(Model.ActualsList.MarketQuotations=="空头强势"){%> selected <%} %>>空头强势</option>
                <option value="空头占优" <%if(Model.ActualsList.MarketQuotations=="空头占优"){%> selected <%} %>>空头占优</option>
                <option value="多空平衡" <%if(Model.ActualsList.MarketQuotations=="多空平衡"){%> selected <%} %>>多空平衡</option>
                <option value="多头占优" <%if(Model.ActualsList.MarketQuotations=="多头占优"){%> selected <%} %>>多头占优</option>
                <option value="多头强势" <%if(Model.ActualsList.MarketQuotations=="多头强势"){%> selected <%} %>>多头强势</option>
              </select>
              人气指标：<input type="text" id="actualsPopularity" value="<%=Model.ActualsList.Popularity%>" />
              <input type="button" value="修改" id="actualsbutton" class="inButton"/>
                <%if (Model.ActualsList.MarketQuotations == "空头强势")
                 {%>
               <div style="margin-left:100px;" class="actualsimg"><img src="/themes/default/images/admin/d1.png" /></div>
               <%} %>
                <%if (Model.ActualsList.MarketQuotations == "空头占优")
                 {%>
               <div style="margin-left:100px;" class="actualsimg"><img src="/themes/default/images/admin/d2.png" /></div>
               <%} %>
                <%if (Model.ActualsList.MarketQuotations == "多空平衡")
                 {%>
               <div style="margin-left:100px;"class="actualsimg"><img src="/themes/default/images/admin/d3.png" /></div>
               <%} %>
                <%if (Model.ActualsList.MarketQuotations == "多头占优")
                 {%>
               <div style="margin-left:100px;" class="actualsimg"><img src="/themes/default/images/admin/d4.png" /></div>
               <%} %>
                <%if (Model.ActualsList.MarketQuotations == "多头强势")
                 {%>
               <div style="margin-left:100px;" class="actualsimg"><img src="/themes/default/images/admin/d5.png" /></div>
               <%} %>
            
           </div>

            <div class="cz_xx">
                <span style="font-size:14px; font-weight:bold;color:#ff0000; margin:0 20px;">期货市场</span>
    
              <select id="futuresMarketQuotations" onchange="changefuturesimg(this.options[this.selectedIndex].value);">
                <option value="空头强势" <%if(Model.FuturesList.MarketQuotations=="空头强势"){%> selected <%} %>>空头强势</option>
                <option value="空头占优" <%if(Model.FuturesList.MarketQuotations=="空头占优"){%> selected <%} %>>空头占优</option>
                <option value="多空平衡" <%if(Model.FuturesList.MarketQuotations=="多空平衡"){%> selected <%} %>>多空平衡</option>
                <option value="多头占优" <%if(Model.FuturesList.MarketQuotations=="多头占优"){%> selected <%} %>>多头占优</option>
                <option value="多头强势" <%if(Model.FuturesList.MarketQuotations=="多头强势"){%> selected <%} %>>多头强势</option>
              </select>
              人气指标：<input type="text" id="futuresPopularity" value="<%=Model.FuturesList.Popularity%>" />
               <input type="button" value="修改" id="futuresbutton" class="inButton"/>
               <%if (Model.FuturesList.MarketQuotations == "空头强势")
                 {%>
               <div style="margin-left:100px;" class="futuresimg"><img src="/themes/default/images/admin/d1.png" /></div>
               <%} %>
                <%if (Model.FuturesList.MarketQuotations == "空头占优")
                 {%>
               <div style="margin-left:100px;" class="futuresimg"><img src="/themes/default/images/admin/d2.png" /></div>
               <%} %>
                <%if (Model.FuturesList.MarketQuotations == "多空平衡")
                 {%>
               <div style="margin-left:100px;"class="futuresimg"><img src="/themes/default/images/admin/d3.png" /></div>
               <%} %>
                <%if (Model.FuturesList.MarketQuotations == "多头占优")
                 {%>
               <div style="margin-left:100px;" class="futuresimg"><img src="/themes/default/images/admin/d4.png" /></div>
               <%} %>
                <%if (Model.FuturesList.MarketQuotations == "多头强势")
                 {%>
               <div style="margin-left:100px;" class="futuresimg"><img src="/themes/default/images/admin/d5.png" /></div>
               <%} %>
          
             
            
           </div>


           <div class="cz_xx">
                <span style="font-size:14px; font-weight:bold;color:#ff0000; margin:0 20px;">贵金属市场</span>
    
              <select id="gjsMarketQuotations" onchange="changegjsimg(this.options[this.selectedIndex].value);">
                <option value="空头强势" <%if(Model.GjsList.MarketQuotations=="空头强势"){%> selected <%} %>>空头强势</option>
                <option value="空头占优" <%if(Model.GjsList.MarketQuotations=="空头占优"){%> selected <%} %>>空头占优</option>
                <option value="多空平衡" <%if(Model.GjsList.MarketQuotations=="多空平衡"){%> selected <%} %>>多空平衡</option>
                <option value="多头占优" <%if(Model.GjsList.MarketQuotations=="多头占优"){%> selected <%} %>>多头占优</option>
                <option value="多头强势" <%if(Model.GjsList.MarketQuotations=="多头强势"){%> selected <%} %>>多头强势</option>
              </select>
              人气指标：<input type="text" id="gjsPopularity" value="<%=Model.GjsList.Popularity%>" />
               <input type="button" value="修改" id="gjsbutton" class="inButton"/>
               <%if (Model.GjsList.MarketQuotations == "空头强势")
                 {%>
               <div style="margin-left:100px;" class="gjsimg"><img src="/themes/default/images/admin/d1.png" /></div>
               <%} %>
                <%if (Model.GjsList.MarketQuotations == "空头占优")
                 {%>
               <div style="margin-left:100px;" class="gjsimg"><img src="/themes/default/images/admin/d2.png" /></div>
               <%} %>
                <%if (Model.GjsList.MarketQuotations == "多空平衡")
                 {%>
               <div style="margin-left:100px;"class="gjsimg"><img src="/themes/default/images/admin/d3.png" /></div>
               <%} %>
                <%if (Model.GjsList.MarketQuotations == "多头占优")
                 {%>
               <div style="margin-left:100px;" class="gjsimg"><img src="/themes/default/images/admin/d4.png" /></div>
               <%} %>
                <%if (Model.GjsList.MarketQuotations == "多头强势")
                 {%>
               <div style="margin-left:100px;" class="gjsimg"><img src="/themes/default/images/admin/d5.png" /></div>
               <%} %>
          
             
            
           </div>


        </div>

    </div>


    
</body>
</html>
