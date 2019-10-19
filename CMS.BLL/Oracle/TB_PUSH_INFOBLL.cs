using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;
using ServiceStack.OrmLite;
using CMS.DAL;
using ServiceStack.DataAnnotations;
using System.Data.OracleClient;
using Common;
using CMS.Model.Oracle;
using Factory;

namespace CMS.BLL.Oracle
{
    public class TB_PUSH_INFOBLL
    {
        OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(SqlConnectFactory.BaiduPush, OracleDialect.Provider);
        #region 获取推送信息表分页显示数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [Cache("TB_PUSH_INFOBLL", 10)]
        public virtual List<TB_PUSH_INFO> GetPagerData(int PageIndex, int PageSize)
        {
            List<TB_PUSH_INFO> tirList = new List<TB_PUSH_INFO>();
            using (var db = dbFactory.OpenDbConnection())
            {
                tirList = db.Query<TB_PUSH_INFO>("SELECT T.* FROM (SELECT ROW_NUMBER() OVER(ORDER BY tb.OPERATEDATE DESC nulls last) RNUM,tb.TITLE,tb.INFOABSTRACT,tb.OPERATEDATE,tb.PLANCOUNT,tb.REALCOUNT,tb.CLICKCOUNT,tb.EDITOR,tb.PLATFORM FROM TB_PUSH_INFO tb) T WHERE T.RNUM >:minpageindex AND T.RNUM <=:maxpageindex", new { minpageindex = PageIndex * PageSize, maxpageindex = ((PageIndex * PageSize) + PageSize) });
               // tirList = db.Select<TB_PUSH_INFO>(we => we.OrderByDescending(t => t.OPERATEDATE)).Skip(PageIndex * PageSize).Take(PageSize).ToList();
            }
            return tirList;
        }
        
        /// <summary>
        /// 总条数
        /// </summary>
        /// <returns></returns>
        [Cache("TB_PUSH_INFOBLL", 10)]
        public virtual int GetPagerTotalRecord()
        {
            int Total = 0;
            using (var db = dbFactory.OpenDbConnection())
            {
                Total = db.Query<TB_PUSH_INFO>("SELECT FID FROM TB_PUSH_INFO ").Count;
            }
            return Total;
        }      
        #endregion
    }
}
