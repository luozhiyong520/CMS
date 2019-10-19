using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;
using System.Data;
using Common;

namespace CMS.ViewModel
{
    public class AnalystLivePageModel
    {
        public AnalystLive AnalystLive { get; set; }
        public int AnalystId { get; set; }
        public string AnalystName { get; set; }
        public int AnalystType { get; set; }
       // public AnalystLive TransStatisticsNum { get; set; }
    }
}
