using System.Collections.Generic;
using System.ServiceModel;
using CMS.Model;
using System;

namespace CMS.HtmlService.Contract
{
    [ServiceContract]
    public interface IHtmlEngine
    {
        /// <summary>
        /// 生成单篇文章的静态页
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="operateType">add：新增，edit：修改</param>
        [OperationContract]
        bool CreateSingleNews(int newsId, string operateType);

        /// <summary>
        /// 生成模板页面静态页面
        /// </summary>
        /// <param name="templateId"></param>
        [OperationContract]
        bool CreateTemplatePage(int templateId, Dictionary<string, string> dic = null);

        /// <summary>
        /// 生成和channelId有关的模板页面
        /// </summary>
        /// <param name="channelId"></param>
        [OperationContract]
        bool CreateTemplatePageByChannelId(string channelId, Dictionary<string, string> dic = null);

        /// <summary>
        /// 得到模板的预览内容
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        [OperationContract]
        string GetTemplagePreview(int templateId, DateTime? reDate);

    

    }
}
