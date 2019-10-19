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
   public partial class SendMsgBLL
    {
       public PagedResult<SendMsg> GetSendMsgList(int? page, int rows, int authorityParentId, int TypeContent, string MsgType, string MsgContent, string TuTimeStart, string TuTimeEnd, string ReplyTimeStart, string ReplyTimeEnd, string YesOrNo, string Editor)
       {
           int adminId = UserCookies.AdminId;
           PagedResult<SendMsg> st = dal.GetSendMsgList(page, rows, adminId, authorityParentId, TypeContent, MsgType, MsgContent, TuTimeStart, TuTimeEnd, ReplyTimeStart, ReplyTimeEnd, YesOrNo, Editor);
           return st;
       }
       public SendMsg GetSendMsgById(int msgId)
       {
           return dal.GetSendMsgById(msgId);
       }
       //上一题
       public SendMsg GetSendMsgbackById(int msgid,string MsgIds)
       {
           int msgTypeId = UserCookies.AdminId;
           return dal.GetSendMsgbackById(msgid,msgTypeId,MsgIds);
       }
       //下一题
       public SendMsg GetSendMsggoById(int msgid, string MsgIds)
       {
           int msgTypeId = UserCookies.AdminId;
           return dal.GetSendMsggoById(msgid, msgTypeId,MsgIds);
       }
    }
}
