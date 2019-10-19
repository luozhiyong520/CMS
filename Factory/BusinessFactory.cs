using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Factory
{
    public sealed class BusinessFactory
    {
        private BusinessFactory() { }

        /// <summary>
        /// bll对象缓存池
        /// </summary>
        private static Hashtable bllObjectCachePool = new Hashtable();

        /// <summary>
        /// 创建Bll对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="proxy">是否创建代理</param>
        /// <param name="extend">扩展接口</param>
        /// <returns></returns>
        public static T CreateBll<T>(bool proxy, IExtendProxyAble extend)
        {
            Type type = typeof(T);
            string className = type.FullName;
            string key = string.Format("{0}_{1}_{2}", className, proxy, extend.CacheAbsoluteExpiration);
            object ret = null;


            //先从缓存里看看有没有此对象
            lock (bllObjectCachePool)
            {
                if (bllObjectCachePool.ContainsKey(key))
                {
                    ret = bllObjectCachePool[key];
                }
                else
                {
                    //从缓存里找不到，创建一个
                    ret = Activator.CreateInstance(type);
                    if (proxy)
                    {
                        ret = ObjectProxy.NewInstance<T>(ret, extend);
                    }
                    bllObjectCachePool.Add(key, ret);
                }
            }
            return (T)ret;
        }

        /// <summary>
        /// 创建bll对象
        /// </summary>
        /// <typeparam name="T">要创建的类型</typeparam>
        /// <param name="proxy">是否创建代理对象</param>
        /// <returns></returns>
        public static T CreateBll<T>(bool proxy)
        {
            return CreateBll<T>(proxy, new NullExtendProxyImpl());
        }

        /// <summary>
        /// 创建非代理的BLL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateBll<T>()
        {
            return CreateBll<T>(false);
        }

        /// <summary>
        /// 清除Bll对象池
        /// </summary>
        /// <returns></returns>
        public static int ClearBllPool()
        {
            int count = bllObjectCachePool.Count;
            bllObjectCachePool.Clear();
            return count;
        }
    }
}
