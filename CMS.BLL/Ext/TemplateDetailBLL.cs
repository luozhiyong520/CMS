using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.DAL;
using System.Data;

namespace CMS.BLL
{
     public partial class TemplateDetailBLL
    {        
         public int DeleteTemplateDetailByTemplateId(int templateId)
         {             
             return dal.DeleteTemplateDetailByTemplateId(templateId);
         }
         public bool ShieldTemplateDetailCheck(int templateId)
         {
             return dal.ShieldTemplateDetailCheck(templateId);
         }
         public DataTable GetTemplateDetailChecked(int templateId)
         {
             return dal.GetTemplateDetailChecked(templateId);
         }
    }
}
