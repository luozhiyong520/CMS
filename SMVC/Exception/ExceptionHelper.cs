using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SMVC
{
    internal static class ExceptionHelper 
    {
        public static void Throw403Exception(HttpContext context)
        {   
            if(context == null)
                throw new HttpException("对不起，你没有权限访问该页面");
            throw new HttpException(string.Format("对不起，你没有权限访问该页面:{0}", context.Request.RawUrl));
        }

        public static void Throw404Exception(HttpContext context)
        {
            if (context == null)
                throw new HttpException("你访问的页面不存在");
            throw new HttpException(string.Format("你访问的页面不存在：{0}", context.Request.RawUrl));
        }

        public static void Throw405Exception(HttpContext context)
        {
            if (context == null)
                throw new HttpException("你的参数有误");
            throw new HttpException(string.Format("页面:{0}，参数有误", context.Request.RawUrl));
        }
    }
}
