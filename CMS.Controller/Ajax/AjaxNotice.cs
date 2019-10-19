using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMVC;
using CMS.Model;
using CMS.BLL;
using Common;

namespace CMS.Controller
{
     [Authorize]
   public class AjaxNotice
    {
         NoticeBLL NoticeBLL = Factory.BusinessFactory.CreateBll<NoticeBLL>();
         [Action]
         public string EditNotice(Notice notice)
         {
             NoticeBLL.EditNotice(notice);
             return "";
         }
    }
}
