<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagertest.aspx.cs" Inherits="CMS.Web.fragment.pagertest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/themes/default/styles/manage.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../themes/default/scripts/jquery-easyui-1.3/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../themes/default/scripts/jquery-easyui-1.3/themes/icon.css" />
    <script type="text/javascript" src="../themes/default/scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../themes/default/scripts/jquery-easyui-1.3/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../themes/default/scripts/jquery-easyui-1.3/easyui-lang-zh_CN.js"></script>
</head>
<!--body class="easyui-layout">
<div region="center"  style="overflow:hidden">
<div class="easyui-layout" fit="true">
        <div region="center" id="contentBody" style="overflow:hidden">
            <table id="tblQueryResult">
            </table>
        </div>
</div>
</div>
</body-->
<body>
    <div class="con_p"> 
        <div class="cz_bk">
            <div class="lm_div">
                管理选项</div>
            <div class="cz_xx">
                <a id="add" href="#">新增模板</a>
            </div>
        </div>
       <div class="cz_bk">     
               <table id="tblQueryResult">
            </table>      
        </div>
    </div>
       
             
   
</body>
<script type="text/javascript">
    $(function () {

//        $('#tblQueryResult').datagrid({
//            title: 'My DataGrid',
//            height: 500,
//            nowrap: true,
//            autoRowHeight: false,
//            striped: true,
//            collapsible: true,
//            url: 'datagrid_data.aspx',
//            sortName: 'code',
//            sortOrder: 'desc',
//            remoteSort: false,
//            idField: 'code',
//            pagination: true,
//            rownumbers: true,
//            columns: [[
//			        { title: 'Base Information', colspan: 3 },
//					{ field: 'opt', title: 'Operation', width: 100, align: 'center', rowspan: 2,
//					    formatter: function (value, rec) {
//					        return '<span style="color:red">Edit Delete</span>';
//					    }
//					}
//				], [
//					{ field: 'name', title: 'Name', width: 120 },
//					{ field: 'addr', title: 'Address', width: 220, rowspan: 2, sortable: true,
//					    sorter: function (a, b) {
//					        return (a > b ? 1 : -1);
//					    }
//					},
//					{ field: 'col4', title: 'Col41', width: 150, rowspan: 2 }
//				]]

//        });
        $('#tblQueryResult').datagrid({
            title: "碎片列表",              
            height: 500,
            nowrap: true, //设置为true，当数据长度超出列宽时将会自动截取。
            striped: true, //隔行变色
            collapsible: false,   //是否可折叠的,           
            idField: 'FragmentId',
            pageSize: 2, //每页显示的记录条数，默认为10 
            pageNumber: 1,
            pageList: [2, 5, 10, 15], //可以设置每页记录条数的列表  
            pagination: true, //设置true将在数据表格底部显示分页工具栏   
            rownumbers: true, //行号  
            collapsible: false,
            border: false, //显示面板的边界
            singleSelect: true, //是否单选  
            url: '/Ajaxfragment/GetPagedFragements.cspx',
            columns: [[
             { title: "ID", field: "FragmentId", width: 300 },
			 { title: "内容", field: "Content", width: 300 },
			 { title: "地址", field: "TypeId", width: 300 }
		    ]]       
        });

    });
  
</script>
</html>
