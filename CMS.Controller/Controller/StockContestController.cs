using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMVC;
using CMS.BLL;
using CMS.Model.Oracle;

namespace CMS.Controller.Controller
{
    public class StockContestController
    {

        /// <summary>
        /// 炒股大赛推送
        /// </summary>
        [Action]
        [PageUrl(Url = "/StockContest/SosPush.aspx")]
        public string SosPush(string id, string userId, string stockCode, string stockName, string quantity, string price, string operateUserName, string operateUserImg, string operateType, string operateTime, int staticTag)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(stockCode) || string.IsNullOrEmpty(stockName) || string.IsNullOrEmpty(price) || string.IsNullOrEmpty(operateType) || string.IsNullOrEmpty(operateTime))
                return "缺少参数";
            StockContestData scd = new StockContestData();
            scd.Id = "ds" + Guid.NewGuid().ToString();
            scd.UserId = userId;
            scd.StockCode = stockCode;
            scd.StockName = stockName;
            scd.Quantity = quantity;
            scd.Price = price;
            scd.OperateUserName = operateUserName;
            scd.OperateUserImg = operateUserImg;
            scd.OperateType = operateType;
            scd.OperateTime = operateTime;
            scd.StaticTag = staticTag == 1 ? "4" : "3"; //来源数据: 0为模拟盘, 1为实盘; 手机推送表为3 跟 4

            SsoServer.SsoPush(scd);
            return "1";
        }
    }
}
