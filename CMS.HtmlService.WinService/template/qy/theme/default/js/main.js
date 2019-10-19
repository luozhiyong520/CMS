$(function(){$("a,lable").focus(function(){this.blur();});});

$(function(){
    var islide = new Swiper(".islide .swiper-container",{
        autoplay: 3000,
        autoplayDisableOnInteraction: false,
        loop: true,
        pagination: ".islide .pagination",
        paginationClickable: true
    });

    var sidevideo = new Swiper(".side-video .swiper-container", {
        autoplay: 3000,
        autoplayDisableOnInteraction: false,
        loop: true,
        pagination: ".side-video .pagination",
        paginationClickable: true
    });

    var icase = new Swiper(".icase .swiper-container",{
        autoplay: 4000,
        autoplayDisableOnInteraction: false,
        loop: true
    });
    $(".icase-container").on("mouseenter",function(){
        icase.stopAutoplay();
    });
    $(".icase-container").on("mouseleave",function(){
        icase.startAutoplay();
    });

    $(".sv-point span").on("mouseenter",function(){
        var index = $(this).index();
        $(".sv-point span").removeClass("act") && $(this).addClass("act");
        $(".sv-block .mv-box").removeClass("act") && $(".sv-block .mv-box").eq(index).addClass("act");
    });

    var adslide = new Swiper(".ad-slide .swiper-container",{
        autoplay: 3000,
        autoplayDisableOnInteraction: false,
        loop: true,
        pagination: ".ad-slide .pagination",
        paginationClickable: true
    });

    $(".app-list .al-nav span").on("mouseenter",function(){
        var index = $(this).index();
        $(".app-list .al-nav span").removeClass("act") && $(this).addClass("act");
        $(".app-list .al-box ul").removeClass("act") && $(".app-list .al-box ul").eq(index).addClass("act");
    });
    $(".cancel").click(function(){
        $(".fly-password").hide();
    })
    $(".fly-close").click(function(){
        $(".fly-password").hide();
    })

});
    function checkdk(){
        if($("#word").val()==""){
            alert("请输入查询内容");
            $("#word").focus();
            return false;
        }
        $(".fly-password").show();
        $("#pass").focus();
        $("#title").val($("#word").val())
      
        return false;
    }
    function ckeckm(){
        if($("#pass").val()==""){
            $("#pass").focus();
            alert("请输入查询密码");
            return false;
        }
        if($("#title").val()==""){
            alert("请输入查询内容");
            $(".fly-password").hide();
            $("#word").focus();
            return false;
        }
        jQuery.getJSON("http://cms.zongxue.net/news/GetPassWord.aspx?pass=" + $("#pass").val() + "&callbak=?", function (data) {
            if (data["result"] == "false") {
                alert('密码不正确!')
            } else {
                window.location.href = '/queryallsite.html?keywords=' + encodeURIComponent(encodeURIComponent($("#title").val())) + '&pass=' + $("#pass").val();
            }
        });

    }
    function checkh(){
        if ($("#title2").val() == "") {
            alert("请输入公司名称");
            $("#title2").focus();
            return false;
        } else {
		 
            var keyword = encodeURIComponent(encodeURIComponent($("#title2").val()));
            window.location.href = '/queryqy.html?keywords=' + keyword;
        }
    }