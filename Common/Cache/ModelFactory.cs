using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public sealed class ModelFactory
    {
        private ModelFactory() { }

        public readonly static ModelFactory Instance = new ModelFactory();

        private static Hashtable modelPool = new Hashtable();

        public  T CreateModel<T>()
        {
            Type type = typeof(T);
            string key = type.FullName;
            object ret = null;
            lock (modelPool)
            {
                if (modelPool.Contains(key))
                {
                    ret = modelPool[key];
                }
                else
                {
                    ret = Activator.CreateInstance(type);
                    modelPool.Add(key, ret);
                }
            }
            return (T)ret;
        }

        public int ClearModelPool()
        {
            int count = modelPool.Count;
            modelPool.Clear();
            return count;
        }
    }
}
