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
 

namespace CMS.Controller
{
    [Authorize]
   public class AjaxSSOResult
    {
        SSOResultBLL SSORsultBLL = Factory.BusinessFactory.CreateBll<SSOResultBLL>();
        SSOResult SSOResult = new SSOResult();
        PagingInfo PageInfo = new PagingInfo();
        /// <summary>
        /// 获取SSO推送列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [Action]
        public JsonResult SearchSSOResultList(int? page, int rows)
        {
            PagedResult<SSOResult> pagedResult = SSORsultBLL.GetSSOResultList(page, rows);
            PageInfo.PageIndex = page.HasValue ? page.Value - 1 : 0;
            PageInfo.PageSize = rows;
            PageInfo.TotalRecords = pagedResult.Total;

            var result = new GridResult<SSOResult>(pagedResult.Result, pagedResult.Total);
            return new JsonResult(result);

        }
        /// <summary>
        /// SSO数据重新推送
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Action]
        public void ReSsoPush(int Id)
        {
            SsoServer.SsoPushOne(Id);
        }

    }
}
