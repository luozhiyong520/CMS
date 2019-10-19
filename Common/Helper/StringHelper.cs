using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Collections;

namespace Common
{
    public static class StringHelper
    {

        /// <summary>
        /// 获取url解码的字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetUrlDecode(this string str)
        {
            return HttpUtility.UrlDecode(str);
        }

        /// <summary>
        /// 截取字符,无全角和半角之分
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string LimitString(string str, int len)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            if (str.Length > len)
                return str.Substring(0, len) + "...";
            else
                return str;

        }


        /// <summary>
        /// 截取过长字符串
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <param name="len">最大长度，一个中文的长度为2</param>
        /// <param name="appendStr">截断后，附加的省略字符</param>
        /// <returns>返回截取后的字符串</returns>
        public static string StringLeft(string inputStr, int len, string appendStr)
        {
            if (inputStr == null) return "";
            inputStr = inputStr.Trim();
            Byte[] array = System.Text.Encoding.Unicode.GetBytes(inputStr);
            if (array.Length > len)
            {
                int leftLen = len - appendStr.Length;
                int countLen = 0;
                int charCount = 0;
                int charCount2 = 0;
                for (int i = 0; i < array.Length; i++)
                {
                    i++;
                    if (array[i] == 0)
                    {
                        countLen++;
                    }
                    else
                    {
                        countLen += 2;
                    }
                    if (countLen <= leftLen)
                    {
                        charCount++;
                    }
                    if (countLen > len)
                    {
                        break;
                    }
                    charCount2++;
                }
                if (charCount2 < inputStr.Length)
                {
                    return inputStr.Substring(0, charCount) + appendStr;
                }
                else
                {
                    return inputStr;
                }
            }
            else
            {
                return inputStr;
            }
        }

        /// <summary>
        /// 截取过长字符串
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <param name="len">最大长度，一个中文的长度为2</param>
        /// <returns>返回截取后的字符串</returns>
        public static string StringLeft(string inputStr, int len)
        {
            return StringLeft(inputStr, len, "...");
        }

