using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.Compilation;
using System.IO;

namespace SMVC
{
    public static class PageExecutor
    {
        public static void SetPageModel(IHttpHandler handler, object model)
        {
            if (handler == null)
                return;

            if (model != null)
            {
                BasePage page = handler as BasePage;
                if (page != null)
                    page.SetModel(model);
            }
        }

        public static string Render(HttpContext context, string pageVirtualPath,object model)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (string.IsNullOrEmpty(pageVirtualPath))
                pageVirtualPath = context.Request.FilePath;

            Page page = BuildManager.CreateInstanceFromVirtualPath(pageVirtualPath, typeof(object)) as Page;
            if (page == null)
                throw new InvalidOperationException(string.Format("你访问的地址:{0},不存在", pageVirtualPath));
            SetPageModel(page, model);
            StringWriter output = new StringWriter();
            context.Server.Execute(page, output, false);
            return output.ToString();
        }

        public static void ResponseWrite(string pageVirtualPath, object model, bool flush)
        {
            HttpContext context = HttpContext.Current;
            if (context == null)
                return;
            if (string.IsNullOrEmpty(context.Response.ContentType))
                context.Response.ContentType = "text/html";
            string html = Render(context, pageVirtualPath, model);
            context.Response.Write(html);
            if (flush)
                context.Response.Flush();
        }
    }
}
