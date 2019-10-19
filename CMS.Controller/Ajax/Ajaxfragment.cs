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
    public class AjaxFragment
    {

        FragmentBLL fragmentBLL = Factory.BusinessFactory.CreateBll<FragmentBLL>();
       /// <summary>
       /// 新增碎片
       /// </summary>
       /// <param name="channelID">栏目ID</param>
       /// <param name="content">碎片内容</param>
       /// <param name="orderNum">排序号</param>
       /// <param name="isDelete">是否删除</param>
       /// <param name="Type">类型</param>
       /// <returns></returns>
        [Action]
        public int InsertFragment(Fragment fragment)
        {
            int result = 0;
            fragment.CreatedUser = UserCookies.AdminName;
            fragment.CreatedTime = DateTime.Now;
            result = fragmentBLL.Add(fragment);
            //重新根据模板生成页面
            HtmlServer.CreateTemplatePageByChannelId(fragment.ChannelId);
            return result;
        }

        /// <summary>
        /// 修改碎片
        /// </summary>
        /// <param name="fragmentId">碎片ID</param>
        /// <param name="channelID">栏目ID</param>
        /// <param name="content">碎片内容</param>
        /// <param name="orderNum">排序号</param>
        /// <param name="isDelete">是否删除</param>
        /// <param name="TypeId">类型</param>
        /// <returns></returns>
        [Action]
        public int UpdateFragment(Fragment fragment)
        {
            fragment.CreatedUser = UserCookies.AdminName;
            fragment.CreatedTime = DateTime.Now;
            int result = fragmentBLL.Add(fragment);
            //重新根据模板生成页面
            HtmlServer.CreateTemplatePageByChannelId(fragment.ChannelId);
            return result;           
        }
        /// <summary>
        /// 恢复历史记录
        /// </summary>
        /// <param name="fragment"></param>
        /// <returns></returns>
        [Action]
        public int UpdateFragmentBack(Fragment fragment)
        {
            fragment.UpdatedUser = UserCookies.AdminName;
            fragment.UpdatedTime = DateTime.Now;
            fragment.CreatedUser = UserCookies.AdminName;
            fragment.CreatedTime = DateTime.Now;
            if (fragment.IsDeleted == true)
            {
                fragment.DeletedUser = UserCookies.AdminName;
                fragment.DeletedTime = DateTime.Now;
            }
            int result = fragmentBLL.Update(fragment);
            //重新根据模板生成页面
            HtmlServer.CreateTemplatePageByChannelId(fragment.ChannelId);
            return result;
        }

        /// <summary>
        /// 依据碎片内容重新生成模板
        /// </summary>
        /// <param name="channelId">栏目ID</param>
        //public void ReProudctListPageHtml(string channelId)
        //{
        //    List<int> templateIdList = fragmentBLL.GetTemplateIdListByChanel(channelId);
        //    foreach (int templateId in templateIdList)
        //    {
        //        (new AjaxPageTemplate()).ProductListTemplateHtml(templateId);
        //    }
        //}

        /// <summary>
        /// 删除碎片
        /// </summary>
        /// <param name="Id">碎片ID</param>
        /// <returns></returns>
        [Action]
        public int DeleteFragment(int fragmentId)
        {
            Fragment fragment = new Fragment();
            fragment.FragmentId = fragmentId;
            int result= fragmentBLL.Delete(fragment);
            //return new RedirectResult("/fragment/fragmentlist.aspx");
            return result;
        }

        /// <summary>
        /// 根据碎片ID查找数据
        /// </summary>
        /// <param name="Id">碎片ID</param>
        /// <returns>返回json对象</returns>
        [Action]
        public JsonResult GetFragmentById(int fragmentId)
        {
            Fragment fragment = fragmentBLL.Get("FragmentId", fragmentId);
            JsonResult json = new JsonResult(fragment);
            return json;
        }
        /// <summary>
        /// 得到历史记录数据
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        [Action]
        public JsonResult GetFragmentHistory(string channelId)
        {
            DataTable dt = fragmentBLL.GetFragmentHistory(channelId);
            Fragment fragment = null;
            if (dt.Rows.Count == 1)
            {
                fragment = new Fragment();
                fragment.Content = dt.Rows[0]["Content"].ToString();
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 1)
                    {
                        fragment = new Fragment();
                        fragment.Content = dt.Rows[i]["Content"].ToString();
                        break;
                    }
                }
            }
            JsonResult json = new JsonResult(fragment);
            return json; 
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="Type">类型</param>
        /// <param name="searchdrpchannelID">栏目ID</param>
        /// <param name="searchContent">碎片内容</param>
        /// <param name="Id">碎片ID</param>
        /// <param name="page">页的索引</param>
        /// <returns>返回json对象</returns>
        [Action]
        public JsonResult SearchFragment(int typeId, string channelId, string keywords, int? fragmentId, int? page, int rows)
        {
            FragmentPageModel model = new FragmentPageModel();
            FragmentSerachInfo info = new FragmentSerachInfo();
            keywords = string.Format("%{0}%", keywords);
            PagedResult pagedResult = fragmentBLL.GetPageList(typeId, channelId, keywords, rows, page);
            model.DataTable = pagedResult.Result;
            info.FragmentId = fragmentId.HasValue ? fragmentId.Value : 0;
            info.PageIndex = page.HasValue ? page.Value - 1 : 0;
            info.PageSize = rows;
            info.TotalRecords = pagedResult.Total;
            if (info.FragmentId == 0)
            {
                if (model.DataTable.Rows.Count > 0)
                {
                    info.FragmentId = int.Parse(model.DataTable.Rows[0]["FragmentId"].ToString());
                }
            }
            model.FragmentInfo = new FragmentInfoModel(
                model.DataTable, new Fragment(info.FragmentId, info.SearchWord)
                );
            model.PagingInfo = info;
            model.TypeId = typeId;
            var result = new GridResult<Fragment>(pagedResult.Result.ToList<Fragment>(), pagedResult.Total);
            return new JsonResult(result);
        }
    }
}
