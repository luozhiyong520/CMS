using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.BLL;
using CMS.Model;
using CMS.HtmlService;
using Common;
using Factory;
using CMS.HtmlService.Contract;
using System.Data;
namespace CMS.Web
{
    public partial class preview : System.Web.UI.Page
    {
        string templateid;
        DateTime? reDate;

         protected void Page_Load(object sender, EventArgs e)
        {
            templateid = Request.QueryString["TemplateId"];
            try
            {
                var date = Request.QueryString["ReDate"];
                if(date != null)
                    reDate = Convert.ToDateTime(date);
            }
            catch (Exception)
            {
                reDate = null;
            }
            DataTable dt = new DataTable();
            // dt.DefaultView.RowFilter("not )"
        
        }

        protected override void Render(HtmlTextWriter writer)
        {
            try
            {
                
                //using (WCFFactory<IHtmlEngine> ChannelFactory = WCFFactory<IHtmlEngine>.GetFactorty())
                //{
                //    IHtmlEngine proxy = ChannelFactory.CreateChannel();

                //    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                //    dictionary.Add("TemplateId", templateid);
                //    PageTemplateBLL templateBll = Factory.BusinessFactory.CreateBll<PageTemplateBLL>();

                //    TemplateBModel template = templateBll.Get(dictionary).ToBModel();
                //    result = proxy.GetHTMLByTemplate(template, true)[0];
                //}


                string result = HtmlServer.GetTemplagePreview(int.Parse(templateid), reDate);

                writer.Write(result);
                writer.Write("<script type=\"text/javascript\" src=\"js/preview.js\"></script>");
            }
            catch (Exception ex)
            {
                Loger.Error(ex, string.Format("模板预览出错,tempalteId={0}", templateid));
                throw ex;
            }
        }
    }
}