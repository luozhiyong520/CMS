using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.BLL;
using SMVC;
using Common;
using System.Web;
using CMS.Model;
using CMS.ViewModel;


namespace CMS.Controller
{
     [Authorize]
    public class AjaxDirection
    {
         DirectionBLL directionBLL = Factory.BusinessFactory.CreateBll<DirectionBLL>();
         /// <summary>
         /// 修改风向标
         /// </summary>
         /// <param name="dir"></param>
         /// <returns></returns>
         [Action]
         public string EditDirection(Direction dir)
         {
             if (string.IsNullOrEmpty(dir.Popularity))
                 return "000001";   //人气指标不能为空
             dir.Id = int.Parse(dir.TypeId.ToString());
             directionBLL.Update(dir).ToString();
             HtmlServer.CreateTemplatePage(1);
             HtmlServer.CreateTemplatePage(107);
             return "000000";
            
          }
    }
}
