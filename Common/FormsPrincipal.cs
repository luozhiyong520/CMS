using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using System.Web.Script.Serialization;

namespace Common
{
    public class FormsPrincipal<TUserData> : IPrincipal where TUserData : class, new()
    {
        private IIdentity identity;
        private TUserData userData;

        public FormsPrincipal(FormsAuthenticationTicket ticket, TUserData userData)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");
            if (userData == null)
                throw new ArgumentNullException("userData");
            this.userData = userData;
            this.identity = new FormsIdentity(ticket);
        }

        //禁止别的类new()实例化
        protected FormsPrincipal()
        {
        }

        public IIdentity Identity
        {
            get { return identity; }
        }

        public TUserData UserData
        {
            get { return userData; }
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

        public static void SignIn(string username, TUserData userData, int expires)
        {
            if (userData == null)
                throw new ArgumentNullException("userData");
            string serializeStr = new JavaScriptSerializer().Serialize(userData);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, username, DateTime.Now, DateTime.Now.AddMinutes(120), true, serializeStr);
            HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            ck.Path = FormsAuthentication.FormsCookiePath;
            ck.Secure = FormsAuthentication.RequireSSL;
            ck.Domain = FormsAuthentication.CookieDomain;
            if (expires > 0)
                ck.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            HttpContext.Current.Response.Cookies.Add(ck);
        }

        public static void SetFomrsIdentityUserData()
        {
            try
            {
                HttpCookie ck = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (ck == null || string.IsNullOrEmpty(ck.Value))
                    return;
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(ck.Value);
                if (ticket == null || string.IsNullOrEmpty(ticket.UserData))
                    return;
                TUserData userData = new JavaScriptSerializer().Deserialize<TUserData>(ticket.UserData);
                if (userData == null)
                    return;
                HttpContext.Current.User = new FormsPrincipal<TUserData>(ticket, userData);
            }
            catch
            {
                Loger.Error("设置表单用户身份时错误");
            }
        }
    }
}
