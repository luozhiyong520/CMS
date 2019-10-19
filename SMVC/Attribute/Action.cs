using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMVC
{
    /// <summary>
    /// 一个空的属性，主要是用来做方法的Action标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ActionAttribute : Attribute
    {

    }
}
