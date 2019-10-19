using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;
using System.Data.SqlClient;
using Common;
using System.Data;

namespace CMS.DAL
{
    public partial class CourseRecommendDAL
    {

        /// <summary>
        /// 删除单条记录
        /// </summary>
        public int Delete(string version)
        {
            string sql = "Delete From CourseRecommend Where SoftVersion = '" + version + "'";
            using (var da = new DataAccess(ConnString))
            {
                return da.ExecuteNonQuery(sql);
            }
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        public int Update(int crId, int status)
        {
            string sql = "Update CourseRecommend set Status = " + status + " Where Id = " + crId;
            using (var da = new DataAccess(ConnString))
            {
                return da.ExecuteNonQuery(sql);
            }
        }


        public List<CourseRecommend> GetCourseRecommend(string softVersion, int status)
        {
            string sql = "select * from CourseRecommend where SoftVersion=@SoftVersion and Status=@Status";
            SqlParameter[] parsms = new SqlParameter[] {
                    new SqlParameter("@SoftVersion",softVersion),
                    new SqlParameter("@Status",status)
                };
            List<CourseRecommend> list;
            //string result;
            //result =SqlHelper.ExecuteScalar(SqlConnectFactory.CMS, CommandType.Text, sql, parsms).ToString();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
            {
                list = EntityHelper.GetEntityListByDataReader<CourseRecommend>(dr, null);
            }
            return list;
        }
    }
}
