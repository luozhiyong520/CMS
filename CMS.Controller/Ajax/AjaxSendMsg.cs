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
    public class AjaxSendMsg
    {

        SendMsgBLL sendMsgBLL = Factory.BusinessFactory.CreateBll<SendMsgBLL>();
        MsgTypeBLL msgTypeBLL = Factory.BusinessFactory.CreateBll<MsgTypeBLL>();
        ReceiveMsgBLL receiveMsgBLL = Factory.BusinessFactory.CreateBll<ReceiveMsgBLL>();

        /// <summary>
        /// 后台留言列表
        /// </summary>
        /// <param name="page">当前页索引</param>
        /// <param name="rows">每页显示的条数</param>
        /// <param name="TypeContent">问题类型</param>
        /// <param name="MsgType">问题类别</param>
        /// <param name="TuTimeStart">问题开始时间</param>
        /// <param name="TuTimeEnd">问题结束时间</param>
        /// <param name="ReplyTimeStart">回复开始时间</param>
        /// <param name="ReplyTimeEnd">回复结束时间</param>
        /// <param name="YesOrNo">是否回复</param>
        /// <param name="Editor">回复人</param>
        /// <returns></returns>
        [Action]
        public JsonResult SearchSendMsg(int? page, int rows, int TypeContent, string MsgType, string MsgContent, string TuTimeStart, string TuTimeEnd, string ReplyTimeStart, string ReplyTimeEnd, string YesOrNo, string Editor)
        {

            PagingInfo PageInfo = new PagingInfo();
            var authorityDotBLL = Factory.BusinessFactory.CreateBll<AuthorityDotBLL>(true);
            var authorityDot = authorityDotBLL.GetAuthorityDotByName("留言问题类型");
            PagedResult<SendMsg> pagedResult = sendMsgBLL.GetSendMsgList(page, rows, authorityDot.Id, TypeContent, MsgType, MsgContent, TuTimeStart, TuTimeEnd, ReplyTimeStart, ReplyTimeEnd, YesOrNo, Editor);
            PageInfo.PageIndex = page.HasValue ? page.Value - 1 : 0;
            PageInfo.PageSize = rows;
            PageInfo.TotalRecords = pagedResult.Total;
            var result = new GridResult<SendMsg>(pagedResult.Result, pagedResult.Total);
            return new JsonResult(result);
        }
        /// <summary>
        /// 获取问题类型
        /// </summary>
        /// <returns></returns>
        [Action]
        public JsonResult GetMsgTypeData()
        {
            var authorityDotBLL = Factory.BusinessFactory.CreateBll<AuthorityDotBLL>(true);
            var authorityDot = authorityDotBLL.GetAuthorityDotByName("留言问题类型");
            var result = msgTypeBLL.GetMsgTypeByAdminId(authorityDot.Id);
            if (result != null)
            {
                return new JsonResult(result);
            }
            return null;
        }
        //修改留言
        [Action]
        public int UpdateSendMsgById(int msgid, string replyContent, string replier, DateTime replyTime)
        {
            SendMsg sendMsg = sendMsgBLL.Get("MsgId", msgid);
            sendMsg.ReplyContent = replyContent;
            sendMsg.Replier = replier;
            sendMsg.ReplyTime = replyTime;
            sendMsg.Editor = UserCookies.AdminName;
            int count = sendMsgBLL.Update(sendMsg);
            //添加到收件箱表
            ReceiveMsg receiveMsg = new ReceiveMsg();
            receiveMsg.Content = sendMsg.ReplyContent;
            receiveMsg.SenderId = 0;
            receiveMsg.Sender = sendMsg.Replier;
            receiveMsg.ReceiverId = sendMsg.QuCustomerId;
            receiveMsg.Receiver = sendMsg.QuCustomerName;
            receiveMsg.CreaedTime = DateTime.Now;
            receiveMsg.SendTime = DateTime.Now;
            receiveMsg.MsgSource = 2;
            receiveMsg.MsgSourceId = sendMsg.MsgId;
            receiveMsg.ReplyQuestion = sendMsg.MsgContent;
            receiveMsg.Status = 1;
            receiveMsgBLL.Add(receiveMsg);
            return count;
        }
        //上一题
        [Action]
        public JsonResult getSendMsgbackById(int msgid, string MsgIds)
        {
            SendMsg sendMsg = sendMsgBLL.GetSendMsgbackById(msgid, MsgIds);
            if (sendMsg != null)
            {
                sendMsg.Replier = "UP量化安全炒股卫士";
                if (!string.IsNullOrEmpty(sendMsg.ReplyContent))
                {
                    sendMsg.ReplyTime = sendMsg.ReplyTime;
                }
                else
                {
                    sendMsg.ReplyTime = DateTime.Now;
                }
            }
            JsonResult json = null;
            if (sendMsg != null)
            {
                json = new JsonResult(sendMsg);
                return json;
            }
            else
                return null;
        }
        //下一题
        [Action]
        public JsonResult getSendMsggoById(int msgid, string MsgIds)
        {
            SendMsg sendMsg = sendMsgBLL.GetSendMsggoById(msgid, MsgIds);
            if (sendMsg != null)
            {
                sendMsg.Replier = "UP量化安全炒股卫士";
                if (!string.IsNullOrEmpty(sendMsg.ReplyContent))
                {
                    sendMsg.ReplyTime = sendMsg.ReplyTime;
                }
                else
                {
                    sendMsg.ReplyTime = DateTime.Now;
                }
            }

            JsonResult json = null;
            if (sendMsg != null)
            {
                json = new JsonResult(sendMsg);
                return json;
            }
            else
                return null;
        }
    }
}
