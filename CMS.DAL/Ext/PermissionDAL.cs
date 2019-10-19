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
    public partial class PermissionDAL
    {
        //创建权限功能Procedure
        readonly string up_Permission_CreatePermissionAndFunction = "up_Permission_CreatePermissionAndFunction";

        //清除权限Procedure
        readonly string up_Permission_Clear = "up_Permission_Clear";
        readonly string up_Permission_Get = "up_Permission_GetAdminOrGroupPermission";
        readonly string up_Permission_GetPermissionId = "up_Permission_GetPermissionId";
        readonly string up_Permission_GetPermissionList = "up_Permission_GetPermissionList";
   
        /// <summary>
        /// 获取单个权限ID
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="groupId"></param>
        /// <param name="permissionType"></param>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        public int GetPermissionId(int adminId, int groupId, string permissionType, int moduleId)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_Permission_Get);
            parms[0].Value = adminId;
            parms[1].Value = groupId;
            parms[2].Value = permissionType;
            parms[3].Value = moduleId;
            SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_Permission_Get, parms);
            if (parms[4].Value.ToString()!="")
            {
                return int.Parse(parms[4].Value.ToString());
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="groupId"></param>
        /// <param name="permissionType"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>

        public List<Permission> GetAdminOrGroupPermission(int adminId, int groupId, string permissionType, int menuId)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_Permission_GetPermissionId);
            parms[0].Value = adminId;
            parms[1].Value = groupId;
            parms[2].Value = permissionType;
            parms[3].Value = menuId;
            List<Permission> list;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_Permission_GetPermissionId, parms))
            {
                list = EntityHelper.GetEntityListByDataReader<Permission>(dr, null);               
            }
            return list;
        }
        

        /// <summary>
        /// 清除权限
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="groupId"></param>
        /// <param name="menuId"></param>
        /// <param name="permissionType"></param>
        public void ClearPermission(int adminId,int groupId,int menuId,string permissionType)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_Permission_Clear);
            parms[0].Value = adminId;
            parms[1].Value = groupId;
            parms[2].Value = menuId;
            parms[3].Value = permissionType;
            SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_Permission_Clear, parms);
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="groupId"></param>
        /// <param name="menuId"></param>
        /// <param name="permissionType"></param>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public int CreatePermissionAndFunction(int adminId, int groupId, int menuId, string permissionType, int functionId)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_Permission_CreatePermissionAndFunction);
            parms[0].Value = adminId;
            parms[1].Value = groupId;
            parms[2].Value = menuId;
            parms[3].Value = permissionType;
            parms[4].Value = functionId;
            SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_Permission_CreatePermissionAndFunction, parms);
            return int.Parse(parms[5].Value.ToString());
        }


        public List<AdminMenuModule> GetPermissionModuleList(int adminId, int groupId, int FunctionId, int ParentId, string MenuType)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_Permission_GetPermissionList);
            parms[0].Value = adminId;
            parms[1].Value = groupId;
            parms[2].Value = FunctionId;
            parms[3].Value = ParentId;
            parms[4].Value = MenuType;
            List<AdminMenuModule> list;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_Permission_GetPermissionList, parms))
            {
                list = EntityHelper.GetEntityListByDataReader<AdminMenuModule>(dr, null);
            }
            return list;
        }

    }
}
