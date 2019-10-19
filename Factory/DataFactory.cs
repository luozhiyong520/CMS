using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

namespace Factory
{
    public sealed class DataFactory
    {
        private DataFactory() { }

        /// <summary>
        /// dal对象缓存池
        /// </summary>
        private static Hashtable dalObjectCachePool = new Hashtable();

        /// <summary>
        /// 创建Dal对象
        /// </summary>
        /// <typeparam name="T">接口类，而非具体类</typeparam>
        /// <returns></returns>
        public static T CreateDAL<T>()
        {
            Type type = typeof(T);
            string className = type.FullName;
            object ret = null;

            //先从缓存里看看有没有此对象
            lock (dalObjectCachePool)
            {
                if (dalObjectCachePool.ContainsKey(className))
                {
                    ret = dalObjectCachePool[className];
                }
                else
                {
                    //从缓存里找不到，创建一个
                    ret = Activator.CreateInstance(type);
                    dalObjectCachePool.Add(className, ret);
                }
            }
            
            return (T)ret;
        }


        /// <summary>
        /// 清除Dal对象池
        /// </summary>
        /// <returns></returns>
        public static int ClearDALPool()
        {
            int count = dalObjectCachePool.Count;
            dalObjectCachePool.Clear();
            return count;
        }
    }
}
