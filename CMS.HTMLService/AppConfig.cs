using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace CMS.HtmlService
{
    public class AppConfig
    {
        /// <summary>
        /// html模板存放的文件夹
        /// </summary>
        public static string TemplateBasePath = ConfigurationManager.AppSettings["TemplageBasePath"].ToString();

        /// <summary>
        /// html生成存放文件夹
        /// </summary>
        public static string HtmlSaveBasePath = ConfigurationManager.AppSettings["HtmlSaveBasePath"].ToString();

        /// <summary>
        /// 上传文件存放文件夹
        /// </summary>
        public static string FileSaveBasePath = ConfigurationManager.AppSettings["FileSaveBasePath"].ToString();

        /// <summary>
        /// ftpip
        /// </summary>
        public static string FtpServerIP = ConfigurationManager.AppSettings["FtpServerIP"].ToString();

        /// <summary>
        /// ftp登陆名
        /// </summary>
        public static string FtpUserId = ConfigurationManager.AppSettings["FtpUserId"].ToString();

        /// <summary>
        /// ftp登陆密码
        /// </summary>
        public static string FtpPassword = ConfigurationManager.AppSettings["FtpPassword"].ToString();

        /// <summary>
        /// ftp上html的基础路径
        /// </summary>
        public static string FtpHtmlBasePath = ConfigurationManager.AppSettings["FtpHtmlBasePath"].ToString();

        /// <summary>
        /// ftp上file的基础路径
        /// </summary>
        public static string FtpFileBasePath = ConfigurationManager.AppSettings["FtpFileBasePath"].ToString();

        /// <summary>
        /// 是否需要ftp上传
        /// </summary>
        public static bool FtpUpload
        {
            get
            {
                bool flag = false;
                if (bool.TryParse(ConfigurationManager.AppSettings["FtpUpload"].ToString(), out flag))
                    return flag;
                else
                    return false;
            }
        }
    }
}
