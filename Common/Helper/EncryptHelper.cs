using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Common
{
    public class EncryptHelper
    {
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string EncryptString(string strValue)
        {
            var sha384 = new SHA384Managed();
            var uEncode = new UnicodeEncoding();
            Byte[] bytValue;
            Byte[] bytTemp;

            bytValue = uEncode.GetBytes(strValue);
            bytTemp = sha384.ComputeHash(bytValue);
            return Convert.ToBase64String(bytTemp);
        }

        /// <summary>
        /// 返回密码的MD5码
        /// </summary>
        /// <param name="pSourcePassword"></param>
        /// <returns></returns>
        public static string MD5(string pSourcePassword)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(pSourcePassword, "md5"); //MD5加密
        }

        /// <summary>
        /// 返回16位md5散列
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Md516(string source)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(source)), 4, 8);
            t2 = t2.Replace("- ", " ").Replace("-", "");
            return t2;
        }

        /// <summary>
        /// 64位格式化
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string EncodeToBase64(string text)
        {
            if (text == "")
            {
                return "";
            }
            byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(text);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 64位解码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string DecodeFromBase64(string text)
        {
            if (text == "")
            {
                return "";
            }
            byte[] bytes = Convert.FromBase64String(text);
            return Encoding.GetEncoding("utf-8").GetString(bytes);
        }

        #region DESEnCode DES加密

        private static readonly char[] HEXTAB =
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'a', 'b', 'c', 'd', 'e', 'f'
            };

        private static string DES_Key = "9553Qlzq"; //（必须是8位且不能含中文）
        private static string DES_Iv = "84625440"; //（必须是8位且不能含中文）

        public static string DESEnCode(string pToEncrypt)
        {
            var des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.GetEncoding("UTF-8").GetBytes(pToEncrypt);

            //建立加密对象的密钥和偏移量    
            //原文使用ASCIIEncoding.ASCII方法的GetBytes方法    
            //使得输入密码必须输入英文文本    
            des.Key = Encoding.ASCII.GetBytes(DES_Key);
            des.IV = Encoding.ASCII.GetBytes(DES_Iv);
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            byte[] encrypted = ms.ToArray();

            var sb = new StringBuilder();

            for (int i = 0; i < encrypted.Length; i++)
            {
                sb.Append(HEXTAB[(encrypted[i] >> 4) & 0x0f]);
                sb.Append(HEXTAB[(encrypted[i] & 0x0f)]);
            }
            return sb.ToString();
        }

        #endregion

        #region DESDeCode DES解密

        /// <summary>
        /// 对DES加密后的字符串进行解密
        /// </summary>
        /// <param name="encryptedString">待解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string DESDeCode(string encryptedString)
        {
            byte[] btKey = Encoding.Default.GetBytes(DES_Key);
            byte[] btIV = Encoding.Default.GetBytes(DES_Iv);
            var des = new DESCryptoServiceProvider();

            using (var ms = new MemoryStream())
            {
                var ms2 = new MemoryStream();
                int nSrcPos = 0;
                for (int nI = 0; nI < encryptedString.Length/2; nI++)
                {
                    byte bActByte = 0;
                    bool blConvertOK = true;
                    for (int nJ = 0; nJ < 2; nJ++)
                    {
                        bActByte <<= 4;
                        char cActChar = encryptedString[nSrcPos++];
                        if (cActChar >= 'a' && cActChar <= 'f')
                        {
                            bActByte |= (byte) (cActChar - 97 + 10);
                            continue;
                        }
                        if (cActChar >= '0' && cActChar <= '9')
                        {
                            bActByte |= (byte) (cActChar - 48);
                        }
                        else
                        {
                            blConvertOK = false;
                        }
                    }
                    if (blConvertOK)
                    {
                        ms2.WriteByte(bActByte);
                    }
                }
                byte[] inData = ms2.ToArray();
                try
                {
                    using (var cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
                catch
                {
                    throw;
                }
            }
        }

        #endregion
    }
}