        /// <summary>
        /// 替换掉sql元符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ReplaceSQLMetaChar(string input)
        {
            string str = String.IsNullOrEmpty(input) ? "" : input.ToLower();
            if (string.IsNullOrEmpty(input))
                return "";
            else if (str.Contains("'")
                || str.Contains("0x")
                || str.Contains("%27")
                || str.Contains(";")
                || str.Contains("%3b")
                || str.Contains("--")
                || str.Contains("%2d%2d")
                || str.Contains("exec")
                || str.Contains("insert")
                || str.Contains("select")
                || str.Contains("delete")
                || str.Contains("update")
                || str.Contains("count")
                || str.Contains("truncate")
                || str.Contains("/add")
                || str.Contains("drop table")
                || str.Contains("master.dbo.xp_cmdshell")
                || str.Contains("net localgroup administrators"))
            {
                Loger.Warn("注入拦截" + HttpContext.Current.Request.Url.PathAndQuery);
                return input.Replace("'", "")
                    .Replace("0x", "")
                    .Replace("%27", "")
                    .Replace(";", "")
                    .Replace("%3b", "")
                    .Replace("--", "")
                    .Replace("%2d%2d", "")
                    .Replace("exec", "")
                    .Replace("insert", "")
                    .Replace("select", "")
                    .Replace("delete", "")
                    .Replace("update", "")
                    .Replace("count", "")
                    .Replace("truncate", "")
                    .Replace("/add", "")
                    .Replace("drop table", "")
                    .Replace("master.dbo.xp_cmdshell", "")
                    .Replace("net localgroup administrators", "");

            }
            else
            {
                return input;
            }
        }
        /// <summary>
        /// 检查内容中是否存在敏感字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool CheckStringMetaChar(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            string MetaStr = "'|0x|--|;|exec|insert|select|delete|update|count|truncate|drop table|master.dbo.xp_cmdshell|net localgroup administrators";
            string[] Metas = MetaStr.Split('|');
            foreach (string meta in Metas)
            {
                if (input.ToLower().IndexOf(meta) > -1)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 去除所有的html格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string WipeHtml(string str)
        {
            string _content = str.ToLower();
            int s1 = _content.IndexOf("<style>");
            int s2 = _content.IndexOf("</style>");

            if (s2 > 0)
            {
                try
                {
                    str = _content.Remove(s1, s2 + 8);
                }
                catch (Exception ex)
                {

                    Loger.Error(ex.Message);
                }
            }
            string pattern = @"<[\s\S]*?>";
            str = Regex.Replace(str, pattern, "", RegexOptions.IgnoreCase);
            return str.Replace("&nbsp;", "");
        }


        /// <summary>
        /// 去除所有的html格式(另一种方式)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string NoHTML(string Htmlstring)
        {

            Htmlstring = Regex.Replace(Htmlstring, @"<script(\s[^>]*?)?>[\s\S]*?</script>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<style(\s[^>]*?)?>[\s\S]*?</style>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            return Htmlstring;
        }

        /// <summary>
        /// 只保留<br><p></p>标签
        /// </summary>
        /// <param name="Htmlstring"></param>
        /// <returns></returns>
        public static string RetentionHTML(string Htmlstring)
        {
            Htmlstring = Regex.Replace(Htmlstring, @"<(?!br|p|/p).*?>", "", RegexOptions.IgnoreCase);
            string Removestyle = Regex.Replace(Htmlstring, @"style\s*=(['""\s]?)[^'""]*?\1", "", RegexOptions.IgnoreCase);    //去除style
            return Removestyle;
        }

        /// <summary>
        /// 取得传入的汉字的首个拼音字母
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string GetChineseFirstSpell(string strText)
        {
            int len = strText.Length;
            string myStr = "", OneStr = "";
            for (int i = 0; i < len; i++)
            {
                OneStr = strText.Substring(i, 1);
                if (ValidateHelper.IsNumber(OneStr))
                {
                    myStr += OneStr;
                }
                else if (ValidateHelper.IsLetterWithChinese(OneStr))
                {
                    myStr += getSpell(OneStr);
                }
            }
            return myStr;
        }

        private static string getSpell(string cnChar)
        {
            byte[] arrCN = Encoding.Default.GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "O";
            }
            else return cnChar;
        }

        /// <summary>
        /// 格式化IP,最后一个IP数字用*代替
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FormatIP(string str)
        {
            string[] strs = str.Split('.');
            if (strs == null || strs.Length < 3)
                return "127.0.0.*";
            return strs[0] + "." + strs[1] + "." + strs[2] + ".*";
        }

        /// <summary>
        /// 判断是否含有非法字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsIllegallyString(string str)
        {
            bool flag = false;
            string FilterString = ConfigurationManager.AppSettings["FilterString"].ToString();
            if (FilterString != "")
            {
                string[] arrayStr = FilterString.Split(',');
                for (int i = 0; i < arrayStr.Length; i++)
                {
                    string pattern = arrayStr[i].ToString();
                    Regex reg = new Regex(pattern, RegexOptions.IgnoreCase);
                    if (reg.IsMatch(str))
                        return true;

                    //if (str.IndexOf(arrayStr[i].ToString().Trim(),StringComparison.OrdinalIgnoreCase) >= 0)
                    //    return true;
                }
            }
            return flag;
        }
        /// <summary>
        /// 获取蜘网Flash地址
        /// </summary>
        public static string CobWebFlashUrl
        {
            get
            {
                string url = ConfigurationManager.AppSettings["CobWebFlashUrl"];
                if (url != null)
                {
                    return url.ToString();
                }
                return "";
            }
        }
        /// <summary>
        /// 获取蜘网Flash地址
        /// </summary>
        public static string FCNUrl
        {
            get
            {
                string url = ConfigurationManager.AppSettings["FCNUrl"];
                if (url != null)
                {
                    return url.ToString();
                }
                return "";
            }
        }
        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        public static string FormatEnterToHtml(string pContext)
        {
            string val = pContext;
            if (val != "" && val != null)
            {
                val = val.Replace("\r\n", "<br>");
                val = val.Replace("\r", "<br>");
                val = val.Replace("\n", "<br>");
                val = val.Replace(" ", "&nbsp;&nbsp;");

            }
            return val;
            //return HttpUtility.HtmlEncode(pContext);
        }
        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        public static string FormatToHtml(string pContext)
        {
            string val = pContext;
            if (val != "" && val != null)
            {
                val = val.Replace("\r\n", "<br>");
                val = val.Replace("\r", "<br>");
                val = val.Replace("\n", "<br>");
            }
            return val;
        }
        /// <summary>
        /// 编码字符串
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        public static string EncodeURI(string pContext)
        {
            string val = pContext;
            if (val != "")
            {
                val = System.Web.HttpUtility.UrlEncode(val, Encoding.UTF8);
            }
            return val;
        }
        /// <summary>
        /// 解码字符串
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        public static string DecodeURI(string pContext)
        {
            string val = pContext;
            if (val != "")
            {
                val = System.Web.HttpUtility.UrlDecode(val, Encoding.UTF8);
            }
            return val;
        }
        /// <summary>
        /// 将字符串编码
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="text">字符串</param>
        /// <returns></returns>
        public static string EncodeToBase64(string code, string text)
        {
            if (text == "")
            {
                return "";
            }
            byte[] bytes = Encoding.GetEncoding(code).GetBytes(text);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 将字符串解码
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="text">解码字符串</param>
        /// <returns></returns>
        public static string DecodeFromBase64(string code, string text)
        {
            if (text == "")
            {
                return "";
            }
            byte[] bytes = Convert.FromBase64String(text);
            return Encoding.GetEncoding(code).GetString(bytes);
        }

        /// <summary>
        /// 将半角冒号转为全角的冒号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ReplaceColonDBC2SBC(string input)
        {
            return input.Replace(":", "：");
        }

        /// <summary>
        /// 获取QueryString[]值
        /// </summary>
        /// <param name="queryStringName"></param>
        /// <param name="defaultString"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string GetQueryString(string key, string defaultString)
        {
            return GetQueryString(key, defaultString, HttpContext.Current.Request);
        }
        public static DateTime GetQueryString(string key, DateTime defaultTime)
        {
            DateTime dt;
            if (DateTime.TryParse(HttpContext.Current.Request[key], out dt))
                return dt;
            else
                return defaultTime;
        }

        public static DateTime? GetQueryString(string key, DateTime? defaultTime)
        {
            DateTime dt;
            if (DateTime.TryParse(HttpContext.Current.Request[key], out dt))
                return dt;
            else
                return defaultTime;
        }
        /// <summary>
        /// 获取QueryString[]值
        /// </summary>
        /// <param name="queryStringName"></param>
        /// <param name="defaultString"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string GetQueryString(string queryStringName, string defaultString, HttpRequest Request)
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(queryStringName) || string.IsNullOrEmpty(Request[queryStringName]))
                return defaultString;
            str = StringHelper.ReplaceSQLMetaChar(Request[queryStringName]);
            return str;
        }
        /// <summary>
        /// 获取QueryString[]值
        /// </summary>
        /// <param name="queryStringName"></param>
        /// <param name="defaultString"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static int GetQueryString(string key, int defaultVal)
        {
            return GetQueryString(key, defaultVal, HttpContext.Current.Request);
        }
        /// <summary>
        /// 获取QueryString[]值
        /// </summary>
        /// <param name="queryStringName"></param>
        /// <param name="defaultString"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static bool GetQueryString(string key, bool defaultVal)
        {
            bool str = false;
            if (string.IsNullOrEmpty(key))
                return defaultVal;
            if (Boolean.TryParse(HttpContext.Current.Request[key], out str))
                return str;
            else
                return defaultVal;
        }
        /// <summary>
        /// 获取QueryString[]值
        /// </summary>
        /// <param name="queryStringName"></param>
        /// <param name="defaultString"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static int GetQueryString(string queryStringName, int defaultNum, HttpRequest Request)
        {
            int str = 0;
            if (string.IsNullOrEmpty(queryStringName))
                return defaultNum;
            if (Int32.TryParse(Request[queryStringName], out str))
                return str;
            else
                return defaultNum;
        }


