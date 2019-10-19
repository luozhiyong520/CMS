using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Upchina.Security;
using CMS.BLL;
using CMS.Model;
using CMS.Model.Oracle;
using Common;
using System.Web;
using System.Net;
using System.IO;
using CMS.HtmlService.Contract;
using System.Web.Script.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;
using CMS.BLL.Oracle;

namespace CMS.HtmlService
{
    public class SsoOutWindow : ISsoOutWindow
    {
        /// <summary>
        /// 弹窗链接
        /// </summary>
        string OutWindowUrl = ConfigurationManager.AppSettings["OutWindowUrl"];
        /// <summary>
        /// 弹窗链接(资讯弹窗)
        /// </summary>
        string OutWindowUrlInfo = ConfigurationManager.AppSettings["OutWindowUrlInfo"];

        /// <summary>
        /// 新增弹窗消息接口
        /// </summary>
        string AddOutWindow = ConfigurationManager.AppSettings["AddOutWindow"];

        /// <summary>
        /// 修改消息状态接口
        /// </summary>
        string ChangeOutWindow = ConfigurationManager.AppSettings["ChangeOutWindow"];

        /// <summary>
        /// 加密钥
        /// </summary>
        string accesskey = ConfigurationManager.AppSettings["accesskey"];
        string clientId = ConfigurationManager.AppSettings["clientId"];

        /// <summary>
        /// SSO数据推送
        /// </summary>
        /// <param name="planId">planId</param>
        /// <param name="version">推送版本,金蝴蝶:1000, 严林版:3100, 金牡丹:5100, 渤商版:6100, 所有版本:0</param>
        public void SsoPush(int planId, string version)
        {
            if (string.IsNullOrEmpty(version))
                version = "0";
            try
            {
                PopupMsgPlanBLL opupMsgPlanBll = Factory.BusinessFactory.CreateBll<PopupMsgPlanBLL>();
                PopupMsgPlan popupMsgPlan = opupMsgPlanBll.Get("PlanId", planId);
                List<string> listUserName = new List<string>();

                if (popupMsgPlan != null)
                {
                    int ssoResultId = 0;
                    List<CustomerGroup> allUserName = new List<CustomerGroup>();

                    //判断是否用户组
                    if (popupMsgPlan.ReceiverType == 0 || popupMsgPlan.ReceiverType == 2)
                    {
                        allUserName = GetUserName(popupMsgPlan.Receiver, version);
                        string versionLog = version == "0" ? "所有" : 
                                        (version == "1000" ? "金蝴蝶" : 
                                        (version == "3100" ? "严林版" : 
                                        (version == "5100" ? "金牡丹" : 
                                        (version == "6100" ? "渤商版" : "其它"))));
                        Loger.Info("\r\n=====推送类型: " + popupMsgPlan.DataType
                                 + "\r\n=====推送用户组: " + popupMsgPlan.Receiver
                                 + "\r\n=====推送版本: " + versionLog
                                 + "\r\n=====推送平台: " + popupMsgPlan.PushPlatform
                                 + "\r\n=====planId: " + planId);
                        if (allUserName != null)
                            listUserName = GetUserNameToGroup(allUserName);
                        else
                        {
                            Loger.Info("PlanId:" + planId + ", 此用户组, 不存在用户!");
                            return;
                        }
                    }
                    else if (popupMsgPlan.ReceiverType == 1)
                    {
                        allUserName = AddUserName(popupMsgPlan.Receiver);
                        listUserName.Add(popupMsgPlan.Receiver);
                    }

                    //更新计划接收用户数
                    popupMsgPlan.ReceiverCount = allUserName.Count;
                    opupMsgPlanBll.Update(popupMsgPlan);

                    if (popupMsgPlan.DataType == "资讯弹窗")
                    {
                        //包括移动终端推送
                        if (popupMsgPlan.PushPlatform.IndexOf("android") >= 0 || popupMsgPlan.PushPlatform.IndexOf("ios") >= 0)
                        {
                            Thread thOracle = new Thread(new ThreadStart(delegate()
                            {
                                UpDataMoveData(popupMsgPlan, allUserName);
                            }));
                            thOracle.Start();
                        }

                        //不包括PC终端, 返回
                        if (popupMsgPlan.PushPlatform.IndexOf("pc") < 0)
                            return;

                        if (popupMsgPlan.ReceiverType == 0 || popupMsgPlan.ReceiverType == 2)   //用户组
                        {
                            Thread th = new Thread(new ThreadStart(delegate()
                            {
                                NewsPopupUserUpdataIn(popupMsgPlan, allUserName, version);
                            }));
                            th.Start();
                        }
                        else if (popupMsgPlan.ReceiverType == 1)    //用户(手动输入)
                        {
                            Thread th = new Thread(new ThreadStart(delegate()
                            {
                                NewsPopupUserUpdata(popupMsgPlan, allUserName);
                            }));
                            th.Start();
                        }
                    };
                    //return;
                    string parameter = string.Empty;
                    List<SsoRes> listSsoRes = new List<SsoRes>();

                    if (listUserName.Count > 0)
                        Loger.Info(" [SSO][开始] SSO推送中...");
                    else
                        return;

                    DateTime srb = DateTime.Now;

                    foreach (var userName in listUserName)
                    {
                        parameter = GetAddOutWindowParameter(popupMsgPlan, userName);
                        string res = RequestHelper.WebRequest(AddOutWindow, "post", parameter, "UTF-8", true);
                        if (!string.IsNullOrEmpty(res))
                        {
                            res = new EncDecUtil().decyptData(res, accesskey);
                            SsoRes ssoRes = JsonHelper.DeserializeJson<SsoRes>(res);
                            listSsoRes.Add(ssoRes);

                            // 调用接口状态码，0：成功，1：该消息id 已存在，2：链接地址不存在，3：用户群组不存在，4：开始时间与结束时间不匹配，9：其它异常
                            if (ssoRes.error != 0)//失败部分单独更新
                            {
                                Loger.Info("SSO接口调用返回失败, 此批用户入库<失败用户>");
                                Loger.Info("PlanId:" + planId + ",SSO数据推送返回结果:" + res);
                                ssoResultId = UpDataSsoResult(popupMsgPlan, userName, ssoRes, ssoResultId);
                            }
                            //Loger.Info("PlanId:" + planId + ",SSO数据推送返回结果: ssoRes = " + ssoRes.error);
                        }
                        else 
                        {
                            Loger.Info("SSO接口调用返回空=====error=====(一般情况不会出现, 出现在超时或者无法调用)");
                        }
                    }

                    Loger.Info(" [SSO][结束] SSO推送完毕, 用时: " + (int)(DateTime.Now - srb).TotalSeconds + " 秒");
                    UpDataSsoResult(popupMsgPlan, listSsoRes, ssoResultId);

                    //if (popupMsgPlan.DataType == "广告弹窗")
                    //    ReceiveMsgPush(popupMsgPlan, allUserName);
                    //else
                    //if (popupMsgPlan.DataType == "资讯弹窗")
                    //{
                    //    //InformationOfUserPush(popupMsgPlan, allUserName);
                    //    NewsPopupUserUpdata(popupMsgPlan, allUserName);
                    //}
                }
            }
            catch (Exception ex)
            {
                Loger.Error(ex, "\r\n=====error=====\r\nPlanId:" + planId + ",SSO数据推送异常:");
            }

        }

