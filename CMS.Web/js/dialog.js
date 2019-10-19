function ShowEditItemDialog(itemId, divId, width, heigth, okFunc, shownFunc){
	if( typeof(width) != "number") width = 600;
	if (typeof (height) != "number") height = 430;
	
	var isEdit = ( itemId.length > 0 );	
	var j_dialog = $("#" + divId);
	
	if( j_dialog.attr("srcTitle") == undefined )
		j_dialog.attr("srcTitle", j_dialog.attr("title"));	// title属性会在创建对话框后被清除！
	
	var dlgTitle = (isEdit ? "编辑" : "添加" ) + j_dialog.attr("srcTitle");
	
	j_dialog.show().dialog({
        width: width, height: heigth, modal: true, resizable: true , title: dlgTitle , closable: true,
        buttons: [
            { text: (isEdit ? "保存" : "创建"), iconCls: 'icon-ok', plain: true,
                handler: function() {
					if( typeof(okFunc) == "function")
						okFunc(j_dialog);
                }
            }, 
            { text: '取消', iconCls: 'icon-cancel',  plain: true,
                handler: function() { 
					j_dialog.dialog('close');
			    }
            }],
		onOpen: function() { 
			if( typeof(shownFunc) == "function")
				shownFunc(j_dialog);
			
			j_dialog.find(":text.myTextbox").first().focus(); 
		}
	});
}

function ShowEditUploadDialog(itemId, divId, width, heigth){

	if( typeof(width) != "number") width = 600;
	if (typeof (height) != "number") height = 430;
	
	var isEdit = ( itemId.length > 0 );	
	var j_dialog = $("#" + divId);
	
	if( j_dialog.attr("srcTitle") == undefined )
		j_dialog.attr("srcTitle", j_dialog.attr("title"));	// title属性会在创建对话框后被清除！
	
	var dlgTitle = (isEdit ? "编辑" : "添加" ) + j_dialog.attr("srcTitle");
	  
	j_dialog.show().dialog({
        width: width, height: heigth, modal: true, resizable: true , title: dlgTitle , closable: true,
        buttons: [
//            { text: (isEdit ? "保存" : "创建"), iconCls: 'icon-ok', plain: true,
//                handler: function() {
//					if( typeof(okFunc) == "function")
//						okFunc(j_dialog);
//                }
//            }, 
            { text: '确定', iconCls: 'icon-ok',  plain: true,
                handler: function() { 
					j_dialog.dialog('close');
			    }
            }]
//		onOpen: function() { 
//			if( typeof(shownFunc) == "function")
//				shownFunc(j_dialog);
//			
//			j_dialog.find(":text.myTextbox").first().focus(); 
//		}
	});
}

function ShowEditItemDialogDel(itemId, divId, width, heigth, okFunc,delFunc, shownFunc){
	if( typeof(width) != "number") width = 600;
	if (typeof (height) != "number") height = 430;
	
	var isEdit = ( itemId.length > 0 );	
	var j_dialog = $("#" + divId);
	
	if( j_dialog.attr("srcTitle") == undefined )
		j_dialog.attr("srcTitle", j_dialog.attr("title"));	// title属性会在创建对话框后被清除！
	
	var dlgTitle = (isEdit ? "编辑频道" : "添加频道" );
	
	j_dialog.show().dialog({
        width: width, height: heigth, modal: true, resizable: true , title: dlgTitle , closable: true,
        buttons: [
            { text:"删除", iconCls: 'icon-no', plain: true,id:'del',
                handler: function() {
					if( typeof(delFunc) == "function")
						delFunc(j_dialog);
                }
            }, 
            { text: (isEdit ? "保存" : "创建"), iconCls: 'icon-ok', plain: true,
                handler: function() {
					if( typeof(okFunc) == "function")
						okFunc(j_dialog);
                }
            }, 
            { text: '取消', iconCls: 'icon-cancel',  plain: true,
                handler: function() { 
                     $("#drpTypeID").removeAttr('disabled');
					j_dialog.dialog('close'); 
			    }
            }],
		onOpen: function() { 
			if( typeof(shownFunc) == "function")
				shownFunc(j_dialog);
			
			j_dialog.find(":text.myTextbox").first().focus(); 
		}
	});
}


function ShowAddItemDialog(itemId, divId, width, heigth, okFunc, ModuleId) {
    if (typeof (width) != "number") width = 600;
    if (typeof (height) != "number") height = 430;

    var isEdit = (itemId.length > 0);
    var j_dialog = $("#" + divId);

    if (j_dialog.attr("srcTitle") == undefined)
        j_dialog.attr("srcTitle", j_dialog.attr("title")); // title属性会在创建对话框后被清除！

    var dlgTitle = (isEdit ? "编辑" : "添加") + j_dialog.attr("srcTitle");

    j_dialog.show().dialog({
        width: width, height: heigth, modal: true, resizable: true, title: dlgTitle, closable: true,
        buttons: [
            { text: (isEdit ? "保存" : "创建"), iconCls: 'icon-ok', plain: true,
                handler: function () {
                    if (typeof (okFunc) == "function")
                        okFunc(j_dialog,ModuleId);
                }
            },
            { text: '取消', iconCls: 'icon-cancel', plain: true,
                handler: function () {
                    j_dialog.dialog('close');
                }
            }]
    });
}



function ShowPickerDialog(divId, okFunc, shownFunc, width, height) {
	if( typeof(width) != "number") width = 850;
	if( typeof(height) != "number") height = 530;
	
	var j_dialog = $("#" + divId);
	
    j_dialog.show().dialog({
        height: height, width: width, modal: true, resizable: true, 
        buttons: [
            { text: '确定', iconCls: 'icon-ok', plain: true,
                handler: function() {
                    if( typeof(okFunc) == "function")
						okFunc(j_dialog);
                }
            }, 
            { text: '取消', iconCls: 'icon-cancel',  plain: true,
                handler: function() { 
				    j_dialog.dialog('close');
			    }
            }],
		onOpen: function() { 
			if( typeof(shownFunc) == "function")
				shownFunc(j_dialog);
		}
    });
}


function ShowViewerDialog(divId, width, height) {
	if( typeof(width) != "number") width = 850;
	if( typeof(height) != "number") height = 530;
	
    $("#" + divId).show().dialog({
        height: height, width: width, modal: true, resizable: true, 
        buttons: [
            { text: '关闭', iconCls: 'icon-cancel',  plain: true,
                handler: function() { 
				    $("#" + divId).dialog('close');
			    }
            }]
    });
}
