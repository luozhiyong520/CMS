using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMVC;
using CMS.BLL;
using CMS.ViewModel;
using CMS.Model;

namespace CMS.Controller
{
    [Authorize]
    public class NewsController
    {
        NewsBLL newsBll = Factory.BusinessFactory.CreateBll<NewsBLL>();

        [Action]
        [PageUrl(Url = "/news/editarticle.aspx")]
        [PageUrl(Url = "/news/editemptyarticle.aspx")]
        [PageUrl(Url = "/news/editpicarticle.aspx")]
        [PageUrl(Url = "/news/edittitlearticle.aspx")]
        public object GetEditArticle(int newsId)
        {
            var news = newsBll.GetNewsInfo(newsId);
            var model = new ArticlePageModel();
            model.News = news;
            model.Channels = new AjaxChannel().GetChannelNodeSelected(1, news.ChannelId);
            model.Prefix = new AjaxTitlePrefix().GetChildInit(newsId);
            return new PageResult(null, model);
        }      
    }
}
