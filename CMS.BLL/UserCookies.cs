using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Common;

namespace CMS.BLL
{
    public class UserCookies
    {
        /// <summary>
        /// 用户是否登录
        /// </summary>
        public static bool IsLogin
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        /// <summary>
        /// 管理员Id
        /// </summary>
        public static int AdminId
        {
            get
            {
                FormsPrincipal<CookiesModel> ck = HttpContext.Current.User as FormsPrincipal<CookiesModel>;
                if (ck != null)
                    return ck.UserData.AdminId;
                return 0;
            }
        }

        /// <summary>
        /// 管理员用户名
        /// </summary>
        public static string AdminName
        {
            get
            {
                FormsPrincipal<CookiesModel> ck = HttpContext.Current.User as FormsPrincipal<CookiesModel>;
                if(ck != null)
                    return ck.UserData.AdminName;
                return string.Empty;
            }
        }

        /// <summary>
        /// 管理员所属组Id
        /// </summary>
        public static int GroupId
        {
            get
            {
                FormsPrincipal<CookiesModel> ck = HttpContext.Current.User as FormsPrincipal<CookiesModel>;
                if (ck != null)
                    return ck.UserData.GroupId;
                return 0;
            }
        }
    }

    public class CookiesModel
    {
        /// <summary>
        /// 管理员Id
        /// </summary>
        public int AdminId { get; set; }

        /// <summary>
        /// 管理员用户名
        /// </summary>
        public string AdminName { get; set; }

        /// <summary>
        /// 管理员所属组Id
        /// </summary>
        public int GroupId { get; set; }
    }
}
