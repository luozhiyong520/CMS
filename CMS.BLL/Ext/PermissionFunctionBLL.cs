using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using CMS.Model;

namespace CMS.BLL
{
    public partial class PermissionFunctionBLL
    {

        /// <summary>
        /// 根据单个权限获取功能列表
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>        
        public List<PermissionFunction> GetFunctionByPermissionId(int permissionId)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            SqlWhereList sql = new SqlWhereList();
            sql.Add("PermissionId", permissionId.ToString());
            return dal.GetAll(sql);
        }


        /// <summary>
        /// 根据多个权限获取功能列表
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        public List<PermissionFunction> GetFunctionByPermissionList(string permissionId)
        {
            return dal.GetAdminOrGroupPermissionFuction(permissionId);
        }



        public List<PermissionFunction> GetAdminOrGroupAllFunction(int adminId, int gourpId, string permissionType)
        {
            return dal.GetAdminOrGroupAllFunction(adminId, gourpId, permissionType);
        }
        

    }
}
