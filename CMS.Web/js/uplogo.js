var tgTypeName;
var bhTypeName;
var bhmaxDeposit;
var tgmaxDeposit;
var version = GetVersion();
var QuCustomerName = User.GetUserName();
GetCode();


//获取版本
function GetVersion() {
    //return "金牡丹";
    var productId = 5001;
    try {
        if (external.iWinMain)
            productId = external.iWinMain("cmd_productid");
        else
            return "jinmudan";
    } catch (e) {
        return "jinmudan";
    }
    if (productId > 3000 && productId < 3101)
        return "yanlin";
    if (productId > 5000 && productId < 5101)
        return "jinmudan";
    if (productId > 6000 && productId < 6101)
        return "bohai";
    if (productId > 7000 && productId < 7101)
        return "baichuan";
    return "jinhudie";
}

//判断入金额
function GetmaxDeposit(maxDeposit) {
    if (maxDeposit >= 0 && maxDeposit < 10000) {
        return "0_1";
    } else if (maxDeposit >= 10000 && maxDeposit < 50000) {
        return "1_5";
    } else if (maxDeposit >= 50000 && maxDeposit < 100000) {
        return "5_10";
    } else if (maxDeposit >= 100000 && maxDeposit < 150000) {
        return "10_15";
    } else if (maxDeposit >= 150000 && maxDeposit < 200000) {
        return "15_20";
    } else if (maxDeposit >= 200000 && maxDeposit < 300000) {
        return "20_30";
    } else if (maxDeposit >= 300000) {
        return "30_100000000";
    }
}

function GetCode() {
    $.ajax({
        url: '../analystlive/getSignCode.aspx',
        type: 'GET',
        async: false,
        data: { "userName": QuCustomerName },
        success: function (data) {
            var str = eval(data);
            tgTypeName = "no";
            bhTypeName = "no";
            tgmaxDeposit = "0_1";
            bhmaxDeposit = "0_1";
            if (str != "") {
                for (var i = 0; i < str.length; i++) {
                    if (str[i].TypeName == "现货") {
                        bhTypeName = "yes";
                        bhmaxDeposit = GetmaxDeposit(str[i].maxDeposit);
                    }
                    if (str[i].TypeName == "贵金属") {
                        tgTypeName = "yes";
                        tgmaxDeposit = GetmaxDeposit(str[i].maxDeposit);
                    }
                }
            }
        }
    });
}