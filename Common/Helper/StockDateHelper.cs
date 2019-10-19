using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Common
{
    public class StockDateHelper
    {
        private static string StockOpeningDateStr = ConfigurationManager.AppSettings["StockOpeningDate"];
        /// <summary>
        /// 获取开市的所有日期列表
        /// </summary>
        private static List<DateTime> StockOpeningDateList
        {
            get
            {
                string StockOpeningDateStr = ConfigurationManager.AppSettings["StockOpeningDate"];
                List<DateTime> dateList = new List<DateTime>();
                string[] strArray = StockOpeningDateStr.Split(',');
                foreach (string str in strArray)
                {
                    dateList.Add(DateTime.Parse(str));
                }
                return dateList;
            }
        }
        /// <summary>
        /// 判断现在是否在开市时间中
        /// </summary>
        public static bool IsOpeningTime
        {
            get
            {
                DateTime now = DateTime.Now;
                string shortTime = string.Format("{0:HH:mm}", now);
                if (shortTime.CompareTo("09:30") == -1 || (shortTime.CompareTo("11:29") == 1 && shortTime.CompareTo("13:00") == -1) || shortTime.CompareTo("14:59") == 1)
                {
                    return false;
                }
                else
                {
                    return StockOpeningDateList.Any(s => s.Date == now.Date);
                }
            }
        }

        /// <summary>
        /// 判断现在是否在开市日期中
        /// </summary>
        public static bool IsOpeningDate
        {
            get
            {
                DateTime now = DateTime.Now;
                return StockOpeningDateList.Any(s => s.Date == now.Date);
            }
        }

        /// <summary>
        /// 判断是否需要更新行情数据
        /// </summary>
        public static bool IsMustUpdateStockQuotes
        {
            get
            {
                DateTime now = DateTime.Now;
                string shortTime = string.Format("{0:HH:mm}", now);
                if (shortTime.CompareTo("09:29") == -1 || (shortTime.CompareTo("11:35") == 1 && shortTime.CompareTo("13:00") == -1) || shortTime.CompareTo("15:10") == 1)
                {
                    return false;
                }
                else
                {
                    return StockOpeningDateList.Any(s => s.Date == now.Date);
                }
            }
        }


        /// <summary>
        /// 获取离当前日期的下某个交易日
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static DateTime GetNextTradingDay(int n)
        {
            IOrderedEnumerable<DateTime> io = StockOpeningDateList.Where(s => s >= DateTime.Now).OrderBy(s=>s);
            List<DateTime> dlist = io.ToList();
            if (n >= dlist.Count)
            {
                n = dlist.Count - 1;
            }
            return dlist[n];
        }

        /// <summary>
        /// 获取离当前日期的下某个交易日
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static DateTime GetThePreviousDay(int n)
        {
            IOrderedEnumerable<DateTime> io = StockOpeningDateList.Where(s => s <= DateTime.Now).OrderByDescending(s => s);
            List<DateTime> dlist = io.ToList();
            if (n >= dlist.Count)
            {
                n = dlist.Count - 1;
            }
            return dlist[n];
        }
    }
}
