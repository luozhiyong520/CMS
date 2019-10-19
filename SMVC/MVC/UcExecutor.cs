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
    public static class UcExecutor
    {
        public static string Render(string virtualUcPagePath, object model)
        {
            if (string.IsNullOrEmpty(virtualUcPagePath))
                throw new ArgumentNullException("virtualUcPagePath");
            Page page = new Page();
            Control ctrl = page.LoadControl(virtualUcPagePath);
            if (ctrl == null)
                throw new InvalidOperationException(string.Format("指定的用户控件:{0}没有找到", virtualUcPagePath));
            if (model != null)
            {
                BaseUserControl userControl = ctrl as BaseUserControl;
                if (userControl != null)
                    userControl.SetModel(model);
            }
            // 将用户控件放在Page容器中。
            page.Controls.Add(ctrl);
            StringWriter output = new StringWriter();
            HtmlTextWriter html = new HtmlTextWriter(output);
            page.RenderControl(html);

            // 这个方法也可以用
            // RenderControl不支持服务器控件，原因在于它利用了页面的一种独特编译方式。
            // Execute可以支持服务器控件，因为它会执行一次完整的页面生命周期。
            //上面这段代码就算使用Execute，也只能支持部分简单的服务器控件，因为一些复杂的服务器控件需要在HtmlForm中才能运行。 因此，如果需要支持所有的服务器控件，那么还必须创建HtmlForm对象，并调整包含关系，还有就是还需要去掉产生的多余HTML代码。
            // HttpContext.Current.Server.Execute(page, output, false);
            

            return output.ToString();
            
            
        }

        public static void ResponseWrite(string virtualUcPathPage, object model, bool responseFlush)
        {
            HttpContext context = HttpContext.Current;
            if (context == null)
                return;
            if (string.IsNullOrEmpty(context.Response.ContentType))
                context.Response.ContentType = "text/html";

            string html = Render(virtualUcPathPage, model);
            HttpContext.Current.Response.Write(html);
            if (responseFlush)
                HttpContext.Current.Response.Flush();
        }
    }
}
