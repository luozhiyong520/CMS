using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.BLL;
using SMVC;
using Common;
using System.Web;
using CMS.Model;
using CMS.ViewModel;
using System.Configuration;
using System.Xml;

namespace CMS.Controller
{
    [Authorize]
    public class AjaxUserManage
    {
        AdminMenuModule MenuModule = new AdminMenuModule();
        AdminMenuModuleBLL MenuModuleBLL = Factory.BusinessFactory.CreateBll<AdminMenuModuleBLL>();
        SqlWhereList where = new SqlWhereList();
        Functions Functions = new Functions();
        FunctionsBLL FunctionsBLL = Factory.BusinessFactory.CreateBll<FunctionsBLL>();
        AdminGroup Group = new AdminGroup();
        AdminGroupBLL GroupBLL = Factory.BusinessFactory.CreateBll<AdminGroupBLL>();
        AdministratorBLL AdminBLL = Factory.BusinessFactory.CreateBll<AdministratorBLL>();
        Administrator Administrator = new Administrator();
        PermissionBLL Permission = Factory.BusinessFactory.CreateBll<PermissionBLL>();
        UserManagePageModel model = new UserManagePageModel();
        NewsBLL newsBll = Factory.BusinessFactory.CreateBll<NewsBLL>();
        /// <summary>
        /// 添加一级菜单
        /// </summary>
        /// <param name="modulename"></param>
        /// <returns></returns>
        [Action]
        public string AddMenuModule(string modulename)
        {
            if (modulename.Length < 1)
            {
                return "000000";  //验证一级菜单名不能为空;
            }
 
           
            MenuModule.ModuleName = modulename;
            MenuModule.ParentId = 0;
            MenuModule.TargetUrl = "";
            MenuModule.Status = 1;
            return MenuModuleBLL.Add(MenuModule).ToString();
           
        }
        
      
       
        /// <summary>
        /// 编辑获取一级菜单名
        /// </summary>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        //[Action]
        //public object GetByModuleId(int moduleid)
        //{
        //    return new JsonResult(MenuModuleBLL.Get("ModuleId",moduleid.ToString()));
        //}


        /// <summary>
        /// 更新一级菜单 
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="modulename"></param>
        /// <returns></returns>
        [Action]
        public string UpdateMenuModule(int moduleid, string modulename)
        {
            if (modulename.Length < 1)
            {
                return "000000";  //验证一级菜单名不能为空;
            }
            MenuModule.ModuleName = modulename;
            MenuModule.ModuleId = moduleid;
            MenuModule.ParentId = 0;
            MenuModule.TargetUrl = "";
            MenuModule.Status = 1;
            return MenuModuleBLL.Update(MenuModule).ToString();

        }

        /// <summary>
        /// 删除一级菜单
        /// </summary>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        [Action]
        public string DelOneModule(int moduleid)
        {
            where.Add("ParentId", moduleid);
            int result = MenuModuleBLL.GetAll(where).Count;
            if (result != 0)
            {
                return "000000";  //子记录不能为空，不能删除
            }
            MenuModule.ModuleId = moduleid;
            return MenuModuleBLL.Delete(MenuModule).ToString();
        }

        

        /// <summary>
        /// 添加二级菜单
        /// </summary>
        /// <param name="moduletwoname"></param>
        /// <param name="targeturl"></param>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        [Action]
        public string AddTwoMenuModule(string moduletwoname, string targeturl, int moduleid)
        {
            if (moduletwoname.Length < 1)
            {
                return "000000";  //验证二级菜单名不能为空;
            }
            MenuModule.ModuleName = moduletwoname;
            MenuModule.ParentId = Convert.ToInt32(moduleid);
            MenuModule.TargetUrl = targeturl;
            MenuModule.Status = 1;
            return MenuModuleBLL.Add(MenuModule).ToString();

        }

        /// <summary>
        /// 编辑获取二级菜单名
        /// </summary>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        [Action]
        public object GetByTwoModuleId(int moduleid)
        {
            return new JsonResult(MenuModuleBLL.Get("ModuleId", moduleid.ToString()));
        }

        /// <summary>
        /// 更新二级菜单
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="parentid"></param>
        /// <param name="modulename"></param>
        /// <param name="targeturl"></param>
        /// <returns></returns>
        [Action]
        public string UpdateTwoMenuModule(int moduleid,int parentid, string modulename, string targeturl)
        {
            if (modulename.Length < 1)
            {
                return "000000";  //验证一级菜单名不能为空;
            }
            MenuModule.ModuleName = modulename;
            MenuModule.ModuleId = moduleid;
            MenuModule.ParentId = parentid;
            MenuModule.TargetUrl = targeturl;
            MenuModule.Status = 1;
            return MenuModuleBLL.Update(MenuModule).ToString();

        }

