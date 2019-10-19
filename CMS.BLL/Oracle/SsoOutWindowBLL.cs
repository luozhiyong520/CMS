using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;
using CMS.Model.Oracle;
using ServiceStack.OrmLite;
using CMS.DAL;
using ServiceStack.DataAnnotations;
using System.Data.OracleClient;
using Common;

namespace CMS.BLL.Oracle
{
    public class SsoOutWindowBLL
    {
        /// <summary>
        /// 更新Oracle移动终端推送信息表(所有)
        /// </summary>
        /// <param name="popupMsgPlan">推送信息</param>
        /// <param name="news">资讯</param>
        public int UpDataTB_MOBPUSH_USERINFO(PopupMsgPlan popupMsgPlan, News news)
        {
            return UpDataTB_MOBPUSH_USERINFO(popupMsgPlan, news,"");
        }

        /// <summary>
        /// 更新Oracle移动终端推送信息表(指定用户)
        /// </summary>
        /// <param name="popupMsgPlan">推送信息</param>
        /// <param name="news">资讯</param>
        /// <param name="userId">用户ID, 以逗号分隔</param>
        /// <returns></returns>
        public int UpDataTB_MOBPUSH_USERINFO(PopupMsgPlan popupMsgPlan, News news, string userId)
        {
            var dbFactory = new OrmLiteConnectionFactory(SqlConnectFactory.BaiduPush, OracleDialect.Provider);
            int res = 0;
            string users = "";
            if (userId != "")
                users = " and UP_USERID in (" + userId + ") ";

            string PLATFORM = "0";
            if (popupMsgPlan.PushPlatform.IndexOf("android") >= 0 && popupMsgPlan.PushPlatform.IndexOf("ios") >= 0)
                PLATFORM = "2";
            else if (popupMsgPlan.PushPlatform.IndexOf("ios") >= 0)
                PLATFORM = "1";

            string addDeviceType = string.Empty;
            if (PLATFORM != "2")
                addDeviceType = " and DeviceType = '" + PLATFORM + "' ";

            string statictag = string.Empty;
            statictag = popupMsgPlan.PushColumn == "公告" ? "1" : (popupMsgPlan.PushColumn == "解盘" ? "2" : "0");

            string set = "PUSHEDSTATUS = '1' " +
                        ", PUSHTYPE = '2' " +
                        ", STATICTAG = '" + statictag + "' " +
                        ", CREATETIME =  to_date('" + DateTime.Now.ToString() + "','yyyy-mm-dd hh24:mi:ss')" +
                        ", PUSHTIME = null " +
                        ", PLATFORM = " + PLATFORM +
                        ", MSGTITLE = " + "'" + news.Title.Replace("'", "\'") + "'" +
                        ", MSGCONTENT = '" + popupMsgPlan.NewsId + "'";

            //if (DateTime.Now.ToString("yyyy-MM-dd") == "2014-06-17")
            //    Loger.Info("TB_MOBPUSH_USERINFO\r\n" + set + "\r\n" + "(PUSHEDSTATUS is null or PUSHEDSTATUS != '1')" + addDeviceType + users);

            using (var db = dbFactory.OpenDbConnection())
            {
                res = db.Update("TB_MOBPUSH_USERINFO", set, "(PUSHEDSTATUS is null or PUSHEDSTATUS != '1')" + addDeviceType + users);
            }
            return res;
        }

        /// <summary>
        /// 添加Oracle移动终端资讯内容表
        /// </summary>
        /// <returns></returns>
        public long AddTB_PUSH_INFO(News news, string infoType, PopupMsgPlan popupMsgPlan)
        {

            long res = 0;
            //var dbFactory = new OrmLiteConnectionFactory(SqlConnectFactory.BaiduPush, OracleDialect.Provider);
            //TB_PUSH_INFO pushInfo = new TB_PUSH_INFO();
            //pushInfo.INFOID = news.NewsId;
            //pushInfo.TITLE = news.Title;
            //pushInfo.INFOABSTRACT = news.NewsAbstract;
            //pushInfo.AUTHOR = news.Author;
            //pushInfo.CREATEDTIME = news.CreatedTime;
            //pushInfo.INFOTYPE = infoType;
            //pushInfo.INFOCONTENT = news.Content;

            //using (var db = dbFactory.OpenDbConnection())
            //{
            //    res = db.InsertParam<TB_PUSH_INFO>(pushInfo);
            //}
            //return res;



            OracleConnection Con = new System.Data.OracleClient.OracleConnection(SqlConnectFactory.BaiduPush);
            Con.Open();
            string cmdText = "insert into tb_push_info " +
                            "(fid, infoid, title, infoabstract, author, createdtime, infotype, infocontent,OperateDate,PlanCount,RealCount,ClickCount,EDITOR,PLATFORM) " +
                            "values " +
                            "(seq_push_info.nextval, '" + news.NewsId + "', '" + news.Title + "', '" + news.NewsAbstract + "', '" + news.Author + "', to_date（'" + news.CreatedTime + "','yyyy-mm-dd hh24:mi:ss'), '" + infoType + "', :infocontent,sysdate,0,0,0,'" + popupMsgPlan.Editor + "','" + popupMsgPlan.PushPlatform + "')";
            OracleCommand cmd = new OracleCommand(cmdText, Con);
            OracleParameter op = new OracleParameter("infocontent", OracleType.Clob);
            op.Value = StringHelper.RetentionHTML(news.Content);
            cmd.Parameters.Add(op);
            cmd.ExecuteNonQuery();
            Con.Close();
            return res;
        }

