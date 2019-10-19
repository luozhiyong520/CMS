using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMVC;
using CMS.BLL;
using CMS.ViewModel;
using Common;
using CMS.Utility;
using CMS.Model;


namespace CMS.Controller
{
    [Authorize]
    public class AjaxPopup
    {
        CustomerGroupBLL groupBll = Factory.BusinessFactory.CreateBll<CustomerGroupBLL>();
        NewsBLL newsBLL = Factory.BusinessFactory.CreateBll<NewsBLL>();
        NewsContentBLL newsContentBLL = Factory.BusinessFactory.CreateBll<NewsContentBLL>();
        PopupMsgPlanBLL opupMsgPlanBll = Factory.BusinessFactory.CreateBll<PopupMsgPlanBLL>();
        NewsPopupBLL newsPopupBll = Factory.BusinessFactory.CreateBll<NewsPopupBLL>();

        /// <summary>
        /// 广告推送
        /// </summary>
        /// <param name="pop"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [Action]
        public int AddPopup(PopupMsgPlan pop, string type)
        {
            if (type == "add")
            {
                pop.PopupType = 1;
                pop.DataType = "广告弹窗";
                if (pop.Content == "" || pop.Content == null)
                    pop.PopupType = 0;
                pop.Status = 0;
                pop.CreatedTime = DateTime.Now;
                pop.Editor = UserCookies.AdminName;
                //不设置开始时间，默认当前时间
                if (pop.BeginTime == null)
                    pop.BeginTime = DateTime.Now;
                //不设置时效的情况下，默认时效为30分钟
                if (pop.EndTime == null)
                    pop.EndTime = Convert.ToDateTime(pop.BeginTime).AddMinutes(30);
                int res = opupMsgPlanBll.Add(pop);
                SsoServer.SsoPush(res,"0");
                return res;
            }
            return 0;
        }
        
        /// <summary>
        /// 资讯推送
        /// </summary>
        /// <param name="pop"></param>
        /// <param name="type"></param>
        /// <param name="NewsId"></param>
        /// <returns></returns>
        [Action]
        public int AddPopupzx(PopupMsgPlan pop, string type, int NewsId)
        {
           News news=newsBLL.Get("NewsId", NewsId);
           NewsContent newsContent = newsContentBLL.Get("NewsId", NewsId);
           pop.Title = news.Title;
           pop.ImgUrl = news.ImgUrl;
           pop.PageUrl = "id=" + news.NewsId;
           pop.NewsId = news.NewsId;
           pop.Content = string.IsNullOrEmpty(news.NewsAbstract) ? (newsContent.Content.Length > 90 ? StringHelper.NoHTML(newsContent.Content.Substring(0, 90).Trim()) : newsContent.Content) : (news.NewsAbstract.Length > 90 ? news.NewsAbstract.Substring(0, 90).Trim() : news.NewsAbstract.Trim());

            if (type == "add")
            {
                pop.PopupType = 1;
                //if (pop.Content == "" || pop.Content == null)
                //    pop.PopupType = 0;
                pop.Status = 0;
                pop.CreatedTime = DateTime.Now;
                pop.Editor = UserCookies.AdminName;
                
                //不设置开始时间，默认当前时间
                if (pop.BeginTime == null)
                    pop.BeginTime = DateTime.Now;
                //不设置时效的情况下，默认时效为30分钟
                if (pop.EndTime == null)
                    pop.EndTime = Convert.ToDateTime(pop.BeginTime).AddMinutes(30);
                int res = opupMsgPlanBll.Add(pop);
                if (res > 0)
                {
                    NewsPopup np = new NewsPopup();
                    np.NewsId = NewsId;
                    np.Title = news.Title;
                    np.Author = news.Author;
                    np.CreatedTime = news.CreatedTime;
                    np.PushColumn = pop.PushColumn;
                    newsPopupBll.Add(np);
                }

                SsoServer.SsoPush(res, pop.PushVersion);
                return res;
            }
            return 0;
        }
        [Action]
        public string GetGroup()
        {
            List<CustomerGroup> listGroup = groupBll.GetAll("GroupName", "group by GroupName");
            StringBuilder sb = new StringBuilder();

            if (listGroup != null && listGroup.Count > 0)
            {
                foreach (var item in listGroup)
                {
                    sb.Append("," + item.GroupName);
                }
                return sb.ToString().Substring(1);
            }
            else
                return "";
            
        }
    }
}