        /// <summary>
        /// SSO数据推送, 重推
        /// </summary>
        /// <param name="ssoResultId">ssoResultId</param>
        public void SsoPushOne(int ssoResultId)
        {
            int planId = 0;
            try
            {
                SSOResultBLL ssoResultBll = Factory.BusinessFactory.CreateBll<SSOResultBLL>();
                SSOResult ssoResult = ssoResultBll.Get("Id", ssoResultId);

                if (ssoResult != null)
                {
                    PopupMsgPlanBLL opupMsgPlanBll = Factory.BusinessFactory.CreateBll<PopupMsgPlanBLL>();
                    PopupMsgPlan popupMsgPlan = opupMsgPlanBll.Get("PlanId", ssoResult.PlanId);
                    planId = (int)ssoResult.PlanId;

                    if (popupMsgPlan != null)
                    {
                        List<string> listUserName = GetUserNameToGroup(ssoResult.ErrorUser); //所有错误用户, 每100个一组
                        string parameter = string.Empty;
                        int i = 0;
                        foreach (var userName in listUserName)
                        {
                            parameter = GetAddOutWindowParameter(popupMsgPlan, userName);
                            string res = RequestHelper.WebRequest(AddOutWindow, "post", parameter, "UTF-8", true);
                            if (!string.IsNullOrEmpty(res))
                            {
                                res = new EncDecUtil().decyptData(res, accesskey);
                            }
                            Loger.Info("PlanId:" + planId + ",SSO数据重推返回结果:" + res);
                            UpDataSsoResult(userName, res, ssoResult, i);
                            i++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Loger.Error(ex,"\r\n=====error=====\r\nPlanId:" + planId + ",SSO数据重推异常:");
            }
        }

        /// <summary>
        /// 修改消息状态接口
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public int UpDateStatus(int id, int status)
        {
            try
            {
                string parameter = GetChangeOutWindowParameter(id, status);
                string res = RequestHelper.WebRequest(ChangeOutWindow, "post", parameter, "UTF-8", true);
                if (!string.IsNullOrEmpty(res))
                {
                    res = new EncDecUtil().decyptData(res, accesskey);
                }
                Loger.Info("PlanId:" + id + ",修改消息状态接口返回结果:" + res);
                SsoRes listSsoRes = JsonHelper.DeserializeJson<SsoRes>(res);

                if (listSsoRes == null)
                    return -1;
                return listSsoRes.error;
            }
            catch (Exception ex)
            {
                Loger.Error(ex,"\r\n=====error=====\r\nPlanId:" + id + ",修改消息状态异常:");
                return -1;
            }
        }

        /// <summary>
        /// SSO接口重推记录更新
        /// </summary>
        /// <param name="userName">此次调用接口用户</param>
        /// <param name="res">SSO接口返回值</param>
        /// <param name="ssoResultID">调用SSO结果表ID</param>
        /// <param name="i">是否第一次标识</param>
        private void UpDataSsoResult(string userName, string res, SSOResult ssoResult, int i)
        {
            SSOResultBLL ssoResultBll = Factory.BusinessFactory.CreateBll<SSOResultBLL>();
            if (string.IsNullOrEmpty(res))//sso接口返回空时，不往下执行了
                return ;
            SsoRes ssoRes = JsonHelper.DeserializeJson<SsoRes>(res);

            ssoResult.SuccessUser += UserArryToString(ssoRes.success_user);
            ssoResult.Status = ssoRes.error;

            //重推首次覆盖错误用户
            if (i == 0 && ssoRes.error == 0)
            {
                ssoResult.ErrorUser = UserArryToString(ssoRes.error_user);
                ssoResult.RepeatUser = UserArryToString(ssoRes.repeat_user);
                ssoResult.UndefinedUser = UserArryToString(ssoRes.undefined_user);
            }
            else
            {
                ssoResult.ErrorUser += UserArryToString(ssoRes.error_user) == "" ? "" : "," + UserArryToString(ssoRes.error_user);
                ssoResult.RepeatUser += UserArryToString(ssoRes.repeat_user) == "" ? "" : "," + UserArryToString(ssoRes.repeat_user);
                ssoResult.UndefinedUser += UserArryToString(ssoRes.undefined_user) == "" ? "" : "," + UserArryToString(ssoRes.undefined_user);
            }

            ssoResultBll.Update(ssoResult);

        }

        /// <summary>
        /// SSO接口调用记录更新(失败部分)
        /// </summary>
        /// <param name="popupMsgPlan">弹窗信息计划</param>
        /// <param name="userName">此次调用接口用户</param>
        /// <param name="res">SSO接口返回值</param>
        /// <param name="ssoResultId">调用SSO结果表ID</param>
        /// <returns></returns>
        private int UpDataSsoResult(PopupMsgPlan popupMsgPlan, string userName, SsoRes ssoRes, int ssoResultId)
        {
            SSOResultBLL ssoResultBll = Factory.BusinessFactory.CreateBll<SSOResultBLL>();
            //if(string.IsNullOrEmpty(res))//sso接口返回空时，不往下执行了
            //    return 0;
            //SsoRes ssoRes = JsonHelper.DeserializeJson<SsoRes>(res);

            SSOResult ssoResult;
            if (ssoResultId > 0)
                ssoResult = ssoResultBll.Get("Id", ssoResultId);
            else
                ssoResult = new SSOResult();

            ssoResult.PlanId = popupMsgPlan.PlanId;
            ssoResult.Url = OutWindowUrl + popupMsgPlan.PlanId;
            ssoResult.BeginTime = popupMsgPlan.BeginTime;
            ssoResult.EndTime = popupMsgPlan.EndTime;
            ssoResult.PopupType = popupMsgPlan.PopupType;
            ssoResult.Status = ssoRes.error;

            //SSO接口返回调用成功
            if (ssoRes.error == 0)
            {
                //数据库已经存在这条数据
                if (ssoResultId > 0)
                {
                    ssoResult.Id = ssoResultId;
                    ssoResult.SuccessUser += UserArryToString(ssoRes.success_user) == "" ? "" : "," + UserArryToString(ssoRes.success_user);
                    ssoResult.ErrorUser += UserArryToString(ssoRes.error_user) == "" ? "" : "," + UserArryToString(ssoRes.error_user);
                    ssoResult.RepeatUser += UserArryToString(ssoRes.repeat_user) == "" ? "" : "," + UserArryToString(ssoRes.repeat_user);
                    ssoResult.UndefinedUser += UserArryToString(ssoRes.undefined_user) == "" ? "" : "," + UserArryToString(ssoRes.undefined_user);
                    ssoResultBll.Update(ssoResult);
                    return ssoResultId;
                }
                else
                {
                    ssoResult.SuccessUser = UserArryToString(ssoRes.success_user);
                    ssoResult.ErrorUser = UserArryToString(ssoRes.error_user);
                    ssoResult.RepeatUser = UserArryToString(ssoRes.repeat_user);
                    ssoResult.UndefinedUser = UserArryToString(ssoRes.undefined_user);
                    return ssoResultBll.Add(ssoResult);
                }
            }
            else //SSO接口调用失败,所有用户加到出错用户字段
            {
                //数据库已经存在这条数据
                if (ssoResultId > 0)
                {
                    ssoResult.Id = ssoResultId;
                    ssoResult.ErrorUser += ssoResult.ErrorUser == "" ?  "" : "," + userName;
                    ssoResultBll.Update(ssoResult);
                    return ssoResultId;
                }
                else
                {
                    ssoResult.ErrorUser = userName;
                    return ssoResultBll.Add(ssoResult);
                }
            }
        }


        /// <summary>
        /// SSO接口调用记录更新(成功部分)
        /// </summary>
        /// <param name="popupMsgPlan">弹窗信息计划</param>
        /// <param name="userName">此次调用接口用户</param>
        /// <param name="res">SSO接口返回值</param>
        /// <param name="ssoResultId">调用SSO结果表ID</param>
        /// <returns></returns>
        private int UpDataSsoResult(PopupMsgPlan popupMsgPlan, List<SsoRes> listSsoRes, int ssoResultId)
        {
            SSOResultBLL ssoResultBll = Factory.BusinessFactory.CreateBll<SSOResultBLL>();
            //if(string.IsNullOrEmpty(res))//sso接口返回空时，不往下执行了
            //    return 0;
            //SsoRes ssoRes = JsonHelper.DeserializeJson<SsoRes>(res);

            SSOResult ssoResult;
            if (ssoResultId > 0)
                ssoResult = ssoResultBll.Get("Id", ssoResultId);
            else
                ssoResult = new SSOResult();

            ssoResult.PlanId = popupMsgPlan.PlanId;
            ssoResult.Url = OutWindowUrl + popupMsgPlan.PlanId;
            ssoResult.BeginTime = popupMsgPlan.BeginTime;
            ssoResult.EndTime = popupMsgPlan.EndTime;
            ssoResult.PopupType = popupMsgPlan.PopupType;
            ssoResult.Status = 0;

            string success_user = "";
            string error_user = "";
            string repeat_user = "";
            string undefined_user = "";

            foreach (var item in listSsoRes)
            {
                string success = UserArryToString(item.success_user);
                string error = UserArryToString(item.error_user);
                string repeat = UserArryToString(item.repeat_user);
                string undefined = UserArryToString(item.undefined_user);

                if (success != "")
                    success_user += (success_user == "" ? success : "," + success);
                if (error != "")
                    error_user += (error_user == "" ? error : "," + error);
                if (repeat != "")
                    repeat_user += (repeat_user == "" ? repeat : "," + repeat);
                if (undefined != "")
                    undefined_user += (undefined_user == "" ? undefined : "," + undefined);
            }


            //数据库已经存在这条数据
            if (ssoResultId > 0)
            {
                ssoResult.Id = ssoResultId;
                ssoResult.SuccessUser += success_user;
                ssoResult.ErrorUser += error_user;
                ssoResult.RepeatUser += repeat_user;
                ssoResult.UndefinedUser += undefined_user;
                ssoResultBll.Update(ssoResult);
                Loger.Info("[调用结果][更新] SSO调用记录: PlanId = " + popupMsgPlan.PlanId + ", 成功 = " + (ssoResult.SuccessUser == "" ? "0" : ssoResult.SuccessUser.Split(',').Length.ToString()) + " , 失败 = " + (ssoResult.ErrorUser == "" ? "0" : ssoResult.ErrorUser.Split(',').Length.ToString()) + " , 重复 = " + (ssoResult.RepeatUser == "" ? "0" : ssoResult.RepeatUser.Split(',').Length.ToString()) + " , 不存在 = " + (ssoResult.UndefinedUser == "" ? "0" : ssoResult.UndefinedUser.Split(',').Length.ToString()));
                return ssoResultId;
            }
            else
            {
                ssoResult.SuccessUser = success_user;
                ssoResult.ErrorUser = error_user;
                ssoResult.RepeatUser = repeat_user;
                ssoResult.UndefinedUser = undefined_user;
                Loger.Info("[调用结果][新增] SSO调用记录: PlanId = " + popupMsgPlan.PlanId + ", 成功 = " + (ssoResult.SuccessUser == "" ? "0" : ssoResult.SuccessUser.Split(',').Length.ToString()) + " , 失败 = " + (ssoResult.ErrorUser == "" ? "0" : ssoResult.ErrorUser.Split(',').Length.ToString()) + " , 重复 = " + (ssoResult.RepeatUser == "" ? "0" : ssoResult.RepeatUser.Split(',').Length.ToString()) + " , 不存在 = " + (ssoResult.UndefinedUser == "" ? "0" : ssoResult.UndefinedUser.Split(',').Length.ToString()));
                return ssoResultBll.Add(ssoResult);
            }
        }

        /// <summary>
        /// 字符串数组转换成","分隔字符串(用于处理SSO接口返回)
        /// </summary>
        /// <param name="userArry"></param>
        /// <returns></returns>
        private string UserArryToString(string[] userArry)
        {
            StringBuilder sb = new StringBuilder();
            if (userArry != null && userArry.Length > 0)
            {
                foreach (var item in userArry)
                {
                    sb.Append("," + item);
                }
                return sb.ToString().Substring(1);
            }
            else
                return "";
        }

        /// <summary>
        /// 获取新增弹窗消息接口参数
        /// </summary>
        /// <param name="popupMsgPlan">弹窗信息计划</param>
        /// <param name="userName">此次调用接口用户</param>
        /// <returns></returns>
        private string GetAddOutWindowParameter(PopupMsgPlan popupMsgPlan, string userName)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();

            AddKey ak = new AddKey();
            ak.id = popupMsgPlan.PlanId;
            if (popupMsgPlan.DataType == "资讯弹窗")
                ak.url = OutWindowUrlInfo + popupMsgPlan.PlanId;
            else
                ak.url = OutWindowUrl + popupMsgPlan.PlanId;
            ak.app_user = userName;
            if (popupMsgPlan.BeginTime != null)
                ak.begin_time = (DateTime)popupMsgPlan.BeginTime;
            if (popupMsgPlan.EndTime != null)
                ak.end_time = (DateTime)popupMsgPlan.EndTime;
            ak.popup_type = (int)popupMsgPlan.PopupType;

            string jsonStr = jss.Serialize(ak);

            return EncodeString(jsonStr);
        }

        /// <summary>
        /// 获取修改消息状态接口参数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private string GetChangeOutWindowParameter(int id, int status)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();

            ChangeKey ck = new ChangeKey();
            ck.id = id;
            ck.status = status;

            string jsonStr = jss.Serialize(ck);

            return EncodeString(jsonStr);
        }

        /// <summary>
        /// 参数加密
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        private string EncodeString(string jsonStr)
        {
            string result = string.Empty;

            EncDecUtil encDecUtil = new EncDecUtil();
            string key = encDecUtil.encyptData(jsonStr, accesskey);
            string sign = encDecUtil.signData(key, accesskey);

            result = string.Format("key={0}&sign={1}&clientId={2}",
                                   HttpUtility.UrlEncode(key),
                                   HttpUtility.UrlEncode(sign),
                                   clientId);
            return result;
        }

        /// <summary>
        /// 把以","分隔的用户名, 加到List对象中
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private List<CustomerGroup> AddUserName(string userName)
        {
            List<CustomerGroup> list = new List<CustomerGroup>();

            if (userName.IndexOf(",") < 0)
            {
                CustomerGroup user = new CustomerGroup();
                user.CustomerName = userName;
                list.Add(user);
            }
            else
            {
                foreach (var item in userName.Split(','))
                {
                    CustomerGroup user = new CustomerGroup();
                    user.CustomerName = item;
                    list.Add(user);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取关联表相对于用户组表, 不存在的用户名和用户ID
        /// </summary>
        /// <param name="groups">用户组</param>
        /// <param name="version">推送版本,金蝴蝶:1000, 严林版:3100, 金牡丹:5100, 渤商版:6100, 所有版本:0</param>
        private List<CustomerGroup> GetUserNameNonexistent(string groups, string version)
        {
            return GetUserName(groups, version, " and CustomerName not in(select Receiver from NewsPopupUser) ");
        }

        /// <summary>
        /// 获取所有用户名和用户ID
        /// </summary>
        /// <param name="groups">用户组</param>
        /// <param name="version">推送版本,金蝴蝶:1000, 严林版:3100, 金牡丹:5100, 渤商版:6100, 所有版本:0</param>
        private List<CustomerGroup> GetUserName(string groups, string version)
        {
            return GetUserName(groups, version, "");
        }

        /// <summary>
        /// 获取所有用户名和用户ID
        /// </summary>
        /// <param name="groups">用户组</param>
        /// <param name="version">推送版本,金蝴蝶:1000, 严林版:3100, 金牡丹:5100, 渤商版:6100, 所有版本:0</param>
        /// <param name="addAnd">增加筛选条件</param>
        private List<CustomerGroup> GetUserName(string groups, string version, string addAnd)
        {
            CustomerGroupBLL groupBll = Factory.BusinessFactory.CreateBll<CustomerGroupBLL>();

            string where = string.Empty;

            //多组
            if (groups.IndexOf(",") > 0)
            {
                foreach (var item in groups.Split(','))
                {
                    if (string.IsNullOrEmpty(where))
                        where = "GroupName in ('" + item + "'";
                    else
                        where += ",'" + item + "'";
                }

                where += ")";
            }
            else//单组
	        {
                where = "GroupName = '" + groups + "'";
	        }

            if (version != "0")
                where += " and RelevanceId = '" + version + "' ";

            if (addAnd != "")
                where += addAnd;

            List<CustomerGroup> listGroup = groupBll.GetAll("[CustomerId],[CustomerName]", "where " + where + " group by [CustomerId],[CustomerName]");

            return listGroup;
        }

        /// <summary>
        /// 拆分所有用户名以","分隔, 100个一组
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<string> GetUserNameToGroup(List<CustomerGroup> listGroup)
        {
            List<string> listUserName = new List<string>();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < listGroup.Count; i++)
            {
                sb.Append("," + listGroup[i].CustomerName);

                if ((i > 0 && i % 100 == 0) || i == listGroup.Count - 1) //每100条一组
                {
                    listUserName.Add(sb.ToString().Substring(1));
                    sb.Remove(0, sb.Length);
                }
            }

            return listUserName;
        }

        /// <summary>
        /// 拆分所有用户名以","分隔, 100个一组
        /// </summary>
        /// <param name="userNames"></param>
        /// <returns></returns>
        private List<string> GetUserNameToGroup(string userNames)
        {
            List<string> listUserName = new List<string>();
            StringBuilder sb = new StringBuilder();

            string[] users = userNames.Split(',');

            for (int i = 0; i < users.Length; i++)
            {
                sb.Append("," + users[i]);

                if ((i > 0 && i % 100 == 0) || i == users.Length - 1) //每100条一组
                {
                    listUserName.Add(sb.ToString().Substring(1));
                    sb.Remove(0, sb.Length);
                }
            }

            return listUserName;
        }

        /// <summary>
        /// 弹窗消息进用户收件箱
        /// </summary>
        /// <param name="planId"></param>
        private void ReceiveMsgPush(PopupMsgPlan popupMsgPlan, List<CustomerGroup> userName)
        {
            //批量插入数量
            int batchNum = 0;

            batchNum = Convert.ToInt32(ConfigurationManager.AppSettings["batchNum"]);

            if (batchNum == 0)
                batchNum = 100000;

            DataTable dt = new DataTable();
            dt = GetTableSchema();

            for (int i = 0; i < userName.Count; i++)
            {
                DataRow r = dt.NewRow();
                r[0] = 0;
                r[1] = popupMsgPlan.Title;
                r[2] = popupMsgPlan.Content;
                r[3] = popupMsgPlan.ImgUrl;
                r[4] = popupMsgPlan.PageUrl;
                r[5] = 0;
                r[6] = "UP量化安全炒股卫士";
                r[7] = userName[i].CustomerId == null ? 0 : userName[i].CustomerId;
                r[8] = userName[i].CustomerName;
                r[9] = popupMsgPlan.CreatedTime;
                r[10] = popupMsgPlan.BeginTime == null ? DateTime.Now : popupMsgPlan.BeginTime;
                r[11] = 1;
                r[12] = popupMsgPlan.PlanId;
                r[13] = "";
                r[14] = 1;

                dt.Rows.Add(r);

                if ((i > 0 && i % batchNum == 0) || i == userName.Count - 1)
                {
                    BulkToDB(dt, "ReceiveMsg");
                    dt.Reset();
                    dt = GetTableSchema();
                }
            }
        }

        /// <summary>
        /// 资讯弹窗消息进关联表
        /// </summary>
        private void InformationOfUserPush(PopupMsgPlan popupMsgPlan, List<CustomerGroup> userName)
        {
            //批量插入数量
            int batchNum = 0;
            
            batchNum = Convert.ToInt32(ConfigurationManager.AppSettings["batchNum"]);

            if (batchNum == 0)
                batchNum = 100000;

            DataTable dt = new DataTable();
            dt = GetTableSchemaIou();

            for (int i = 0; i < userName.Count; i++)
            {
                DataRow r = dt.NewRow();
                r[0] = 0;
                r[1] = userName[i].CustomerId == null ? 0 : userName[i].CustomerId;
                r[2] = userName[i].CustomerName;
                r[3] = popupMsgPlan.NewsId;
                r[4] = popupMsgPlan.PlanId;
                r[5] = popupMsgPlan.PushColumn == "公告" ? 1 : 2;

                dt.Rows.Add(r);

                if ((i > 0 && i % batchNum == 0) || i == userName.Count - 1)
                {
                    BulkToDB(dt, "InformationOfUser");
                    dt.Reset();
                    dt = GetTableSchemaIou();
                }
            }
        }

        /// <summary>
        /// 更新资讯弹窗关联表(in 批量更新)
        /// </summary>
        /// <param name="popupMsgPlan"></param>
        /// <param name="userName"></param>
        private void NewsPopupUserUpdataIn(PopupMsgPlan popupMsgPlan, List<CustomerGroup> userName, string version)
        {
            try
            {
                Loger.Info("[入库][开始] 资讯弹窗关联表更新中...");
                DateTime begin = DateTime.Now;
                StringBuilder sb = new StringBuilder();
                List<string> sqlList = new List<string>();

                for (int i = 0; i < userName.Count; i++)
                {
                    sb.Append("," + userName[i].CustomerId);
                    if (i != 0 && (i % 5000 == 0 || i == userName.Count - 1))
                    {
                        sqlList.Add(sb.ToString().Substring(1, sb.Length - 1));
                        sb.Remove(0, sb.Length);
                    }
                }

                //int myNum = 1;
                foreach (var item in sqlList)
                {
                    //DateTime b = DateTime.Now;
                    string sql = "update NewsPopupUser set NewsIds = CAST(NewsIds AS VARCHAR(MAX))  + '," + popupMsgPlan.NewsId + "' where ReceiverId in(" + item + ")";
                    SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["CMS"].ConnectionString, CommandType.Text, sql);
                    //Loger.Debug("第 " + myNum++ + " 次 " + (int)(DateTime.Now - b).TotalMilliseconds + " 毫秒");
                }

                Loger.Info("[入库]  批量更新完成, 处理新增关联用户中...");

                List<CustomerGroup> nsertUser = GetUserNameNonexistent(popupMsgPlan.Receiver, version);
                int insNum = 0;
                if (nsertUser != null)
                {
                    NewsPopupUserPush(popupMsgPlan, nsertUser);
                    insNum = nsertUser.Count;
                }
                Loger.Info("[入库][结束] 资讯弹窗关联表更新完毕, 推送用户: " + userName.Count + " 条, 新增关联: " + insNum + " 条, 用时: " + (int)(DateTime.Now - begin).TotalSeconds + " 秒");
            }
            catch (Exception ex)
            {
                Loger.Error(ex,"更新资讯弹窗关联表(in 批量更新):");
            }
        }
        
        /// <summary>
        /// 更新资讯弹窗关联表(新)(SqlDataAdapter 批量更新)
        /// </summary>
        private void NewsPopupUserUpdata(PopupMsgPlan popupMsgPlan, List<CustomerGroup> userName)
        {
            try
            {
                Loger.Info("[入库][开始] (手动输入用户)资讯弹窗关联表更新中...");
                DateTime begin = DateTime.Now;

                string constr = ConfigurationManager.ConnectionStrings["CMS"].ConnectionString;
                SqlConnection sqlConn = new SqlConnection(constr);

                NewsPopupUserBLL popupUserBll = Factory.BusinessFactory.CreateBll<NewsPopupUserBLL>();
                int userCount = popupUserBll.GetCount();

                int selectNum = 10000;          //每次从数据库中提取作查询的数据数量
                int eachNum = userCount;    //需要从数据库中循环读取的次数
                if (userCount != 0)         //如果数据库存在数据, 则计算需要读取的次数
                    eachNum = userCount / selectNum + (userCount % selectNum == 0 ? 0 : 1);

                Loger.Info("[入库]  (手动输入用户)批量更新关联表已存用户总数 " + userCount + " 条, 每次提取 " + selectNum + " 条, 需要读取 " + eachNum + " 次, 本次需更新用户 " + userName.Count + " 条");

                string connectionString = constr;
                for (int k = 0; k < eachNum; k++)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        int unTopNum = k * selectNum;   //排除已经提取过的条目数
                        string sql = string.Empty;
                        if (unTopNum == 0)
                            sql = "select top " + selectNum + " * from NewsPopupUser";
                        else
                            sql = "select top " + selectNum + " * from NewsPopupUser where id not in (select top " + unTopNum + " id from NewsPopupUser)";

                        SqlDataAdapter sd = new SqlDataAdapter();
                        sd.SelectCommand = new SqlCommand(sql, conn);
                        DataSet dataset = new DataSet();
                        sd.Fill(dataset);

                        sd.UpdateCommand = new SqlCommand("update NewsPopupUser set ReceiverId = @ReceiverId,Receiver = @Receiver,NewsIds = @NewsIds where Id = @Id", conn);
                        sd.UpdateCommand.Parameters.Add("@Id", SqlDbType.Int, 8, "Id");
                        sd.UpdateCommand.Parameters.Add("@ReceiverId", SqlDbType.Int, 8, "ReceiverId");
                        sd.UpdateCommand.Parameters.Add("@Receiver", SqlDbType.NVarChar, 20, "Receiver");
                        sd.UpdateCommand.Parameters.Add("@NewsIds", SqlDbType.Text, 10000, "NewsIds");
                        sd.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
                        sd.UpdateBatchSize = 0;
                    
                        for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                        {
                            CustomerGroup thisUser = userName.Where(a => a.CustomerName == dataset.Tables[0].Rows[i]["Receiver"].ToString()).FirstOrDefault();

                            if (thisUser != null)
                            {
                                dataset.Tables[0].Rows[i].BeginEdit();
                                dataset.Tables[0].Rows[i]["NewsIds"] = dataset.Tables[0].Rows[i]["NewsIds"] + "," + popupMsgPlan.NewsId;    //增加资讯关联
                                dataset.Tables[0].Rows[i].EndEdit();
                                userName.Remove(thisUser);  //删除需要推送的用户记录
                            }
                        }

                        sd.Update(dataset.Tables[0]);   //更新数据库

                        dataset.Tables[0].Clear();
                        sd.Dispose();
                        dataset.Dispose();
                        conn.Close();

                        //如果用户列表已清空, 跳出循环
                        if (userName.Count == 0)
                            break;
                    }
                }

                //剩下用户列表为数据库不存在用户, 批量插入
                if (userName.Count > 0)
                    NewsPopupUserPush(popupMsgPlan, userName);

                Loger.Info("[入库][结束] (手动输入用户)资讯弹窗关联表更新完毕, 用时: " + (int)(DateTime.Now - begin).TotalSeconds + " 秒");
            }
            catch (Exception ex)
            {
                Loger.Error(ex,"更新资讯弹窗关联表(新)(SqlDataAdapter 批量更新):");
            }
        }


