using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Common
{
    /// <summary>
    /// WEB信息
    /// </summary>
    public class ServerHelper
    {
        /// <summary>
        /// 取得网站的根目录的URL
        /// </summary>
        /// <returns></returns>
        public static string GetRootURI()
        {
            string AppPath = "";
            HttpContext HttpCurrent = HttpContext.Current;
            HttpRequest Req;
            if (HttpCurrent != null)
            {
                Req = HttpCurrent.Request;

                string UrlAuthority = Req.Url.GetLeftPart(UriPartial.Authority);
                if (Req.ApplicationPath == null || Req.ApplicationPath == "/")
                    //直接安装在   Web   站点   
                    AppPath = UrlAuthority;
                else
                    //安装在虚拟子目录下   
                    AppPath = UrlAuthority + Req.ApplicationPath;
            }
            return AppPath;
        }
        /// <summary>
        /// 取得网站的根目录的URL
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        public static string GetRootURI(HttpRequest Req)
        {
            string AppPath = "";
            if (Req != null)
            {
                string UrlAuthority = Req.Url.GetLeftPart(UriPartial.Authority);
                if (Req.ApplicationPath == null || Req.ApplicationPath == "/")
                    //直接安装在   Web   站点   
                    AppPath = UrlAuthority;
                else
                    //安装在虚拟子目录下   
                    AppPath = UrlAuthority + Req.ApplicationPath;
            }
            return AppPath;
        }
        /// <summary>
        /// 取得网站根目录的物理路径
        /// </summary>
        /// <returns></returns>
        public static string GetRootPath()
        {
            string AppPath = "";
            try
            {
                HttpContext HttpCurrent = HttpContext.Current;
                if (HttpCurrent != null)
                {
                    AppPath = HttpCurrent.Server.MapPath("~");
                }
                else
                {
                    AppPath = AppDomain.CurrentDomain.BaseDirectory;
                    if (Regex.Match(AppPath, @"\\$", RegexOptions.Compiled).Success)
                        AppPath = AppPath.Substring(0, AppPath.Length - 1);
                }
            }
            catch (Exception) { }
            return AppPath;
        }
        /// <summary>
        /// 获取文件的完整地址
        /// </summary>
        /// <param name="path">相对路径地址</param>
        /// <returns></returns>
        public static string GetPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }

        /// <summary>
        /// 取得网站域名的根目录(绝对路径及相对路径)
        /// </summary>
        /// <returns></returns>
        public static string getUrl()
        {
            string flg = "";
            string dirdumm = "/";

            string sitedomain = System.Configuration.ConfigurationManager.AppSettings["NewsImgPath"];
            if (sitedomain.IndexOf("http://") > -1) { flg = sitedomain; }
            else
            {
                flg = "http://" + sitedomain;
                if (ServerPort != "80")
                {
                    flg += ":" + ServerPort;
                }
                flg += dirdumm;
            }
            return flg;
        }

        /// <summary>
        /// 端口
        /// </summary>
        public static string ServerPort
        {
            get
            {
                string paths = null;
                if (HttpContext.Current == null)
                {
                    paths = System.Threading.Thread.CurrentThread.Name;
                }
                else
                {
                    paths = HttpContext.Current.Request.ServerVariables["Server_Port"].ToString();
                }
                return paths;
            }
        }
    }
}
