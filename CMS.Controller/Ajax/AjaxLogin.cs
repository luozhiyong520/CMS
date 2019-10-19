using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.BLL;
using SMVC;
using Common;
using System.Web;
using System.Web.Security;

namespace CMS.Controller
{
    public class AjaxLogin 
    {
        [Action]
        [SessionMode(SessionModeStatus = SessionStatus.ReadOnly)]
        public string Login(string loginname, string loginpassword, string strValidityCode)
        {
            if (string.IsNullOrEmpty(loginname) || loginname.Length > 10 || loginname.Length < 1)
            {
                return "000001";
            }
            else if (string.IsNullOrEmpty(loginpassword) || loginpassword.Length > 32 || loginpassword.Length < 1)
            {
                return "000002";
            }
            else if (HttpContext.Current.Session["strValidityCode"] == null)
            {
                return "000000";
            }
            string sessionstrValidityCode = HttpContext.Current.Session["strValidityCode"].ToString();
            if (sessionstrValidityCode.ToLower() == strValidityCode.ToString().ToLower())
            {
                LoginBLL adminLogin = Factory.BusinessFactory.CreateBll<LoginBLL>();
                string result = adminLogin.Login(loginname, loginpassword).ToString();              
                return result;
            }
            else
            {
                return "000000";
            }
        }


        /// <summary>
        /// 注销操作
        /// </summary>
        /// <returns></returns>
        [Action]
        public void Logout()
        {
            FormsAuthentication.SignOut();
           
        }
    }
}
