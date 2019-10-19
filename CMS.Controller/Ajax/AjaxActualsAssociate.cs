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
    public class AjaxActualsAssociate
    {
        
        ActualsAssociateBLL actualsAssociateBLL = Factory.BusinessFactory.CreateBll<ActualsAssociateBLL>();
        /// <summary>
        /// 新增现货关联
        /// </summary>
        /// <returns></returns>
        [Action]
        public string InsertActualsAssociate(ActualsAssociate actualsAssociate)
        {
            string result ="";
            actualsAssociate.CreatedUser = UserCookies.AdminName;
            actualsAssociate.CreatedTime = DateTime.Now;
            result = actualsAssociateBLL.InsertActualsAssociate(actualsAssociate);
            return result;
        }

        /// <summary>
        /// 修改现货关联
        /// </summary>
        [Action]
        public string UpdateActualsAssociate(ActualsAssociate actualsAssociate)
        {
            actualsAssociate.CreatedUser = UserCookies.AdminName;
            actualsAssociate.CreatedTime = DateTime.Now;
            string result = actualsAssociateBLL.UpdateActualsAssociate(actualsAssociate);
            return result;
        }
        /// <summary>
        /// 删除现货关联
        /// </summary>
        /// <param name="Id">现货关联ID</param>
        /// <returns></returns>
        [Action]
        public int DeleteActualsAssociate(int Id)
        {
            ActualsAssociate actualsAssociate = new ActualsAssociate();
            actualsAssociate.Id =Id;
            int result = actualsAssociateBLL.Delete(actualsAssociate);
            //return new RedirectResult("/actualsAssociate/actualsAssociatelist.aspx");
            return result;
        }

        /// <summary>
        /// 根据现货关联ID查找数据
        /// </summary>
        /// <param name="Id">现货关联ID</param>
        /// <returns>返回json对象</returns>
        [Action]
        public JsonResult GetActualsAssociateById(int Id)
        {
            ActualsAssociate actualsAssociate = actualsAssociateBLL.Get("Id",Id);
            JsonResult json = new JsonResult(actualsAssociate);
            return json;
        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <returns>返回json对象</returns>
        [Action]
        public JsonResult SearchActualsAssociate(string actualsName, string stockName, int typeId, int? page, int rows)
        {
            PagingInfo PageInfo = new PagingInfo();
            if (!string.IsNullOrEmpty(actualsName))
            {
                actualsName = string.Format("%{0}%", actualsName);
            }
            if (!string.IsNullOrEmpty(stockName))
            {
                stockName = string.Format("%{0}%", stockName);
            }
            PagedResult<ActualsAssociate> pagedResult =actualsAssociateBLL.GetActualsAssociateList(typeId,actualsName,stockName,page, rows);
            PageInfo.PageIndex = page.HasValue ? page.Value - 1 : 0;
            PageInfo.PageSize = rows;
            PageInfo.TotalRecords = pagedResult.Total;
            var result = new GridResult<ActualsAssociate>(pagedResult.Result, pagedResult.Total);
            return new JsonResult(result);
        }
    }
}
