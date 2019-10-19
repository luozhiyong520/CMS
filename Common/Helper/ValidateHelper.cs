using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;

namespace Common
{
    /// <summary>
    /// ������ʽ��֤��
    /// </summary>
    public class ValidateHelper
    {
        /// <summary>
        /// ������ʽ��֤�ֻ�
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsMobile(string str)
        {
            string pattern = @"^(13[0-9]|15[0-9]|18[0-9]|14[0-9]|17[0-9])\d{8}$";
            return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// ������ʽ��֤�绰���룬������
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsCodeWithPhone(string str)
        {
            string pattern = @"^0\d{10,11}$";
            return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// ������ʽ��֤�绰���룬��������
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsPhone(string str)
        {
            string pattern = @"^[1-9]\d{6,7}$";
            return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// ������ʽ��֤�Ƿ�绰����
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsCode(string str)
        {
            string pattern = @"^0\d{2,3}$";
            return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// ������ʽ��֤�Ƿ�����
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
        /// ������ʽ��֤�Ƿ��Ʊ��
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsStockNo(string str)
        {
            string pattern = @"^[0|6|3]\d{5}$";
            return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// ������ʽ��֤���ֺ���ĸ
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumWithLetter(string str)
        {
            string pattern = @"^[a-z0-9]+$";
            return Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// ������ʽ��֤���ֺ���ĸ
        /// </summary>
        /// <param name="str"></param>
        /// <param name="minLen">�ַ�������С����</param>
        /// <param name="maxLen">�ַ�������󳤶�</param>
        /// <returns></returns>
        public static bool IsNumWithLetter(string str, int minLen, int maxLen)
        {
            string pattern = @"^[a-z0-9]{" + minLen + "," + maxLen + "}$";
            return Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// ������ʽ��֤�Ƿ�����
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmail(string str)
        {
            string pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// ������ʽ��֤���ֺ���ĸ�ͺ���
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
        /// ������ʽ��֤���ֺ���ĸ�ͺ���
        /// </summary>
        /// <param name="str"></param>
        /// <param name="minLen">�ַ�������С����</param>
        /// <param name="maxLen">�ַ�������󳤶�</param>
        /// <returns></returns>
        public static bool IsNumWithLetterWithChinese(string str,int minLen,int maxLen)
        {
            string pattern = @"^[0-9a-zA-Z\u4e00-\u9fa5]{" + minLen + "," + maxLen + "}$";
            return Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// �ж��Ƿ���ĸ�ͺ���
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static bool IsLetterWithChinese(string str)
        {
            string pattern = @"[a-zA-Z\u4e00-\u9fa5]+$";
            return Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// �Ƿ���ĸ
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsLetter(string str)
        {
            string pattern = @"^[a-z]+$";
            return Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// �ж��ַ����Ƿ�����ĸ������
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
        /// �ж��Ƿ��зǷ��ַ�
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
        /// �ж��Ƿ�Ϊip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");

        }

        /// <summary>
        /// �Ƿ�Ϊ��ַ
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsURL(string str)
        {
            return Regex.IsMatch(str, @"([\w-]+\.)+[a-zA-z]+(/[\w- ./?%&=]*)?");//http(s)?://
        }

        /// <summary>
        /// �ж��������Ƿ����Url
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
        /// �ж��Ƿ��������˺� 19λ����
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
        /// �ж��Ƿ������֤
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
        /// �������֤�Ż�ȡ����
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
        /// �������֤��ȡ�Ա�
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
                rtn = "Ů";
            }
            else
            {
                rtn = "��";
            }
            return rtn;
        }

        /// <summary>
        /// �ж��Ƿ����ʱ�
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
