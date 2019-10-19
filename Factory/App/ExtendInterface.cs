using System;
using System.Collections.Generic;
using System.Text;

namespace Factory
{
    /// <summary>
    /// 缓存扩展接口
    /// </summary>
    public interface IExtendProxyAble
    {
        /// <summary>
        /// 缓存绝对过期时间
        /// </summary>
        int CacheAbsoluteExpiration { get;set;}
    }

    /// <summary>
    /// 空实现
    /// </summary>
    public class NullExtendProxyImpl : IExtendProxyAble
    {
        public int CacheAbsoluteExpiration
        {
            get
            {
                return 0;
            }
            set
            {
                ;
            }
        }
    };

    /// <summary>
    /// 缓存扩展接口普通实现
    /// </summary>
    public class CommonExtendProxyImpl : IExtendProxyAble
    {
        private int expiration;

        public CommonExtendProxyImpl(int expiration)
        {
            this.expiration = expiration;
        }

        public int CacheAbsoluteExpiration
        {
            get
            {
                return this.expiration;
            }
            set
            {
                ;
            }
        }

    };
}
