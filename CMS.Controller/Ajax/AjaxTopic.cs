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

namespace CMS.Controller
{
    [Authorize]
    public class AjaxTopic
    {
        
        TopicBLL topicBLL = Factory.BusinessFactory.CreateBll<TopicBLL>();
        TopicTypeBLL topicTypeBLL = Factory.BusinessFactory.CreateBll<TopicTypeBLL>();
        PageTemplateBLL pageTemplateBLL = Factory.BusinessFactory.CreateBll<PageTemplateBLL>();
        /// <summary>
        /// 新增专题类型
        /// </summary>
        /// <returns></returns>
        [Action]
        public string InsertTopicType(TopicType TopicType)
        {
            string result = "";
            result = topicTypeBLL.Add(TopicType).ToString();
            return result;
        }
        /// <summary>
        /// 获取模板的信息
        /// </summary>
        /// <returns></returns>
        [Action]
        public string GetTopicType()
        {
            string shtml = "";
           var list=pageTemplateBLL.GetAll();
           foreach (var item in list)
           {
               shtml += "<option value=\""+item.TemplateId+"\">"+item.TemplateName+"</option>";
           }
           return shtml;
        }
        /// <summary>
        /// 专题类型下拉数据
        /// </summary>
        /// <returns></returns>
        [Action]
        public string GetDrpTopicType()
        {
            string shtml = "";
            var list = topicTypeBLL.GetAll();
            if (list != null)
            {
                foreach (var item in list)
                {
                    shtml += "<option value=\"" + item.TopicTypeId + "\">" + item.TopicTypeName + "</option>";
                }
            }
            return shtml;
        }
        /// <summary>
        /// 新增专题
        /// </summary>
        /// <returns></returns>
        [Action]
        public string InsertTopic(Topic Topic)
        {
            string result ="";
            Topic.CreatedUser = UserCookies.AdminName;
            Topic.Status = "未发布";
            if (string.IsNullOrEmpty(Topic.CreatedTime.ToString()))
            {
                Topic.CreatedTime = DateTime.Now;
            }
            result = topicBLL.Add(Topic).ToString();
            return result;
        }

        /// <summary>
        /// 修改专题
        /// </summary>
        [Action]
        public string UpdateTopic(Topic Topic)
        {
            //Topic.CreatedUser = UserCookies.AdminName;
            //Topic.CreatedTime = DateTime.Now;
            string result = topicBLL.Update(Topic).ToString();
            return result;
        }
        [Action]
        public string GetTopicFabuById(int topicId)
        {
            string result = "";
            Topic topic = topicBLL.Get("TopicId", topicId);
            topic.PublishTime = DateTime.Now;
            topic.Status = "已发布";
            result = topicBLL.Update(topic).ToString();
            return result;
        }
        /// <summary>
        /// 删除专题
        /// </summary>
        /// <param name="Id">专题ID</param>
        /// <returns></returns>
        [Action]
        public int DeleteTopic(int topicId)
        {
            Topic Topic = new Topic();
            Topic.TopicId = topicId;
            int result = topicBLL.Delete(Topic);
            //return new RedirectResult("/Topic/Topiclist.aspx");
            return result;
        }

        /// <summary>
        /// 根据专题ID查找数据
        /// </summary>
        /// <param name="Id">专题ID</param>
        /// <returns>返回json对象</returns>
        [Action]
        public JsonResult GetTopicById(int topicId)
        {
            Topic Topic = topicBLL.Get("TopicId", topicId);
            JsonResult json = new JsonResult(Topic);
            return json;
        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <returns>返回json对象</returns>
        [Action]
        public JsonResult SearchTopic(string topicName, int topicTypeId, int? page, int rows)
        {
            PagingInfo PageInfo = new PagingInfo();
            if (!string.IsNullOrEmpty(topicName))
            {
                topicName = string.Format("%{0}%", topicName);
            }
            PagedResult<Topic> pagedResult = topicBLL.GetTopicList(topicName, topicTypeId, page, rows);
            PageInfo.PageIndex = page.HasValue ? page.Value - 1 : 0;
            PageInfo.PageSize = rows;
            PageInfo.TotalRecords = pagedResult.Total;
            var result = new GridResult<Topic>(pagedResult.Result, pagedResult.Total);
            return new JsonResult(result);
        }
        
        [Action]
        public string CreatePage(int topicId, DateTime? reDate)
        {
            Topic topic = topicBLL.GetTopic(topicId);
            Dictionary<string, string> dic = null;
            if (topic.TopicTypeGenerate != "最新")
            {
                dic = new Dictionary<string, string>();
                dic.Add("newsdate", topic.CreatedTime.ToString());
            }
            if (topic.TopicTypeGenerate == "每日")
                dic.Add("reDate", reDate.ToString());
            if(HtmlServer.CreateTemplatePage(topic.TemplateId, dic))
                return "发布成功";
            return "发布失败";
        }
    }
}
