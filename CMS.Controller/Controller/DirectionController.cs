using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.BLL;
using SMVC;
using CMS.Model;
using CMS.ViewModel;
using Common;
using System.Web;
using System.Configuration;

namespace CMS.Controller
{
     [Authorize]
    public class DirectionController
    {
         DirectionBLL directionBLL = Factory.BusinessFactory.CreateBll<DirectionBLL>();
         DirectionPageModel model = new DirectionPageModel();
         /// <summary>
         /// 风向标
         /// </summary>
         /// <returns></returns>
         ///  
         [Action]
         [PageUrl(Url = "/updirection/updirection.aspx")]
         public object GetDirectionList()
         {
             List<Direction> list = new List<Direction>();
             list = directionBLL.GetAll();
             model.StockList = list.SingleOrDefault(p => p.TypeId == 1);
             model.ActualsList = list.SingleOrDefault(p => p.TypeId == 2);
             model.FuturesList = list.SingleOrDefault(p => p.TypeId == 3);
             model.GjsList = list.SingleOrDefault(p => p.TypeId == 4);
             return new PageResult(null, model); 
         }

        
    }
}
