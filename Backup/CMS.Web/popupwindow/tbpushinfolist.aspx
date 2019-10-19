<%@ Page Language="C#" Inherits="PageView<PopupMsgPlanModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<!-- #include file="/controls/header.htm" -->
<link href="../themes/default/styles/upload.css" rel="stylesheet" type="text/css"/>
<link href="/themes/default/styles/manage.css" type="text/css" rel="stylesheet"/>
<title>推送信息列表</title>
<script type="text/javascript">
    $(function () {
        self.parent.document.body.style.overflow = 'hidden';
        DataBatatbpushinfolist();
    })

    //绑定模糊查询数据
    function DataBatatbpushinfolist() {
        $('#tbpushinfolist').datagrid({
            title: "推送信息列表",
            height: self.parent.document.body.clientHeight - 150,
            nowrap: true, //设置为true，当数据长度超出列宽时将会自动截取。
            striped: true, //隔行变色
            collapsible: false,   //是否可折叠的,           
            idField: 'FID',
            pageSize: 30, //每页显示的记录条数，默认为10 
            pageNumber: 1,
            pagination: true, //设置true将在数据表格底部显示分页工具栏   
            rownumbers: true, //行号  
            collapsible: false,
            border: false, //显示面板的边界
            singleSelect: true, //是否单选  
            pageList: [10, 30, 50], //可以设置每页记录条数的列表 
            showPageList: false,
            fitColumns: true,
            url: '/AjaxTBPUSHINFO/GetTB_PUSH_INFOList.cspx',
            columns: [[
             { title: "<b>标题</b>", field: "TITLE", align: 'center', width: fillSize(0.1) },
              { title: "<b>摘要</b>", field: "INFOABSTRACT", align: 'center', width: fillSize(0.3) },
              { title: "<b>写入时间</b>", field: "OPERATEDATE", align: 'center', width: fillSize(0.1),
                  formatter: function (value) {
                      var OPERATEDATE = '';
                      if (value != null) {
                          OPERATEDATE = ConertJsonTimeAndFormat(value, 'yyyy-MM-dd hh:mm:ss');
                      }
                      return OPERATEDATE;
                  }
              },
              { title: "<b>编辑人员</b>", field: "EDITOR", align: 'center', width: fillSize(0.1) },
              { title: "<b>推送平台</b>", field: "PLATFORM", align: 'center', width: fillSize(0.1) },
             { title: "<b>计划接收人数</b>", field: "PLANCOUNT", align: 'center', width: fillSize(0.1) },
             { title: "<b>实际接收人数</b>", field: "REALCOUNT", align: 'center', width: fillSize(0.1) },
              { title: "<b>点击数</b>", field: "CLICKCOUNT", align: 'center', width: fillSize(0.1) }
		     ]],
            onLoadSuccess: function () {
                $($('#tbpushinfolist').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton();
            }
        });
    }
</script>
</head>
<body>
    <div class="con_p">
        <div class="cz_bk">
            <table id="tbpushinfolist"></table>
        </div>
    </div>
</body>
</html>