        /// <summary>
        /// 资讯弹窗消息进关联表(新)
        /// </summary>
        private void NewsPopupUserPush(PopupMsgPlan popupMsgPlan, List<CustomerGroup> userName)
        {
            if (userName == null || popupMsgPlan == null)
                return;

            //批量插入数量
            int batchNum = 0;

            batchNum = Convert.ToInt32(ConfigurationManager.AppSettings["batchNum"]);

            if (batchNum == 0)
                batchNum = 100000;

            DataTable dt = new DataTable();
            dt = GetTableSchemaNpu();

            for (int i = 0; i < userName.Count; i++)
            {
                DataRow r = dt.NewRow();
                r[0] = 0;
                r[1] = userName[i].CustomerId == null ? 0 : userName[i].CustomerId;
                r[2] = userName[i].CustomerName;
                r[3] = popupMsgPlan.NewsId;

                dt.Rows.Add(r);

                if ((i > 0 && i % batchNum == 0) || i == userName.Count - 1)
                {
                    BulkToDB(dt, "NewsPopupUser");
                    dt.Reset();
                    dt = GetTableSchemaNpu();
                }
            }
        }

        /// <summary>
        /// 一次性把Table中的数据插入到数据库
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tableName">表名</param>
        private void BulkToDB(DataTable dt, string tableName)
        {
            string constr = ConfigurationManager.ConnectionStrings["CMS"].ConnectionString;
            SqlConnection sqlConn;

            if (constr == "")
                sqlConn = new SqlConnection("Data Source=(local);Initial Catalog=CMS;Integrated Security=true");
            else
                sqlConn = new SqlConnection(constr);

            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn);
            bulkCopy.DestinationTableName = tableName; 
            bulkCopy.BatchSize = dt.Rows.Count;
            bulkCopy.BulkCopyTimeout = 999;

