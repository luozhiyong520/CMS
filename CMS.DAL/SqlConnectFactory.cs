using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Text.RegularExpressions;


namespace CMS.DAL
{
    public class SqlConnectFactory
    {
        private static string defaultConnName;

        static SqlConnectFactory()
        {
            defaultConnName = ConfigurationManager.AppSettings["defaultDatabase"];
        }


        public static string DefaultConnecting
        {
            get
            {
                string defaultConn = ConfigurationManager.ConnectionStrings[defaultConnName].ConnectionString;
                if (string.IsNullOrEmpty(defaultConn))
                    throw new ArgumentNullException("defaultConn");
                return defaultConn;
            }

        }

        public static string CMS = ConfigurationManager.ConnectionStrings["CMS"].ConnectionString;
        public static string BOCE = ConfigurationManager.ConnectionStrings["BOCE"].ConnectionString;
        public static string BaiduPush = ConfigurationManager.ConnectionStrings["BaiduPush"].ConnectionString;
        public static string CODE = ConfigurationManager.ConnectionStrings["CODE"].ConnectionString;

        //private static Hashtable connHash
        //{
        //    get
        //    {
        //        string ConnCacheKey = "ConnCacheKey";
        //        Hashtable hash = null;
        //        if ((hash = (Hashtable)HttpRuntime.Cache[ConnCacheKey]) == null)
        //        {
        //            hash = new Hashtable();
        //            int len = EnDecrypt.Encrypt.DefaultStr.Length;
        //            RegistryKey software = Registry.LocalMachine.OpenSubKey(EnDecrypt.Encrypt.Location);
        //            EnDecrypt.UrlsCollection conns = EnDecrypt.Config.GetConfig().Urls;
        //            for (int i = 0; i < conns.Count; i++)
        //            {
        //                string key = conns[i].Name;
        //                object value = software.GetValue(key);
        //                if (value != null)
        //                {
        //                    string decryptValue = EnDecrypt.Decrypt.Decrypt3DES(value.ToString(), EnDecrypt.Encrypt.Key);
        //                    string[] strs = Regex.Split(decryptValue, Regex.Escape(EnDecrypt.Encrypt.SplitStr));
        //                    string connStr = string.Format("server={0};database={1};user id={2};password={3};", strs[0], key, strs[1].Substring(0, strs[1].Length - len), strs[2].Substring(0, strs[2].Length - len));
        //                    hash.Add(key, connStr);
        //                }
        //            }
        //            System.Web.Caching.CacheDependency cd = new System.Web.Caching.CacheDependency(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Web.config");
        //            HttpRuntime.Cache.Insert(ConnCacheKey, hash, cd);
        //        }
        //        else
        //        {
        //            hash = (Hashtable)HttpRuntime.Cache[ConnCacheKey];
        //        }
        //        return hash;
        //    }
        //}
    }
}
