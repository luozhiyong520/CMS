using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.BLL;
using SMVC;
using Common;
using System.Web;
using CMS.Model;
using CMS.ViewModel;
using CMS.Utility;
using System.Data;

namespace CMS.Controller
{
   public class AjaxPopupMsgPlan
    {
       PopupMsgPlanBLL popupMsgPlanBLL = Factory.BusinessFactory.CreateBll<PopupMsgPlanBLL>();
       PopupMsgPlan PopupMsgPlan = new PopupMsgPlan();
       [Action]
       public JsonResult SearchPopupMsgPlan(int? page, int rows)
       {
           PagingInfo PageInfo = new PagingInfo();
           PagedResult<PopupMsgPlan> pagedResult =popupMsgPlanBLL.GetPopupMsgPlanList(page,rows);
           PageInfo.PageIndex = page.HasValue ? page.Value - 1 : 0;
           PageInfo.PageSize = rows;
           PageInfo.TotalRecords = pagedResult.Total;
           //if (pagedResult.Result.Count>0)
           //{
               
           //    foreach (var item in pagedResult.Result)
           //    {
           //        if (item.BeginTime<DateTime.Now)
           //         {
           //             item.StatusVaule = "待发送";
           //         }
           //        else if (item.BeginTime==DateTime.Now)
           //        {
           //            item.StatusVaule = "正在发送";
           //        }
           //        else if (item.EndTime>DateTime.Now)
           //        {
           //            item.StatusVaule = "发送完成";
           //        }
           //        else if (item.Status==4)
           //        {
           //            item.StatusVaule = "已取消发送";
           //        }
           //        else if (item.Status == 5)
           //        {
           //            item.StatusVaule = "发送中被中止";
           //        }
           //        pagedResult.Result.Add(item);
           //    }
           //}
           var result = new GridResult<PopupMsgPlan>(pagedResult.Result, pagedResult.Total);
           return new JsonResult(result);
       }

       /// <summary>
       /// 获取弹窗信息计划表的数据
       /// </summary>
       /// <param name="PlanId"></param>
       /// <returns></returns>
       [Action]
       public object GetPopupMsgPlan(int PlanId)
       {
           if (string.IsNullOrEmpty(PlanId.ToString()))
           {
               return null;
           }
           return new JsonResult(popupMsgPlanBLL.Get("PlanId", PlanId.ToString()));
       }

       /// <summary>
       /// 取消计划发送
       /// </summary>
       /// <param name="PlanId"></param>
       /// <returns></returns>
       [Action]
       public string CancelPlan(int PlanId)
       {
           PopupMsgPlan = popupMsgPlanBLL.Get("PlanId", PlanId);
           PopupMsgPlan.Status = 3;
           if (SsoServer.UpDateStatus(PlanId, 0)==0)
           {
               if(popupMsgPlanBLL.UpdateReceiveMsg(PlanId))
               {
                  popupMsgPlanBLL.Update(PopupMsgPlan).ToString();
               }
               return "000000";
           }
           return "000001";
 
       }

       /// <summary>
       /// 中止计划发送
       /// </summary>
       /// <param name="PlanId"></param>
       /// <returns></returns>
       [Action]
       public string StopPlan(int PlanId)
       {
           PopupMsgPlan = popupMsgPlanBLL.Get("PlanId", PlanId);
           PopupMsgPlan.Status = 4;
           if (SsoServer.UpDateStatus(PlanId, 0)==0)
           {
               if (popupMsgPlanBLL.UpdateReceiveMsg(PlanId))
               {
                   popupMsgPlanBLL.Update(PopupMsgPlan).ToString();
               }
               return "000000";
           }
           return "000001";
           
          
       }

    }
}