        /// <summary>
        /// 删除二级菜单
        /// </summary>
        /// <param name="moduleid"></param>
        /// <returns></returns>
        [Action]
        public string DelTwoModule(int moduleid)
        {
            MenuModule.ModuleId = moduleid;
            return MenuModuleBLL.Delete(MenuModule).ToString();
        }

         /// <summary>
         /// 添加功能名
         /// </summary>
         /// <param name="functionname"></param>
         /// <returns></returns>
        [Action]
        public string AddFunction(string functionname)
        {
            if (functionname.Length < 1)
            {
                return "000000";  //验证功能名不能为空;
            }
            Functions.FunctionName = functionname;
            return FunctionsBLL.Add(Functions).ToString();

        }

        /// <summary>
        /// 添加用户组名
        /// </summary>
        /// <param name="groupname"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        [Action]
        public string AddGroup(string groupname,string remark)
        {
            if (groupname.Length < 1)
            {
                return "000000";  //用户组名不能为空;
            }
            Group.GroupName = HttpUtility.UrlDecode(groupname);
            Group.Remark = HttpUtility.UrlDecode(remark);
            return GroupBLL.Add(Group).ToString();

        }


        /// <summary>
        /// 编辑获取用户组名
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        //[Action]
        //public object GetByAdminGroupId(int groupid)
        //{
        //    return new JsonResult(GroupBLL.Get("GroupId", groupid.ToString()));
        //}

        /// <summary>
        /// 更新用户组名
        /// </summary>
        /// <param name="groupid"></param>
        /// <param name="groupname"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        [Action]
        public string UpdateAdminGroup(int groupid, string groupname, string remark)
        {
            if (groupname.Length < 1)
            {
                return "000000";  //验证用户组名不能为空;
            }
            Group.GroupId = groupid;
            Group.GroupName = groupname;
            Group.Remark = remark;
            return GroupBLL.Update(Group).ToString();

        }
        [Action]
        public string DelGroup(int groupid)
        {
            Group.GroupId = groupid;
            return GroupBLL.Delete(Group).ToString();
        }


        /// <summary>
        /// 编辑获取管理用户名
        /// </summary>
        /// <param name="adminid"></param>
        /// <returns></returns>
        //[Action]
        //public object GetByAdminId(int adminid)
        //{
        //    return new JsonResult(AdminBLL.Get("AdminId", adminid.ToString()));
        //}

        /// <summary>
        /// 编辑管理用户
        /// </summary>
        /// <param name="adminname"></param>
        /// <param name="password"></param>
        /// <param name="adminid"></param>
        /// <param name="groupid"></param>
        /// <returns></returns>
        [Action]
        public string UpdateAdministrator(string adminname, string password, int adminid, int groupid)
        {
            if (adminname.Length < 1)
            {
                return "000000"; //用户名不能为空
            }
            Administrator = AdminBLL.Get("AdminId",adminid);           
            Administrator.AdminName = adminname;
            if (password != "null")
            {
                Administrator.Password = EncryptHelper.MD5(password);
            }
            Administrator.GroupId = groupid;      
            Administrator.LastLoginDate = DateTime.Now;
            return AdminBLL.Update(Administrator).ToString();
        }

       /// <summary>
       /// 修改用户密码
       /// </summary>
       /// <param name="oldpassword"></param>
       /// <param name="password"></param>
       /// <param name="repassword"></param>
       /// <param name="adminid"></param>
       /// <returns></returns>
        [Action]
        public string UpdatePassWord(string oldpassword, string password, string repassword, int adminid)
        {
            Administrator = AdminBLL.Get("AdminId", adminid);
            if (Administrator.Password != EncryptHelper.MD5(oldpassword))
            {
                return "000001";   //原始密码输入有误
            }
            else if (repassword != password)
            {
                return "000002";
            }
            else
            {
                Administrator.Password = EncryptHelper.MD5(password);
            }
            Administrator.LastLoginDate = DateTime.Now;
            return AdminBLL.Update(Administrator).ToString();
        }