        /// <summary>
        /// 字符串拆分成字符串数组，相当于string.Split
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="filter">分隔符</param>
        /// <returns>分隔后的数组</returns>
        public static string[] Split(string source, string filter)
        {
            List<string> array = new List<string>();
            int pos1 = 0, pos2 = 0;
            while ((pos2 = source.IndexOf(filter, pos1)) > -1)
            {
                array.Add(source.Substring(pos1, pos2 - pos1));
                pos1 = pos2 + filter.Length;
            }
            array.Add(source.Substring(pos1, source.Length - pos1));
            return array.ToArray();
        }
        /// <summary>
        /// 根据内容获取话题（#号之间的话题）
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string GetTopicByContent(string content)
        {
            if (content.IndexOf("#") > -1)
            {
                return content.Split('#')[1];
            }
            return "";
        }

        /// <summary>
        /// 去除输入框的换行符
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        public static string FormatEnterToText(string pContext)
        {
            string val = pContext;
            if (val != "" && val != null)
            {
                val = val.Replace("\r\n", "");
                val = val.Replace("\r", "");
                val = val.Replace("\n", "");
            }
            return val;
        }


        /// <summary> 
        /// 计算某日起始日期（礼拜一的日期） 
        /// </summary> 
        /// <param name="someDate">该周中任意一天</param> 
        /// <returns>返回礼拜一日期，后面的具体时、分、秒和传入值相等</returns> 
        public static DateTime GetMondayDate(DateTime someDate)
        {
            int i = someDate.DayOfWeek - DayOfWeek.Monday;
            if (i == -1) i = 6;// i值 > = 0 ，因为枚举原因，Sunday排在最前，此时Sunday-Monday=-1，必须+7=6。 
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return someDate.Subtract(ts);
        }
        /// <summary> 
        /// 计算某日结束日期（礼拜日的日期） 
        /// </summary> 
        /// <param name="someDate">该周中任意一天</param> 
        /// <returns>返回礼拜日日期，后面的具体时、分、秒和传入值相等</returns> 
        public static DateTime GetSundayDate(DateTime someDate)
        {
            int i = someDate.DayOfWeek - DayOfWeek.Sunday;
            if (i != 0) i = 7 - i;// 因为枚举原因，Sunday排在最前，相减间隔要被7减。 
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return someDate.Add(ts);
        }
        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = String.Empty;

            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            if (string.IsNullOrEmpty(result) || !ValidateHelper.IsIP(result))
            {
                return "127.0.0.1";
            }

