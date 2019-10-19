using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using CMS.Model;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CMS.DAL
{
    public partial class ActualsAssociateDAL
    {
        private const string up_ActualsAssociatePageList = "up_ActualsAssociatePageList";
        private const string up_ActualsAssociateInsert = "up_ActualsAssociateInsert";
        private const string up_ActualsAssociateUpdate = "up_ActualsAssociateUpdate";
        public PagedResult<ActualsAssociate> GetActualsAssociateList(int typeId, string actualsName, string stockName, int? page, int rows)
        {
            PagedResult<ActualsAssociate> pagest = new PagedResult<ActualsAssociate>();
            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_ActualsAssociatePageList);
            commandParameters[0].Value = rows; //pageSize 每页显示记录数
            commandParameters[1].Value = page;//pageindex 当前页码
            commandParameters[2].Value = actualsName;
            commandParameters[3].Value = stockName;
            commandParameters[4].Value = typeId;
            using (DataSet ds = SqlHelper.ExecuteDataset(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_ActualsAssociatePageList, commandParameters))
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    pagest.Result = EntityHelper.GetEntityListByDT<ActualsAssociate>(ds.Tables[0], null);
                }
                pagest.Total = int.Parse(commandParameters[5].Value.ToString());

            }
            return pagest;
        }

        public string InsertActualsAssociate(ActualsAssociate actualsAssociate)
        {
            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_ActualsAssociateInsert);
            commandParameters[0].Value = actualsAssociate.ActualsCode;
            commandParameters[1].Value = actualsAssociate.ActualsName;
            commandParameters[2].Value = actualsAssociate.StockCode;
            commandParameters[3].Value = actualsAssociate.StockName;
            commandParameters[4].Value = actualsAssociate.TypeId;
            commandParameters[5].Value = actualsAssociate.Exchange;
            commandParameters[6].Value = actualsAssociate.CreatedUser;
            commandParameters[7].Value = DateTime.Now;
            int result = SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_ActualsAssociateInsert, commandParameters);

            return commandParameters[8].Value.ToString();
        }

        public string UpdateActualsAssociate(ActualsAssociate actualsAssociate)
        {
            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_ActualsAssociateUpdate);
            commandParameters[0].Value = actualsAssociate.ActualsCode;
            commandParameters[1].Value = actualsAssociate.ActualsName;
            commandParameters[2].Value = actualsAssociate.StockCode;
            commandParameters[3].Value = actualsAssociate.StockName;
            commandParameters[4].Value = actualsAssociate.TypeId;
            commandParameters[5].Value = actualsAssociate.Exchange;
            commandParameters[6].Value = actualsAssociate.CreatedUser;
            commandParameters[7].Value =  DateTime.Now;
            commandParameters[8].Value = actualsAssociate.Id;
            int result = SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_ActualsAssociateUpdate, commandParameters);
            return commandParameters[9].Value.ToString();
        }

    }
}
