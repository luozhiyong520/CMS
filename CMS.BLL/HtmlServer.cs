using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using CMS.HtmlService.Contract;
using Common;
using System.Drawing;

namespace CMS.BLL
{
    public class HtmlServer
    {
        /// <summary>
        /// 生成单篇文章的静态页
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="operateType">add：新增，update：修改</param>
        /// <returns></returns>
        public static bool CreateSingleNews(int newsId, string operateType)
        {
            var factory = new ChannelFactory<IHtmlEngine>("HtmlService");
            var channel = factory.CreateChannel();
            try
            {
                bool flag = channel.CreateSingleNews(newsId, operateType);
                factory.Close();
                Loger.Info(string.Format("CreateSingleNews：NewsId：{0}，OperateType：{1}，成功", newsId.ToString(), operateType));
                return flag;
            }
            catch (Exception ex)
            {
                Loger.Error(ex);
                factory.Abort();
                return false;
            }
        }

        /// <summary>
        /// 生成模板页面静态页面
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public static bool CreateTemplatePage(int templateId, Dictionary<string, string> dic = null)
        {
            var factory = new ChannelFactory<IHtmlEngine>("HtmlService");
            var channel = factory.CreateChannel();
            try
            {
                bool flag = channel.CreateTemplatePage(templateId, dic);
                factory.Close();
                Loger.Info(string.Format("CreateTemplatePage：TemplateId：{0}，成功", templateId.ToString()));
                return flag;
            }
            catch (Exception ex)
            {
                Loger.Error(ex);
                factory.Abort();
                return false;
            }
        }

        /// <summary>
        /// 生成和channelId有关的模板页面
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public static bool CreateTemplatePageByChannelId(string channelId, Dictionary<string, string> dic = null)
        {
            var factory = new ChannelFactory<IHtmlEngine>("HtmlService");
            var channel = factory.CreateChannel();
            try
            {
                bool flag = channel.CreateTemplatePageByChannelId(channelId, dic);
                factory.Close();
                Loger.Info(string.Format("CreateTemplatePageByChannelId：ChannelId：{0}，成功", channelId));
                return flag;
            }
            catch (Exception ex)
            {
                Loger.Error(ex);
                factory.Abort();
                return false;
            }
        }

        /// <summary>
        /// 得到模板的预览内容
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public static string GetTemplagePreview(int templateId, DateTime? reDate)
        {
            var factory = new ChannelFactory<IHtmlEngine>("HtmlService");
            var channel = factory.CreateChannel();
            try
            {
                string result = channel.GetTemplagePreview(templateId, reDate);
                factory.Close();
                Loger.Info(string.Format("GetTemplagePreview：TemplateId：{0}，成功", templateId.ToString()));
                return result;
            }
            catch (Exception ex)
            {
                Loger.Error(ex);
                factory.Abort();
                return string.Empty;
            }
        }

       
       

    }
}
