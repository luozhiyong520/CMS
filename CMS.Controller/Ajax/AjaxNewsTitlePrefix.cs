using System;
using System.Collections.Generic;
using CMS.BLL;
using SMVC;
using Common;
using CMS.Model;
using CMS.ViewModel;
using CMS.Utility;
using Factory;
using System.IO;
using System.Web;
using System.Text;

namespace CMS.Controller
{
    [Authorize]
    public class AjaxNewsTitlePrefix
    {
        NewsTitlePrefixBLL newsTitlePrefixBLL = Factory.BusinessFactory.CreateBll<NewsTitlePrefixBLL>();

        /// <summary>
        /// 添加标题前标签
        /// </summary>
        /// <param name="Prefix">标题前标签名称</param>
        /// <returns></returns>
        [Action]
        public int InsertNewsTitlePrefix(NewsTitlePrefix newsTitlePrefix)
        {
            int msg = newsTitlePrefixBLL.Add(newsTitlePrefix);
            return msg;
        }
        /// <summary>
        /// 更新标题前标签
        /// </summary>
        /// <param name="Id">标题前标签Id</param>
        /// <param name="Prefix">标题前标签名称</param>
        /// <returns></returns>
        [Action]
        public int UpdateNewsTitlePrefix(NewsTitlePrefix newsTitlePrefix)
        {
            int msg = newsTitlePrefixBLL.Update(newsTitlePrefix);
            return msg;
        }
        /// <summary>
        /// 删除标题前标签
        /// </summary>
        /// <param name="Id">标题前标签ID</param>
        /// <returns></returns>
        [Action]
        public int DeleteNewsTitlePrefix(int Id)
        {
            NewsTitlePrefix newsTitlePrefix = newsTitlePrefixBLL.Get("Id", Id);
            int result = newsTitlePrefixBLL.Delete(newsTitlePrefix);
            return result;
        }
        /// <summary>
        /// 根据标题前标签ID查找数据
        /// </summary>
        /// <param name="Id">标题前标签ID</param>
        /// <returns>返回json对象</returns>
        [Action]
        public JsonResult GetNewsTitlePrefixById(int Id)
        {
            NewsTitlePrefix newsTitlePrefix = newsTitlePrefixBLL.Get("Id", Id);
            JsonResult json = new JsonResult(newsTitlePrefix);
            return json;
        }
        /// <summary>
        /// 标题前标签查询
        /// </summary>
        /// <param name="page">页的索引</param>
        /// <returns>返回json对象</returns>
        [Action]
        public JsonResult SearchNewsTitlePrefix(int? page, int rows)
        {
            PagingInfo PageInfo = new PagingInfo();
            PagedResult<NewsTitlePrefix> pagedResult =newsTitlePrefixBLL.GetNewsTitlePrefixList(page, rows);
            PageInfo.PageIndex = page.HasValue ? page.Value - 1 : 0;
            PageInfo.PageSize = rows;
            PageInfo.TotalRecords = pagedResult.Total;
            var result = new GridResult<NewsTitlePrefix>(pagedResult.Result, pagedResult.Total);
            return new JsonResult(result);
        }
    }
}
