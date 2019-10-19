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
    public class AjaxArticle
    {
        NewsBLL newsBLL = Factory.BusinessFactory.CreateBll<NewsBLL>();
        NewsContentBLL newsContentBLL = Factory.BusinessFactory.CreateBll<NewsContentBLL>();
        ChannelBLL channelBLL = Factory.BusinessFactory.CreateBll<ChannelBLL>();
        SqlWhereList where = new SqlWhereList();
        PagingInfo PageInfo = new PagingInfo();


        /// <summary>
        /// 添加/修改新文章
        /// </summary>
        /// <param name="news"></param>
        /// <param name="operateType">add:新增</param>
        /// <returns></returns>
        [Action]
        public string AddEditArticle(News news, string operateType,string blod,int checkAbstract)
        {
            
            if (string.IsNullOrEmpty(news.Title))
            {
                return "000001";   //标题不能为空
            }
            var channel = channelBLL.Get("ChannelId", news.ChannelId);
            if (news.ChannelId == "0" || channel == null)
            {
                return "000002";  //分类有误
            }
            //if (string.IsNullOrEmpty(news.Author))
            //{
            //    return "000004"; //来源不能为空
            //}
            if (string.IsNullOrEmpty(news.Content))
            {
                return "000003";  //内容不能为空
            }
            if (checkAbstract == 0) //自动获取摘要
            {
                if (string.IsNullOrEmpty(news.NewsAbstract)) { 
                    if (!string.IsNullOrEmpty(news.Content))
                    {
                        news.NewsAbstract = Regex.Replace(news.Content, @"<.+?>|&(.+?);|\n|-|—|chr(.+?)", "", RegexOptions.IgnoreCase);
                        if (news.NewsAbstract.Length > 100)
                        {
                            news.NewsAbstract = news.NewsAbstract.Substring(0, 100);
                        }
                    }
               }
            }

            string absoluteUrl = "http://img.pinyifu.com";
            news.Content= Regex.Replace(news.Content, "<(.*?)(src|href)=\"(?!http)(.*?)\"(.*?)>", "<$1$2=\"" + absoluteUrl + "$3\"$4>",RegexOptions.IgnoreCase | RegexOptions.Multiline);
            //return value.Replace(absoluteUrl + "/", absoluteUrl);
            //news.Content = news.Content;
            if (string.IsNullOrEmpty(blod))
                news.IsBold = (0).ToString();  
            else
                news.IsBold = (1).ToString();

            if (string.IsNullOrEmpty(news.CreatedTime.ToString()))
            {
                news.CreatedTime = DateTime.Now;
            }
            if (operateType == "add")
            {

                news.OrderDay = string.Format("{0:yyyyMMdd}", news.CreatedTime);
                news.OrderTime = string.Format("{0:HHmmss}", news.CreatedTime);
                news.Url = string.Format("/{0}/{1:yyyyMM}/{1:ddHHmmss}{2:fff}.html", channel.ChannelEnName, news.CreatedTime, DateTime.Now);
                news.SecondUrl = news.Url;
                news.SecondTitle = news.Title;
            }
            else
            {
                if (string.IsNullOrEmpty(news.Url))
                {
                    news.Url = news.SecondUrl;
                }
                news.UpdatedTime = DateTime.Now;
            }

            string strForeCast = "";
            if(!string.IsNullOrEmpty(news.ForeCast))
            {
                string[] str;
                str = news.ForeCast.Replace("\n", "@").Split('@');
                for (int i=0; i < str.Length; i++)
                {
                    if(i==str.Length-1)
                        strForeCast += str[i];
                    else
                        strForeCast += str[i] + "|";
                } 
            }

            news.ForeCast = strForeCast;

            int result = newsBLL.AddOrEdit(news, operateType);
          //  Loger.Info(result.ToString() + "----------------");
            HtmlServer.CreateSingleNews(result, operateType);
            if (!string.IsNullOrEmpty(news.lmid))
            {
                RelatedEmptyArticle(result, news.lmid, operateType);
            }
            return result.ToString(); //添加/修改成功           
        }

        [Action]
        public void RelatedEmptyArticle(int newsId, string lmid, string operateType)
        {
             newsBLL.IsLm(newsId,lmid);
            News news = new News();
            news = newsBLL.Get("NewsId", newsId);
            string[] strlm; 
            strlm = lmid.Split(',');
            for (var i = 0; i < strlm.Length; i++ )
            {
                news.ChannelId = strlm[i];
                news.Sort = 2;
                news.ParentNewsId = newsId;
                news.IsRetinue = true;
                news.TitleColor = "#333333";
                if (!newsBLL.IsLmExist(newsId, strlm[i]))
                {
                    newsBLL.AddOrEdit(news, "add");
                 }
                else
                {
                    newsBLL.AddOrEdit(news, operateType);
                   
                }
                HtmlServer.CreateTemplatePageByChannelId(strlm[i]);
            }
              
        }
        /// <summary>
        /// 获取栏目关联数据
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        [Action]
        public string GetLmRelated(int newsId)
        {
            string lmstr = "";
            List<News> list=newsBLL.GetLmRelated(newsId);
            int i = 0;
            if (list != null)
            {
                foreach (News ch in list)
                {
                    if (i == list.Count - 1)
                    {
                        lmstr += ch.ChannelId;
                    }
                    else
                    {
                        lmstr += ch.ChannelId + ",";
                    }
                    i++;
                }
            }
     
            return lmstr;
        }


        /// <summary>
        /// 一键选择ID生成
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="OperateType"></param>
        /// <returns></returns>

        [Action]
        public string MakeArticle(string newsId, string OperateType)
        {
          //  string[] arrId;
         //   arrId = newsId.Split(',');
         //   foreach (var i in arrId)
        //    {
                List<News> news = newsBLL.MakeArticle(newsId);
                for (var i = 0; i < news.Count; i++)
                {
                var channel = channelBLL.Get("ChannelId", news[i].ChannelId);
                //news.OrderDay = string.Format("{0:yyyyMMdd}", news.CreatedTime);
                //news.OrderTime = string.Format("{0:HHmmss}", news.CreatedTime);
                //news.OrderNum = 60;

                if (string.IsNullOrEmpty(news[i].Url))
                {
                    var createdTime = news[i].CreatedTime ?? DateTime.Now;
                    news[i].Url = string.Format("/{0}/{1:yyyyMM}/{1:ddHHmmss}{2:fff}.html", channel.ChannelEnName, createdTime, DateTime.Now);
                    newsBLL.UpdateNewsUrl(news[i].NewsId, news[i].Url);
                }
                HtmlServer.CreateSingleNews(news[i].NewsId, "add");
            }
            return "000000";

        }

        /// <summary>
        /// 按栏目生成文章
        /// </summary>
        /// <param name="ChannelId"></param>
        /// <returns></returns>
        [Action]
        public string MakeChannelIdArticle(string ChannelId)
        {
            List<News> news = newsBLL.GetChannelIdList(ChannelId);
            for (var i = 0; i < news.Count; i++)
            {
                var channel = channelBLL.Get("ChannelId", news[i].ChannelId);
                if (string.IsNullOrEmpty(news[i].Url))
                {
                    var createdTime = news[i].CreatedTime ?? DateTime.Now;
                    news[i].Url = string.Format("/{0}/{1:yyyyMM}/{1:ddHHmmss}{2:fff}.html", channel.ChannelEnName, createdTime, DateTime.Now);
                    newsBLL.UpdateNewsUrl(news[i].NewsId, news[i].Url);
                }
                HtmlServer.CreateSingleNews(news[i].NewsId, "add");
            }
            return "000000";

        }

        /// <summary>
        /// 生成未生成的资讯
        /// </summary>
        /// <param name="ChannelId"></param>
        /// <returns></returns>
        [Action]
        public string MakeNoArticle()
        {
            List<News> news = newsBLL.MakeNoArticle();
            for (var i = 0; i < news.Count; i++)
            {
                var channel = channelBLL.Get("ChannelId", news[i].ChannelId);
                if (string.IsNullOrEmpty(news[i].Url))
                {
                    var createdTime = news[i].CreatedTime ?? DateTime.Now;
                    news[i].Url = string.Format("/{0}/{1:yyyyMM}/{1:ddHHmmss}{2:fff}.html", channel.ChannelEnName, createdTime, DateTime.Now);
                    newsBLL.UpdateNewsUrl(news[i].NewsId, news[i].Url);
                }

                HtmlServer.CreateSingleNews(news[i].NewsId, "add");
            }
            return "000000";

        }

        /// <summary>
        /// 一键生成所有的
        /// </summary>
        /// <returns></returns>
        [Action]
        public string MakeAllArticle()
        {
            List<News> news = newsBLL.GetNewsIdList();
            for (var i = 0; i < news.Count; i++) {
                var channel = channelBLL.Get("ChannelId", news[i].ChannelId);
                if (string.IsNullOrEmpty(news[i].Url))
                {
                    var createdTime = news[i].CreatedTime ?? DateTime.Now;
                    news[i].Url = string.Format("/{0}/{1:yyyyMM}/{1:ddHHmmss}{2:fff}.html", channel.ChannelEnName, createdTime, DateTime.Now);
                    newsBLL.UpdateNewsUrl(news[i].NewsId, news[i].Url);
                }

               HtmlServer.CreateSingleNews(news[i].NewsId, "add");
        }
            return "000000";

        }

        /// <summary>
        ///  添加/编辑空文章
        /// </summary>
        /// <param name="news"></param>
        /// <param name="operateType"></param>
        /// <returns></returns>
        [Action]
        public string AddEditEmptyArticle(News news, string operateType,string blod)
        {
            string[] arraytitle;
            string[] arrayurl;

            if (string.IsNullOrEmpty(news.Title))
            {
                return "000001";   //标题不能为空
            }
            var channel = channelBLL.Get("ChannelId", news.ChannelId);
            if (news.ChannelId == "0" || channel == null)
            {
                return "000002";  //分类有误
            }
            arraytitle = news.Title.Split(',');
            arrayurl = news.Url.Split(',');
            if (string.IsNullOrEmpty(news.CreatedTime.ToString()))
            {
                news.CreatedTime = DateTime.Now;
            }
            if (operateType == "add")
            {
                news.Title = arraytitle[0];
                news.Url = arrayurl[0];
                if (string.IsNullOrEmpty(news.TitleColor))
                {
                    news.TitleColor = "";
                }
                news.OrderDay = string.Format("{0:yyyyMMdd}", news.CreatedTime);
                news.OrderTime = string.Format("{0:HHmmss}", news.CreatedTime);
            }
            else
            {
                news.Title = arraytitle[0];
                news.Url = arrayurl[0];
                news.UpdatedTime = DateTime.Now;
            }
            int result = newsBLL.AddOrEdit(news, operateType);
            HtmlServer.CreateTemplatePageByChannelId(news.ChannelId);
            return result.ToString(); //添加/修改成功
        }

      

        /// <summary>
        /// 添加/修改图片文章
        /// </summary>
        /// <param name="news"></param>
        /// <param name="operateType"></param>
        /// <returns></returns>
        [Action]
        public string AddEditPicArticle(News news, string operateType, string blod)
        {
         

            if (string.IsNullOrEmpty(news.Title))
            {
                return "000001";   //标题不能为空
            }
           
            var channel = channelBLL.Get("ChannelId", news.ChannelId);
            if (string.IsNullOrEmpty(news.Url))
            {
                return "000002";  //文章url不能为空
            }
            if (news.ChannelId == "0" || channel == null)
            {
                return "000003";  //分类有误
            }
            
           
           
           
            if (string.IsNullOrEmpty(blod))
            {
                news.IsBold = (0).ToString();
            }
            else
            {
                news.IsBold = (1).ToString();
            }
            if (string.IsNullOrEmpty(news.CreatedTime.ToString()))
            {
                news.CreatedTime = DateTime.Now;
            }
            if (operateType == "add")
            {
                news.OrderDay = string.Format("{0:yyyyMMdd}", news.CreatedTime);
                news.OrderTime = string.Format("{0:HHmmss}", news.CreatedTime);
                news.SecondUrl = news.Url;
                news.SecondTitle = news.Title;
            }
            else
            {
                news.UpdatedTime = DateTime.Now;
            }

            int result = newsBLL.AddOrEdit(news, operateType);
            HtmlServer.CreateTemplatePageByChannelId(news.ChannelId);
            return result.ToString(); //添加/修改成功             
        }

        /// <summary>
        /// 标题文章
        /// </summary>
        /// <param name="news"></param>
        /// <param name="operateType"></param>
        /// <param name="blod"></param>
        /// <returns></returns>
        [Action]
        public string AddEditTitleArticle(News news, string operateType, string blod)
        {

            if (string.IsNullOrEmpty(news.Title))
            {
                return "000001";   //标题不能为空
            }
            var channel = channelBLL.Get("ChannelId", news.ChannelId);
            if (news.ChannelId == "0" || channel == null)
            {
                return "000002";  //分类有误
            }
          
            if (string.IsNullOrEmpty(blod))
                news.IsBold = (0).ToString();
            else
                news.IsBold = (1).ToString();

            if (string.IsNullOrEmpty(news.CreatedTime.ToString()))
            {
                news.CreatedTime = DateTime.Now;
            }
            if (operateType == "add")
            {
                news.OrderDay = string.Format("{0:yyyyMMdd}", news.CreatedTime);
                news.OrderTime = string.Format("{0:HHmmss}", news.CreatedTime);
                if (string.IsNullOrEmpty(news.Url))
                    news.SecondUrl = "http://";
                else
                    news.SecondUrl = news.Url;
                news.SecondTitle = news.Title;
            }
            else
            {
                news.UpdatedTime = DateTime.Now;
            }

            int result = newsBLL.AddOrEdit(news, operateType);
            newsBLL.NewsStockRelation(result, news.StockCode);
            HtmlServer.CreateSingleNews(result, operateType);
            if (!string.IsNullOrEmpty(news.lmid))
            {
                RelatedEmptyArticle(result, news.lmid, operateType);
            }
            return result.ToString(); //添加/修改成功           
        }

       

        /// <summary>
        /// 列表页编辑文章
        /// </summary>
        /// <param name="str"></param>
        /// <param name="newsabstract"></param>
        /// <param name="ctime"></param>
        /// <returns></returns>
        [Action]
        public string EditListNews(News news)
        {
            var channel = channelBLL.Get("ChannelId", news.ChannelId);
            if (news.ChannelId == "0" || channel == null)
            {
                return "000002";  //分类有误
            }
            int result = newsBLL.EditListNews(news);
            HtmlServer.CreateTemplatePageByChannelId(news.ChannelId);
            return result.ToString(); //添加/修改成功
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
        public JsonResult SearchArticle(int? page, int rows, string txtKeyword, string txtStartDate, string txtEndDate, string ChannelId)
        {
       
            PagedResult<News> pagedResult = newsBLL.GetBackNewsList(page, rows, txtKeyword, txtStartDate, txtEndDate, ChannelId);
            PageInfo.PageIndex = page.HasValue ? page.Value - 1 : 0;
            PageInfo.PageSize = rows;
            PageInfo.TotalRecords = pagedResult.Total;
            //NewsModel.DataTable = pagedResult.Result;

            var result = new GridResult<News>(pagedResult.Result, pagedResult.Total);
            return new JsonResult(result);

        }

        /// <summary>
        /// 删除文章信息
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [Action]
        public string DelArticle(int newsId, string OperateType)
        {
        //    News news = new News();
           // news = newsBLL.Get("NewsId", newsId);
            newsBLL.NewsOperate(null,newsId,null,null,null, OperateType);
            //MakeRelateTemplateTwo(news.ChannelId,news.CreatedTime.ToString());
            return "000000";
        }
        /// <summary>
        /// 生成关联模版
        /// </summary>
        /// <param name="newsId"></param>
        public void MakeRelateTemplate(int newsId)
        {
            News news = new News();
            news = newsBLL.Get("NewsId", newsId);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("newsdate", news.CreatedTime.ToString());
            HtmlServer.CreateTemplatePageByChannelId(news.ChannelId,dic);
        }

        /// <summary>
        /// 生成关联模版
        /// </summary>
        /// <param name="newsId"></param>
        public void MakeRelateTemplateTwo(string channelId, string createTime)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("newsdate", createTime);
            HtmlServer.CreateTemplatePageByChannelId(channelId, dic);
        }
        /// <summary>
        /// 删除多条文章信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Action]
        public string DelAllArticle(string newsId, string OperateType)
        {
            newsBLL.NewsOperate(newsId, null, null, null, null, OperateType);
            string[] arrId;
            arrId = newsId.Split(',');
            foreach (var i in arrId)
            {
                MakeRelateTemplate(int.Parse(i.ToString()));
            }
            return "000000";
        }

        /// <summary>
        /// 公开/拒绝文章信息
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [Action]
        public string AuditArticle(int newsId,int status,string OperateType)
        {
            newsBLL.NewsOperate(null, newsId, status, null, null, OperateType);
            MakeRelateTemplate(newsId);
            return "000000";
        }


        /// <summary>
        /// 公开/拒绝多条文章信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Action]
        public string AuditBatchArticle(string newsId,string OperateType)
        {
            newsBLL.NewsOperate(newsId, null, null, null, null, OperateType);
            string[] arrId;
            arrId = newsId.Split(',');
            foreach (var i in arrId)
            {
                MakeRelateTemplate(int.Parse(i.ToString()));
            }
            return "000000";
        }


        /// <summary>
        /// 刷新时间
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [Action]
        public string RefreshTime(int newsId,string OperateType)
        {
            string OrderDay = string.Format("{0:yyyyMMdd}", DateTime.Now);
            newsBLL.NewsOperate(null,newsId,null,null,OrderDay,OperateType);
            // MakeRelateTemplate(newsId);
            return "000000";
        }

        /// <summary>
        /// 修改权重
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        [Action]
        public string EidtOrderNum(int newsId,int orderNum,string OperateType)
        {
            newsBLL.NewsOperate(null,newsId,null,orderNum,null,OperateType);
            MakeRelateTemplate(newsId);
            return "000000";
        }


        /// <summary>
        /// 获取文章单条信息
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        [Action]
        public object GetArticleInfo(int newsId)
        {
            return new JsonResult(newsBLL.GetNewsInfo(newsId));
        }

        /// <summary>
        /// 获取空文章信息
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        [Action]
        public object GetEmptyArticleInfo(int newsId)
        {
            return new JsonResult(newsBLL.Get("NewsId",newsId));
        }

        [Action]
        public object GetTzrlInfo(string dateTime)
        {
            return new JsonResult(newsBLL.GetTzrlInfo(dateTime));
        }

    }
}
