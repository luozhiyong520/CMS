using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMVC
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class ReadOnlyAttribute : Attribute
    {
    }
}
