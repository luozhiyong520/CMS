using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SMVC
{
    public static class CookieHelper
    {
        [ThreadStatic]
        private static HttpCookieCollection cookieCollection;

        private static HttpCookieCollection CookieCollection
        {
            get
            {
                if (!TestEnvironment.IsTestEnvironment)
                    throw new InvalidOperationException("只有测试环境才能使用");
                if (cookieCollection == null)
                    cookieCollection = new HttpCookieCollection();
                return cookieCollection;
            }
        }

        public static void SetCookie(string cookieName,string value)
        {
            HttpCookie hc = new HttpCookie(cookieName);
            hc.Value = value;
            if (TestEnvironment.IsTestEnvironment)
                CookieCollection.Set(hc);
            else
                HttpContext.Current.Response.Cookies.Set(hc);
        }

        public static void AddCookie(string cookieName, string value)
        {
            HttpCookie hc = new HttpCookie(cookieName);
            hc.Value = value;
            if (TestEnvironment.IsTestEnvironment)
                CookieCollection.Add(hc);
            else
                HttpContext.Current.Response.Cookies.Add(hc);
        }

        private static HttpCookie GetCookie(string cookieName)
        {
            if (TestEnvironment.IsTestEnvironment)
                return CookieCollection[cookieName];
            return HttpContext.Current.Request.Cookies[cookieName];
        }

        public static string GetCookieValue(string cookieName)
        {
            HttpCookie hc = GetCookie(cookieName);
            if (hc != null)
                return hc.Value;
            return null;
        }

        public static void ClearCookie()
        {
            if (TestEnvironment.IsTestEnvironment)
                CookieCollection.Clear();
            else
                HttpContext.Current.Response.Cookies.Clear();
        }

        public static void RemoveCookie(string cookieName)
        {
            if (TestEnvironment.IsTestEnvironment)
                CookieCollection.Remove(cookieName);
            else
                HttpContext.Current.Response.Cookies.Remove(cookieName);
        }
    }
}