        /// <summary>
        /// 添加Oracle移动终端资讯内容表
        /// </summary>
        /// <returns></returns>
        public long AddTB_PUSH_INFO(PopupMsgPlan popupMsgPlan)
        {

            long res = 0;
            OracleConnection Con = new System.Data.OracleClient.OracleConnection(SqlConnectFactory.BaiduPush);
            Con.Open();
            string cmdText = "insert into tb_push_info " +
                            "(fid, infoid, title, infoabstract, author, createdtime, infotype, infocontent,OperateDate,PlanCount,RealCount,ClickCount,EDITOR,PLATFORM) " +
                            "values " +
                            "(seq_push_info.nextval, 'ds" + popupMsgPlan.PlanId + "', '" + popupMsgPlan.Title + "', '" + popupMsgPlan.Content + "', '系统推送', to_date（'" + popupMsgPlan.CreatedTime + "','yyyy-mm-dd hh24:mi:ss'), '" + popupMsgPlan.PushColumn + "', :infocontent,sysdate,0,0,0,'" + popupMsgPlan.Editor + "','" + popupMsgPlan.PushPlatform + "')";
            OracleCommand cmd = new OracleCommand(cmdText, Con);
            OracleParameter op = new OracleParameter("infocontent", OracleType.Clob);
            op.Value = StringHelper.RetentionHTML(popupMsgPlan.Content);
            cmd.Parameters.Add(op);
            cmd.ExecuteNonQuery();
            Con.Close();
            return res;
        }

        #region 炒股大赛

        /// <summary>
        /// 更新Oracle移动终端推送信息表(炒股大赛)
        /// </summary>
        public int UpDataTB_MOBPUSH_USERINFO_StockContest(StockContestData scd, string msgTitle)
        {
            var dbFactory = new OrmLiteConnectionFactory(SqlConnectFactory.BaiduPush, OracleDialect.Provider);
            int res = 0;
            string users = "";
            if (scd.UserId == "")
            {
                Loger.Error("用户id为空, 来自: 移动终端推送信息表(炒股大赛)");
                return -1;
            }
            if (scd.UserId.IndexOf(",") < 0)
                users = " and UP_USERID = '" + scd.UserId + "' ";
            else
                users = " and UP_USERID in ('" + scd.UserId.Replace(",","','") + "') ";
            
            string set = "PUSHEDSTATUS = '1' " +
                        ", PUSHTYPE = '2' " +
                        ", STATICTAG = '" + scd.StaticTag + "' " +
                        ", CREATETIME =  to_date('" + DateTime.Now.ToString() + "','yyyy-mm-dd hh24:mi:ss')" +
                        ", PUSHTIME = null " +
                        ", PLATFORM = 2 " +
                        ", MSGTITLE = " + "'" + msgTitle + "'" +
                        ", MSGCONTENT = '" + scd.OperateUserName + "'";

            for (int i = 0; i < 30; i++)
            {
                int k = 0;
                using (var db = dbFactory.OpenDbConnection())
                {
                    k = db.Select<object>("select * from TB_MOBPUSH_USERINFO t where PUSHEDSTATUS = 1 " + users).Count;
                }
                //Loger.Info("未能推送状态PusherStatus=1的有:" + k + "条");
                if (k == 0)
                    break;
                else
                    System.Threading.Thread.Sleep(30000);
            }

            Loger.Info("Update TB_MOBPUSH_USERINFO set " + set + " where (PUSHEDSTATUS is null or PUSHEDSTATUS != '1') " + users);

            using (var db = dbFactory.OpenDbConnection())
            {
                res = db.Update("TB_MOBPUSH_USERINFO", set, "(PUSHEDSTATUS is null or PUSHEDSTATUS != '1')" + users);
            }
            return res;
        }

        #endregion
    }

    //public class TB_PUSH_INFO
    //{
    //    [AutoIncrement]
    //    [Sequence("SEQ_PUSH_INFO")]
    //    public int Fid { get; set; }
    //    /// <summary>
    //    /// 资讯Id
    //    /// </summary>
    //    public int INFOID { set; get; }
    //    /// <summary>
    //    /// 标题
    //    /// </summary>
    //    public string TITLE { set; get; }
    //    /// <summary>
    //    /// 摘要
    //    /// </summary>
    //    public string INFOABSTRACT { set; get; }
    //    /// <summary>
    //    /// 作者
    //    /// </summary>
    //    public string AUTHOR { set; get; }
    //    /// <summary>
    //    /// 创建时间
    //    /// </summary>
    //    public DateTime? CREATEDTIME { set; get; }
    //    /// <summary>
    //    /// 类型
    //    /// </summary>
    //    public string INFOTYPE { set; get; }
    //    /// <summary>
    //    ///  正文内容
    //    /// </summary>
    //    public string INFOCONTENT { set; get; }
    //}

}
