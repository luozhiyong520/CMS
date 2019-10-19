using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class PagingInfo
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }

        public int CalPageCount()
        {
            if (this.PageSize != 0 && this.TotalRecords != 0)
            {
                return (int)Math.Ceiling((double)(((double)this.TotalRecords) / ((double)this.PageSize)));
            }
            return 0;
        }

    }
}
