<%@ Page Language="C#" Inherits="PageView<TemplatedetailPageModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
        <!-- #include file="/controls/header.htm" -->  
        <script src="../themes/default/scripts/jquery_ztree_3.5.02/js/jquery.ztree.core-3.5.js"
        type="text/javascript"></script>
    <script src="../themes/default/scripts/jquery_ztree_3.5.02/js/jquery.ztree.excheck-3.5.js"
        type="text/javascript"></script>
    <link href="../themes/default/scripts/jquery_ztree_3.5.02/css/zTreeStyle/zTreeStyle.css"
        rel="stylesheet" type="text/css" />
    <script src="../js/templatedetail.js?333" type="text/javascript"></script>
    <title>栏目选择列表页</title>
</head>
<body>
     <input id="hdtemplateId" type="hidden" value="<%=Model.templateId %>"/>
     <input id="hdtempleteType" type="hidden" value="<%=Model.templeteType %>"/>
    <form id="form1" runat="server">
    <div class="content_wrap" style=" width:100%;overflow: scroll; height:630px;">
    <div style="margin-bottom:20px">
    <div class="zTreeDemoBackground left">
        <ul id="xmtree" class="ztree">
        </ul>
    </div>
    </div>
    </div>
    <div style="  position:absolute; bottom:20px;left:200px; ">
        <input id="btnSave" type="button" value="保存" />
        <input id="btnColse" type="button" onclick="javascript: window.close();" value="关闭" />
    </div>
    </form>
</body>
</html>
