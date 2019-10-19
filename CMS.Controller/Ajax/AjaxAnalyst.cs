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

namespace CMS.Controller
{
    [Authorize]
   public class AjaxAnalyst
    {
        AnalystBLL analystBLL = Factory.BusinessFactory.CreateBll<AnalystBLL>();
        //初始化数据
        [Action]
        public JsonResult AnalystListPage(int? page, int rows, int analysType, int vipType)
        {
            PagingInfo PageInfo = new PagingInfo();
            PagedResult<Analyst> pagedResult = analystBLL.AnalystListPage(page, rows, analysType, vipType);
            PageInfo.PageIndex = page.HasValue ? page.Value - 1 : 0;
            PageInfo.PageSize = rows;
            PageInfo.TotalRecords = pagedResult.Total;
            var result = new GridResult<Analyst>(pagedResult.Result, pagedResult.Total);
            return new JsonResult(result);
        }

        /// <summary>
        /// 获取现货分析师/贵金属分析师列表
        /// </summary>
        /// <param name="AnalystType"></param>
        /// <returns></returns>
        [Action]
        public JsonResult Analystlist(int AnalystType)
        {
            List<Analyst> list = analystBLL.Analystlist(AnalystType);
            if(list!=null)
                return new JsonResult(list);
            return null;
           
        }
        [Action]
        public JsonResult AnalystByAnalystId(int AnalystId)
        {
            JsonResult json = null;
            Analyst analyst = analystBLL.Get("AnalystId", AnalystId);
            if (analyst!=null)
            {
                 json = new JsonResult(analyst);
            }
            return json;
        }
        /// <summary>
        /// 分析师的修改
        /// </summary> 
        /// <param name="analyst"></param>
        /// <returns></returns>
        [Action]
        public string EditAnalyst(Analyst analyst)
        {
           // analyst.AdminId = UserCookies.AdminId;
            return analystBLL.EditAnalyst(analyst);
        }

        [Action]
        public string EditAnalystForSort(int analystId, int sort)
        {

            return analystBLL.EditAnalystSort(analystId, sort);
        }
        /// <summary>
        /// 新增分析师
        /// </summary>
        /// <param name="analyst"></param>
        /// <returns></returns>
        [Action]
        public string AnalystAdd(Analyst analyst)
        {
            string msg = "";
            Analyst _analyst = analystBLL.Get("AnalystName",analyst.AnalystName);
            if (_analyst != null)
            {
               msg = "cz";
            }
            else
            {
               // analyst.AdminId = UserCookies.AdminId;
                msg = analystBLL.Add(analyst).ToString();
               //  HtmlServer.CreateTemplatePage(15);
            }
            return msg;
        }

        /// <summary>
        /// 关联分析师表
        /// </summary>
        /// <param name="analystId"></param>
        [Action]
        public void AnalystRelative(string analystId,int adminId)
        {
            analystBLL.AnalystRelative(analystId, adminId);
        }


    }
}
