using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMVC
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class SessionModeAttribute : Attribute
    {
        public SessionStatus SessionModeStatus { get; set; }
    }

    public enum SessionStatus
    {
        //不支持
        NoSupport,

        //只读
        ReadOnly,

        //支持
        Support
    }
}
