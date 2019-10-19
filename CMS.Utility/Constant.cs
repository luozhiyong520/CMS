using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;


namespace CMS.Utility
{
    public class Constant
    {
        /// <summary>
        /// 新闻详细页的域名
        /// </summary>
        public static string NEWS_DETAIL_HOST = ConfigurationManager.AppSettings["NewsDetailHost"];
    }
}
