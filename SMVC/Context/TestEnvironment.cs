using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections;

namespace SMVC
{
    public class TestEnvironment
    {
        internal static bool IsTestEnvironment = HttpRuntime.AppDomainAppId == null;

        [ThreadStatic]
        static Hashtable contextHash;

        private static Hashtable ContextHash
        {
            get
            {
                if (!IsTestEnvironment)
                    throw new InvalidOperationException("只有在测试环境下，才能调用该方法");
                if (contextHash == null)
                    contextHash = new Hashtable();
                return contextHash;
            }
        }

        public static object GetValue(object key)
        {
            object val = ContextHash[key];
            if (val == null)
                throw new ArgumentNullException("你忘了给测试环境下面的参数：" + key + "赋值了");
            return val;
        }

        public static void SetValue(object key, object value)
        {
            ContextHash[key] = value;//hash.Add()不用这个方法，因为这方法需要判断key的唯一性
        }

        public static void ClearHash()
        {
            CookieHelper.ClearCookie();
            ContextHash.Clear();
        }
    }
}
