using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using CMS.HtmlService.Contract;
using Common;
using CMS.BLL.Oracle;
using CMS.Model.Oracle;

namespace CMS.BLL
{
    public class SsoServer
    {
        /// <summary>
        /// SSO数据推送服务
        /// </summary>
        /// <param name="planId"></param>
        public static void SsoPush(int planId, string version)
        {
            var factory = new ChannelFactory<ISsoOutWindow>("SsoService");
            var channel = factory.CreateChannel();
            try
            {
                //IAsyncResult result = channel.BeginSsoPush(planId, version, null, null);
                channel.SsoPush(planId, version);
                factory.Close();
                Loger.Info("SSO接口调用成功");
            }
            catch (Exception ex)
            {
                Loger.Error(ex);
                factory.Abort();
            }
        }

        /// <summary>
        /// SSO数据推送, 单条推送记录
        /// </summary>
        /// <param name="planId"></param>
        public static void SsoPushOne(int ssoResultId)
        {
            var factory = new ChannelFactory<ISsoOutWindow>("SsoService");
            var channel = factory.CreateChannel();
            try
            {
                channel.SsoPushOne(ssoResultId);
                factory.Close();
                Loger.Info("SSO数据推送, 单条推送记录");
            }
            catch (Exception ex)
            {
                Loger.Error(ex);
                factory.Abort();
            }
        }

        /// <summary>
        /// 修改消息状态接口
        /// </summary>
        /// <param name="planId"></param>
        public static int UpDateStatus(int id, int status)
        {
            var factory = new ChannelFactory<ISsoOutWindow>("SsoService");
            var channel = factory.CreateChannel();
            int res;

            try
            {
                res = channel.UpDateStatus(id, status);
                factory.Close();
                Loger.Info("修改消息状态接口调用成功");
                return res;
            }
            catch (Exception ex)
            {
                Loger.Error(ex);
                factory.Abort();
                return -1;
            }
        }

        /// <summary>
        /// SSO数据推送服务(炒股大赛)
        /// </summary>
        /// <param name="planId"></param>
        public static void SsoPush(StockContestData scd)
        {
            var factory = new ChannelFactory<ISsoOutWindow>("SsoService");
            var channel = factory.CreateChannel();
            try
            {
                channel.SsoPushStockContest(scd);
                factory.Close();
                Loger.Info("SSO接口调用成功[炒股大赛]");
            }
            catch (Exception ex)
            {
                Loger.Error(ex);
                factory.Abort();
            }
        }

    }
}