            try
            {
                sqlConn.Open();
                if (dt != null && dt.Rows.Count != 0)
                    bulkCopy.WriteToServer(dt);
            }
            catch (Exception ex)
            {
                Loger.Error(ex,"批量插入出错");
            }
            finally
            {
                sqlConn.Close();
                bulkCopy.Close();
            }
        }

        #region 对应表结构对象

        /// <summary>
        /// 构建和数据库ReceiveMsg表结构一样的DataTable对象
        /// </summary>
        /// <returns></returns>
        private DataTable GetTableSchema()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]{
                new DataColumn("MsgId",typeof(int)),
                new DataColumn("Title",typeof(string)),
                new DataColumn("Content",typeof(string)),
                new DataColumn("ImgUrl",typeof(string)),
                new DataColumn("PageUrl",typeof(string)),
                new DataColumn("SenderId",typeof(int)),
                new DataColumn("Sender",typeof(string)),
                new DataColumn("ReceiverId",typeof(int)),
                new DataColumn("Receiver",typeof(string)),
                new DataColumn("CreaedTime",typeof(DateTime)),
                new DataColumn("SendTime",typeof(DateTime)),
                new DataColumn("MsgSource",typeof(int)),
                new DataColumn("MsgSourceId",typeof(int)),
                new DataColumn("ReplyQuestion",typeof(string)),
                new DataColumn("Status",typeof(int))
            });

            return dt;
        }

        /// <summary>
        /// 构建和数据库InformationOfUser表结构一样的DataTable对象
        /// </summary>
        /// <returns></returns>
        private DataTable GetTableSchemaIou()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]{
                new DataColumn("Id",typeof(int)),
                new DataColumn("ReceiverId",typeof(int)),
                new DataColumn("Receiver",typeof(string)),
                new DataColumn("NewsId",typeof(int)),
                new DataColumn("ParentId",typeof(int)),
                new DataColumn("PushColumn",typeof(int))
            });

            return dt;
        }

        /// <summary>
        /// 构建和数据库NewsPopupUser表结构一样的DataTable对象
        /// </summary>
        /// <returns></returns>
        private DataTable GetTableSchemaNpu()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]{
                new DataColumn("Id",typeof(int)),
                new DataColumn("ReceiverId",typeof(int)),
                new DataColumn("Receiver",typeof(string)),
                new DataColumn("NewsIds",typeof(string))
            });

            return dt;
        }

        #endregion

        /// <summary>
        /// 更新Oracle移动终端推送信息表
        /// </summary>
        private void UpDataMoveData(PopupMsgPlan popupMsgPlan, List<CustomerGroup> userName)
        {
            try
            {
                DateTime b1 = DateTime.Now;
                Loger.Info("[移动终端][开始] 移动终端推送信息表更新中...");
                NewsBLL newsBll = Factory.BusinessFactory.CreateBll<NewsBLL>();
                News news = newsBll.GetNewsInfo(popupMsgPlan.NewsId??0);
                SsoOutWindowBLL ssoOutWindowBll = Factory.BusinessFactory.CreateBll<SsoOutWindowBLL>();
                int upDataNum = 0;

                if (String.Compare(popupMsgPlan.Receiver, "UP所有用户", true) == 0)
                    upDataNum = ssoOutWindowBll.UpDataTB_MOBPUSH_USERINFO(popupMsgPlan, news);
                else
                {
                    StringBuilder sb = new StringBuilder();
                    List<string> sqlList = new List<string>();

                    for (int i = 0; i < userName.Count; i++)
                    {
                        sb.Append("," + userName[i].CustomerId);
                        if (i != 0 && (i % 990 == 0 || i == userName.Count - 1))
                        {
                            sqlList.Add(sb.ToString().Substring(1, sb.Length - 1));
                            sb.Remove(0, sb.Length);
                        }
                    }

                    //int myNum = 1;
                    foreach (var item in sqlList)
                    {
                        //DateTime b = DateTime.Now;
                        upDataNum += ssoOutWindowBll.UpDataTB_MOBPUSH_USERINFO(popupMsgPlan, news, item);
                        //Loger.Debug("第 " + myNum++ + " 次 " + (int)(DateTime.Now - b).TotalMilliseconds + " 毫秒");
                    }
                }
                Loger.Info("[移动终端] 移动终端推送信息表更新完毕, 更新数据: " + upDataNum + " 条, 用时: " + (int)(DateTime.Now - b1).TotalSeconds + " 秒");
                ssoOutWindowBll.AddTB_PUSH_INFO(news, popupMsgPlan.PushColumn, popupMsgPlan);
                Loger.Info("[移动终端][结束] 资讯入库完毕");
            }
            catch (Exception ex)
            {
                Loger.Error(ex, "更新Oracle移动终端推送信息表异常: ");
            }
        }

        #region 炒股大赛


        /// <summary>
        /// 推送(炒股大赛)
        /// </summary>
        public void SsoPushStockContest(StockContestData scd)
        {
            SsoOutWindowBLL ssoOutWindowBll = Factory.BusinessFactory.CreateBll<SsoOutWindowBLL>();
            string msgTitle = string.Format(ConfigurationManager.AppSettings["StockContestMobileMsg"], scd.OperateUserName, scd.OperateType, scd.StockName, scd.Price);

            //pc端推送
            string parameter = GetAddOutWindowParameter(scd);
            string res = RequestHelper.WebRequest(AddOutWindow, "post", parameter, "UTF-8", false);
            if (!string.IsNullOrEmpty(res))
            {
                res = new EncDecUtil().decyptData(res, accesskey);
                SsoRes ssoRes = JsonHelper.DeserializeJson<SsoRes>(res);

                // 调用接口状态码，0：成功，1：该消息id 已存在，2：链接地址不存在，3：用户群组不存在，4：开始时间与结束时间不匹配，9：其它异常
                if (ssoRes.error != 0)
                    Loger.Error("[炒股大赛]  SSO数据推送返回失败, 返回:" + res + "\r\n相关参数:" + JsonHelper.ToJson(scd));
            }
            else
                Loger.Error("SSO接口调用返回空=====error=====(一般情况不会出现, 出现在超时或者无法调用)");

            Loger.Info("[炒股大赛] id=" + scd.Id + "  SSO数据推送返回结果: " + res + "\r\n相关参数:" + JsonHelper.ToJson(scd) + "\r\n手机推送中...");
            //入手机端推送表
            int num = ssoOutWindowBll.UpDataTB_MOBPUSH_USERINFO_StockContest(scd, msgTitle);
            Loger.Info("[炒股大赛] id=" + scd.Id + "  手机推送表更新数:" + num);

        }

        /// <summary>
        /// 获取新增弹窗消息接口参数(炒股大赛)
        /// </summary>
        private string GetAddOutWindowParameter(StockContestData scd)
        {
            /// <summary>
            /// 弹窗链接(炒股大寒高手操盘)
            /// </summary>
            string OutWindowUrlStockContest = ConfigurationManager.AppSettings["OutWindowUrlStockContest"];

            JavaScriptSerializer jss = new JavaScriptSerializer();

            AddKey_Ds ak = new AddKey_Ds();
            ak.id = scd.Id;
            ak.url = OutWindowUrlStockContest + "?" +
                "m=" + scd.OperateUserImg +
                "&n=" + scd.OperateUserName +
                "&d=" + scd.OperateTime +
                "&sn=" + scd.StockName +
                "&sc=" + scd.StockCode +
                "&q=" + scd.Quantity +
                "&p=" + scd.Price +
                "&t=" + scd.OperateType;

            ak.app_user = scd.UserId;
            ak.begin_time = DateTime.Now.AddMinutes(-15);
            ak.end_time = DateTime.Now.AddMinutes(30);
            ak.popup_type = 1;

            string jsonStr = jss.Serialize(ak);

            return EncodeString(jsonStr);
        }
        #endregion
    }

    #region Model

    /// <summary>
    /// SSO接口返回
    /// </summary>
    [Serializable]
    public class SsoRes
    {
        public int error { set; get; }
        public string msg { set; get; }
        /// <summary>
        /// 成功的用户
        /// </summary>
        public string[] success_user { set; get; }
        /// <summary>
        /// 出错的用户
        /// </summary>
        public string[] error_user { set; get; }
        /// <summary>
        /// 重复的用户
        /// </summary>
        public string[] repeat_user { set; get; }
        /// <summary>
        /// 不存在用户
        /// </summary>
        public string[] undefined_user { set; get; }
    }
    
    /// <summary>
    /// 新增弹窗消息参数
    /// </summary>
    public class AddKey
    {
        /// <summary>
        /// 用于修改, 删除时传递的值
        /// </summary>
        public int id { set; get; }
        /// <summary>
        /// 弹出页面的url
        /// </summary>
        public string url { set; get; }
        /// <summary>
        /// 需要推送的用户, 隔号分隔
        /// </summary>
        public string app_user { set; get; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? begin_time { set; get; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? end_time { set; get; }
        /// <summary>
        /// 推送类型, 0:中间弹窗, 1:右下弹窗
        /// </summary>
        public int popup_type { set; get; }
    }

    /// <summary>
    /// 新增弹窗消息参数(炒股大赛)
    /// </summary>
    public class AddKey_Ds
    {
        /// <summary>
        /// 用于修改, 删除时传递的值
        /// </summary>
        public string id { set; get; }
        /// <summary>
        /// 弹出页面的url
        /// </summary>
        public string url { set; get; }
        /// <summary>
        /// 需要推送的用户, 隔号分隔
        /// </summary>
        public string app_user { set; get; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? begin_time { set; get; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? end_time { set; get; }
        /// <summary>
        /// 推送类型, 0:中间弹窗, 1:右下弹窗
        /// </summary>
        public int popup_type { set; get; }
    }

    /// <summary>
    /// 修改消息状态参数
    /// </summary>
    public class ChangeKey
    {
        public int id { set; get; }
        public int status { set; get; }
       
    }

    #endregion
}
