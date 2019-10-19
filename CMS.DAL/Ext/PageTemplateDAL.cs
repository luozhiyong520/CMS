using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;
using CMS.Model;
using Common;


namespace CMS.DAL
{
    public partial class PageTemplateDAL
    {
        private const string up_selectTemplateByNewsID = "up_SelectTemplateByNewsID";
        private const string up_PageTemplatePageList = "up_PageTemplatePageList";
        
        /// <summary>
        /// 根据NewsID获取模板
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        public PageTemplate GetTemplateByNewsID(int newsID)
        {
            DataSet ds = null;
            using (DataAccess da = new DataAccess())//default connectstirng
            {
                ds = da.ExecuteDataSet(CommandType.StoredProcedure, 
                                       up_selectTemplateByNewsID,
                                       new SqlParameter[]{new SqlParameter("NewsID",newsID) }
                                       );
            }
            List<PageTemplate> list = new List<PageTemplate>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = EntityHelper.GetEntityListByDT<PageTemplate>(ds.Tables[0], null);
            }
            return list.Count==1?list[0]:null;
        }

        /// <summary>
        /// 分页读取数据
        /// </summary>
        public PagedResult GetPageTemplatePageList(int templeteType, int? pageIndex, int pageSize, string templateName, string templateFileName)
        {
            PagedResult pagest = new PagedResult();
            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, "up_PageTemplatePageList");
            commandParameters[0].Value = pageSize;//pageSize 每页显示记录数
            commandParameters[1].Value =pageIndex ; //pageindex 当前页码
            commandParameters[2].Value = templeteType;
            commandParameters[3].Value = string.Format("%{0}%", templateName);
            commandParameters[4].Value = string.Format("%{0}%", templateFileName); ;

            DataSet st = SqlHelper.ExecuteDataset(SqlConnectFactory.CMS, CommandType.StoredProcedure, "up_PageTemplatePageList", commandParameters);
            pagest.Result = st.Tables[0];
            pagest.Total = int.Parse(commandParameters[5].Value.ToString());
            return pagest;
        }


        //删除模板
        /// <summary>
        /// 模板事务删除
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public int DeletePageTemplate(int templateId)
        {
            int result = 0;
            string sqlPageTemplate = "delete from PageTemplate where TemplateId=@TemplateId";
            string sqlTemplateDetail = "delete from TemplateDetail where TemplateId=@TemplateId";
            SqlParameter parm = new SqlParameter("@TemplateId", templateId);
            SqlConnection conn = new SqlConnection(SqlConnectFactory.CMS);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();           
            try
            {
                result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sqlTemplateDetail, parm);
                result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sqlPageTemplate, parm);
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new Exception("", ex);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        /// <summary>
        /// 通过ChannelId获取详细页的模板信息
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public PageTemplate GetDetailPageTemplateIdByChannelId(string channelId)
        {
            string sql = "select * from v_TemplateDetail where ChannelId=@ChannelId and RelationType=2";
            SqlParameter parm = new SqlParameter("@ChannelId", channelId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parm))
            {
                if (dr.Read())
                {
                    return EntityHelper.GetEntityByDataReader<PageTemplate>(dr, null);
                }
                return null;
            }
        }
    }
}
