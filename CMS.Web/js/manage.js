function login() {  
    var loginname = $("#loginname").val();
    var loginpassword = hex_md5($("#loginpassword").val());

    var strValidityCode = $("#strValidityCode").val();
    if (loginname == "" || loginname==null) {
        alert('登录名不能为空！')
    } else if (loginpassword == "" || loginpassword==null) {
        alert('密码不能为空！')
    } else if (strValidityCode == "" || strValidityCode==null) {
        alert('验证码不能为空！')
    } else {

    $.ajax({
        url: "/AjaxLogin/Login.cspx?loginname=" + encodeURIComponent(loginname) + "&loginpassword=" + loginpassword + "&strValidityCode=" + strValidityCode + "",
        type: "POST",
        success: function (responseText) {
            if (responseText == 'True') {
                window.location.href = '/index.aspx';
            } else if (responseText == '000000') {
                alert('输入的验证码不正确！')
            } else if (responseText == '000001') {
                alert('用户名不正确！');
            } else if (responseText == '000002') {
                alert('密码不正确！');
            }
            else {
                alert('登录失败！');
            }
        }
    });
    }

}

 