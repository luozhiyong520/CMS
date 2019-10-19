using System;
using System.Collections.Generic;
using CMS.BLL;
using SMVC;
using Common;
using CMS.Model;
using CMS.ViewModel;
using CMS.Utility;
using Factory;
using CMS.HtmlService.Contract;
using System.IO;
using System.Web;
using System.Text;

namespace CMS.Controller
{
    [Authorize]
   public class AjaxPageTemplate
    {
       PageTemplateBLL pageTemplateBLL = Factory.BusinessFactory.CreateBll<PageTemplateBLL>();
      
       /// <summary>
       /// 添加模板
       /// </summary>
       /// <param name="templateName">模板名称</param>
       /// <param name="templateFileName">文件名</param>
       /// <param name="orderNum">排序值</param>
       /// <param name="htmlPath">路径</param>
       /// <param name="status">状态</param>
       /// <param name="remark">简介</param>
       /// <param name="templeteType">模板类型</param>
       /// <returns></returns>
       [Action]
       public int InsertPageTemplate(PageTemplate pageTemplate)
       {
           //PageTemplate pageTemplate = new PageTemplate();
           //pageTemplate.TemplateName = templateName;
           //pageTemplate.TemplateFileName = templateFileName;
           //pageTemplate.OrderNum = orderNum;
           //pageTemplate.HtmlPath = htmlPath;
           //pageTemplate.Status = status;
           //pageTemplate.Remark = remark;
           //pageTemplate.TempleteType=templeteType;
           pageTemplate.CreatedUser = UserCookies.AdminName;
           pageTemplate.CreatedTime = DateTime.Now;
           int msg = pageTemplateBLL.Add(pageTemplate);
           return msg;
       }
       /// <summary>
       /// 更新模板
       /// </summary>
       /// <param name="templateId">模板Id</param>
       /// <param name="templateName">模板名称</param>
       /// <param name="templateFileName">文件名</param>
       /// <param name="orderNum">排序值</param>
       /// <param name="htmlPath">路径</param>
       /// <param name="status">状态</param>
       /// <param name="remark">简介</param>
       /// <param name="templeteType">模板类型</param>
       /// <returns></returns>
       [Action]
       public int UpdatePageTemplate(PageTemplate pageTemplate)
       {
           pageTemplate.UpdatedUser = UserCookies.AdminName;
           pageTemplate.UpdatedTime = DateTime.Now;
           int msg = pageTemplateBLL.Update(pageTemplate);
           return msg;
       }
        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="templateId">模板ID</param>
        /// <returns></returns>
       [Action]
       public int DeletePageTemplate(int templateId)
       {
           //PageTemplate template = pageTemplateBLL.Get("TemplateId",templateId);
           //int result = pageTemplateBLL.Delete(template);
           int result = pageTemplateBLL.DeletePageTemplate(templateId);
           return result;
       }
        /// <summary>
        /// 根据模板ID查找数据
        /// </summary>
        /// <param name="templateId">模板ID</param>
        /// <returns>返回json对象</returns>
        [Action]
        public JsonResult GetTemplateBytemplateById(int templateId)
        {
            PageTemplate pageTemplate =pageTemplateBLL.Get("TemplateId",templateId);
            JsonResult json = new JsonResult(pageTemplate);
            return json;
        }
       /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="templeteType">模板类型</param>
        /// <param name="templateName">模板名称</param>
        /// <param name="templateFileName">文件名</param>
         /// <param name="templateId">模板ID</param>
        /// <param name="page">页的索引</param>
        /// <returns>返回json对象</returns>
         [Action]
        public JsonResult SearchPageTemplate(int templateType, string templateName, string fileName, int? templateId, int? page, int rows)
         {
             PageTemplatePageModel model = new PageTemplatePageModel();
             PageTemplateSerachInfo info = new PageTemplateSerachInfo();
             PagedResult pagedResult = pageTemplateBLL.GetPageTemplatePageList(templateType, page, rows,templateName,fileName );
             model.DataTable = pagedResult.Result;
             info.ID = templateId.HasValue ? templateId.Value : 0;
             info.PageIndex = page.HasValue ? page.Value - 1 : 0;
             info.PageSize = rows;
             info.TotalRecords = pagedResult.Total;
             if (info.ID == 0)
             {
                 if (model.DataTable.Rows.Count <= 0)
                 {
                     info.ID = 0;
                 }
                 else
                 {
                     info.ID = int.Parse(model.DataTable.Rows[0]["TemplateId"].ToString());
                 }
             }
             model.PageTemplateInfo = new PageTemplateInfoModel(
                 model.DataTable,new PageTemplate(info.ID,info.SearchWordTemplateName,info.SearchWordFileName)
                 );
             model.PagingInfo = info;
             model.templeteType =templateType;
             var result = new GridResult<PageTemplate>(pagedResult.Result.ToList<PageTemplate>(), pagedResult.Total);
             return new JsonResult(result);
         }

         /// <summary>
         /// 根据模板生成html文件
         /// </summary>
         /// <param name="templateId"></param>
         [Action]
         public void ProductListTemplateHtml(int templateId)
         {
             HtmlServer.CreateTemplatePage(templateId);
         }

         private string GetFilePath(TemplateBModel template)
         {
             string webPath = HttpContext.Current.Request.PhysicalApplicationPath+"pages\\";
             string dictory=string.Empty;
             string fileName = string.Empty; 
             //处理根目录
             if (template.Path != "0")
             {
                 dictory = template.Path + "\\";
             }
             //获取文件名
             fileName = template.FileName.Substring(0, template.FileName.Length - 6);
             return webPath + dictory + fileName;

         }

    }
}
