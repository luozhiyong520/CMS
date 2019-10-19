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
   public class TemplatedetailController
    {
       [Action]
       [PageUrl(Url = "/news/templatedetailmenu.aspx")]
       public object GetData(string templateId, string templeteType)
       {
           TemplatedetailPageModel Model = new TemplatedetailPageModel();
           Model.templateId = templateId;
           Model.templeteType = templeteType;
           return new PageResult(null, Model);
       }
    }
}
