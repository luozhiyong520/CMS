using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;

namespace CMS.ViewModel
{
    public class AdminControlModel
    {
        public List<AdminMenuModule> AdminOneMenuModuleList { get; set; }
        public Dictionary<int, List<AdminMenuModule>> TwoMenu = new Dictionary<int, List<AdminMenuModule>>();
    }
}
