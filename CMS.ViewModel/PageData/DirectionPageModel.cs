using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;

namespace CMS.ViewModel
{
   public class DirectionPageModel
    {
       public Direction StockList { get; set; }
       public Direction ActualsList { get; set; }
       public Direction FuturesList { get; set; }
       public Direction GjsList { get; set; }
    }
}
