using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace SMVC
{
    public interface IActionResult
    {
        void Output(HttpContext context);
    }

    public class UcResult : IActionResult
    {
        public string VirtualUcPagePath { get; private set; }
        public object Model { get; private set; }

        public UcResult(string virtualUcPagePath, object model)
        {
            this.VirtualUcPagePath = virtualUcPagePath;
            this.Model = model;
        }

        #region IActionResult 成员

        void IActionResult.Output(HttpContext context)
        {
            string html = UcExecutor.Render(VirtualUcPagePath, Model);
            context.Response.ContentType = "text/html";
            context.Response.Write(html);
        }
        
        #endregion
    }

    public class PageResult : IActionResult
    {
        public string VirtualPagePath { get; private set; }
        public object Model { get; private set; }

        public PageResult(string virtualPagePath, object model)
        {
            this.VirtualPagePath = virtualPagePath;
            this.Model = model;
        }

        void IActionResult.Output(HttpContext context)
        {
            string html = PageExecutor.Render(context, VirtualPagePath, Model);
            context.Response.ContentType = "text/html";
            context.Response.Write(html);
        }

    }

    public class JsonResult : IActionResult
    {
        public object Model { get; private set; }

        public JsonResult(object model)
        {
            this.Model = model;
        }

        #region IActionResult 成员

        void IActionResult.Output(HttpContext context)
        {
            if (Model == null)
                throw new ArgumentNullException("Model");
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string html = jss.Serialize(Model);
            context.Response.ContentType = "text/json";
            context.Response.Write(html);
        }

        #endregion
    }

    public class RedirectResult : IActionResult
    {
        public string Url { get; private set; }

        public RedirectResult(string url)
        {
            this.Url = url;
        }

        #region IActionResult 成员

        void IActionResult.Output(HttpContext context)
        {
            if (string.IsNullOrEmpty(Url))
                throw new ArgumentNullException("Url");
            context.Response.Redirect(Url, true);
        }

        #endregion
    }
}
