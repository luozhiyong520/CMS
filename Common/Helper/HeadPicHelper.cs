using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Common
{
    public class HeadPicHelper
    {
        /// <summary>
        /// 获取头像地址
        /// </summary>
        /// <returns></returns>
        public static string GetHeadPic(string username)
        {
            string uidhashcode = Math.Abs(username.GetHashCode()).ToString().Substring(0, 2);
            int headcode = int.Parse(uidhashcode);
            if (headcode > 45)
                headcode = headcode / 3;
            return ConfigurationManager.AppSettings["headpicsrc"] + headcode + ".jpg";
        }
    }
}
