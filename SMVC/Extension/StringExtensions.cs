using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SMVC
{
    /// <summary>
    /// 用于UI输出方面的常用字符串转换
    /// </summary>
    public static class StringExtensions
    {
        public static string HtmlEncode(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            return HttpUtility.HtmlEncode(str);
        }

        public static string HtmlAttributeEncode(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            return HttpUtility.HtmlAttributeEncode(str);
        }

        /// <summary>
        /// sql注入关键字过滤
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterSqlInject(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;   //返回null，是为了支持Nullable类型
            string input = str.ToLower();
            if (input.Contains("'")
                || input.Contains("0x")
                || input.Contains("%27")
                || input.Contains(";")
                || input.Contains("%3b")
                || input.Contains("--")
                || input.Contains("%2d%2d")
                || input.Contains("exec")
                || input.Contains("insert")
                || input.Contains("select")
                || input.Contains("delete")
                || input.Contains("update")
                || input.Contains("count")
                || input.Contains("truncate")
                || input.Contains("/add")
                || input.Contains("drop table")
                || input.Contains("master.dbo.xp_cmdshell")
                || input.Contains("net localgroup administrators"))
            {

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
                return str;
            }
        }
    }


}
