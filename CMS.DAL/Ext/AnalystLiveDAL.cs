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
   public partial class AnalystLiveDAL
    {
       private const string up_AnalystLive_Insert = "up_AnalystLive_Insert";
       private const string up_AnalystLive_Update = "up_AnalystLive_Update";
       private const string up_AnalystLive_GetPageList = "up_AnalystLive_GetPageList";
       private const string up_TransStatistics = "up_TransStatistics";
       private const string up_TransStatisticsAll = "up_TransStatisticsAll";
       /// <summary>
       /// 添加直播分析信息
       /// </summary>
       /// <param name="analystLive"></param>
       /// <returns></returns>
       public int AddAnalystLive(AnalystLive analystLive, int AnalystType)
       {
           if (analystLive.LiveType == 2)
           {
              analystLive.ProfitRate =EditKcInfo(analystLive.BuyLiveId, analystLive.TransPrice);
           }

           SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_AnalystLive_Insert);
           commandParameters[0].Value = analystLive.LiveType;
           commandParameters[1].Value = analystLive.ActualName;
           commandParameters[2].Value = analystLive.ActualCode;
           commandParameters[3].Value = analystLive.TransPrice;
           commandParameters[4].Value = analystLive.TransType;
           commandParameters[5].Value = analystLive.StopProfit;
           commandParameters[6].Value = analystLive.StopLoss;
           commandParameters[7].Value = analystLive.CreateTime;
           commandParameters[8].Value = analystLive.AnalystId;
           commandParameters[9].Value = analystLive.AnalystName;
           commandParameters[10].Value = analystLive.Info;
           commandParameters[11].Value = analystLive.ImgUrl;
           commandParameters[12].Value = analystLive.LinkUrl;
           commandParameters[13].Value = analystLive.BuyLiveId;
           commandParameters[14].Value = analystLive.ProfitRate;
           commandParameters[15].Value = analystLive.IsSell;
           SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_AnalystLive_Insert, commandParameters);
           ToatalV(analystLive.LiveType, analystLive.AnalystId, analystLive.AnalystName, AnalystType);
           return int.Parse(commandParameters[16].Value.ToString());


       }
       

       /// <summary>
       /// 获取开多/开空/盈利/亏损/成功次数/比例
       /// </summary>
       /// <param name="AnalystId"></param>
       /// <returns></returns>
       public AnalystLive GetTransStatisticsNum(int AnalystId)
       {
           string sql="";
           AnalystLive model= new AnalystLive();
           SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@AnalystId", AnalystId) };
           sql = "select ProfitNum,LossNum,SuccessRate,MarkupBuyNum,FallBuyNum from TransStatistics where AnalystId=@AnalystId";
           using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
           {
               if (dr.Read())
               {
                   model.ProfitNum = int.Parse(dr["ProfitNum"].ToString());
                   model.LossNum = int.Parse(dr["LossNum"].ToString());
                   model.SuccessRate = Decimal.Parse(dr["SuccessRate"].ToString());
                   model.BigNum = int.Parse(dr["MarkupBuyNum"].ToString());
                   model.EmptyNum = int.Parse(dr["FallBuyNum"].ToString());

               }
           }
           return model;
       }
       

       /// <summary>
       /// 平仓时更改开仓的是否平仓状态/算出盈亏比例
       /// </summary>
       /// <param name="LiveType"></param>
       public decimal EditKcInfo(int? BuyLiveId,decimal? TransPrice)
       {
           string sql = "UPDATE AnalystLive SET IsSell = 1 WHERE LiveId = @BuyLiveId";
           SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@BuyLiveId", BuyLiveId) };
           SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.Text, sql,parsms);

           decimal ProfitRate = 0;
           string TransType="";
           string tsql = "select TransType,TransPrice from AnalystLive where liveId=@BuyLiveId";
           SqlParameter[] tparsms = new SqlParameter[] { new SqlParameter("@BuyLiveId", BuyLiveId) };
           using (DataSet ds = SqlHelper.ExecuteDataset(SqlConnectFactory.CMS, CommandType.Text, tsql, tparsms))
           {
               if (ds != null && ds.Tables[0].Rows.Count > 0)
               {
                   TransType = ds.Tables[0].Rows[0][0].ToString();
                   if (TransType == "买入订立" || TransType == "买入开仓")
                   {

                       ProfitRate = Convert.ToDecimal((TransPrice - Convert.ToDecimal(ds.Tables[0].Rows[0][1].ToString())) / Convert.ToDecimal(ds.Tables[0].Rows[0][1].ToString())) * 100;
                       return Math.Round(ProfitRate, 4, MidpointRounding.AwayFromZero);
                   }
                   else
                   {
                       ProfitRate = Convert.ToDecimal((Convert.ToDecimal(ds.Tables[0].Rows[0][1].ToString()) - TransPrice) / Convert.ToDecimal(ds.Tables[0].Rows[0][1].ToString())) * 100;
                       return Math.Round(ProfitRate, 4, MidpointRounding.AwayFromZero);
                   }
               }
               else
               {
                   
                   return Math.Round(ProfitRate, 4, MidpointRounding.AwayFromZero)*100;
               }

           }
          
       }

       /// <summary>
       /// 编辑直播分析
       /// </summary>
       /// <param name="analystLive"></param>
       /// <returns></returns>
       public int EditAnalystLive(AnalystLive analystLive, int AnalystType)
       {
           if (analystLive.LiveType == 2)
           {
               analystLive.ProfitRate = EditKcInfo(analystLive.BuyLiveId, analystLive.TransPrice);
           }
           SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_AnalystLive_Update);
           commandParameters[0].Value = analystLive.LiveId;
           commandParameters[1].Value = analystLive.LiveType;
           commandParameters[2].Value = analystLive.ActualName;
           commandParameters[3].Value = analystLive.ActualCode;
           commandParameters[4].Value = analystLive.TransPrice;
           commandParameters[5].Value = analystLive.TransType;
           commandParameters[6].Value = analystLive.StopProfit;
           commandParameters[7].Value = analystLive.StopLoss;
           commandParameters[8].Value = analystLive.CreateTime;
           commandParameters[9].Value = analystLive.AnalystId;
           commandParameters[10].Value = analystLive.AnalystName;
           commandParameters[11].Value = analystLive.Info;
           commandParameters[12].Value = analystLive.ImgUrl;
           commandParameters[13].Value = analystLive.LinkUrl;
           commandParameters[14].Value = analystLive.BuyLiveId;
           commandParameters[15].Value = analystLive.ProfitRate;
           commandParameters[16].Value = analystLive.IsSell;
           SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_AnalystLive_Update, commandParameters);
           if (analystLive.LiveType == 2)
           {
               ToatalV(analystLive.LiveType, analystLive.AnalystId, analystLive.AnalystName, AnalystType);
           }
           return analystLive.LiveId;

       }

       /// <summary>
       /// 获取未平仓的商品
       /// </summary>
       /// <returns></returns>
       public List<AnalystLive> GetIsSellList(int AnalystId)
       {
           List<AnalystLive> list;
           string sql = "SELECT LiveId,ActualCode,ActualName,TransPrice,TransType FROM AnalystLive WHERE IsSell=0 and  AnalystId=@AnalystId order by CreateTime desc";
           SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@AnalystId", AnalystId) };
           using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql,parsms))
           {
               list = EntityHelper.GetEntityListByDataReader<AnalystLive>(dr, null);
           }
           return list;
       }

       /// <summary>
       /// 判断开仓的商品已开仓过没平仓
       /// </summary>
       /// <param name="ActualCode"></param>
       /// <param name="AnalystId"></param>
       /// <returns></returns>
       //public bool IsSell(string ActualCode, int AnalystId)
       //{
       //    string sql;
       //    List<AnalystLive> list;
       //    sql = "select LiveId from AnalystLive where ActualCode=@ActualCode and AnalystId=@AnalystId and IsSell=0";
       //    SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@ActualCode", ActualCode), new SqlParameter("@AnalystId", AnalystId) };
       //    using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parsms))
       //    {
       //        list = EntityHelper.GetEntityListByDataReader<AnalystLive>(dr, null);
       //        if (list != null)
       //        {
       //            if (list.Count > 0)
       //            {
       //                return true;
       //            }
       //            else
       //            {
       //                return false;
       //            }
       //        }
       //        return false;
       //    }
       //}



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
       public PagedResult<AnalystLive> GetAnalystLiveList(int? page, int rows, int AnalystId, int totalFlag, int AnalystType)
       {
           PagedResult<AnalystLive> pagest = new PagedResult<AnalystLive>();

           SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_AnalystLive_GetPageList);
           commandParameters[0].Value = rows; //pageSize 每页显示记录数
           commandParameters[1].Value = page;//pageindex 当前页码
           commandParameters[2].Value = AnalystId;//分析师ID
           commandParameters[3].Value = totalFlag;
           commandParameters[4].Value = AnalystType;
           using (DataSet ds = SqlHelper.ExecuteDataset(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_AnalystLive_GetPageList, commandParameters))
           {
               if (ds != null && ds.Tables.Count > 0)
               {
                   pagest.Result = EntityHelper.GetEntityListByDT<AnalystLive>(ds.Tables[0], null);
                  
               }
               pagest.Total = int.Parse(commandParameters[5].Value.ToString());

           }
           return pagest;
       }
      
       /// <summary>
       /// 战绩统计入库
       /// </summary>
       /// <param name="LiveType"></param>
       /// <param name="TransType"></param>
       /// <param name="AnalystId"></param>
       /// <param name="AnalystName"></param>
       /// <param name="BuyLiveId"></param>
       public void ToatalV(int? LiveType, int? AnalystId, string AnalystName, int AnalystType)
       {
           SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, up_TransStatistics);
           commandParameters[0].Value = LiveType;  
           commandParameters[1].Value = AnalystId;//分析师ID
           commandParameters[2].Value = AnalystName;
           commandParameters[3].Value = AnalystType;
           SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_TransStatistics, commandParameters);
           SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.StoredProcedure, up_TransStatisticsAll, commandParameters);
       }
        

       /// <summary>
       /// 分析师最新操盘
       /// </summary>
       /// <returns></returns>
       public List<AnalystLive> GetAnalystLiveListNews(int count,string mkps)
       {
           string sql="";
           List<AnalystLive> list;
           //现货直播动态模块
           //if (mkps == "analystLiveMk")
           //{
           //    sql = "select top(" + count + ") * from AnalystLive  where LiveType<>3   order by CreateTime desc";
           //}
           // 获取现货粉丝关注动态列表，时间排序
           bool isGjs = false;
          switch(mkps) 
          {
             //普通版金蝴蝶粉丝列表
              case "gzfanslist":
                  sql = "select top(" + count + ")  A.* from AnalystFans as A left join Analyst as B on A.AnalystId=B.AnalystId where B.AnalystType=1 and B.AnalystStatus=1 and B.VipType=0 and   B.SoftVersion !=5001  order by CreateTime desc";
                 break;
              //普通版金蝴蝶获取现货人气名家排行榜
              case "actualmjorder":
                 sql = "select Top(" + count + ") t.*,tr.SuccessRate from Analyst t left join TransStatistics tr on t.AnalystId=tr.AnalystId  where t.AnalystType=1 and t.AnalystStatus=1  and t.VipType=0 and   t.SoftVersion !=5001  and tr.SType=2 order by t.FansNum desc";
                break;
              //普通版金蝴蝶现货月战绩榜
              case "successrateorder":
                sql = "select Top(" + count + ") t.*,tr.SuccessRate from Analyst t left join TransStatistics tr on t.AnalystId=tr.AnalystId where t.AnalystType=1 and t.AnalystStatus=1  and t.VipType=0  and   t.SoftVersion !=5001 and tr.SType=1 order by tr.SuccessRate desc";
                break;
              //普通版金蝴蝶获取现货20条数据时间排序
              case "analystlivelist":
                sql = "select Top(" + count + ")  A.*,B.SuccessRate,C.ImgUrl as AnalystImg, C.FansNum,C.Intro from ((AnalystLive as A left join TransStatistics as B on A.AnalystId=B.AnalystId) left join  Analyst as C on A.AnalystId=C.AnalystId) where 1 = 1 and C.AnalystType=1  and  C.AnalystStatus=1  and C.VipType=0 and  C.SoftVersion !=5001 and B.SType=2 order by CreateTime desc";
                break;
              //普通版金蝴蝶现货总战绩榜
              case "zzjtotal":
                sql = "select Top(" + count + ") t.*,tr.SuccessRate from Analyst t left join TransStatistics tr on t.AnalystId=tr.AnalystId where t.AnalystType=1 and t.AnalystStatus=1  and t.VipType=0  and   t.SoftVersion !=5001 and tr.SType=2 order by tr.SuccessRate desc";
                break;

              //普通版金牡丹粉丝列表
              case "gzfanslistjmd":
                sql = "select top(" + count + ")  A.* from AnalystFans as A left join Analyst as B on A.AnalystId=B.AnalystId where B.AnalystType=1 and B.AnalystStatus=1 and B.VipType=0 and   B.SoftVersion !=1 order by CreateTime desc";
                break;
              //普通版金牡丹获取现货人气名家排行榜
              case "actualmjorderjmd":
                sql = "select Top(" + count + ") t.*,tr.SuccessRate from Analyst t left join TransStatistics tr on t.AnalystId=tr.AnalystId  where t.AnalystType=1 and t.AnalystStatus=1  and t.VipType=0 and  t.SoftVersion !=1 and tr.SType=2 order by t.FansNum desc";
                break;
              //普通版金牡丹现货月战绩榜
              case "successrateorderjmd":
                sql = "select Top(" + count + ") t.*,tr.SuccessRate from Analyst t left join TransStatistics tr on t.AnalystId=tr.AnalystId where t.AnalystType=1 and t.AnalystStatus=1  and t.VipType=0 and  t.SoftVersion !=1 and tr.SType=1 order by tr.SuccessRate desc";
                break;
              //普通版金牡丹获取现货20条数据时间排序
              case "analystlivejsonjmd":
                sql =  "select Top(" + count + ")  A.*,B.SuccessRate,C.ImgUrl as AnalystImg, C.FansNum,C.Intro from ((AnalystLive as A left join TransStatistics as B on A.AnalystId=B.AnalystId) left join  Analyst as C on A.AnalystId=C.AnalystId) where 1 = 1 and C.AnalystType=1  and  C.AnalystStatus=1  and C.VipType=0  and  C.SoftVersion !=1 and B.SType=2 order by CreateTime desc";
                break;
              //普通版金牡丹现货总战绩榜
              case "zzjtotaljmd":
                sql = "select Top(" + count + ") t.*,tr.SuccessRate from Analyst t left join TransStatistics tr on t.AnalystId=tr.AnalystId where t.AnalystType=1 and t.AnalystStatus=1  and t.VipType=0 and  t.SoftVersion !=1 and tr.SType=2 order by tr.SuccessRate desc";
                break;

              //普通版普通分析师粉丝列表
              case "gzfanslistpt":
                sql = "select top(" + count + ")  A.* from AnalystFans as A left join Analyst as B on A.AnalystId=B.AnalystId where B.AnalystType=1 and B.AnalystStatus=1 and B.VipType=0 and   B.SoftVersion =0 order by CreateTime desc";
                break;
              //普通版普通分析师获取现货人气名家排行榜
              case "actualmjorderpt":
                sql = "select Top(" + count + ") t.*,tr.SuccessRate from Analyst t left join TransStatistics tr on t.AnalystId=tr.AnalystId  where t.AnalystType=1 and t.AnalystStatus=1  and t.VipType=0 and  t.SoftVersion =0  and tr.SType=2 order by t.FansNum desc";
                break;
              //普通版普通分析师现货月战绩榜
              case "successrateorderpt":
                sql = "select Top(" + count + ") t.*,tr.SuccessRate from Analyst t left join TransStatistics tr on t.AnalystId=tr.AnalystId where t.AnalystType=1 and t.AnalystStatus=1  and t.VipType=0 and  t.SoftVersion =0 and tr.SType=1 order by tr.SuccessRate desc";
                break;
              //普通版普通分析师获取现货20条数据时间排序
              case "analystlivejsonpt":
                sql = "select Top(" + count + ")  A.*,B.SuccessRate,C.ImgUrl as AnalystImg, C.FansNum,C.Intro from ((AnalystLive as A left join TransStatistics as B on A.AnalystId=B.AnalystId) left join  Analyst as C on A.AnalystId=C.AnalystId) where 1 = 1 and C.AnalystType=1  and  C.AnalystStatus=1  and C.VipType=0  and  C.SoftVersion =0 and B.SType=2 order by CreateTime desc";
                break;
              //普通版普通分析师现货总战绩榜
              case "zzjtotalpt":
                sql = "select Top(" + count + ") t.*,tr.SuccessRate from Analyst t left join TransStatistics tr on t.AnalystId=tr.AnalystId where t.AnalystType=1 and t.AnalystStatus=1  and t.VipType=0 and  t.SoftVersion =0 and tr.SType=2 order by tr.SuccessRate desc";
                break;

              //贵金属20条数据时间排序
              case "gjsanalystlivelist":
                sql = "select Top(" + count + ")  A.*,B.SuccessRate,C.ImgUrl as AnalystImg, C.FansNum,C.Intro from ((AnalystLive as A left join TransStatistics as B on A.AnalystId=B.AnalystId) left join  Analyst as C on A.AnalystId=C.AnalystId) where 1 = 1 and C.AnalystType=2  and  C.AnalystStatus=1 and C.VipType=0 and B.SType=2   order by CreateTime desc";
                isGjs = true;
                break;
              //获取现货人气排行榜
              case "gjsactualmjorder":
                sql = "select Top(" + count + ") t.*,tr.SuccessRate from Analyst t left join TransStatistics tr on t.AnalystId=tr.AnalystId  where t.AnalystType=2 and t.AnalystStatus=1  and t.VipType=0  and tr.SType=2 order by t.FansNum desc";
               isGjs = true;
               break;
              //现货战绩榜
              case "gjssuccessrateorder" :
               sql = "select Top(" + count + ") t.*,tr.SuccessRate from Analyst t left join TransStatistics tr on t.AnalystId=tr.AnalystId where t.AnalystType=2 and t.AnalystStatus=1  and t.VipType=0 and tr.SType=2 order by tr.SuccessRate desc";
               isGjs = true;
               break;
              // 获取贵金属粉丝关注动态列表，时间排序
              case "gjsgzfanslist" :
               sql = "select top(" + count + ")  A.* from AnalystFans as A left join Analyst as B on A.AnalystId=B.AnalystId where B.AnalystType=2 and B.AnalystStatus=1  and B.VipType=0  order by CreateTime desc";
               isGjs = true;
               break;

              //至尊金蝴蝶
              case "zzbjhd":
               sql = "select Top(" + count + ")  A.*,B.SuccessRate,C.ImgUrl as AnalystImg, C.FansNum,C.Intro,C.NickName from ((AnalystLive as A left join TransStatistics as B on A.AnalystId=B.AnalystId) left join  Analyst as C on A.AnalystId=C.AnalystId) where 1 = 1 and C.AnalystType=1  and  C.AnalystStatus=1  and C.VipType in(1,2,3) and C.SoftVersion=1 and B.SType=2 order by CreateTime desc";
               break;
              //至尊金牡丹
              case "zzbjmd":
               sql = "select Top(" + count + ")  A.*,B.SuccessRate,C.ImgUrl as AnalystImg, C.FansNum,C.Intro,C.NickName from ((AnalystLive as A left join TransStatistics as B on A.AnalystId=B.AnalystId) left join  Analyst as C on A.AnalystId=C.AnalystId) where 1 = 1 and C.AnalystType=1  and  C.AnalystStatus=1  and C.VipType in(1,2,3) and C.SoftVersion=5001 and B.SType=2 order by CreateTime desc";
               break;
              //白金版金蝴蝶
              case "bjbjhd":
               sql = "select Top(" + count + ")  A.*,B.SuccessRate,C.ImgUrl as AnalystImg, C.FansNum,C.Intro,C.NickName from ((AnalystLive as A left join TransStatistics as B on A.AnalystId=B.AnalystId) left join  Analyst as C on A.AnalystId=C.AnalystId) where 1 = 1 and C.AnalystType=1  and  C.AnalystStatus=1  and C.VipType=1 and C.SoftVersion=1 and B.SType=2 order by CreateTime desc";
               break;
              //白金版金牡丹
              case "bjbjmd":
               sql = "select Top(" + count + ")  A.*,B.SuccessRate,C.ImgUrl as AnalystImg, C.FansNum,C.Intro,C.NickName from ((AnalystLive as A left join TransStatistics as B on A.AnalystId=B.AnalystId) left join  Analyst as C on A.AnalystId=C.AnalystId) where 1 = 1 and C.AnalystType=1  and  C.AnalystStatus=1  and C.VipType=1 and C.SoftVersion=5001 and B.SType=2 order by CreateTime desc";
               break;
              //钻石金蝴蝶
              case "zsbjhd":
               sql = "select Top(" + count + ")  A.*,B.SuccessRate,C.ImgUrl as AnalystImg, C.FansNum,C.Intro,C.NickName from ((AnalystLive as A left join TransStatistics as B on A.AnalystId=B.AnalystId) left join  Analyst as C on A.AnalystId=C.AnalystId) where 1 = 1 and C.AnalystType=1  and  C.AnalystStatus=1  and C.VipType in(1,2) and C.SoftVersion=1 and B.SType=2 order by CreateTime desc";
               break;
              //钻石金牡丹
              case "zsbjmd":
               sql = "select Top(" + count + ")  A.*,B.SuccessRate,C.ImgUrl as AnalystImg, C.FansNum,C.Intro,C.NickName from ((AnalystLive as A left join TransStatistics as B on A.AnalystId=B.AnalystId) left join  Analyst as C on A.AnalystId=C.AnalystId) where 1 = 1 and C.AnalystType=1  and  C.AnalystStatus=1  and C.VipType in(1,2) and C.SoftVersion=5001 and B.SType=2 order by CreateTime desc";
               break;

              //银管家淘金殿
              case "ygjtjd":
               sql = "select Top(" + count + ")  A.*,B.SuccessRate,C.ImgUrl as AnalystImg, C.FansNum,C.Intro,C.NickName from ((AnalystLive as A left join TransStatistics as B on A.AnalystId=B.AnalystId) left join  Analyst as C on A.AnalystId=C.AnalystId) where 1 = 1 and C.AnalystType=2  and  C.AnalystStatus=1  and C.VipType=5 and B.SType=2 order by CreateTime desc";
               isGjs = true;
               break;
              //银管家银利阁
              case "ygjylg":
               sql = "select Top(" + count + ")  A.*,B.SuccessRate,C.ImgUrl as AnalystImg, C.FansNum,C.Intro,C.NickName from ((AnalystLive as A left join TransStatistics as B on A.AnalystId=B.AnalystId) left join  Analyst as C on A.AnalystId=C.AnalystId) where 1 = 1 and C.AnalystType=2  and  C.AnalystStatus=1  and C.VipType=4 and B.SType=2 order by CreateTime desc";
               isGjs = true;
               break;
              
              default:
               break;
            
          }
           using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql))
           {
               list = EntityHelper.GetEntityListByDataReader<AnalystLive>(dr, null);
               if (list != null)
               {
                   for (int i = 0; i < list.Count; i++)
                   {
                       if (!string.IsNullOrEmpty((list[i].CreateTime).ToString()))
                       {
                           list[i].ShowTime = Convert.ToDateTime(list[i].CreateTime).ToString("MM月dd日 HH:mm");
                           list[i].VipProfitRate = Math.Round(Convert.ToDecimal(list[i].ProfitRate) * (isGjs ? 20 : 5), 2);
                           //list[i].VipProfitRate = Math.Round((isGjs ? Convert.ToDecimal(list[i].ProfitRate) * 20 : Convert.ToDecimal(list[i].ProfitRate) * 5), 2);
                       }
                   }
               }
           }
           return list;

          
       }




       /// <summary>
       /// 删除直播数据
       /// </summary>
       /// <param name="liveId"></param>
       /// <param name="TransType"></param>
       /// <param name="AnalystName"></param>
       public void DelAnalystLiveData(int liveId, string transType, string analystName, int buyLiveId, int AnalystId, int AnalystType)
       {
           string sql="";
           string upsql = "";
           if (transType == "买入订立" || transType == "卖出订立" || transType == "买入开仓" || transType == "卖出开仓")
           {
               sql="delete from AnalystLive where BuyLiveId=@liveId or LiveId=@liveId";
           }else
           {
               sql = "delete from  AnalystLive where liveId=@liveId";
           }
           SqlParameter[] parsms = new SqlParameter[] { new SqlParameter("@liveId", liveId), new SqlParameter("@buyLiveId", buyLiveId) };
           SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.Text, sql, parsms);
           if (transType == "卖出转让" || transType == "买入转让" || transType == "买入平仓" || transType == "卖出平仓")
           {
               upsql = "update AnalystLive set IsSell=0,ProfitRate=NULL where LiveId=@buyLiveId";
               SqlParameter[] upparsms = new SqlParameter[] { new SqlParameter("@liveId", liveId), new SqlParameter("@buyLiveId", buyLiveId) };
               SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.Text, upsql, upparsms);
           }
           if (transType != "观点")
           {
               ToatalV(2, AnalystId, analystName, AnalystType);
           }
       }

       


    }
}
