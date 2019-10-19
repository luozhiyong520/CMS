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
        /// dal���󻺴��
        /// </summary>
        private static Hashtable dalObjectCachePool = new Hashtable();

        /// <summary>
        /// ����Dal����
        /// </summary>
        /// <typeparam name="T">�ӿ��࣬���Ǿ�����</typeparam>
        /// <returns></returns>
        public static T CreateDAL<T>()
        {
            Type type = typeof(T);
            string className = type.FullName;
            object ret = null;

            //�ȴӻ����￴����û�д˶���
            lock (dalObjectCachePool)
            {
                if (dalObjectCachePool.ContainsKey(className))
                {
                    ret = dalObjectCachePool[className];
                }
                else
                {
                    //�ӻ������Ҳ���������һ��
                    ret = Activator.CreateInstance(type);
                    dalObjectCachePool.Add(className, ret);
                }
            }
            
            return (T)ret;
        }


        /// <summary>
        /// ���Dal�����
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
