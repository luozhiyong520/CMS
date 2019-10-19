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
    public partial class AnalystDAL
    {
        private const string up_AddEditAnalyst = "up_AddEditAnalyst";
        private const string up_AnalystPageList = "up_AnalystPageList";
        private const string up_AnalystSort = "up_AnalystSort";
        public PagedResult<Analyst> AnalystListPage(int? page, int rows, int analysType, int vipType)
        {
            PagedResult<Analyst> pagest = new PagedResult<Analyst>();
            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_AnalystPageList);
            commandParameters[0].Value = rows; //pageSize 每页显示记录数
            commandParameters[1].Value = page;//pageindex 当前页码
            commandParameters[2].Value = analysType;
            commandParameters[3].Value = vipType;
            using (DataSet ds = SqlHelper.ExecuteDataset(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_AnalystPageList, commandParameters))
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    pagest.Result = EntityHelper.GetEntityListByDT<Analyst>(ds.Tables[0], null);
                }
                pagest.Total = int.Parse(commandParameters[4].Value.ToString());

            }
            return pagest;
        }

        public string EditAnalyst(Analyst analyst)
        {
            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_AddEditAnalyst);
            commandParameters[0].Value = analyst.AnalystId;
            commandParameters[1].Value = analyst.AnalystName;
            commandParameters[2].Value = analyst.FansNum;
            commandParameters[3].Value = analyst.Notice;
            commandParameters[4].Value = analyst.Intro;
            commandParameters[5].Value = analyst.ImgUrl;
            commandParameters[6].Value = analyst.AdminId;
            commandParameters[7].Value = analyst.AnalystType;
            commandParameters[8].Value = analyst.AnalystStatus;
            commandParameters[9].Value = analyst.VipType;
            commandParameters[10].Value = analyst.SoftVersion;
            commandParameters[11].Value = analyst.NickName;
            commandParameters[12].Value = analyst.Accuracy;
            commandParameters[13].Value = analyst.Stability;
            commandParameters[14].Value = analyst.Defense;
            commandParameters[15].Value = analyst.Attack;
            commandParameters[16].Value = analyst.Mentality;
            string result = SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_AddEditAnalyst, commandParameters).ToString();
            return result;
        }

        public string EditAnalystSort(int analystId,int sort)
        {
            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_AnalystSort);
            commandParameters[0].Value = analystId;
            commandParameters[1].Value = sort;
            string result = SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_AnalystSort, commandParameters).ToString();
            return result;
        }

        public List<Analyst> AnalystList(int adminId)
        {
            List<Analyst> list;
            string sql = "SELECT * FROM Analyst WHERE adminId=@adminId or adminId is NULL or adminId=0";
            SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@adminId", adminId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
            {
                list = EntityHelper.GetEntityListByDataReader<Analyst>(dr, null);
            }
            return list;
        }

        /// <summary>
        ///  分析师关联表
        /// </summary>
        /// <param name="analystId"></param>
        /// <param name="adminId"></param>
        public void AnalystRelative(string analystId,int adminId)
        {
            string sql = "update [Analyst] set AdminId=NULL where adminId=@adminId";
            SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@adminId", adminId) };
            SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.Text, sql, parsms);
            if (!string.IsNullOrEmpty(analystId))
            {
                string upsql="";
                string[] array;
                array = analystId.Split(',');
                for ( int i=0; i<array.Length;i++)
                {
                    upsql = "update [Analyst] set AdminId=@adminId where AnalystId =" + array[i] ;
                    SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.Text, upsql, parsms);
                }
            }
            
        }

        /// <summary>
        /// 获取现货分析师/贵金属分析师列表
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="AnalystType"></param>
        /// <returns></returns>
        public List<Analyst> Analystlist(int adminId, int AnalystType)
        {
            List<Analyst> list=null;
            string sql = "select * from [Analyst] where adminId =@adminId and AnalystType=@AnalystType";
            SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@adminId", adminId), new SqlParameter("@AnalystType", AnalystType) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
            {
                list = EntityHelper.GetEntityListByDataReader<Analyst>(dr, null);
            }
            return list;
        }
         
    }
}
