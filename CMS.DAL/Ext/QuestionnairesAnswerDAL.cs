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
    public partial class QuestionnairesAnswerDAL
    {
        /// <summary>
        /// 提交问卷
        /// </summary>
        /// <param name="optionId"></param>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string SubmitInfo(int qId, string optionId, int customerId, string userName)
        {
           
            string[] arrayid;
            string strmsg="";
            int OptionsId=0;
            int j = 0;
           
            if (string.IsNullOrEmpty(optionId))
            {
                return strmsg;
            }
            arrayid = optionId.Split(',');
            if (CheckUser(customerId)<1)
            {
                foreach (string i in arrayid)
                {
                    string info = string.Empty;
                    string[] arraytext = i.Split('|');
                    if (arraytext.Count() > 1)
                    {
                        info = arraytext[1].ToString();
                        OptionsId = int.Parse(arraytext[0].ToString());
                    }
                    else
                    {

                        OptionsId = int.Parse(arrayid[j]);
                    }
                    j++;
                    string sql = "insert into QuestionnairesAnswer(QId,QoId,Info,CustomerId,UserName,CreateTime) values(@qId,@OptionsId,@info,@CustomerId,@userName,@CreateTime)";
                    SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@qId", qId), new SqlParameter("@OptionsId", OptionsId),new SqlParameter("@info", info),
                    new SqlParameter("@CustomerId", customerId),new SqlParameter("@userName", userName),new SqlParameter("@CreateTime", DateTime.Now)};
                    strmsg = SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.Text, sql, parsms).ToString();
                }
            }
            return strmsg;
        }

        /// <summary>
        /// 检查用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int CheckUser(int customerId)
        {
            int result=0;
            string sql = "select count(CustomerId) as num from QuestionnairesAnswer where CustomerId=@CustomerId";
            SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@CustomerId", customerId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
            {
                if (dr.Read())
                {
                    result = int.Parse(dr["num"].ToString());
                }
            }
            return result;
        }
        /// <summary>
        /// 选项投票人数
        /// </summary>
        /// <param name="QoId"></param>
        /// <returns></returns>
        public int GetAnswerCount(int qoId, string strat, string end)
        {
            string sql = "select count(1) from dbo.QuestionnairesAnswer where QoId=@QoId ";
            if (!string.IsNullOrEmpty(strat))
                sql += " and CreateTime >@Start";
            if (!string.IsNullOrEmpty(end))
                sql += " and CreateTime <=@End";

            SqlParameter[] parsms = new SqlParameter[]{
                    new SqlParameter("@QoId",qoId),
                    new SqlParameter("@Start",strat),
                    new SqlParameter("@End",end)
                };
            object obj = SqlHelper.ExecuteScalar(SqlConnectFactory.CMS, CommandType.Text, sql, parsms);
            if (obj != null)
                return int.Parse(obj.ToString());
            return 0;
        }
         
        /// <summary>
        /// 参与调查人数
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public int GetSurveyed(int qId, string strat, string end)
        {
            string sql = "select count(1) from dbo.QuestionnairesAnswer where QId=@QId ";
            if (!string.IsNullOrEmpty(strat))
                sql += " and CreateTime >@Start";
            if (!string.IsNullOrEmpty(end))
                sql += " and CreateTime <=@End";
            sql += " group by CustomerId,UserName";

            SqlParameter[] parsms = new SqlParameter[] {
                    new SqlParameter("@QId", qId),
                    new SqlParameter("@Start", strat),
                    new SqlParameter("@End", end)
                };
            //object obj = SqlHelper.ExecuteScalar(SqlConnectFactory.CMS, CommandType.Text, sql, parsms);
            //if (obj != null)
            //    return int.Parse(obj.ToString());
            List<QuestionnairesAnswer> list;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
            {
                list = EntityHelper.GetEntityListByDataReader<QuestionnairesAnswer>(dr, null);
            }
            if (list==null)
            {
                return 0;
            }
            return list.Count;
        }
        /// <summary>
        /// 问卷建议
        /// </summary>
        /// <param name="QId"></param>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public List<QuestionnairesAnswer> GetAllProposal(int qId, string strat, string end)
        {
            string sql = "select * from dbo.QuestionnairesAnswer where QId=@QId and Info != ''";
            if (!string.IsNullOrEmpty(strat))
                sql += " and CreateTime >@Start";
            if (!string.IsNullOrEmpty(end))
                sql += " and CreateTime <=@End";

            SqlParameter[] parsms = new SqlParameter[] {
                    new SqlParameter("@QId",qId),
                    new SqlParameter("@Start", strat),
                    new SqlParameter("@End", end)
                };
            List<QuestionnairesAnswer> list;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
            {
                list = EntityHelper.GetEntityListByDataReader<QuestionnairesAnswer>(dr, null);
            }
            return list;
        }
        //public int SelectOptionsId(int OptionsId)
        //{
        //    string sql = "select * from dbo.QuestionnairesAnswer where OptionsId=" + OptionsId + "";
        //    //return SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.Text, sql);
        //    DataSet st = SqlHelper.ExecuteDataset(SqlConnectFactory.CMS, CommandType.Text, sql);
        //    return int.Parse(st.Tables[0].Rows.Count.ToString());
        //}
    }
}
