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
    public partial class MsgTypeDAL
    {
        private const string up_MsgTypeListByAdminId = "up_MsgTypeListByAdminId";
        /// <summary>
        /// 根据登录Id获取留言类型的权限数据
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="authorityParentId"></param>
        /// <returns></returns>
        public List<MsgType> GetMsgTypeByAdminId(int adminId, int authorityParentId)
        {
            //注释这种方法始终是少了一条数据

            //string sql = "select * from MsgType where MsgTypeId in(select RelevanceId from AuthorityDot where Id in(select AuthorityDotId from dbo.AuthorityDotAdmin where AdminId=@adminId and ParentId=@ParentId)) order by MsgTypeId asc";
            //SqlParameter[] parsms= new SqlParameter[] { new SqlParameter("@adminId", adminId), new SqlParameter("@ParentId", authorityParentId) };
            //using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
            //{
            //    if (dr.Read())
            //    {
            //        list = EntityHelper.GetEntityListByDataReader<MsgType>(dr, null);
            //    }
            //    return list;
            //}
            PagedResult<MsgType> pagest = new PagedResult<MsgType>();
            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_MsgTypeListByAdminId);
            commandParameters[0].Value = adminId; 
            commandParameters[1].Value = authorityParentId;
            using (DataSet ds = SqlHelper.ExecuteDataset(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_MsgTypeListByAdminId, commandParameters))
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    pagest.Result = EntityHelper.GetEntityListByDT<MsgType>(ds.Tables[0], null);
                    return pagest.Result;
                }
                return null;
            }
        }
    }
}
