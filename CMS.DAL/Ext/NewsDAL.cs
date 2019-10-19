using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace CMS.DAL
{
    public partial class NewsDAL
    {
        private const string up_News_Insert = "up_News_Insert";
        private const string up_News_Update = "up_News_Update";
        private const string up_News_GetPageList = "up_News_GetPageList";
        private const string up_News_GetNewsInfo = "up_News_GetNewsInfo";
        private const string up_News_Operate = "up_News_Operate";
        private const string up_News_Updatelist = "up_News_Updatelist";
        private const string up_News_GetPageListIn = "up_News_GetPageListIn";
        private const string up_News_MakeArticle = "up_News_MakeArticle";
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="News"></param>
        /// <returns></returns>
        public int AddNews(News News)
        {

            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_News_Insert);
            commandParameters[0].Value = News.ChannelId;  
            commandParameters[1].Value = News.Title; 
            commandParameters[2].Value = News.Author;
            commandParameters[3].Value = News.KeyWord;
            commandParameters[4].Value = News.NewsAbstract;
            commandParameters[5].Value = News.OrderDay;
            commandParameters[6].Value = News.OrderTime;
            commandParameters[7].Value = News.OrderNum;
            commandParameters[8].Value = News.Url;
            commandParameters[9].Value = News.Sort;
            commandParameters[10].Value = News.Status;
            commandParameters[11].Value = News.CreatedUser;
            commandParameters[12].Value = News.CreatedTime;
            commandParameters[13].Value = News.SecondTitle;
            commandParameters[14].Value = News.SecondUrl;
            commandParameters[15].Value = News.TitleColor;
            commandParameters[16].Value = News.IsBold;
            commandParameters[17].Value = News.ImgUrl;
            commandParameters[18].Value = News.Content;
            commandParameters[19].Value = News.Prefix;
            commandParameters[20].Value = News.IsRetinue;
            commandParameters[21].Value = News.ParentNewsId;
            commandParameters[22].Value = News.FileUrl;
            commandParameters[23].Value = News.StockCode;
            commandParameters[24].Value = News.Important;
            commandParameters[25].Value = News.Reliability;
            commandParameters[26].Value = News.Effect;
            commandParameters[27].Value = News.DeadLine;
            commandParameters[28].Value = News.LastValue;
            commandParameters[29].Value = News.ForeCast;
            commandParameters[30].Value = News.HUrl;
            commandParameters[31].Value = News.CUrl;
            commandParameters[32].Value = News.TabImgUrl;
            SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS,CommandType.StoredProcedure,up_News_Insert,commandParameters);
            return int.Parse(commandParameters[33].Value.ToString());
             
        }
        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <param name="News"></param>
        /// <returns></returns>
        public int EditNews(News News)
        {
            
            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_News_Update);
            commandParameters[0].Value = News.NewsId;
            commandParameters[1].Value = News.ChannelId;
            commandParameters[2].Value = News.Title;
            commandParameters[3].Value = News.Author;
            commandParameters[4].Value = News.KeyWord;
            commandParameters[5].Value = News.NewsAbstract;
            commandParameters[6].Value = News.OrderNum;
            commandParameters[7].Value = News.Sort;
            commandParameters[8].Value = News.Status;
            commandParameters[9].Value = News.CreatedTime;
            commandParameters[10].Value = News.UpdatedUser;
            commandParameters[11].Value = News.UpdatedTime;
            commandParameters[12].Value = News.SecondTitle;
            commandParameters[13].Value = News.SecondUrl;
            commandParameters[14].Value = News.TitleColor;
            commandParameters[15].Value = News.IsBold;
            commandParameters[16].Value = News.ImgUrl;
            commandParameters[17].Value = News.Content;
            commandParameters[18].Value = News.Prefix;
            commandParameters[19].Value = News.IsRetinue;
            commandParameters[20].Value = News.ParentNewsId;
            commandParameters[21].Value = News.FileUrl;
            commandParameters[22].Value = News.Url;
            commandParameters[23].Value = News.StockCode;
            commandParameters[24].Value = News.Important;
            commandParameters[25].Value = News.Reliability;
            commandParameters[26].Value = News.Effect;
            commandParameters[27].Value = News.DeadLine;
            commandParameters[28].Value = News.LastValue;
            commandParameters[29].Value = News.ForeCast;
            commandParameters[30].Value = News.HUrl;
            commandParameters[31].Value = News.CUrl;
            commandParameters[32].Value = News.TabImgUrl;
            SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_News_Update, commandParameters);
     
            return News.NewsId;

        }

        /// <summary>
        /// 更新文章列表页的文章
        /// </summary>
        /// <param name="News"></param>
        /// <returns></returns>
        public int EditListNews(News News)
        {

            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_News_Updatelist);
            commandParameters[0].Value = News.NewsId;
            commandParameters[1].Value = News.SecondTitle;
            commandParameters[2].Value = News.TitleColor;
            commandParameters[3].Value = News.SecondUrl;
            commandParameters[4].Value = News.IsBold;
            SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_News_Updatelist, commandParameters);
            return News.NewsId;

        }

        /// <summary>
        /// 获取文章信息
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public News GetNewsInfo(int newsId)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_News_GetNewsInfo);
            parms[0].Value = newsId;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS,CommandType.StoredProcedure,up_News_GetNewsInfo,parms))
            {
                if (dr.Read())
                {
                    return EntityHelper.GetEntityByDataReader<News>(dr, null);
                }
                return null;
            }
        }

        /// <summary>
        /// 选择生成资讯
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public List<News> MakeArticle(string newsId)
        {
            List<News> list;
            SqlParameter[] parms = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_News_MakeArticle);
            parms[0].Value = newsId;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_News_MakeArticle, parms))
            {
 
                    return list = EntityHelper.GetEntityListByDataReader<News>(dr, null);
          
            }
        }

        /// <summary>
        /// 根据栏目ID查询最新一条文章信息
        /// </summary>
        /// <param name="clsId"></param>
        /// <returns></returns>
        public List<News> GetNewsOneInfo(string clsId, int count)
        {
            List<News> list;
            string sql = "SELECT top " + count + " * FROM dbo.News a inner join dbo.NewsContent b ON a.NewsID=b.NewsID WHERE a.ChannelId=@clsId order by a.OrderDay desc,a.OrderNum desc,a.OrderTime desc";
            SqlParameter[] parsms = new SqlParameter[] {new SqlParameter("@clsId", clsId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
            {
                 list = EntityHelper.GetEntityListByDataReader<News>(dr, null); 
            }
            return list;
        }       

        /// <summary>
        /// 信息列表查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="txtKeyword"></param>
        /// <param name="txtStartDate"></param>
        /// <param name="txtEndDate"></param>
        /// <param name="ChannelId"></param>
        /// <param name="status">值为1000时，就是全选</param>
        /// <param name="totalFlag">是否查询总数的标志，1:查询总数,0:不查询总数</param>
        /// <param name="sort">0-空文章,1-新文章,2-图片文章。值为1000时，就全选</param>
        /// <returns></returns>
        public PagedResult<News> GetNewsList(int? page, int rows, string txtKeyword, string txtStartDate, string txtEndDate, string ChannelId, int status, int totalFlag, int sort)
        {

            if (!string.IsNullOrEmpty(txtEndDate))
            {
                txtEndDate = DateTime.Parse(txtEndDate).AddDays(1).ToShortDateString();
            }

            if (!string.IsNullOrEmpty(txtKeyword))
            {
                txtKeyword = "%" + txtKeyword + "%";
            }

            PagedResult<News> pagest = new PagedResult<News>();

            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_News_GetPageList);
            commandParameters[0].Value = rows; //pageSize 每页显示记录数
            commandParameters[1].Value = page;//pageindex 当前页码
            commandParameters[2].Value = status;
            commandParameters[3].Value = totalFlag;
            commandParameters[4].Value = ChannelId;
            commandParameters[5].Value = txtKeyword;
            commandParameters[6].Value = txtStartDate;
            commandParameters[7].Value = txtEndDate;
            commandParameters[8].Value = sort;
            using (DataSet ds = SqlHelper.ExecuteDataset(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_News_GetPageList, commandParameters))
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    pagest.Result = EntityHelper.GetEntityListByDT<News>(ds.Tables[0], null);
                }
                pagest.Total = int.Parse(commandParameters[9].Value.ToString());

            }
            return pagest;
        }

        /// <summary>
        /// 信息列表查询(支持多栏目ID查询)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="txtKeyword"></param>
        /// <param name="txtStartDate"></param>
        /// <param name="txtEndDate"></param>
        /// <param name="ChannelId"></param>
        /// <param name="status">值为1000时，就是全选</param>
        /// <param name="totalFlag">是否查询总数的标志，1:查询总数,0:不查询总数</param>
        /// <param name="sort">0-空文章,1-新文章,2-图片文章。值为1000时，就全选</param>
        /// <returns></returns>
        public PagedResult<News> GetNewsListIn(int? page, int rows, string txtKeyword, string txtStartDate, string txtEndDate, string ChannelId, int status, int totalFlag, int sort)
        {

            if (!string.IsNullOrEmpty(txtEndDate))
            {
                txtEndDate = DateTime.Parse(txtEndDate).AddDays(1).ToShortDateString();
            }

            if (!string.IsNullOrEmpty(txtKeyword))
            {
                txtKeyword = "%" + txtKeyword + "%";
            }

            PagedResult<News> pagest = new PagedResult<News>();

            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_News_GetPageListIn);
            commandParameters[0].Value = rows; //pageSize 每页显示记录数
            commandParameters[1].Value = page;//pageindex 当前页码
            commandParameters[2].Value = status;
            commandParameters[3].Value = totalFlag;
            commandParameters[4].Value = ChannelId;
            commandParameters[5].Value = txtKeyword;
            commandParameters[6].Value = txtStartDate;
            commandParameters[7].Value = txtEndDate;
            commandParameters[8].Value = sort;
            using (DataSet ds = SqlHelper.ExecuteDataset(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_News_GetPageListIn, commandParameters))
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    pagest.Result = EntityHelper.GetEntityListByDT<News>(ds.Tables[0], null);
                }
                pagest.Total = int.Parse(commandParameters[9].Value.ToString());

            }
            return pagest;
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
        public void NewsOperate(string newsBatchId,int? newsId,int? status,int? orderNum,string orderDay,string operateType)
        {
            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_News_Operate);
            commandParameters[0].Value = newsBatchId;
            commandParameters[1].Value = newsId;
            commandParameters[2].Value = status;
            commandParameters[3].Value = orderNum;
            commandParameters[4].Value = orderDay;
            commandParameters[5].Value = operateType;
            SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_News_Operate, commandParameters);
        }
        /// <summary>
        /// 获取栏目关联栏目
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public List<News> GetLmRelated(int newsId)
        {
            List<News> list;
            string sql = "select ChannelId from News where ParentNewsId=@newsId";
            SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@newsId", newsId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
            {
                list = EntityHelper.GetEntityListByDataReader<News>(dr, null);
            }
            return list;
        }

        public List<News> GetNewsIdList()
        {
            List<News> list;
            string sql = "select newsid,CreatedTime,ChannelId,Url from News";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql))
            {
                list = EntityHelper.GetEntityListByDataReader<News>(dr, null);
            }
            return list;
        }

        public List<News> MakeNoArticle()
        {
            List<News> list;
            string sql = "select newsid,CreatedTime,ChannelId,Url from News where url=''";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql))
            {
                list = EntityHelper.GetEntityListByDataReader<News>(dr, null);
            }
            return list;
        }

        public List<News> GetChannelIdList(string ChannelId)
        {
            List<News> list;
            string sql = "select newsid,CreatedTime,ChannelId,Url from News where ChannelId=@ChannelId";
            SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@ChannelId", ChannelId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
            {
                list = EntityHelper.GetEntityListByDataReader<News>(dr, null);
            }
            return list;
        }

        public void UpdateNewsUrl(int NewsId, string Url)
        {
            string sql = "update News set Url=@Url from News where NewsId=@NewsId";
            SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@Url", Url), new SqlParameter("@NewsId", NewsId) };
            SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.Text, sql, parsms);
        }


        /// <summary>
        /// 检查所选择栏目在表里是否存在，如果不存在就删除
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="ChannelId"></param>
        public void IsLm(int newsId, string ChannelId)
        {
            string sql, rsql;
            string RChannelId;
            sql = "select ChannelId  from News where ParentNewsId=@newsId ";
            SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@newsId", newsId) };
            using (DataSet ds = SqlHelper.ExecuteDataset(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
            {
                if (ds != null && ds.Tables[0].Rows.Count>0)
                {
                    for (var i = 0; i <= ds.Tables.Count-1; i++)
                    {
                        RChannelId = ds.Tables[0].Rows[i]["ChannelId"].ToString();
                        int result = ChannelId.IndexOf(RChannelId);
                        if (result < 0)
                        {
                            rsql = "delete from News where ParentNewsId=@newsId and ChannelId=@RChannelId";
                            SqlParameter[] rparsms = new SqlParameter[] { new SqlParameter("@newsId", newsId), new SqlParameter("@RChannelId", RChannelId) };
                            int query = SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.Text, rsql, rparsms);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 判断栏目是否存在，不存在添加一条，存在则更新。
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="ChannelId"></param>
        /// <returns></returns>
        public bool IsLmExist(int newsId, string ChannelId)
        {
            string sql;
            List<News> list;
            sql = "select ChannelId  from News where ParentNewsId=@newsId and ChannelId=@ChannelId  ";
            SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@newsId", newsId), new SqlParameter("@ChannelId", ChannelId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
            {
                list = EntityHelper.GetEntityListByDataReader<News>(dr, null);
                if(list!=null)
                {
                    if (list.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
        }

        public List<News> GetTzrlInfo(string dateTime)
        {
            List<News> list;
            DateTime Todate =Convert.ToDateTime(dateTime);
            string sql = "select * from News where DateDiff(dd,CreatedTime,@Todate)=0 order by OrderDay desc,OrderNum desc,OrderTime desc";
            SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@Todate", Todate)};
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
            {
                list = EntityHelper.GetEntityListByDataReader<News>(dr, null);
            }
            return list;
        }

        public void NewsStockRelation(int newsId, string stockCode)
        {
            string sql;
            string[] array;
            string[] arrsc;
            if (!string.IsNullOrEmpty(stockCode))
            {
                array = stockCode.Split(',');
                foreach (string i in array)
                {
                    arrsc = i.ToString().Split('|');
                    var aa= arrsc.Length;
                    if (arrsc.Length > 1)
                    {
                        sql = "insert into NewsStockRelation(NewsId,StockCode) values(@newsId,@stockCode)";
                        SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@newsId", newsId), new SqlParameter("@stockCode", arrsc[0]) };
                        SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.Text, sql, parsms);
                    }
                }
            }
          
        }


        //栏目列表
        public List<Channel> GetChannelList(string channelId) 
        {
            string sql = "select ChannelId, ChannelName from Channel";
            if (!string.IsNullOrEmpty(channelId))
                sql += " where ChannelId = @ChannelId";
            SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@ChannelId", channelId) };
            List<Channel> list;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql,parsms))
            {
                list = EntityHelper.GetEntityListByDataReader<Channel>(dr, null);
            }

            return list;
        }

        //用户列表
        public List<Administrator> GetAdministrator(int adminId)
        {
            int groupId = 4;
            string sql = "select AdminId,AdminName from Administrator where GroupId=@GroupId";
            if (adminId!=0)
                sql += " and AdminId = @AdminId";
            SqlParameter[] para=new SqlParameter[]{new SqlParameter("@GroupId",groupId),
                                new SqlParameter("@AdminId",adminId)

            };

            List<Administrator> list;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql,para))
            {
                list = EntityHelper.GetEntityListByDataReader<Administrator>(dr, null);
            }

            return list;
        }

        /// <summary>
        /// 统计每日编辑的资讯数量
        /// </summary>
        /// <param name="stratTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public int GetInformationCount(string stratTime, string endTime, string createdUser, string channelId)
        {
            string sql = "select sum(News) from News where";
            if (!string.IsNullOrEmpty(endTime))
                sql += " CreatedTime <=@End";
            if (createdUser != "0")
                sql += " and CreatedUser = @CreatedUser";
            if (channelId != "0")
                sql += " and ChannelId = @ChannelId";
            if (!string.IsNullOrEmpty(stratTime))
                sql += " and CreatedTime >@Start";
            
            

            SqlParameter[] parsms = new SqlParameter[]{
                    new SqlParameter("@CreatedUser",createdUser),
                    new SqlParameter("@ChannelId",channelId),
                    new SqlParameter("@Start",stratTime),
                    new SqlParameter("@End",endTime)
                };
            object obj = SqlHelper.ExecuteScalar(SqlConnectFactory.CMS, CommandType.Text, sql, parsms);
            if (obj != null)
                return int.Parse(obj.ToString());
            return 0;
        }

        /// <summary>
        /// 查询信息
        /// </summary>
        /// <param name="stratTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public List<News> GetInformation(string stratTime, string endTime, string createdUser, string channelId)
        {
            int groupId = 4;
            string sql = "select c.ChannelName, n.CreatedUser, sum(nc.ClickNum) as Num,COUNT(n.NewsId) as con from News as n, Channel as c, NewsContent as nc where n.ChannelId=c.ChannelId and n.newsId=nc.NewsId";
            if (createdUser != "0")
            {
                sql += " and n.CreatedUser = @CreatedUser";
            }
            else {
                sql += " and n.CreatedUser in(select AdminName from Administrator where GroupId=@GroupId)";
            }
            if (channelId != "0")
                sql += " and n.ChannelId = @ChannelId";
            if (!string.IsNullOrEmpty(stratTime))
                sql += " and n.CreatedTime >@Start";
            if (!string.IsNullOrEmpty(endTime))
                sql += " and n.CreatedTime <=@End group by c.ChannelName, n.CreatedUser";


            SqlParameter[] parsms = new SqlParameter[]{
                    new SqlParameter("@CreatedUser",createdUser),
                    new SqlParameter("@ChannelId",channelId),
                    new SqlParameter("@GroupId",groupId),
                    new SqlParameter("@Start",stratTime),
                    new SqlParameter("@End",endTime)
                };

            List<News> list;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
            {
                list = EntityHelper.GetEntityListByDataReader<News>(dr, null);
            }

            return list;
        }

        public News GetQueryInfo(string keywords,string ChannelId)
        {
            string sql = "select News.Title,NewsContent.Content from News left join NewsContent on News.NewsId =NewsContent.NewsId where Title=@keywords and ChannelId=@ChannelId";
            SqlParameter[] parsms = new SqlParameter[]{
                    new SqlParameter("@keywords",keywords),
                     new SqlParameter("@ChannelId",ChannelId)
                };
 
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
            {
                if (dr.Read())
                {
                    return EntityHelper.GetEntityByDataReader<News>(dr, null);
                }
                return null;
            }

            
        }

    }
}
