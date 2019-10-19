using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CMS.Model;
using Common;

namespace CMS.DAL
{
    public partial class PopupMsgPlanDAL
    {
        private const string up_PopupMsgPlanPageList = "up_PopupMsgPlanPageList";
        public PagedResult<PopupMsgPlan> GetPopupMsgPlanList(int? page, int rows)
        {

            PagedResult<PopupMsgPlan> pagest = new PagedResult<PopupMsgPlan>();

            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_PopupMsgPlanPageList);
            commandParameters[0].Value = rows; //pageSize 每页显示记录数
            commandParameters[1].Value = page;//pageindex 当前页码
            using (DataSet ds = SqlHelper.ExecuteDataset(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_PopupMsgPlanPageList, commandParameters))
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        switch (dt.Rows[i]["PushVersion"].ToString())
                        {
                            case "1000":
                                dt.Rows[i]["PushVersion"] = "金蝴蝶";
                                break;
                            case "3100":
                                dt.Rows[i]["PushVersion"] = "严林版";
                                break;
                            case "5100":
                                dt.Rows[i]["PushVersion"] = "金牡丹";
                                break;
                            case "6100":
                                dt.Rows[i]["PushVersion"] = "渤商版";
                                break;
                            case "0":
                                dt.Rows[i]["PushVersion"] = "所有版本";
                                break;
                        }
                    }
                    pagest.Result = EntityHelper.GetEntityListByDT<PopupMsgPlan>(dt, null);
                }
                pagest.Total = int.Parse(commandParameters[2].Value.ToString());

            }
            return pagest;
        }
        /// <summary>
        /// 更新收件箱状态
        /// </summary>
        /// <param name="PlanId"></param>
        /// <returns></returns>
        public bool UpdateReceiveMsg(int PlanId)
        {
            string sql = "update ReceiveMsg set Status=0 where MsgSourceId=@PlanId and MsgSource=1";
            SqlParameter[] parsms = new SqlParameter[] {new SqlParameter("@PlanId", PlanId) };
            int result= SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.Text, sql, parsms);
            if (result >= 0)
            {
                return true;
            }
            return false;
        }

    }
}
