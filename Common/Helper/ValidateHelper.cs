using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;

namespace Common
{
    /// <summary>
    /// 正则表达式验证类
    /// </summary>
    public class ValidateHelper
    {
        /// <summary>
        /// 正则表达式验证手机
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsMobile(string str)
        {
            string pattern = @"^(13[0-9]|15[0-9]|18[0-9]|14[0-9]|17[0-9])\d{8}$";
            return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// 正则表达式验证电话号码，带区号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsCodeWithPhone(string str)
        {
            string pattern = @"^0\d{10,11}$";
            return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// 正则表达式验证电话号码，不带区号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsPhone(string str)
        {
            string pattern = @"^[1-9]\d{6,7}$";
            return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// 正则表达式验证是否电话区号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsCode(string str)
        {
            string pattern = @"^0\d{2,3}$";
            return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// 正则表达式验证是否数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumber(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            string pattern = @"^[0-9]+$";
            return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// 正则表达式验证是否股票号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsStockNo(string str)
        {
            string pattern = @"^[0|6|3]\d{5}$";
            return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// 正则表达式验证数字和字母
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumWithLetter(string str)
        {
            string pattern = @"^[a-z0-9]+$";
            return Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 正则表达式验证数字和字母
        /// </summary>
        /// <param name="str"></param>
        /// <param name="minLen">字符串的最小长度</param>
        /// <param name="maxLen">字符串的最大长度</param>
        /// <returns></returns>
        public static bool IsNumWithLetter(string str, int minLen, int maxLen)
        {
            string pattern = @"^[a-z0-9]{" + minLen + "," + maxLen + "}$";
            return Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 正则表达式验证是否邮箱
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmail(string str)
        {
            string pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// 正则表达式验证数字和字母和汉字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumWithLetterWithChinese(string str)
        {
            string pattern = @"^[0-9a-zA-Z\u4e00-\u9fa5]+$";
            bool success=Regex.IsMatch(str, pattern,RegexOptions.IgnoreCase);
            return success;
        }

        /// <summary>
        /// 正则表达式验证数字和字母和汉字
        /// </summary>
        /// <param name="str"></param>
        /// <param name="minLen">字符串的最小长度</param>
        /// <param name="maxLen">字符串的最大长度</param>
        /// <returns></returns>
        public static bool IsNumWithLetterWithChinese(string str,int minLen,int maxLen)
        {
            string pattern = @"^[0-9a-zA-Z\u4e00-\u9fa5]{" + minLen + "," + maxLen + "}$";
            return Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 判断是否字母和汉字
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static bool IsLetterWithChinese(string str)
        {
            string pattern = @"[a-zA-Z\u4e00-\u9fa5]+$";
            return Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 是否字母
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsLetter(string str)
        {
            string pattern = @"^[a-z]+$";
            return Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 判断字符串是否含有字母，汉字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ExistLetterWithChinese(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if ((IsLetterWithChinese(str.Substring(i, 1))))
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 判断是否含有非法字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsIllegally(string str)
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
                }
            }
            return flag;
        }


        /// <summary>
        /// 判断是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");

        }

        /// <summary>
        /// 是否为网址
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsURL(string str)
        {
            return Regex.IsMatch(str, @"([\w-]+\.)+[a-zA-z]+(/[\w- ./?%&=]*)?");//http(s)?://
        }

        /// <summary>
        /// 判断内容中是否存在Url
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool HasUrl(string str)
        {
            string pattern = @"([\w-]+\.)+[a-zA-z]+(/[\w- ./?%&=]*)?";
            MatchCollection mc = Regex.Matches(str, pattern, RegexOptions.IgnoreCase);
            if (mc.Count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 判断是否是银行账号 19位数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsBank(string str)
        {
            string pattern = @"^\d{19}$";
            MatchCollection mc = Regex.Matches(str, pattern, RegexOptions.IgnoreCase);
            if (mc.Count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 判断是否是身份证
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsIdCard(string str)
        {
            string pattern = @"^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$";
            MatchCollection mc = Regex.Matches(str, pattern, RegexOptions.IgnoreCase);
            if (mc.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 根据身份证号获取生日
        /// </summary>
        /// <param name="IdCard"></param>
        /// <returns></returns>
        public static string GetBrithdayFromIdCard(string IdCard)
        {
            string rtn = "1900-01-01";
            if (IdCard.Length == 15)
            {
                rtn = IdCard.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            }
            else if (IdCard.Length == 18)
            {
                rtn = IdCard.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            }
            return rtn;
        }

        /// <summary>
        /// 根据身份证获取性别
        /// </summary>
        /// <param name="IdCard"></param>
        /// <returns></returns>
        public static string GetSexFromIdCard(string IdCard)
        {
            string rtn;
            string tmp = "";
            if (IdCard.Length == 15)
            {
                tmp = IdCard.Substring(IdCard.Length - 3);
            }
            else if (IdCard.Length == 18)
            {
                tmp = IdCard.Substring(IdCard.Length - 4);
                tmp = tmp.Substring(0, 3);
            }
            int sx = int.Parse(tmp);
            int outNum;
            Math.DivRem(sx, 2, out outNum);
            if (outNum == 0)
            {
                rtn = "女";
            }
            else
            {
                rtn = "男";
            }
            return rtn;
        }

        /// <summary>
        /// 判断是否是邮编
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsZip(string str)
        {
            string pattern = @"^[1-9][0-9]{5}$";
            MatchCollection mc = Regex.Matches(str, pattern, RegexOptions.IgnoreCase);
            if (mc.Count > 0)
                return true;
            else
                return false;
        }

    }
}
