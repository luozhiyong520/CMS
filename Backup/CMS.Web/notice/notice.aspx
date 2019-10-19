<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title>编辑公告</title>
     <!-- #include file="/controls/header.htm" --> 
     <script language="javascript">
         $(function () {
             $.ajax({
                 url: '/AjaxAuthorityDot/GetAuthoritydot.cspx?productId=77',
                 success: function (shtml) {
                     $("#productId").html('');
                     $("#productId").append("<option value=\"请选择\">请选择</option>" + shtml);
                 }
             });

             $("#editNotice").click(function () {
                 var str = decodeURI($("#noticeinfo :input").fieldSerialize());
                 var gourl = "http://localhost:12924/test/test1.aspx?" + str;
                 jQuery.getScript(gourl, function () {
                     if (json.res == undefined || json.res == "" || json.res == null || json.res == "null")
                         return false;
                     var obj = eval('(' + json.res + ')');
                     if (obj.result == "true") {
                         alert('编辑成功');
                     } else {
                         alert('编辑失败');
                     }
                 });

             })

         });
         function GetAgentInfo(productId) {
             var url = "http://localhost:12924/test/test.aspx?productId=" + productId;
             jQuery.getScript(url, function () {
                 if (json.res == undefined || json.res == "" || json.res == null || json.res == "null")
                     return false;
                 var result = json.res;
                 var obj = eval('(' + result + ')');
                 $("#tel").val(obj.tel);
                 $("#url").val(obj.url);
                 $("#email").val(obj.email);
                 $("#company").val(obj.company);
                 $("#agenturl").val(obj.agenturl);
                 $("#title").val(obj.agentschool[0].title);
                 $("#description").val(obj.agentschool[0].description);
                 $("#pageurl").val(obj.agentschool[0].pageurl);
                 $("#roomid").val(obj.agentschool[0].roomid);
             });
         }
         
     </script> 
</head>
<body>
  
     <div class="con_p"> 
     <form name="form1" id="form1" action="http://localhost:12924/test/test1.aspx" method="post">
        <div class="cz_bk">
        <div class="lm_div">编辑公告</div>
      
        <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" class="t_table" id="noticeinfo">
            <TR>
              <TD  width="10%" align="right" nowrap> 投放版本：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Option.gif" width="16" align="absMiddle">&nbsp;
                <SELECT id="productId" name="productId" style="width:160px" onchange="GetAgentInfo(this.options[this.selectedIndex].value)">
                </SELECT>
               </TD>
            </TR>

            
            <TR>
              <TD width="10%" align="right" nowrap> 电话：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
                <INPUT name="tel" class= "inTextbox" id="tel" value=""  size="60"/>
                </TD>
            </TR>

             <TR>
              <TD width="10%" align="right" nowrap> 地址：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
                <INPUT name="url" class= "inTextbox" id="url" value="" size="80" />
                </TD>
            </TR>

             <TR>
              <TD width="10%" align="right" nowrap> 邮箱：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
                <INPUT name="email" class= "inTextbox" id="email" value="" />
                </TD>
            </TR>

 

            <TR>
              <TD align="right" nowrap>公司：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               <INPUT name="company" size="50" id="company" class= "inTextbox"  value=""/>&nbsp;&nbsp; 
               </TD>
            </TR>
            <TR>
              <TD align="right" nowrap>代理地址：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               <INPUT name="agenturl" size="80" id="agenturl" class= "inTextbox"   value=""/>&nbsp;&nbsp;
               </TD>
            </TR>
              <TR>
                <TD align="right" nowrap>标题：</TD>
                <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">  
                <INPUT name="title" size="50" id="title" class= "inTextbox"   value=""/>&nbsp;&nbsp; </TD>
            </TR>
              <TR>
                <TD align="right" nowrap>描述：</TD>
                <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
                <textarea style="margin-bottom:6px; width:500px; height:100px;" class="inTextbox" id="description"  name="description"></textarea>
                 </TD>
            </TR>

               <TR>
                <TD align="right" nowrap>页面地址：</TD>
                <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">  
                  <INPUT name="pageurl" size="80" id="pageurl" class= "inTextbox"   value=""/>&nbsp;&nbsp; </TD>
            </TR>

               <TR>
                <TD align="right" nowrap>课堂号：</TD>
                <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">  
                  <INPUT name="roomid" size="20" id="roomid" class= "inTextbox"   value=""/>&nbsp;&nbsp;</TD>
            </TR>

            
            <TR>
                <TD></TD>
                <TD colspan="3" >
                <input type="hidden" value="<%=CMS.BLL.UserCookies.AdminName%>" name="username" />
                <input type="hidden" value="<%=CMS.BLL.UserCookies.AdminId%>" name="userid" />
                <INPUT class="inButton" type="button" value="编 辑" name="editNotice" id="editNotice"  >
                <INPUT class="inButton" type="reset" value="重 置" >
                </TD>
            </TR>

        </TABLE>
   
    </div>
       </form>
    </div>
     

</body>
</html>
