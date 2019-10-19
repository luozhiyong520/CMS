using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;
using System.Data;
using Common;

namespace CMS.ViewModel
{
   public class PageTemplatePageModel
    {
        public PagingInfo PagingInfo { get; set; }
        public DataTable DataTable { get; set; }
        public int templeteType { get; set; }
        public List<PageTemplate> List;
        public PageTemplateInfoModel PageTemplateInfo { get; set; }

        public PageTemplatePageModel()
        { 
            
        }
    }
}
