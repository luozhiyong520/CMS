using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;
using DynamicProxy;

namespace Factory
{
    public class ObjectProxy : IInterceptor
    {
        private Object target = null;
        private IExtendProxyAble extend;

        public object Intercept(InvocationInfo info)
        {
            object[] attrs = info.TargetMethod.GetCustomAttributes(true);
            object ret = null;
            if(attrs == null || attrs.Length == 0)
            {
                //没有发现任何自定义特性，直接调用原方法然后返回结果
                return info.TargetMethod.Invoke(target, info.Arguments); 
            }
            foreach(object o in attrs)
            {
                //如果发现了缓存特性
                if(o.GetType() == typeof(CacheAttribute))
                {
                    ret = doCache(info, o, ref ret);
                }
            }
            return ret;
        }

        private object doCache(InvocationInfo info, object arrt, ref object ret)
        {
            CacheAttribute cacheAttr = (CacheAttribute)arrt;
            //此为客户端自定义的缓存过期时间
            //要注意这里，如果扩展接口中的缓存过期时间不为空，则采用扩展接口中的缓存过期时间
            int absoluteExpiration = extend.CacheAbsoluteExpiration > 0 ? extend.CacheAbsoluteExpiration : cacheAttr.AbsoluteExpiration;

            //当缓存的键前缀为空时忽略
            if (string.IsNullOrEmpty(cacheAttr.CacheKey))
            {
                return info.TargetMethod.Invoke(target, info.Arguments);
            }
            string cacheKey = getCacheKey(cacheAttr.CacheKey, info);
            //当指定缓存此方法的结果时，先从缓存里查找
            if ((ret = HttpContext.Current.Cache.Get(cacheKey)) == null)
            {
                //缓存中不存在，直接调用方法，并取得结果
                ret = info.TargetMethod.Invoke(target, info.Arguments);
                if (ret != null)
                {
                    //在这里添加缓存策略的回调
                    onBeforeInsertObjectToCache(HttpContext.Current.Cache, cacheKey, ret, cacheAttr);
                    //将方法的返回值插入到缓存中，可以自己增加更多的策略
                    if (absoluteExpiration > 0 )
                    {
                        //设置缓存绝对过期时间
                        HttpContext.Current.Cache.Insert(cacheKey, ret, null, DateTime.Now.AddMinutes(absoluteExpiration), System.Web.Caching.Cache.NoSlidingExpiration);
                    }
                    else
                    {
                        //缓存永久有效
                        HttpContext.Current.Cache.Insert(cacheKey, ret);
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 在插入对象到缓存的时候可以实现此方法
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="cacheKey"></param>
        /// <param name="ret"></param>
        /// <param name="cacheAttr"></param>
        private void onBeforeInsertObjectToCache(Cache cache, string cacheKey, object ret, CacheAttribute cacheAttr)
        {
            
        }

        /// <summary>
        /// 生成缓存的键
        /// </summary>
        /// <param name="p"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        private string getCacheKey(string key, InvocationInfo info)
        {
            StringBuilder buffer = new StringBuilder();
            buffer.AppendFormat("{0}_{1}_{2}", key, info.Target.GetType().FullName, info.TargetMethod.Name);
            foreach(object o in info.Arguments)
            {
                buffer.AppendFormat("_{0}", o);
            }

            return buffer.ToString();
        }

        private ObjectProxy(Object target, IExtendProxyAble extend)
        {
            this.target = target;
            this.extend = extend;
        }

        public static T NewInstance<T>(Object target, IExtendProxyAble extend)
        {
            return new ProxyFactory().CreateProxy<T>(new ObjectProxy(target, extend));
        }
    }
}
