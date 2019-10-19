using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.DAL;
using System.Data;
using Common;
using CMS.Model;
using System.Text.RegularExpressions;

namespace CMS.BLL
{
    public partial class PopupMsgPlanBLL
    {
       public PagedResult<PopupMsgPlan> GetPopupMsgPlanList(int? page, int rows)
        {
            PagedResult<PopupMsgPlan> st = dal.GetPopupMsgPlanList(page, rows);
            return st;
        }

        /// <summary>
        /// 更新收件箱状态
        /// </summary>
        /// <param name="PlanId"></param>
        /// <returns></returns>
       public bool UpdateReceiveMsg(int PlanId)
       {
           return dal.UpdateReceiveMsg(PlanId);
       }
    }
}
