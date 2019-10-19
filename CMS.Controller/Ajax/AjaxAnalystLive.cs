using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.BLL;
using SMVC;
using Common;
using System.Web;
using CMS.Model;
using CMS.ViewModel;
using CMS.Utility;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using CMS.HtmlService.Contract;
using Factory;

namespace CMS.Controller
{
    [Authorize]
    public class AjaxAnalystLive
    {
        AnalystLiveBLL analystLiveBLL = Factory.BusinessFactory.CreateBll<AnalystLiveBLL>();
        PagingInfo PageInfo = new PagingInfo();
        /// <summary>
        /// 添加/编辑直播分析
        /// </summary>
        /// <param name="analystlive"></param>
        /// <returns></returns>
        [Action]
        public string AddEditAnalystLive(AnalystLive analystlive, int pc_LiveId, string pc_ActualCode, string pc_ActualName, string operate, int AnalystType)
        {
            analystlive.AnalystName = analystlive.AnalystName;
            analystlive.AnalystId = analystlive.AnalystId;
            analystlive.Info = analystlive.Content;
            if (analystlive.LiveType == 0)
            {
                return "000001";  //请选择一个操作类型
            }
            else if (analystlive.LiveType == 1 || analystlive.LiveType == 2)
            {
                if (analystlive.TransType == "0")
                {
                    return "000002"; //请选择一个交易类型
                }
            }
            if (analystlive.LiveType == 1)
            {
                if (operate == "add")
                {
                    if (string.IsNullOrEmpty(analystlive.ActualName.ToString()))
                    {
                        return "000005";
                    }
                }
                else
                {
                    analystlive.ActualName = pc_ActualName;
                }
            }
            else if (analystlive.LiveType == 2)
            {
                if (string.IsNullOrEmpty(pc_ActualName.ToString()))
                {
                    return "000005";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(analystlive.Info))
                {
                    return "000006";
                }
                
            }
            if (!string.IsNullOrEmpty(analystlive.Info))
            {
                if (StringHelper.WipeHtml(analystlive.Info).Length > 2000)
                {
                    return "000007";
                }
            }
            if (string.IsNullOrEmpty(analystlive.TransPrice.ToString()) && analystlive.LiveType!=3)
            {
                return "000003";
            }
            try
            {
                Convert.ToDecimal(analystlive.TransPrice);
            }
            catch
            {
                return "000003";
            }
            if (analystlive.LiveType == 1)
            {
                try
                {
                    if (!string.IsNullOrEmpty(analystlive.StopProfit.ToString()) || !string.IsNullOrEmpty(analystlive.StopLoss.ToString()))
                    {
                        Convert.ToDecimal(analystlive.StopProfit);
                        Convert.ToDecimal(analystlive.StopLoss);
                    }
                }
                catch
                {
                    return "000003";
                }
                if ( string.IsNullOrEmpty(analystlive.IsSell.ToString()))
                {
                    analystlive.IsSell = 0;
                }
               //if(analystLiveBLL.IsSell(analystlive.ActualCode))
               //{
               //    return "000004";  //这个商品已经开仓过还未平仓
               //}

            }
            else if (analystlive.LiveType == 2)
            {
                if (string.IsNullOrEmpty(pc_ActualName))
                {
                    return "000005";
                }
                else
                {
                    analystlive.ActualName = pc_ActualName;
                }
             
                analystlive.IsSell = 1;
                analystlive.ActualCode = pc_ActualCode;
                analystlive.BuyLiveId = pc_LiveId;

            }
            else
            {
                analystlive.TransType = "观点";
            }
            if (string.IsNullOrEmpty(analystlive.CreateTime.ToString()))
            {
                analystlive.CreateTime = DateTime.Now;
            }
            int result = analystLiveBLL.AddEditAnalystLive(analystlive, operate, AnalystType);
            MakeData();
            return result.ToString();
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="liveType"></param>
        /// <returns></returns>
        [Action]
        public string GetProductCode(int liveType, int AnalystId, int AnalystType)
        {
            return analystLiveBLL.GetProductCode(liveType, AnalystId, AnalystType);
        }

        /// <summary>
        /// 判断操作类型
        /// </summary>
        /// <param name="liveId"></param>
        /// <returns></returns>
        [Action]
        public string IsTransType(int liveId)
        {
            AnalystLive model = new AnalystLive();
            model = analystLiveBLL.Get("LiveId",liveId);
            return model.TransType;
        }

     
       

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="txtKeyword"></param>
        /// <param name="txtStartDate"></param>
        /// <param name="txtEndDate"></param>
        /// <param name="ChannelId"></param>
        /// <returns></returns>
        [Action]
        public JsonResult SearchAnalystLive(int? page, int rows, int AnalystId,int AnalystType)
        {
            PagedResult<AnalystLive> pagedResult = analystLiveBLL.GetAnalystLiveList(page, rows, AnalystId, AnalystType);
            PageInfo.PageIndex = page.HasValue ? page.Value - 1 : 0;
            PageInfo.PageSize = rows;
            PageInfo.TotalRecords = pagedResult.Total;
            //NewsModel.DataTable = pagedResult.Result;

            var result = new GridResult<AnalystLive>(pagedResult.Result, pagedResult.Total);
            
            return new JsonResult(result);

        }

        /// <summary>
        /// 删除直播数据
        /// </summary>
        /// <param name="liveId"></param>
        /// <param name="TransType"></param>
        /// <returns></returns>
        [Action]
        public string DelAnalystLive(int liveId, string transType, int buyLiveId, string AnalystName, int AnalystId, int AnalystType)
        {
            analystLiveBLL.DelAnalystLiveData(liveId, transType, AnalystName, buyLiveId, AnalystId,AnalystType);
            MakeData();
            return "000000";
        }

        /// <summary>
        /// 生成数据调用方法
        /// </summary>
        private void MakeData()
        {
            //分析师直播动态模块
             HtmlServer.CreateTemplatePage(12);
            //现货首页直播分析师实时动态
            HtmlServer.CreateTemplatePage(13);
            //现货战绩榜
            HtmlServer.CreateTemplatePage(16);
            //现货首页调用的三条直播
            HtmlServer.CreateTemplatePage(22);
            HtmlServer.CreateTemplatePage(78);
            HtmlServer.CreateTemplatePage(79);
            //现货首页直播分析师实时动态 - 金牡丹
            HtmlServer.CreateTemplatePage(61);
            //现货战绩榜 - 金牡丹
            HtmlServer.CreateTemplatePage(64);
            //现货首页直播分析师实时动态 - 普通
            HtmlServer.CreateTemplatePage(65);
            //现货战绩榜 - 普通
            HtmlServer.CreateTemplatePage(68);
            //分析师--总战绩榜
            HtmlServer.CreateTemplatePage(84);
            HtmlServer.CreateTemplatePage(85);
            HtmlServer.CreateTemplatePage(86);

            //本地贵金属首页直播分析师实时动态
          // HtmlServer.CreateTemplatePage(101);
            //本地贵金属战绩榜
           // HtmlServer.CreateTemplatePage(103);

            
            //贵金属首页直播分析师实时动态
            HtmlServer.CreateTemplatePage(24);
            //贵金属战绩榜
            HtmlServer.CreateTemplatePage(26);

            //测试现货直播
           // HtmlServer.CreateTemplatePage(89);
           // HtmlServer.CreateTemplatePage(90);
            //vip直播
            HtmlServer.CreateTemplatePage(32);
            HtmlServer.CreateTemplatePage(33);
            HtmlServer.CreateTemplatePage(34);
            HtmlServer.CreateTemplatePage(35);
            HtmlServer.CreateTemplatePage(36);
            HtmlServer.CreateTemplatePage(37);
            HtmlServer.CreateTemplatePage(38);
            HtmlServer.CreateTemplatePage(39);
            HtmlServer.CreateTemplatePage(40);
            HtmlServer.CreateTemplatePage(41);
            HtmlServer.CreateTemplatePage(42);
            HtmlServer.CreateTemplatePage(43);

            //贵金属淘金殿
            HtmlServer.CreateTemplatePage(74);
            HtmlServer.CreateTemplatePage(75);
            HtmlServer.CreateTemplatePage(76);
            HtmlServer.CreateTemplatePage(77);
        }

    }
}
