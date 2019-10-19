using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace SMVC
{
    /// <summary>
    /// 用于访问当前请求上下文的工具类。这个类对测试环境仍然有效。
    /// </summary>
    public static class HttpContextHelper
    {
        public static string AppRootPath
        {
            get
            {
                if (TestEnvironment.IsTestEnvironment)
                    return TestEnvironment.GetValue("AppDomainAppPath") as string;
                return HttpRuntime.AppDomainAppPath;
            }
            set
            {
                TestEnvironment.SetValue("AppDomainAppPath", value);
            }
        }

        public static string RequestFilePath
        {
            get
            {
                if (TestEnvironment.IsTestEnvironment)
                    return TestEnvironment.GetValue("RequestFilePath") as string;
                return HttpContext.Current.Request.FilePath;
            }
            set
            {
                TestEnvironment.SetValue("RequestFilePath", value);
            }
        }

        public static string RequestRawUrl
        {
            get
            {
                if (TestEnvironment.IsTestEnvironment)
                    return TestEnvironment.GetValue("RequestRawUrl") as string;
                return HttpContext.Current.Request.RawUrl;
            }
            set
            {
                TestEnvironment.SetValue("RequestRawUrl", value);
            }
        }

        public static string IdentityName
        {
            get
            {
                if (TestEnvironment.IsTestEnvironment)
                    return TestEnvironment.GetValue("IdentiryName") as string;
                if (!HttpContext.Current.Request.IsAuthenticated)
                    return null;
                return HttpContext.Current.User.Identity.Name;
            }
            set
            {
                TestEnvironment.SetValue("IdentityName", value);
            }
        }
    }
}
