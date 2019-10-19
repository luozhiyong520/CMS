using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.BLL;
using SMVC;
using CMS.Model;
using CMS.ViewModel;
using Common;
using System.Web;

namespace CMS.Controller
{
    [Authorize]
    public class UserManageController
    {
        AdminMenuModuleBLL menuBLL = Factory.BusinessFactory.CreateBll<AdminMenuModuleBLL>();
        FunctionsBLL funcBLL = Factory.BusinessFactory.CreateBll<FunctionsBLL>();
        AdminGroupBLL groupBLL = Factory.BusinessFactory.CreateBll<AdminGroupBLL>();
        AdministratorBLL adminBLL = Factory.BusinessFactory.CreateBll<AdministratorBLL>();
        PermissionFunctionBLL pfBLL = Factory.BusinessFactory.CreateBll<PermissionFunctionBLL>();
        PermissionBLL permissionBLL = Factory.BusinessFactory.CreateBll<PermissionBLL>();
        UserManagePageModel model = new UserManagePageModel();
        SqlWhereList where = new SqlWhereList();
        AdminControlModel ControlModel = new AdminControlModel();
        
        /// <summary>
        ///  获取后台的全部功能菜单
        /// </summary>       
        [Action]
        [PageUrl(Url = "/user/menumodule.aspx")]
        public object GetMenueList()
        { 
            where.Add("ParentID", 0); 
            model.AdminMenuList = menuBLL.GetAll(where);
            if (model.AdminMenuList != null)
            {
                foreach (AdminMenuModule m in model.AdminMenuList)
                {
                    SqlWhereList wheretwo = new SqlWhereList();
                    wheretwo.Add("ParentID", m.ModuleId);
                    if (!model.Dic.ContainsKey(m.ModuleId))
                        model.Dic[m.ModuleId] = menuBLL.GetAll(wheretwo);
                }
            }           
            return new PageResult(null,model); 
        }

        /// <summary>
        /// 获取后台头部一级菜单
        /// </summary>
        /// <returns></returns>
        [Action]
        [PageUrl(Url = "/controls/pageheader.aspx")]
        public object GetOneMenuList()
        {
            string ucUrl = "~/controls/pageheader.ascx";
            ControlModel.AdminOneMenuModuleList = permissionBLL.GetPermissionModuleList(UserCookies.AdminId, UserCookies.GroupId, 4, 0, "TopMenu");
            return new UcResult(ucUrl, ControlModel);
        }

        [Action]
        [PageUrl(Url = "/controls/mainmenu.aspx")]
        public object GetTwoMenuList()
        {
            string ucUrl = "~/controls/mainmenu.ascx";
          //  where.Add("ParentID", 0);
          // ControlModel.AdminOneMenuModuleList = menuBLL.GetAll(where);
          ControlModel.AdminOneMenuModuleList = permissionBLL.GetPermissionModuleList(UserCookies.AdminId, UserCookies.GroupId, 4, 0, "TopMenu");
            if (ControlModel.AdminOneMenuModuleList!= null)
            {
                foreach (AdminMenuModule m in ControlModel.AdminOneMenuModuleList)
                {
                    SqlWhereList wheretwo = new SqlWhereList();
                    wheretwo.Add("ParentID", m.ModuleId);
                    if (!ControlModel.TwoMenu.ContainsKey(m.ModuleId))
                     // ControlModel.TwoMenu[m.ModuleId] = menuBLL.GetAll(wheretwo);
                         ControlModel.TwoMenu[m.ModuleId] = permissionBLL.GetPermissionModuleList(UserCookies.AdminId, UserCookies.GroupId, 4, m.ModuleId, "LeftMenu");
                }
            }
            return new UcResult(ucUrl, ControlModel); 
        }


        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <returns></returns>
              
        [Action]
        [PageUrl(Url = "/user/functions.aspx")]
        public object GetFunctionsList()
        {
        
            model.FunctionsList = funcBLL.GetAll();
            return new PageResult(null, model);
        }
        /// <summary>
        /// 获取用户组列表
        /// </summary>
        /// <returns></returns>
        
        [Action]
        [PageUrl(Url = "/user/usergroup.aspx")]
        public object GetAdminGroupList()
        {
            model.AdminGroupList = groupBLL.GetAll();
            return new PageResult(null, model);
        }

