using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMVC;
using CMS.BLL;
using CMS.ViewModel;
using CMS.Model;

namespace CMS.Controller
{
    public class AjaxInformationStatistic
    {
        NewsBLL newsBll = Factory.BusinessFactory.CreateBll<NewsBLL>();
        //资讯数量及所发资讯的点击数量进行统计
        [Action]
        public JsonResult GetInformationStatistics(string stratTime, string endTime, string adminName, string channelId)
        {
            List<News> list;
            string createdUser = adminName;
 

            list = newsBll.GetInformation(stratTime, endTime, createdUser, channelId);
            if (list == null)
            {
                return null;
            }
            else
            {
                return new JsonResult(list);
            }
        }
    }
}
