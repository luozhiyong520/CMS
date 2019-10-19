using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace CMS.DAL
{
    public partial class CustomerGroupDAL
    {
        /// <summary>
        /// 删除已存在的用户组用户信息
        /// </summary>
        /// <param name="GroupName">用户组名</param>
        /// <returns></returns>
        public int DeleteGroup(string GroupName)
        {
            string sql = "delete from CustomerGroup where GroupName=@GroupName";
            SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@GroupName", GroupName) };
            return SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.Text, sql, parsms);
        }

        /// <summary>
        /// 查询用户组
        /// </summary>
        /// <returns></returns>
        public List<CustomerGroup> CustomerGroupList()
        {
            string sql = "SELECT GroupName FROM CustomerGroup group by GroupName";
            List<CustomerGroup> list;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql))
            {
                list = EntityHelper.GetEntityListByDataReader<CustomerGroup>(dr, null);
            }
            return list;
        }


        /// <summary>
        /// 一次性把Table中的数据插入到数据库
        /// </summary>
        /// <param name="dt"></param>
        public void BulkToDB(DataTable dt)
        {

            SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlConnectFactory.CMS);
            bulkCopy.DestinationTableName = "CustomerGroup";
            bulkCopy.BatchSize = dt.Rows.Count;
            bulkCopy.BulkCopyTimeout = 999;

            try
            {
                if (dt != null && dt.Rows.Count != 0)
                    bulkCopy.WriteToServer(dt);
            }
            catch (Exception ex)
            {
                Loger.Info("批量插入出错:" + ex.ToString());
                throw ex;
            }
            finally
            {
                bulkCopy.Close();
            }
        }

    }
}
