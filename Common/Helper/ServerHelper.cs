using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Common
{
    /// <summary>
    /// WEB��Ϣ
    /// </summary>
    public class ServerHelper
    {
        /// <summary>
        /// ȡ����վ�ĸ�Ŀ¼��URL
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
                    //ֱ�Ӱ�װ��   Web   վ��   
                    AppPath = UrlAuthority;
                else
                    //��װ��������Ŀ¼��   
                    AppPath = UrlAuthority + Req.ApplicationPath;
            }
            return AppPath;
        }
        /// <summary>
        /// ȡ����վ�ĸ�Ŀ¼��URL
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
                    //ֱ�Ӱ�װ��   Web   վ��   
                    AppPath = UrlAuthority;
                else
                    //��װ��������Ŀ¼��   
                    AppPath = UrlAuthority + Req.ApplicationPath;
            }
            return AppPath;
        }
        /// <summary>
        /// ȡ����վ��Ŀ¼������·��
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
        /// ��ȡ�ļ���������ַ
        /// </summary>
        /// <param name="path">���·����ַ</param>
        /// <returns></returns>
        public static string GetPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }

        /// <summary>
        /// ȡ����վ�����ĸ�Ŀ¼(����·�������·��)
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
        /// �˿�
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
