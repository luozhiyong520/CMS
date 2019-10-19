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
    public partial class PermissionFunctionDAL
    {
        private const string up_Function_GetAdminOrGroupAllFunction = "up_Function_GetAdminOrGroupAllFunction";
        /// <summary>
        /// 根据多个权限获取功能列表
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        public List<PermissionFunction> GetAdminOrGroupPermissionFuction(string permissionId)
        {
            string sql = "select FunctionId from PermissionFunction where [PermissionId] in(" + permissionId + ")";
            List<PermissionFunction> list;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql))
            {
                list = EntityHelper.GetEntityListByDataReader<PermissionFunction>(dr, null);
            }
            return list;
        }

        public List<PermissionFunction> GetAdminOrGroupAllFunction(int adminId, int gourpId, string permissionType)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_Function_GetAdminOrGroupAllFunction);
            parms[0].Value = adminId;
            parms[1].Value = gourpId;
            parms[2].Value = permissionType;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_Function_GetAdminOrGroupAllFunction, parms))
            {
                return EntityHelper.GetEntityListByDataReader<PermissionFunction>(dr, null);
            }
        }
    }
}
