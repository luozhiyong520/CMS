<%@ Page Language="C#" Inherits="PageView<PopupMsgPlanModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<!-- #include file="/controls/header.htm" -->
<link href="../themes/default/styles/upload.css" rel="stylesheet" type="text/css">
<link href="/themes/default/styles/manage.css" type="text/css" rel="stylesheet"/>
<script src="../js/popupmsgplanhistory.js" type="text/javascript"></script>
 

 

<title>弹窗消息留痕记录</title>
</head>
<body>
    <div class="con_p">

     <div class="cz_bk">
      <div class="lm_div">管理选项</div>
        <div class="cz_xx">
           <a class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add' id="popupMsg">添加弹窗消息</a>  
        </div>
    </div>

        <div class="cz_bk">
            <table id="PopupMsgPlanInfo"></table>
        </div>
    </div>

    <div id="PopupMsgInfo" title="文件" class="easyui-draggable" data-options="handle:'#title'" style=" display:none; position:absolute; width:800px;  top:40px; left:200px; border:1px solid #99BBE8;background:transparent url(/themes/default/scripts/jquery-easyui-1.3/themes/default/images/panel_title.png); padding:5px; "> 
        <div style="height:32px; line-height:24px; font-weight:bold;" id="title">
            <div style="float:left; padding-left:5px; color:#15428b">发布弹窗</div>
            <div style="float:right; height:16px; width:20px; margin-top:2px; background:url('/themes/default/scripts/jquery-easyui-1.3/themes/default/images/panel_tools.gif') no-repeat -16px 0px; cursor:pointer;" id="closepic"></div>
        </div>
        <div style="clear:both"></div>
        <div style="height:490px; overflow:auto; background:#ffffff; overflow:hidden" id="changepopup">
        <iframe src="popupadd.aspx?aa=11" width="798px"  frameborder="0" height="100%"></iframe>
        </div>
    </div>

</body>
</html>
