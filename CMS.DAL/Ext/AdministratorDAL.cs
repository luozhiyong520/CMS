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
    public partial class AdministratorDAL
    {
        /// <summary>
        /// 获取用户组与用户表关联的列表
        /// </summary>
        /// <returns></returns>
        public List<Administrator> GetAdministratorList()
        {
            string sql = "select * from Administrator As A inner join AdminGroup As G on A.GroupId=G.GroupId";
            List<Administrator> list;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql))
            {
                list = EntityHelper.GetEntityListByDataReader<Administrator>(dr, null);
            }
            return list;
        }
        /// <summary>
        /// 获取有条件的用户组与用户表关联的列表
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>

        public List<Administrator> GetAdministratorList(int adminId)
        {
            string sql = "select * from Administrator As A inner join AdminGroup As G on A.GroupId=G.GroupId where A.AdminId="+adminId;
            List<Administrator> list;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql))
            {
                list = EntityHelper.GetEntityListByDataReader<Administrator>(dr, null);
            }
            return list;
        }


       
        

    }
}
