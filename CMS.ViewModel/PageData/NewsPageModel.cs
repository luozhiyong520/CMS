using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;
using System.Data;
using Common;

namespace CMS.ViewModel
{
    public class NewsPageModel
    {
        public List<News> NewsList;
        public DataTable DataTable { get; set; }
        public List<Channel> List;
        public List<Administrator> AdministratorList;

        public string ChannelName{get;set;}
        public int Num { get; set; }
        public string Time { get; set; }
    }
}
