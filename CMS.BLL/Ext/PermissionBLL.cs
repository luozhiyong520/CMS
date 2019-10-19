using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.DAL;
using CMS.Model;

namespace CMS.BLL
{
    public partial class PermissionBLL
    {
        /// <summary>
        /// 获取单个权限id
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="groupId"></param>
        /// <param name="permissionType"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public int GetPermissionId(int adminId, int groupId, string permissionType, int moduleId)
        {
            return dal.GetPermissionId(adminId,groupId,permissionType,moduleId);
        }
       /// <summary>
       /// 获取多个权限列表
       /// </summary>
       /// <param name="adminId"></param>
       /// <param name="groupId"></param>
       /// <param name="permissionType"></param>
       /// <param name="menuId"></param>
       /// <returns></returns>
        public List<Permission> GetAdminOrGroupPermission(int adminId, int groupId, string permissionType, int menuId)
        {
            return dal.GetAdminOrGroupPermission(adminId, groupId, permissionType, menuId);  
        }
        /// <summary>
        /// 清除权限
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="groupId"></param>
        /// <param name="menuId"></param>
        /// <param name="permissionType"></param>
        public void ClearPermission(int adminId, int groupId, int menuId, string permissionType)
        {
            dal.ClearPermission(adminId, groupId, menuId, permissionType);
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
            return dal.CreatePermissionAndFunction(adminId, groupId, menuId, permissionType, functionId);
        }

        public List<AdminMenuModule> GetPermissionModuleList(int adminId, int groupId, int FunctionId, int ParentId, string MenuType)
        {
            return dal.GetPermissionModuleList(adminId, groupId, FunctionId, ParentId, MenuType);
        }
    }
}
