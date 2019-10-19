using System;
using System.Collections.Generic;
using System.Text;

namespace Factory
{
    /// <summary>
    /// 缓存的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CacheAttribute : Attribute  
    {
        /// <summary>
        /// 缓存的键
        /// </summary>
        private string cacheKey;
        private int absoluteExpiration;
        private bool isDetailPage;

        public CacheAttribute(string cacheKey)
        {
            this.cacheKey = cacheKey;
        }

        /// <summary>
        /// 缓存
        /// </summary>
        /// <param name="cacheKey">缓存的键</param>
        /// <param name="absoluteExpiration">缓存过期时间，以分钟为单位</param>
        public CacheAttribute(string cacheKey, int absoluteExpiration)
        {
            this.cacheKey = cacheKey;
            this.absoluteExpiration = absoluteExpiration;
            this.isDetailPage = false;
        }

        public CacheAttribute(string cacheKey, int absoluteExpiration, bool isDetailPage)
        {
            this.cacheKey = cacheKey;
            this.absoluteExpiration = absoluteExpiration;
            this.isDetailPage = isDetailPage;
        }


        #region 公开属性

        /// <summary>
        /// 设置、获取 是否详细页的缓存
        /// </summary>
        public bool IsDetailPage
        {
            get
            {
                return this.isDetailPage;
            }
            set
            {
                this.isDetailPage = value;
            }
        }

        /// <summary>
        /// 缓存键
        /// </summary>
        public string CacheKey
        {
            get
            {
                return this.cacheKey;
            }
            set
            {
                this.cacheKey = value;
            }
        }

        /// <summary>
        /// 设置、获取 缓存过期时间，以分钟为单位
        /// </summary>
        public int AbsoluteExpiration
        {
            get
            {
                return this.absoluteExpiration;
            }
            set
            {
                this.absoluteExpiration = value;
            }
        }
        #endregion
       
    }
}
