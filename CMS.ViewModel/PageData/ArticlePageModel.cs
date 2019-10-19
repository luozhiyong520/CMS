using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;
using System.Data;
using Common;

namespace CMS.ViewModel
{
    public class ArticlePageModel
    {
        public List<News> NewsList;
        public DataTable DataTable { get; set; }
        public List<Channel> List;

        public News News { get; set; }
        public string Channels { get; set; }
        public string Prefix { get; set; }

    }
}
