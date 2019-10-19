using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class NumHelper
    {
        /// <summary>
        /// 产生6位随机码
        /// </summary>
        /// <returns></returns>
        public static string RadomSixLengthNum()
        {
            return RadomNum(6, true);
        }

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <returns></returns>
        public static string RadomNum(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            string result = "";
            System.Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }

        /// <summary>
        /// 保留2位小数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ConToDec(object obj)
        {
            return ConToDec(obj, 2);
        }

        /// <summary>
        /// 保留2位小数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ConToDec(object obj, int num)
        {
            decimal result = 0m;
            try
            {
                result = Convert.ToDecimal(obj);
            }
            catch
            {
            }
            return Math.Round(result, num);
        }
    }
}