       /// <summary>
       /// 添加管理用户
       /// </summary>
       /// <param name="adminname"></param>
       /// <param name="password"></param>
       /// <param name="groupid"></param>
       /// <returns></returns>
        [Action]
        public string AddAdministrator(string adminname, string password, int groupid)
        {
            if (adminname.Length < 1)
            {
                return "000000";  //验证用户名不能为空;
            }
            if (password.Length < 1)
            {
                return "000001";  //验证密码名不能为空;
            }
            Administrator.AdminName = HttpUtility.UrlDecode(adminname);
            Administrator.Password = EncryptHelper.MD5(password);
            Administrator.GroupId = groupid;
            Administrator.Status = 1;
            Administrator.LastLoginDate = DateTime.Now;
            return AdminBLL.Add(Administrator).ToString();

        }
        /// <summary>
        /// 删除管理用户
        /// </summary>
        /// <param name="adminid"></param>
        /// <returns></returns>
        [Action]
        public string DelAdministrator(int adminid)
        {
            Administrator.AdminId = adminid;
            return AdminBLL.Delete(Administrator).ToString();
        }

        /// <summary>
        /// 设置权限
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Action]
        public string UpdatePermission(string key)
        {
            string[] array;
            array = key.Split('&');
            string[] arrkey;
            Dictionary<string, object> keyvalue = new Dictionary<string, object>();  
            foreach (string i in array)
            {
                arrkey = i.ToString().Split('=');
                keyvalue.Add(arrkey[0],arrkey[1]);
            }
            if (!keyvalue.ContainsKey("parentid"))
            {
                return "000001";  //模块父ID为空，返回失败。
            }
            
            where.Add("ParentId", keyvalue["parentid"]);
            List<AdminMenuModule> MenuList= MenuModuleBLL.GetAll(where);
            foreach (AdminMenuModule Module in MenuList)
            {
                string add = string.Format("add{0}", Module.ModuleId);
                string edit = string.Format("edit{0}", Module.ModuleId);
                string del = string.Format("del{0}", Module.ModuleId);
                string look = string.Format("look{0}", Module.ModuleId);
                int adminid = 0;
                int groupid = 0;
                string permissionType="";
               
                if (keyvalue.ContainsKey("adminid"))
                {
                    adminid = Int32.Parse(keyvalue["adminid"].ToString());
                }
                
                if (keyvalue.ContainsKey("groupid"))
                {
                    groupid = Int32.Parse(keyvalue["groupid"].ToString());
                }
                
                int menuid = Int32.Parse(Module.ModuleId.ToString());

                if (keyvalue.ContainsKey("m_type"))
                {
                    permissionType = keyvalue["m_type"].ToString();
                }

                Permission.ClearPermission(adminid, groupid, menuid, permissionType);

                if (keyvalue.ContainsKey(add))
                {
                    int addfunc = Int32.Parse(keyvalue[add].ToString());
                    Permission.CreatePermissionAndFunction(adminid, groupid, menuid, permissionType, addfunc);
                }
                if (keyvalue.ContainsKey(edit))
                {
                    int editfunc = Int32.Parse(keyvalue[edit].ToString());
                    Permission.CreatePermissionAndFunction(adminid, groupid, menuid, permissionType, editfunc);
                }
                if (keyvalue.ContainsKey(del))
                {
                    int delfunc = Int32.Parse(keyvalue[del].ToString());
                    Permission.CreatePermissionAndFunction(adminid, groupid, menuid, permissionType, delfunc);
                }
                if (keyvalue.ContainsKey(look))
                {
                    int lookfunc = Int32.Parse(keyvalue[look].ToString());
                    Permission.CreatePermissionAndFunction(adminid, groupid, menuid, permissionType, lookfunc);
                }
                  
            
            }
            return "000000";
        }

        //获取用户列表
        [Action]
        public JsonResult Getadminstor()
        {
            int adminId = 0;
            var Administrator = newsBll.GetAdministratorList(adminId);
            if (Administrator == null)
            {
                return null;
            }
            else
            {
                return new JsonResult(Administrator);
            }
        }

        [Action]
        public string updatepass(string pass)
        {

            UpdateAppSetting("PassWord", pass);
            return "000000";
        }

        /// <summary>  
        /// 修改config文件(AppSetting节点)  
        /// </summary>  
        /// <param name="key">键</param>  
        /// <param name="value">要修改成的值</param>  
        public static void UpdateAppSetting(string key, string value)
        {
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径   
            string strFileName = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Web.config";
            doc.Load(strFileName);
            //找出名称为“add”的所有元素   
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性   
                XmlAttribute _key = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素   
                if (_key != null)
                {
                    if (_key.Value == key)
                    {
                        //对目标元素中的第二个属性赋值   
                        _key = nodes[i].Attributes["value"];

                        _key.Value = value;
                        break;
                    }
                }
            }
            //保存上面的修改   
            doc.Save(strFileName);
        }

    }
}
