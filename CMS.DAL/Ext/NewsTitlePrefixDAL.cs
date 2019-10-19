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
  public partial class NewsTitlePrefixDAL
    {
      private const string up_NewsTitlePrefixPageList = "up_NewsTitlePrefixPageList";
      public PagedResult<NewsTitlePrefix> GetNewsTitlePrefixList(int? page, int rows)
      {
          PagedResult<NewsTitlePrefix> pagest = new PagedResult<NewsTitlePrefix>();
          SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_NewsTitlePrefixPageList);
          commandParameters[0].Value = rows; //pageSize 每页显示记录数
          commandParameters[1].Value = page;//pageindex 当前页码
          using (DataSet ds = SqlHelper.ExecuteDataset(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_NewsTitlePrefixPageList, commandParameters))
          {
              if (ds != null && ds.Tables.Count > 0)
              {
                  pagest.Result = EntityHelper.GetEntityListByDT<NewsTitlePrefix>(ds.Tables[0], null);
              }
              pagest.Total = int.Parse(commandParameters[2].Value.ToString());

          }
          return pagest;
      }
    }
}
