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
   public partial class FragmentDAL
    {
       public PagedResult GetPageList(int TypeId, string searchdrphannelID, string searchContent, int pageSize, int? pageindex)
       {
           PagedResult pagest = new PagedResult();
           SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, "up_Fragment_GetPageList_history");
           commandParameters[0].Value = TypeId;
           commandParameters[1].Value = pageSize; //pageSize 每页显示记录数
           commandParameters[2].Value = pageindex; //pageindex 当前页码
           commandParameters[3].Value = searchdrphannelID;
           commandParameters[4].Value = searchContent;

           using (DataSet st = SqlHelper.ExecuteDataset(SqlConnectFactory.CMS, CommandType.StoredProcedure, "up_Fragment_GetPageList_history", commandParameters))
           {
               pagest.Result = st.Tables[0];
               pagest.Total = int.Parse(commandParameters[5].Value.ToString());
               return pagest;
           }
       }

       public DataTable GetFragmentHistory(string channelId)
       {
            string sql = "select top(5) * from Fragment where ChannelId=@ChannelId order by FragmentId Desc";
            SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@ChannelId", channelId) };
            using (DataTable dt = SqlHelper.ExecuteDataset(SqlConnectFactory.CMS, CommandType.Text, sql, parsms).Tables[0])
            {
                return dt;
            }
       }

       /// <summary>
       /// 获取栏目最新的一条碎片
       /// </summary>
       /// <param name="channelId"></param>
       /// <returns></returns>
       public Fragment GetLastOneFrgment(string channelId)
       {
           string sql = "select top(1) * from Fragment where ChannelId=@ChannelId order by Fragmentid desc";
           SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@ChannelId", channelId) };
           using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
           {
               if (dr.Read())
               {
                   return EntityHelper.GetEntityByDataReader<Fragment>(dr, null);
               }
               return null;
           }
       }

    }
}
