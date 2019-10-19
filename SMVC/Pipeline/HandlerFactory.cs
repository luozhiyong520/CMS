using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace SMVC
{
    internal class AjaxHandlerFactory : IHttpHandlerFactory
    {
        public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            ControllerActionPair pair = UrlParser.GetControllerActionPair(url);
            if (pair == null)
                ExceptionHelper.Throw404Exception(context);
            InvokeInfo invoke = ReflectionHelper.GetInvokeInfo(pair);
            if (invoke == null)
                ExceptionHelper.Throw404Exception(context);

            return ActionHandler.CreateHandler(invoke);
        }

        public void ReleaseHandler(IHttpHandler handler)
        {
            
        }
    }

    internal class AspNetPageHandlerFactory : PageHandlerFactory { }

    internal class MvcPageHandlerFactory : IHttpHandlerFactory
    {
        public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            string path = context.Request.Path;
            InvokeInfo invoke = ReflectionHelper.GetInvokeInfo(path);

            //如果没有找到url映射的aspx页面，就用原始的aspx页面的handler处理页面
            if (invoke == null && path.EndsWith(".aspx", StringComparison.OrdinalIgnoreCase))
                return new AspNetPageHandlerFactory().GetHandler(context, requestType, url, pathTranslated);
            else if (invoke == null)
                ExceptionHelper.Throw404Exception(context);
            return ActionHandler.CreateHandler(invoke);
        }

        public void ReleaseHandler(IHttpHandler handler)
        {
            
        }        
    }
}