        /// <summary>
        /// 操盘直播分析师关联管理用户列表
        /// </summary>
        /// <returns></returns>
        [Action]
        [PageUrl(Url = "/cpzb/analystuser.aspx")]
        public object GetAdminList()
        {
            SqlWhereList swl = new SqlWhereList();
             swl.Add("GroupId", 10);
            //swl.Add("GroupId", 4);
            model.AdministratorList = adminBLL.GetAll(swl);
            return new PageResult(null, model);
        }
        

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [Action]
        [PageUrl(Url = "/user/userlist.aspx")]
        public object GetAdminGroupUserList()
        {
            AuthorityDotBLL authorityDotBLL = Factory.BusinessFactory.CreateBll<AuthorityDotBLL>();
            List<MyAuthorityDot> myAuthorityDotList = new List<MyAuthorityDot>();

            SqlWhereList sw = new SqlWhereList();
            sw.Add("ParentId", 0);
            foreach (var authorityDot in authorityDotBLL.GetAll(sw))
	        {
                MyAuthorityDot myAuthorityDot = new MyAuthorityDot();
                SqlWhereList swl = new SqlWhereList();
                swl.Add("ParentId", authorityDot.Id);

                myAuthorityDot.DotName = authorityDot.Text;
                myAuthorityDot.AuthorityDots = authorityDotBLL.GetAll(swl);
                myAuthorityDotList.Add(myAuthorityDot);
	        }
            model.AuthorityDotList = myAuthorityDotList;

            model.AdminGroupList = groupBLL.GetAll();
            model.AdministratorList = adminBLL.GetAdministratorList();
            return new PageResult(null, model);
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="ptype"></param>
        /// <param name="adminid"></param>
        /// <returns></returns>
        
        [Action]
        [PageUrl(Url = "/user/menupermission.aspx")]
        public object GetPermissionMenueList(string ptype, int adminid)
        {
           
            where.Add("ParentID", 0);
            model.AdminMenuList = menuBLL.GetAll(where);
            model.PermissionType = ptype.ToString();
            if (ptype == "Single")
            {
                model.AdminId = adminid;
            }
            else
            {
                model.GroupId = adminid;
            }

            return new PageResult(null, model);

        }

        /// <summary>
        /// 获取用户组列表
        /// </summary>
        /// <returns></returns>
        [Action]
        [PageUrl(Url = "/user/permission.aspx")]
        public object SetPermission(string ptype, int moduleid,int adminid)
        {
            where.Add("ParentID", moduleid);
            model.AdminMenuList = menuBLL.GetAll(where);
            model.PermissionType = ptype;
            model.ParentId = moduleid;

            if (ptype == "Single")
            {              
                model.AdminId = adminid;
            }
            else
            {
                model.GroupId = adminid;
            }
            model.ModuleName = menuBLL.Get("ModuleId", moduleid.ToString()).ModuleName.ToString();
            model.pfList = pfBLL.GetAdminOrGroupAllFunction(model.AdminId, model.GroupId, ptype);

            return new PageResult(null, model);

        }

        /// <summary>
        /// 获取权限功能列表
        /// </summary>
        /// <param name="ptype"></param>
        /// <param name="moduleid"></param>
        /// <param name="adminid"></param>
        /// <returns></returns>
        [Action]
        [PageUrl(Url = "/user/authoritydot.aspx")]
        public object GetauthorityList()
        {
            AuthoritydotPageModel myModel = new AuthoritydotPageModel();
            AuthorityDotBLL authorityDotBLL = Factory.BusinessFactory.CreateBll<AuthorityDotBLL>();

            SqlWhereList sw = new SqlWhereList();
            sw.Add("ParentId", 0);
            myModel.AuthorityDots = authorityDotBLL.GetAll(sw);
            foreach (var item in myModel.AuthorityDots)
            {
                item.Options = GetOptions(item.Id);
            }
            return new PageResult(null, myModel);
        }

        private string GetOptions(int parentId)
        {
            string res = string.Empty;
            AuthoritydotPageModel myModel = new AuthoritydotPageModel();
            AuthorityDotBLL authorityDotBLL = Factory.BusinessFactory.CreateBll<AuthorityDotBLL>();

            SqlWhereList sw = new SqlWhereList();
            sw.Add("ParentId", parentId);

            List<AuthorityDot> list = authorityDotBLL.GetAll(sw);
            foreach (var item in list)
            {
                res += " , " + item.Text;
            }

            if(string.IsNullOrEmpty(res))
                return "";

            return res.Substring(3);
        }

    }
}
