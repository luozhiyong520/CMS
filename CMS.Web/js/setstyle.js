$(function(){
	$("a.btnSetStyle").click(function(){
		var style = $(this).attr("ps");
 		// 其实写cookie也可以直接使用JS去写的。 
		$.ajax({
			url: "http://localhost:83/AjaxStyle/SetStyle.cspx",
			data: {style: style},
			success: function(){
				window.location = window.location;
			}
		});
		return false;
	});
});

