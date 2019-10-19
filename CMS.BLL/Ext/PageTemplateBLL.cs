using CMS.Model;
using Common;
using System.Collections.Generic;

namespace CMS.BLL
{
    public partial class PageTemplateBLL
    {
        public PageTemplate GetTemplateByNewsID(int newsID)
        {
            return dal.GetTemplateByNewsID(newsID);
        }

        /// <summary>
        /// 分页读取数据
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="whereCollection">条件值，例如：key为UserId>=@UserId，值为20</param>
        /// <param name="orderBy">排序字段</param>
        /// <returns></returns>
        public PagedResult GetPageTemplatePageList(int templeteType, int? pageIndex, int pageSize, string templateName, string templateFileName)
        {
            return dal.GetPageTemplatePageList(templeteType,pageIndex,pageSize,templateName,templateFileName);
        }


        /// <summary>
        /// 模板事物删除
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public int DeletePageTemplate(int templateId)
        {
            return dal.DeletePageTemplate(templateId);
        }

        /// <summary>
        /// 通过ChannelId获取详细页的模板信息
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public PageTemplate GetDetailPageTemplateIdByChannelId(string channelId)
        {
            return dal.GetDetailPageTemplateIdByChannelId(channelId);
        }
    }
}
