using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace CMS.ViewModel
{
    public class PageTemplateSerachInfo : PagingInfo
    {
        public int ID;
        public int templeteType;
        public string SearchWordTemplateName;
        public string SearchWordFileName;
    }
}
