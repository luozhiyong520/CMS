using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;
using System.Data;
using Common;

namespace CMS.ViewModel
{
   public class PageTemplateInfoModel
    {
        public List<HtmlOptionItem> Categories;
        public PageTemplate PageTemplate;

        public PageTemplateInfoModel() { }

        public PageTemplateInfoModel(DataTable dt, PageTemplate pageTemplate)
        {
            if (dt == null)
                throw new ArgumentNullException("dt");
            if (pageTemplate == null)
                throw new ArgumentNullException("pageTemplate");
            this.PageTemplate = pageTemplate;
            this.Categories = ConvertCategoryList(dt, pageTemplate.TemplateId);
        }

        public static List<HtmlOptionItem> ConvertCategoryList(DataTable dt, int ID)
        {
            if (dt == null)
                throw new ArgumentNullException("dt");

            List<HtmlOptionItem> categories = new List<HtmlOptionItem>(dt.Rows.Count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                categories.Add(new HtmlOptionItem
                {
                    Text = dt.Rows[i]["TemplateName"].ToString(),
                    Value = dt.Rows[i]["TemplateId"].ToString(),
                    Selected = dt.Rows[i]["TemplateId"].ToString() == ID.ToString()
                });
            }
            return categories;
        }
    }
}
