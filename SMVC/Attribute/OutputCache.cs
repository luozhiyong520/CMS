using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;

namespace SMVC
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class OutputCacheAttribute : Attribute
    {
        OutputCacheParameters cacheParms = new OutputCacheParameters();

        // 摘要:
        //     获取或设置 System.Web.Configuration.OutputCacheProfile 名称，该名称与输出缓存项的设置关联。
        //
        // 返回结果:
        //     与输出缓存项的设置关联的 System.Web.Configuration.OutputCacheProfile 名称。
        public string CacheProfile
        {
            get
            {
                return cacheParms.CacheProfile;
            }
            set
            {
                cacheParms.CacheProfile = value;
            }
        }

        //
        // 摘要:
        //     获取或设置缓存项要保留在输出缓存中的时间。
        //
        // 返回结果:
        //     缓存项保留在输出缓存中的时间（以秒为单位）。默认值为 0，指示没有时间限制。        
        public int Duration
        {
            get
            {
                return cacheParms.Duration;
            }
            set
            {
                cacheParms.Duration = value;
            }
        }

        //
        // 摘要:
        //     获取或设置一个值，该值指示是否对当前内容启用了输出缓存。
        //
        // 返回结果:
        //     如果对当前内容启用了输出缓存，则为 true；否则为 false。默认值为 true。
        public bool Enabled
        {
            get
            {
                return cacheParms.Enabled;
            }
            set
            {
                cacheParms.Enabled = value;
            }
        }

        //
        // 摘要:
        //     获取或设置一个值，该值确定缓存项的位置。
        //
        // 返回结果:
        //     System.Web.UI.OutputCacheLocation 值之一。
        public OutputCacheLocation Location
        {
            get
            {
                return cacheParms.Location;
            }
            set
            {
                cacheParms.Location = value;
            }
        }

        //
        // 摘要:
        //     获取或设置一个值，该值确定是否设置了 HTTP Cache-Control: no-store 指令。
        //
        // 返回结果:
        //     如果在 System.Web.HttpResponse 上设置了 Cache-Control: no-store 指令，则为 true；否则为 false。默认值为
        //     false。
        public bool NoStore
        {
            get
            {
                return cacheParms.NoStore;
            }
            set
            {
                cacheParms.NoStore = value;
            }
        }

        //
        // 摘要:
        //     获取或设置缓存项依赖的一组数据库和表名称对。
        //
        // 返回结果:
        //     一个字符串，标识缓存项依赖的一组数据库和表名称对。如果更新或更改了表的数据，则缓存项过期。
        public string SqlDependency
        {
            get
            {
                return cacheParms.SqlDependency;
            }
            set
            {
                cacheParms.SqlDependency = value;
            }
        }

        //
        // 摘要:
        //     获取或设置用于改变缓存项的一组逗号分隔的字符集（内容编码）。
        //
        // 返回结果:
        //     改变内容时所依据的字符集的列表。
        public string VaryByContentEncoding
        {
            get
            {
                return cacheParms.VaryByContentEncoding;
            }
            set
            {
                cacheParms.VaryByContentEncoding = value;
            }
        }

        //
        // 摘要:
        //     获取或设置一组分号分隔的控件标识符，这些标识符包含在当前页或用户控件内，用于改变当前缓存项。
        //
        // 返回结果:
        //     分号分隔的字符串列表，用于改变项的输出缓存。System.Web.UI.OutputCacheParameters.VaryByControl 属性设置为完全限定的控件标识符，其中标识符是控件
        //     ID 的串联，从顶级父级控件开始并以美元符号 ($) 分隔。
        public string VaryByControl
        {
            get
            {
                return cacheParms.VaryByControl;
            }
            set
            {
                cacheParms.VaryByControl = value;
            }
        }

        //
        // 摘要:
        //     获取输出缓存用来改变缓存项的自定义字符串列表。
        //
        // 返回结果:
        //     自定义字符串列表。
        public string VaryByCustom
        {
            get
            {
                return cacheParms.VaryByCustom;
            }
            set
            {
                cacheParms.VaryByCustom = value;
            }
        }

        //
        // 摘要:
        //     获取或设置用于改变缓存项的一组逗号分隔的标头名称。标头名称标识与请求关联的 HTTP 标头。
        //
        // 返回结果:
        //     改变内容所依据的标头列表。
        public string VaryByHeader
        {
            get
            {
                return cacheParms.VaryByHeader;
            }
            set
            {
                cacheParms.VaryByHeader = value;
            }
        }

        //
        // 摘要:
        //     获取查询字符串或窗体 POST 参数的逗号分隔列表，该列表由输出缓存用来改变缓存项。
        //
        // 返回结果:
        //     查询字符串或窗体 POST 参数的列表。
        public string VaryByParam
        {
            get
            {
                return cacheParms.VaryByParam;
            }
            set
            {
                cacheParms.VaryByParam = value;
            }
        }

        internal void SetResponseCache(HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            OutputCachePage page = new OutputCachePage(cacheParms);
            page.ProcessRequest(context);
        }

        private sealed class OutputCachePage : Page
        {
            OutputCacheParameters cacheParms;
            public OutputCachePage(OutputCacheParameters cacheParms)
            {
                this.ID = Guid.NewGuid().ToString();
                this.cacheParms = cacheParms;
            }

            protected override void FrameworkInitialize()
            {
                base.FrameworkInitialize();
                InitOutputCache(cacheParms);
            }
        }
    }
}
