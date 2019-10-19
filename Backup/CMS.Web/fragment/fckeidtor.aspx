<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <script src="/themes/default/scripts/ueditor/editor_config.js" type="text/javascript"></script>
    <script src="/themes/default/scripts/ueditor/editor_all.js" charset="utf-8" type="text/javascript"></script>
    <link href="/themes/default/scripts/ueditor/themes/default/ueditor.css" rel="stylesheet" type="text/css" />
</head>
<body>
        <script type="text/javascript">
            //初始化编辑器
            var editor = new baidu.editor.ui.Editor({
                UEDITOR_HOME_URL: '/themes/default/scripts/ueditor/',
                autoClearinitialContent: true,
                iframeCssUrl: '/themes/default/scripts/ueditor/themes/default/iframe.css',
                initialContent: '',
                minFrameHeight: 150,
                textarea: 'content'
            });
           // var URL = '/themes/default/scripts/ueditor/';
            editor.render("myEditor");
            editor.ready(function () {
                var person = window.dialogArguments;
                editor.setContent(person.Content);
            })

            function getContent() {
                var arr = [];
                arr.push(editor.getContent());
                window.returnValue = arr.join("\n");
                window.close();
            }
           
        </script>

     <div style="margin:10px;"><textarea cols="" name="message.content" id="myEditor"></textarea></div>
    <div style="text-align:center">
         <input id="BtnReturnValue" type="button" onclick="getContent()" style="cursor:pointer;" value="返回文本值" />
    </div>
</body>
</html>
