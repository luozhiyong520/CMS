using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SMVC
{
    /// <summary>
    /// 用于描述一个Action可以处理的页面Url
    /// 一个Action可以处理多个Url
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class PageUrlAttribute : Attribute
    {
        public string Url { get; set; }
    }
}
