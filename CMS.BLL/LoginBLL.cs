using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;
using CMS.DAL;
using Common;
using System.Web;

namespace CMS.BLL
{
    public class LoginBLL
    {
        AdministratorBLL adminBLL = Factory.BusinessFactory.CreateBll<AdministratorBLL>();

        public bool Login(string adminName, string password)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("AdminName", adminName);
            dic.Add("Password", password);
            dic.Add("Status", (1).ToString());
            Administrator admin = adminBLL.Get(dic);
            if (admin == null)
            {
                return false;
            }
            else
            {
                //设置cookie信息
                CookiesModel ck = new CookiesModel
                {
                    AdminId = admin.AdminId,
                    AdminName = admin.AdminName,
                    GroupId = admin.GroupId
                };
                FormsPrincipal<CookiesModel>.SignIn(ck.AdminName, ck, 0);
                return true;
            }
        }
    }
}
