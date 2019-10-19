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
    public partial class SSOResultDAL
    {
        /// <summary>
        /// 获取SSO数据推送列表
        /// </summary>
        private const string up_SSOResultPageList = "up_SSOResult_PageList";
        public PagedResult<SSOResult> GetSSOResultList(int? page, int rows)
        {

            PagedResult<SSOResult> pagest = new PagedResult<SSOResult>();

            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_SSOResultPageList);
            commandParameters[0].Value = rows; //pageSize 每页显示记录数
            commandParameters[1].Value = page;//pageindex 当前页码
            using (DataSet ds = SqlHelper.ExecuteDataset(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_SSOResultPageList, commandParameters))
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    pagest.Result = EntityHelper.GetEntityListByDT<SSOResult>(ds.Tables[0], null);
                }
                pagest.Total = int.Parse(commandParameters[2].Value.ToString());

            }
            return pagest;
        }
    }
}
