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
using CMS.Utility;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using CMS.HtmlService.Contract;
using Factory;
using CMS.BLL.Oracle;
using CMS.Model.Oracle;
 

namespace CMS.Controller
{
    public class AjaxTBPUSHINFO 
    {
        TB_PUSH_INFOBLL _TB_PUSH_INFOBLL = Factory.BusinessFactory.CreateBll<TB_PUSH_INFOBLL>();
        PagingInfo PageInfo = new PagingInfo();
        /// <summary>
        /// 获取SSO推送列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [Action]
        public JsonResult GetTB_PUSH_INFOList(int? page, int rows)
        {
            int pages = page.HasValue?page.Value-1:0;
            List<TB_PUSH_INFO> pagedResult = _TB_PUSH_INFOBLL.GetPagerData(pages, rows);
            PageInfo.PageIndex = pages;
            PageInfo.PageSize = rows;
            PageInfo.TotalRecords = _TB_PUSH_INFOBLL.GetPagerTotalRecord();
            var result = new GridResult<TB_PUSH_INFO>(pagedResult, _TB_PUSH_INFOBLL.GetPagerTotalRecord());
            return new JsonResult(result);

        }
    }
}
