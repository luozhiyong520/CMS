using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using CMS.Model;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CMS.DAL.Ext
{
    public partial class GetFrontPageListDAl
    {
        public PagedResult GetFrontPageList(int? page, int rows, int TotalFlag, string ChannelId, int Sort)
        {
            PagedResult pagest = new PagedResult();

            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, "up_News_GetFrontPageList");
            commandParameters[0].Value = rows;
            commandParameters[1].Value = page;
            commandParameters[2].Value = TotalFlag;
            commandParameters[3].Value = ChannelId;
            commandParameters[4].Value = Sort;

            DataSet st = SqlHelper.ExecuteDataset(SqlConnectFactory.CMS, CommandType.StoredProcedure, "up_News_GetFrontPageList", commandParameters);
            pagest.Result = st.Tables[0];
            pagest.Total = int.Parse(commandParameters[6].Value.ToString());
            return pagest;
        }

        public List<News> GetWhereNews(string strNewsID)
        {
            string sql;
            sql = "select UploadPath from News where NewsID in(" + strNewsID + ")";
            List<News> list;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql))
            {
                list = EntityHelper.GetEntityListByDataReader<News>(dr, null);
            }
            return list;
        }
    }
}
