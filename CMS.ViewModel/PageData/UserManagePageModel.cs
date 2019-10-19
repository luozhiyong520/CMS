using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;
using System.Configuration;
namespace CMS.ViewModel
{
    public class UserManagePageModel
    {
        public AdminMenuModule AdminMenuModule { get; set; }
        public List<AdminMenuModule> AdminMenuList;
        public Dictionary<int, List<AdminMenuModule>> Dic = new Dictionary<int, List<AdminMenuModule>>();

        public Functions Functions { get; set; }
        public List<Functions> FunctionsList;

        public AdminGroup AdminGroup { get; set; }
        public List<AdminGroup> AdminGroupList;

        public Administrator Administrator { get; set; }
        public List<Administrator> AdministratorList;

        public string PermissionType;
        public int GroupId;
        public int AdminId;

        public string ModuleName;
        public int ParentId;

        public List<Permission> PermissionList;

       public string password { get; set; }
        /// <summary>
        /// 权限的功能列表
        /// </summary>
        public List<PermissionFunction> pfList;

        public string GetFunctionCheckedInfo(int funcId, int moduleId)
        {
            if (pfList == null)
                return "";
            var query = (from pf in pfList
                         where pf.MenuId == moduleId
                         where pf.FunctionId == funcId
                         select pf).ToList();
            if (query.Count > 0)
                return "checked";
            return "";
        }

      

        /// <summary>
        /// 功能点权限
        /// </summary>
        public List<MyAuthorityDot> AuthorityDotList { get; set; }
       

    }

    public class MyAuthorityDot
    {
        /// <summary>
        /// 功能点名称
        /// </summary>
        public string DotName { get; set; }
        /// <summary>
        /// 功能点选项
        /// </summary>
        public List<AuthorityDot> AuthorityDots { get; set; }
    }
}
