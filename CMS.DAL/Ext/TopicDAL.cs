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
    public partial class TopicDAL
    {
        private const string up_TopicPageList = "up_TopicPageList";
        private const string up_Topic_FrontPageList = "up_Topic_FrontPageList";
        public PagedResult<Topic> GetTopicList(string topicName, int topicTypeId, int? page, int rows)
        {
            PagedResult<Topic> pagest = new PagedResult<Topic>();
            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_TopicPageList);
            commandParameters[0].Value = rows; //pageSize 每页显示记录数
            commandParameters[1].Value = page;//pageindex 当前页码
            commandParameters[2].Value = topicName;
            commandParameters[3].Value = topicTypeId;
            using (DataSet ds = SqlHelper.ExecuteDataset(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_TopicPageList, commandParameters))
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    pagest.Result = EntityHelper.GetEntityListByDT<Topic>(ds.Tables[0], null);
                }
                pagest.Total = int.Parse(commandParameters[4].Value.ToString());

            }
            return pagest;
        }

        /// <summary>
        /// 前台专题查询列表
        /// </summary>
        /// <param name="topicName"></param>
        /// <param name="topicTypeId"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public PagedResult<Topic> GetTopicFrontList(string topicName, string topicTypeId, int? page, int rows)
        {
            PagedResult<Topic> pagest = new PagedResult<Topic>();
            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_Topic_FrontPageList);
            commandParameters[0].Value = rows; //pageSize 每页显示记录数
            commandParameters[1].Value = page;//pageindex 当前页码
            commandParameters[2].Value = topicName;
            commandParameters[3].Value = topicTypeId;
            using (DataSet ds = SqlHelper.ExecuteDataset(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_Topic_FrontPageList, commandParameters))
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    pagest.Result = EntityHelper.GetEntityListByDT<Topic>(ds.Tables[0], null);
                }
                pagest.Total = int.Parse(commandParameters[4].Value.ToString());

            }
            return pagest;
        }

 
        public Topic GetTopic(int topicId) 
        {
            string sql = "select * from dbo.Topic t inner join dbo.TopicType y on t.TopicTypeId=y.TopicTypeId where TopicId = @topicId";
            SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@topicId", topicId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
            {
                if (dr.Read())
                    return EntityHelper.GetEntityByDataReader<Topic>(dr, null);
                return null;
            }
        }
    }
}
