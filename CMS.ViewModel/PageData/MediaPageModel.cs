using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;
using System.Data;
using Common;

namespace CMS.ViewModel
{
    public class MediaPageModel
    {

        public List<MediaClass> MediaClassList;
        public List<Media> MediaList;
        public DataTable DataTable { get; set; }
 
    }
}
