using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.DAL;
using CMS.Model;
using System.Data;
using Common;
using CMS.HtmlService.Contract;
using System.ServiceModel;

namespace CMS.BLL
{
   public partial class NoticeBLL
    {
       public string EditNotice(Notice notice)
       {
           string returnstring = RequestHelper.WebRequest("http://localhost:12924/mainpage/test.aspx?productid=3001", "post", "", "utf-8");
           return "";
       }
    }
}
