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
    public partial class CourseProgramDAL
    {
        /// <summary>
        /// 获取对应版本的课程计划
        /// </summary>
        /// <param name="softVersion">软件版本</param>
        public List<CourseProgram> GetCourseProgram(string softVersion)
        {
            string sql = "select * from CourseProgram where SoftVersion = @SoftVersion order by Weeks, OrderNum, CourseType, ProgramTimeStart, ProgramTimeEnd";

            SqlParameter[] parsms = new SqlParameter[] {
                    new SqlParameter("@SoftVersion",softVersion)
                };
            List<CourseProgram> list;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
            {
                list = EntityHelper.GetEntityListByDataReader<CourseProgram>(dr, null);
            }
            return list;
        }

        /// <summary>
        /// 获取所有版本
        /// </summary>
        public string GetVersion()
        {
            string sql = "select SoftVersion from CourseProgram group by SoftVersion";

            List<CourseProgram> list;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql))
            {
                list = EntityHelper.GetEntityListByDataReader<CourseProgram>(dr, null);
            }
            if (list == null)
                return "[]";
            return JsonHelper.ToJson(list);
        }

        /// <summary>
        /// 获取此版本下的所有类别课程
        /// </summary>
        public List<CourseProgram> GetCourseType(string softVersion)
        {
            string sql = "select CourseType, OrderNum from CourseProgram where SoftVersion = @SoftVersion and Id in(select min(Id) from CourseProgram group by CourseType) order by OrderNum";


            SqlParameter[] parsms = new SqlParameter[] {
                    new SqlParameter("@SoftVersion",softVersion)
                };
            List<CourseProgram> list;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
            {
                list = EntityHelper.GetEntityListByDataReader<CourseProgram>(dr, null);
            }
            return list;
        }
        
        /// <summary>
        /// 更新此版本课程排序
        /// </summary>
        public int UpdateCourseType(string softVersion, string setVel)
        {
            setVel = setVel.Replace("|", "' then ");
            string[] courseTypes = setVel.Split(',');
            string sqlCase = "";
            foreach (var item in courseTypes)
	        {
                sqlCase += " when CourseType = '" + item + " ";
	        }
            string sql = "update CourseProgram set OrderNum = " +	
	                        "case " +
		                        sqlCase +
		                        "else 99 " +
	                        "end  ";
            using (var da = new DataAccess(ConnString))
            {
                return da.ExecuteNonQuery(sql);
            }
        }

        /// <summary>
        /// 删除单条记录
        /// </summary>
        public int Delete(int id)
        {
            string sql = "Delete From CourseProgram Where Id = " + id;
            using (var da = new DataAccess(ConnString))
            {
                return da.ExecuteNonQuery(sql);
            }
        }
    }
}
