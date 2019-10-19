using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.DAL;
using CMS.Model.Oracle;
using System.Data;
using Common;
using CMS.Model;
using ServiceStack.OrmLite;
using System.Text.RegularExpressions;

namespace CMS.BLL
{
    public partial class AnalystLiveBLL
    {
        OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(SqlConnectFactory.BOCE, OracleDialect.Provider);
        /// <summary>
        /// 添加/编辑商品信息
        /// </summary>
        /// <param name="analystlive"></param>
        /// <param name="operateType"></param>
        /// <returns></returns>
        public int AddEditAnalystLive(AnalystLive analystlive, string operateType, int AnalystType)
        {
            if (operateType == "add")
            {
                return dal.AddAnalystLive(analystlive, AnalystType);
            }
            else
            {
                return dal.EditAnalystLive(analystlive, AnalystType);
            }
        }
        /// <summary>
        /// 判断开仓的商品已开仓过没平仓
        /// </summary>
        /// <param name="ActualCode"></param>
        /// <returns></returns>
        //public bool IsSell(string ActualCode)
        //{
        //    return dal.IsSell(ActualCode, UserCookies.AdminId);
        //}

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="liveType"></param>
        /// <returns></returns>
        public virtual string GetProductCode(int liveType, int AnalystId, int AnalystType)
        {
            if (liveType == 1)
            {
                if (AnalystType == 1)
                {
                    List<XH_PROCUDT_TYPE> xptList = new List<XH_PROCUDT_TYPE>();
                    using (var db = dbFactory.OpenDbConnection())
                    {
                        xptList = db.Select<XH_PROCUDT_TYPE>();
                    }
                    return JsonHelper.ToJson(xptList);
                }
                else
                {
                    List<GJS_PROCUDT_TYPE> xptList = new List<GJS_PROCUDT_TYPE>();
                    using (var db = dbFactory.OpenDbConnection())
                    {
                        xptList = db.Select<GJS_PROCUDT_TYPE>();
                    }
                    return JsonHelper.ToJson(xptList);
                }
                
            }
            else
            {
                return JsonHelper.ToJson(dal.GetIsSellList(AnalystId));
            }
        }

        


         /// <summary>
       /// 获取开多/开空/盈利/亏损/成功次数/比例
       /// </summary>
       /// <param name="num"></param>
       /// <param name="AnalystId"></param>
       /// <returns></returns>
        //public string QueryCount(int num)
        //{
        //    return dal.QueryCount(num,UserCookies.AdminId);
        //}


        public AnalystLive GetTransStatisticsNum(int AnalystId)
        {
            return dal.GetTransStatisticsNum(AnalystId);
        }

        /// <summary>
        /// 获取后台信息搜索列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="txtKeyword"></param>
        /// <param name="txtStartDate"></param>
        /// <param name="txtEndDate"></param>
        /// <returns></returns>
        public PagedResult<AnalystLive> GetAnalystLiveList(int? page, int rows, int AnalystId, int AnalystType)
        {
            PagedResult<AnalystLive> st = dal.GetAnalystLiveList(page, rows, AnalystId, 1, AnalystType);
            return st;
        }
        /// <summary>
        /// 获取直播列表
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        //public List<AnalystLive> GetAnalystLiveList(int top)
        //{
        //  return dal.GetAnalystLiveList(top);
        //}

        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<AnalystLive> GetAnalystLiveListNews(int count, string mkps)
        
        {
            return dal.GetAnalystLiveListNews(count,mkps);
        }
        /// <summary>
        /// 删除直播数据
        /// </summary>
        /// <param name="liveId"></param>
        /// <param name="TransType"></param>
        /// <param name="AnalystName"></param>
        public void DelAnalystLiveData(int liveId, string TransType, string AnalystName, int buyLiveId, int AnalystId, int AnalystType)
        {
            dal.DelAnalystLiveData(liveId, TransType, AnalystName, buyLiveId, AnalystId, AnalystType);
        }
    }
}
