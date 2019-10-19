using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Common;

namespace CMS.Controller
{
    public class PermissionModule : IHttpModule
    {
        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
            context.AuthorizeRequest += new EventHandler(context_AuthorizeRequest);
        }

        private void context_BeginRequest(object sender,EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            if (app.Context.Request["ModuleId"] != null)
            {
                string mid = app.Context.Request["ModuleId"].ToString();
                if (ValidateHelper.IsNumber(mid))
                {
                    string cookieName = "ModuleId";
                    HttpCookie ck = new HttpCookie(cookieName);
                    ck.Value = mid;
                    app.Context.Response.Cookies.Remove(cookieName);
                    app.Context.Response.Cookies.Add(ck);
                }
            }
        }

        private void context_AuthorizeRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            string filePath = app.Context.Request.FilePath;
            if (!filePath.EndsWith("getverifyimage.aspx", true, null) && !filePath.EndsWith("login.cspx", true, null) && !filePath.EndsWith("index.aspx", true, null) && !filePath.EndsWith("pageheader.aspx", true, null) && !filePath.EndsWith("mainmenu.aspx",true,null))
            {
                bool endWithAspx = filePath.EndsWith(".aspx", true, null);
                bool endWithdCspx = filePath.EndsWith(".cspx", true, null);
                HttpCookie ck = app.Context.Request.Cookies["ModuleId"];
                if (ck == null && (endWithAspx || endWithdCspx))
                {
                    app.Context.Response.Write("你没有权限操作!");
                    app.Context.Response.End();
                }
            }
        }
    }
}
