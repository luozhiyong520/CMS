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
    public partial class SendMsgDAL
    {
        private const string up_SendMsgPageList = "up_SendMsgPageList";
        //private const string up_SendMsgBack = "up_SendMsgBack";
        //private const string up_SendMsgGo = "up_SendMsgGo";

        /// <summary>
        /// 后台留言列表
        /// </summary>
        /// <param name="page">当前页索引</param>
        /// <param name="rows">每页显示的条数</param>
        /// <param name="TypeContent">问题类型</param>
        /// <param name="MsgType">问题类别</param>
        /// <param name="TuTimeStart">问题开始时间</param>
        /// <param name="TuTimeEnd">问题结束时间</param>
        /// <param name="ReplyTimeStart">回复开始时间</param>
        /// <param name="ReplyTimeEnd">回复结束时间</param>
        /// <param name="YesOrNo">是否回复</param>
        /// <param name="Editor">回复人</param>
        /// <returns></returns>
        public PagedResult<SendMsg> GetSendMsgList(int? page, int rows, int adminId, int authorityParentId, int TypeContent, string MsgType, string MsgContent, string TuTimeStart, string TuTimeEnd, string ReplyTimeStart, string ReplyTimeEnd, string YesOrNo, string Editor)
        {
            if (!string.IsNullOrEmpty(TuTimeEnd))
            {
                TuTimeEnd = DateTime.Parse(TuTimeEnd).AddDays(1).ToShortDateString();
            }
            if (!string.IsNullOrEmpty(ReplyTimeEnd))
            {
                ReplyTimeEnd = DateTime.Parse(ReplyTimeEnd).AddDays(1).ToShortDateString();
            }
            if (!string.IsNullOrEmpty(Editor))
            {
                Editor = string.Format("%{0}%", Editor);
            }
            if (!string.IsNullOrEmpty(MsgContent))
            {
                MsgContent = string.Format("%{0}%", MsgContent);
            }
            PagedResult<SendMsg> pagest = new PagedResult<SendMsg>();
            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_SendMsgPageList);
            commandParameters[0].Value = rows; //pageSize 每页显示记录数
            commandParameters[1].Value = page;//pageindex 当前页码
            commandParameters[2].Value = adminId;
            commandParameters[3].Value = authorityParentId;
            commandParameters[4].Value = TypeContent;
            commandParameters[5].Value = MsgType;
            commandParameters[6].Value = TuTimeStart;
            commandParameters[7].Value = TuTimeEnd;
            commandParameters[8].Value = ReplyTimeStart;
            commandParameters[9].Value = ReplyTimeEnd;
            commandParameters[10].Value = YesOrNo;
            commandParameters[11].Value = Editor;
            commandParameters[12].Value = MsgContent;
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_SendMsgPageList, commandParameters))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        pagest.Result = EntityHelper.GetEntityListByDT<SendMsg>(ds.Tables[0], null);
                    }
                    pagest.Total = int.Parse(commandParameters[13].Value.ToString());

                }
            }
            catch (Exception ex)
            {
                Loger.Error("后台留言列表:" + ex.Message);
            }
            return pagest;
        }
        /// <summary>
        /// 根据msgId获取数据
        /// </summary>
        /// <param name="msgId"></param>
        /// <returns></returns>
        public SendMsg GetSendMsgById(int msgId)
        {
            try
            {
            string sql = "select sm.*,mt.TypeContent from SendMsg sm inner join MsgType mt on sm.MsgTypeId=mt.MsgTypeId where sm.MsgId=@MsgId";
            SqlParameter[] parsms = null;
            parsms = new SqlParameter[] { new SqlParameter("@MsgId", msgId) };
                using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
                {
                    if (dr.Read())
                    {
                        return EntityHelper.GetEntityByDataReader<SendMsg>(dr, null);
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Loger.Error("根据msgId获取数据:" + ex.Message);
            }
            return null;
        }
        //上一题
        public SendMsg GetSendMsgbackById(int msgid, int msgTypeId, string MsgIds)
        {
           // string sql = "select top 1 * from SendMsg where MsgId>@MsgId and MsgTypeId in(select RelevanceId from AuthorityDot where Id in(select AuthorityDotId from dbo.AuthorityDotAdmin where AdminId=@msgTypeId)) order by QuTime";

            string sql = "";
            SqlParameter[] parsms = null;
            if (!string.IsNullOrEmpty(MsgIds))
            {
                sql = "exec('select top 1 sm.*,mt.TypeContent from SendMsg sm inner join MsgType mt on sm.MsgTypeId=mt.MsgTypeId where sm.MsgId>" + msgid + " and sm.MsgId in(" + MsgIds + ") order by sm.QuTime')";
               // parsms = new SqlParameter[] { new SqlParameter("@MsgId", msgid), new SqlParameter("@MsgIds", MsgIds) };
            }
            //else
            //{
            //    parsms = new SqlParameter[] { new SqlParameter("@MsgId", msgid), new SqlParameter("@MsgTypeId", msgTypeId) };
            //}          
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
            {
                if (dr.Read())
                {
                    return EntityHelper.GetEntityByDataReader<SendMsg>(dr, null);
                }
                return null;
            }
        }
        //下一题
        public SendMsg GetSendMsggoById(int msgid, int msgTypeId, string MsgIds)
        {
          // string sql = "select top 1 * from SendMsg where MsgId<@MsgId and MsgTypeId in(select RelevanceId from AuthorityDot where Id in(select AuthorityDotId from dbo.AuthorityDotAdmin where AdminId=@msgTypeId)) order by QuTime desc";
            string sql = "";
            SqlParameter[] parsms = null;
            if (!string.IsNullOrEmpty(MsgIds))
            {
                sql = "exec('select top 1 sm.*,mt.TypeContent from SendMsg sm inner join MsgType mt on sm.MsgTypeId=mt.MsgTypeId where sm.MsgId<" + msgid + " and sm.MsgId in(" + MsgIds + ") order by sm.QuTime desc')";
                //parsms = new SqlParameter[] { new SqlParameter("@MsgId", msgid), new SqlParameter("@MsgIds", MsgIds) };
            }
            //else
            //{
            //    parsms = new SqlParameter[] { new SqlParameter("@MsgId", msgid), new SqlParameter("@MsgTypeId", msgTypeId) };
            //}
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
            {
                if (dr.Read())
                {
                    return EntityHelper.GetEntityByDataReader<SendMsg>(dr, null);
                }
                return null;
            }
        }
    }
}
