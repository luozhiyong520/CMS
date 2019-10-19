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
   public class SendMsgController
    {
       SendMsgBLL sendMsgBLL = Factory.BusinessFactory.CreateBll<SendMsgBLL>();
       [Action]
       [PageUrl(Url = "/popupwindow/sendmsgedit.aspx")]
       public object GetData(int msgid, string paramslist)
       {
           SendMsgPageModel Model = new SendMsgPageModel();
           SendMsg sendmsg = sendMsgBLL.GetSendMsgById(msgid);
           if (sendmsg!=null)
           {
               if (sendmsg.ReplyContent!="")
               {
                   Model.ViewText = "disabled='disabled'";
               }
               sendmsg.Replier = "UP量化安全炒股卫士";
               if (!string.IsNullOrEmpty(sendmsg.ReplyContent))
               {
                   sendmsg.ReplyTime = sendmsg.ReplyTime;
               }
               else
               {
                   sendmsg.ReplyTime = DateTime.Now;
               }
               Model.SendMsg = sendmsg;
               Model.ParamsList = paramslist;
               //Model.TypeContent = TypeContent;
               //Model.MsgType = MsgType;
               //Model.TuTimeStart = TuTimeStart;
               //Model.TuTimeEnd = TuTimeEnd;
               //Model.ReplyTimeStart = ReplyTimeStart;
               //Model.ReplyTimeEnd = ReplyTimeEnd;
               //Model.YesOrNo = YesOrNo;
               //Model.Editor = Editor;
           }
           return new PageResult(null, Model);
       }
    }
}
