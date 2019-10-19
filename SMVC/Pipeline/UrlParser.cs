using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SMVC
{
    internal static class UrlParser
    {
        // 用于匹配Ajax请求的正则表达式，
        // 可以匹配的URL：/AjaxClass/method.cspx?id=2
        // 注意：类名必须Ajax做为前缀
        readonly static string ajaxUrlPattern = @"[\s\S]*/(?<name>(\w[\./\w]*)?(?=Ajax)\w+)[/\.](?<method>\w+)\.[a-zA-Z]+";

        //readonly static string servicePattern = @"/(?<name>(\w[\./\w]*)?(?=Ajax)\w+)[/\.](?<method>\w+)[/\?]?";

        internal static ControllerActionPair GetControllerActionPair(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            Match match = Regex.Match(path, ajaxUrlPattern);
            if (!match.Success)
            {
                    return null;
            }

            GroupCollection gc = match.Groups;
            ControllerActionPair pair = new ControllerActionPair
            {
                Controller = gc["name"].Value.Replace("/", "."),//支持ajax页面地址中类的全名中用'/'代替'.'
                Action = gc["method"].Value
            };
            return pair;
        }
    }
}