            return result;

        }
        /// <summary>
        /// url 重建
        /// </summary>
        /// <param name="url"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string BuildUrl(string url, params string[] args)
        {
            string ParamText; string ParamValue;
            for (int i = 0; i < args.Length; i++)
            {
                ParamText = args[i].Split(new char[] { '=' })[0];
                ParamValue = args[i].Split(new char[] { '=' })[1];
                Regex reg = new Regex(string.Format("{0}=[^&]*", ParamText), RegexOptions.IgnoreCase);
                Regex reg1 = new Regex("[&]{2,}", RegexOptions.IgnoreCase);
                url = reg.Replace(url, "");
                if (url.IndexOf("?") == -1)
                    url += string.Format("?{0}={1}", ParamText, ParamValue);//?
                else
                    url += string.Format("&{0}={1}", ParamText, ParamValue);//&
                url = reg1.Replace(url, "&");
                url = url.Replace("?&", "?");
            }
            return url;
        }

        /// <summary>
        /// 过滤输入的脚本，iframe，href等
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string WipeScript(string html)
        {
            if (html != "")
            {
                Regex regex1 = new Regex(@"<script[\s\S]+</script *>", RegexOptions.IgnoreCase);
                //Regex regex2 = new Regex(@" href *= *[\s\S]*script *:", RegexOptions.IgnoreCase | RegexOptions.Compiled); //过滤href=javascript: (<A>) 属性
                Regex regex2 = new Regex("href *= *['\"]([^'\"]+)['\"]", RegexOptions.IgnoreCase);
                Regex regex3 = new Regex(@" on[\s\S]*?=", RegexOptions.IgnoreCase);
                Regex regex4 = new Regex(@"<iframe[\s\S]+</iframe *>", RegexOptions.IgnoreCase);
                Regex regex5 = new Regex(@"<frameset[\s\S]+</frameset *>", RegexOptions.IgnoreCase);
                html = regex1.Replace(html, ""); //过滤<script></script>标记
                html = regex2.Replace(html, "");//过滤href超链接
                html = regex3.Replace(html, ""); //过滤其它控件的on...事件
                html = regex4.Replace(html, ""); //过滤iframe
                html = regex5.Replace(html, ""); //过滤frameset
            }
            return html;
        }

        /// <summary>
        /// 抽取图片
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static ArrayList GetImagePath(string content)
        {
            ArrayList ary = new ArrayList();
            string p = "<img([^>]+)>";//src=[\'\"](?<url>[^>]+)[\'\"]([^>]*)>";
            string p2 = "src=\"([^\"]+)\"";
            Regex re = new Regex(p, RegexOptions.IgnoreCase);
            MatchCollection mc = re.Matches(content);
            for (int i = 0; i < mc.Count; i++)
            {
                Regex re2 = new Regex(p2, RegexOptions.IgnoreCase);
                MatchCollection mcc = re2.Matches(mc[i].ToString());
                for (int j = 0; j < mcc.Count; j++)
                {
                    string subStr = mcc[j].ToString();
                    subStr = subStr.Substring(5, subStr.Length - 6);
                    ary.Add(subStr);
                }
            }
            return ary;
        }

        /// <summary>
        /// 转换成不带签名的utf-8输出
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string GetUTF8WithoutBOMString(string content)
        {
            if (string.IsNullOrEmpty(content))
                return "";
            Encoding utf = new UTF8Encoding(false);
            byte[] buffer = utf.GetBytes(content.ToCharArray(), 0, content.Length);
            if (buffer.Length <= 3)
            {
                return Encoding.UTF8.GetString(buffer);
            }

            byte[] bomBuffer = new byte[] { 0xef, 0xbb, 0xbf };

            if (buffer[0] == bomBuffer[0]
                && buffer[1] == bomBuffer[1]
                && buffer[2] == bomBuffer[2])
            {
                return new UTF8Encoding(false).GetString(buffer, 3, buffer.Length - 3);
            }

            return Encoding.UTF8.GetString(buffer);
        }

        //本地路径转换成URL相对路径
        public static string Urlconvertor(string imagesurl1)
        {
            string tmpRootDir = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            string imagesurl2 = imagesurl1.Replace(tmpRootDir, ""); //转换成相对路径
            imagesurl2 = imagesurl2.Replace(@"\", @"/");
            //imagesurl2 = imagesurl2.Replace(@"Aspx_Uc/", @"");
            return imagesurl2;
        }

        //相对路径转换成服务器本地物理路径
        public static string Urlconvertorlocal(string imagesurl1)
        {
            string tmpRootDir = HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            string imagesurl2 = tmpRootDir + imagesurl1.Replace(@"/", @"\"); //转换成绝对路径
            return imagesurl2;
        }

    }
}
