using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMVC;
using CMS.BLL;
using CMS.ViewModel;
using Common;

namespace CMS.Controller
{
     [Authorize]
    public class AnalystController
    {
         AnalystBLL analystBLL = Factory.BusinessFactory.CreateBll<AnalystBLL>();

         AnalystPageModel model = new AnalystPageModel();
         
         [Action]
         [PageUrl(Url = "/cpzb/analystrelative.aspx")]
         public object GetTransStatisticsList(int LiveId,int adminid)
         {
             
             var AnalystList = analystBLL.AnalystList(adminid);
             var model = new AnalystPageModel();
             model.adminId = adminid;
             model.AnalystList = AnalystList;
             return new PageResult(null, model);
         }

         /// <summary>
         /// 操盘直播分析师列表,默认显示贵金属
         /// </summary>
         /// <returns></returns>
         [Action]
         [PageUrl(Url = "/cpzb/analystrank.aspx")]
         public object GetAdminList(int? analystType)
         {
             analystType = analystType.HasValue ? analystType : 2;
             SqlWhereList sql = new SqlWhereList();
             sql.Add("AnalystType", analystType);
             model.AnalystList = analystBLL.GetAll(sql);
             if (model.AnalystList != null)
             {
                 model.AnalystList = model.AnalystList.OrderByDescending(a => a.AnalystSort).ToList();
             }
             return new PageResult(null, model);
         }
    }
}
