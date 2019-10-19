using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;

namespace Common
{
    public enum Method
    {
        post,
        get
    }

    public class RequestHelper
    {

        public static string WebRequest(string url, string method, string requestData, string charset)
        {
            return WebRequest(url, method, requestData, charset,false);
        }

        /// <summary>
        /// http请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="requestData"></param>
        /// <param name="charset"></param>
        /// <param name="flag">是否要记录日志标志</param>
        /// <returns>远程url返回内容</returns>
        public static string WebRequest(string url, string method, string requestData, string charset, bool flag)
        {
            Method eMethod = (Method) Enum.Parse(typeof (Method), method.ToLower());
            return WebRequest(url, eMethod, requestData, charset, flag);
        }

        

        /// <summary>
        /// http请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="requestData"></param>
        /// <param name="charset"></param>
        /// <param name="flag">是否要记录日志标志</param>
        /// <returns>远程url返回内容</returns>
        private static string WebRequest(string url, Method method, string requestData, string charset,bool flag)
        {
            string strResult = "";
            try
            {
                switch (method)
                {
                    case Method.post:
                        strResult = RequestWithPost(url, requestData, charset);
                     
                        break;
                    case Method.get:
                        strResult = RequestWithGet(url, requestData, charset);
                        break;
                }
                if (flag)
                    Loger.Info(string.Format("WebRequest Url:{0};RequestData:{1}",url,requestData));

            }
            catch (Exception exp)
            {
                Loger.Error(exp, "WebRequest Error Url：" + url);
            }
            return strResult;
        }

        /// <summary>
        /// post 方式提交请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="requestData"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        private static string RequestWithPost(string url, string requestData, string charset)
        {
            Encoding code = Encoding.GetEncoding(charset);
            //把数组转换成流中所需字节数组类型
            byte[] bytesRequestData = code.GetBytes(requestData);
            string strResult = "";
            //设置HttpWebRequest基本信息
            HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(url);
            myReq.Method = "post";
            myReq.ContentType = "application/x-www-form-urlencoded";

            //填充POST数据
            //if (bytesRequestData.Length > 0)//防止提交空数据
            //{
            myReq.ContentLength = bytesRequestData.Length;
            using (Stream requestStream = myReq.GetRequestStream())
            {
                requestStream.Write(bytesRequestData, 0, bytesRequestData.Length);
            }
            //}

            //发送POST数据请求服务器
            using (HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse())
            {
                using (Stream myStream = HttpWResp.GetResponseStream())
                {
                    //获取服务器返回信息
                    StreamReader reader = new StreamReader(myStream, code);
                    strResult = reader.ReadToEnd();
                }
            }
            
            return strResult;
        }

        /// <summary>
        /// get方式提交请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="requestData"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        private static string RequestWithGet(string url, string requestData, string charset)
        {
            Encoding code = Encoding.GetEncoding(charset);
            string address = url + "?" + requestData;
            HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(address);
            myReq.Method = "GET";
            string strResult = string.Empty;
            using (HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse())
            {
                using (Stream myStream = HttpWResp.GetResponseStream())
                {
                    //获取服务器返回信息
                    StreamReader reader = new StreamReader(myStream, code);
                    
                    strResult = reader.ReadToEnd();
                    
                }
            }
            return strResult;
        }

         /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间
        /// </summary>
        /// <param name="d">double 型数字</param>
        /// <returns>DateTime</returns>
        public static System.DateTime ConvertIntDateTime(double d)
        {
            System.DateTime time = System.DateTime.MinValue;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            time = startTime.AddSeconds(d);
            return time;
        }

        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>double</returns>
        public static double ConvertDateTimeInt(System.DateTime time)
        {
            double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (time - startTime).TotalSeconds;
            return intResult;
        }

        public static void log4in(string str)
        {
            Loger.Info(str);
        }

    }
}
