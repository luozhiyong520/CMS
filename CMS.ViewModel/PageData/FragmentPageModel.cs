using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;
using System.Data;
using Common;

namespace CMS.ViewModel
{
   public class FragmentPageModel
    {
        public PagingInfo PagingInfo { get; set; }
        public DataTable DataTable { get; set; }
        public DataTable DataTableHistory { get; set; }
        public string strhtml { get; set; }
        public int TypeId { get; set; }
        public List<Fragment> List;      
        public FragmentInfoModel FragmentInfo { get; set; }

        public FragmentPageModel()
		{
		}
    }
}
