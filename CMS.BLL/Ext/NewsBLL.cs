using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.DAL;
using System.Data;
using Common;
using CMS.Model;
using System.Text.RegularExpressions;
namespace CMS.BLL
{
    public partial class NewsBLL
    {
        
        /// <summary>
        /// 添加/编辑新文章/空文章/图片文章
        /// </summary>
        /// <param name="News"></param>
        /// <returns></returns>
        public int AddOrEdit(News news,string operateType)
        {
            try
            {
                if (operateType == "add")
                    return dal.AddNews(news);
                else
                    return dal.EditNews(news);
            }
            catch (Exception ex)
            {
                Loger.Error(ex);
                return 0;
            }
        }
 

        /// <summary>
        /// 列表页编辑文章
        /// </summary>
        /// <param name="News"></param>
        /// <returns></returns>
        public int EditListNews(News news)
        {
            return dal.EditListNews(news);
        }


        /// <summary>
        /// 获取新文章信息
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public News GetNewsInfo(int newsId)
        {
            return dal.GetNewsInfo(newsId);
        }

        /// <summary>
        /// 选择生成资讯
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public List<News> MakeArticle(string newsId)
        {
            return dal.MakeArticle(newsId);
        }
        /// <summary>
        /// 根据栏目ID获取最新一条文章信息
        /// </summary>
        /// <param name="clsId"></param>
        /// <returns></returns>
        public List<News> GetNewsOneInfo(string clsId, int count)
        {
            return dal.GetNewsOneInfo(clsId, count);
        }
        /// <summary>
        /// 获取栏目关联栏目
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public List<News> GetLmRelated(int newsId)
        {
            return dal.GetLmRelated(newsId);
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
        public PagedResult<News> GetBackNewsList(int? page, int rows, string txtKeyword, string txtStartDate, string txtEndDate, string ChannelId)
        {
            PagedResult<News> st = dal.GetNewsList(page, rows, txtKeyword, txtStartDate, txtEndDate, ChannelId, 1000, 1, 1000);
            return st;
        }

       

        /// <summary>
        /// 查询前台的新闻列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="txtStartDate"></param>
        /// <param name="txtEndDate"></param>
        /// <param name="ChannelId"></param>
        /// <param name="totalFlag">否查询总数的标志，1:查询总数,0:不查询总数</param>
        /// <param name="sort">0-空文章,1-新文章,2-图片文章。值为1000时，就全选</param>
        /// <returns></returns>
        public PagedResult<News> GetFrontNewsList(int? page, int rows, string txtStartDate, string txtEndDate, string ChannelId,int totalFlag,int sort)
        {
            PagedResult<News> st = dal.GetNewsList(page, rows, "", txtStartDate, txtEndDate, ChannelId, 1, totalFlag, sort);
            return st;
        }

        /// <summary>
        /// 查询前台的新闻列表(支持多栏目ID查询)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="txtStartDate"></param>
        /// <param name="txtEndDate"></param>
        /// <param name="ChannelId"></param>
        /// <param name="totalFlag">否查询总数的标志，1:查询总数,0:不查询总数</param>
        /// <param name="sort">0-空文章,1-新文章,2-图片文章。值为1000时，就全选</param>
        /// <returns></returns>
        public PagedResult<News> GetFrontNewsListIn(int? page, int rows, string txtStartDate, string txtEndDate, string ChannelId, int totalFlag, int sort)
        {
            PagedResult<News> st = dal.GetNewsListIn(page, rows, "", txtStartDate, txtEndDate, ChannelId, 1, totalFlag, sort);
            return st;
        }

        /// <summary>
        /// 文章管理细节操作
        /// </summary>
        /// <param name="newsBatchId"></param>
        /// <param name="newsId"></param>
        /// <param name="status"></param>
        /// <param name="orderNum"></param>
        /// <param name="orderDay"></param>
        /// <param name="operateType"></param>
        public void NewsOperate(string newsBatchId, int? newsId, int? status, int? orderNum, string orderDay, string operateType)
        {
            dal.NewsOperate(newsBatchId, newsId, status, orderNum, orderDay, operateType);
        }

        /// <summary>
        /// 检查所选择栏目在表里是否存在，如果不存在就删除
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="ChannelId"></param>
        public void IsLm(int newsId, string ChannelId)
        {
            dal.IsLm(newsId,ChannelId);
        }

        public bool IsLmExist(int newsId, string ChannelId)
        {
            return dal.IsLmExist(newsId, ChannelId);
        }
        /// <summary>
        /// 获取投资日历每个日期信息
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public List<News> GetTzrlInfo(string dateTime)
        {
            return dal.GetTzrlInfo(dateTime);
        }

        public void NewsStockRelation(int newsId,string stockCode)
        {
            dal.NewsStockRelation(newsId, stockCode);
        }

        //获取栏列表
        public List<Channel> GetChannel(string channelId)
        {
            return dal.GetChannelList(channelId);
        }

        //获取用户列表
        public List<Administrator> GetAdministratorList(int adminId)
        {
            return dal.GetAdministrator(adminId);
        }

        //统计次数
        public int GetInformationCount(string stratTime, string endTime, string createdUser, string channelId)
        {
            return dal.GetInformationCount(stratTime, endTime, createdUser, channelId);
        }

        //查询信息
        public List<News> GetInformation(string stratTime, string endTime, string createdUser, string channelId)
        {
            return dal.GetInformation(stratTime, endTime, createdUser, channelId);
        }

        public News GetQueryInfo(string keywords,string ChannelId)
        {
            return dal.GetQueryInfo(keywords, ChannelId);
        }

        public List<News> GetNewsIdList()
        {
            return dal.GetNewsIdList();
        }

        public List<News> MakeNoArticle()
        {
            return dal.MakeNoArticle();
        }

        public List<News> GetChannelIdList(string ChannelId)
        {
            return dal.GetChannelIdList(ChannelId);
        }

        public void UpdateNewsUrl(int NewsId,string Url)
        {
            dal.UpdateNewsUrl(NewsId,Url);
        }
    }
}
