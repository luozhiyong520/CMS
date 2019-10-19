using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMVC;
using CMS.BLL;
using CMS.ViewModel;

namespace CMS.Controller
{
     [Authorize]
    public class AnalystLiveController
    {
         AnalystLiveBLL analystLiveBLL = Factory.BusinessFactory.CreateBll<AnalystLiveBLL>();
         [Action]
         [PageUrl(Url = "/cpzb/zbedit.aspx")]
         [PageUrl(Url = "/cpzb/pcedit.aspx")]
         public object GetEditAnalystLive(int LiveId, int AnalystId, string AnalystName, int AnalystType)
         {
             var analystLive = analystLiveBLL.Get("LiveId", LiveId);
             var TransStatisticsList = analystLiveBLL.GetTransStatisticsNum(AnalystId);
             var model = new AnalystLivePageModel();
             model.AnalystLive = analystLive;
             model.AnalystLive.BigNum = TransStatisticsList.BigNum;
             model.AnalystLive.EmptyNum = TransStatisticsList.EmptyNum;
             model.AnalystLive.ProfitNum = TransStatisticsList.ProfitNum;
             model.AnalystLive.LossNum = TransStatisticsList.LossNum;
             model.AnalystLive.SuccessRate = TransStatisticsList.SuccessRate;
             model.AnalystId = AnalystId;
             model.AnalystName = AnalystName;
             model.AnalystType = AnalystType;
             return new PageResult(null, model);
         }
         [Action]
         [PageUrl(Url = "/cpzb/zbadd.aspx")]
         [PageUrl(Url = "/cpzb/zblist.aspx")]
         [PageUrl(Url = "/cpzb/cprecord.aspx")]
         public object GetTransStatisticsList(int LiveId, int AnalystId, string AnalystName, int AnalystType)
         {
             var TransStatisticsList = analystLiveBLL.GetTransStatisticsNum(AnalystId);
             var model = new AnalystLivePageModel();
             model.AnalystLive = TransStatisticsList;
             model.AnalystId = AnalystId;
             model.AnalystName = AnalystName;
             model.AnalystType = AnalystType;
             return new PageResult(null, model);
         }
    }
}
