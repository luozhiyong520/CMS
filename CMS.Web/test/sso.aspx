

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
<script type="text/javascript" src="http://js.upchina.com/sasweb/actuals/js/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#submit").click(function () {
                $.ajax({
                    url: "/test/getval.aspx",
                    data: { "val": $("#in").val() },
                    type: "post",
                    success: function (shtml) {
                        $("#show").html(shtml);
                        $("#in2").val(shtml);
                    }
                });
            });
        });
        $("#rbr").click(function () {
            $("#show").html($("#show").html().replace("\n","<br\>"));
        });
    </script>
</head>
<body>
    <div><textarea id="in" rows="20" cols="100"></textarea><textarea id="in2" rows="20" cols="100"></textarea></div>
    <div><input type="button" id="submit" value="submit" style="float:left;" /></div>
    <div id="show"></div>
</body>
</html>
