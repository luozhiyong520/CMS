using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common;
using System.Reflection;

namespace Common
{
    public static class ExtensionMethods
    {
        public static List<T> ToList<T>(this DataSet dataSet)
        {
            var list = new List<T>();
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                list = EntityHelper.GetEntityListByDT<T>(dataSet.Tables[0], null);
            }

            return list;
        }

        public static List<T> ToList<T>(this DataTable dataTable)
        {
            var list = new List<T>();
            if (dataTable.Rows.Count > 0)
            {
                list = EntityHelper.GetEntityListByDT<T>(dataTable, null);
            }

            return list;
        }

        public static SqlParameter[] ToParas(this SqlWhereList sqlWhereList)
        {
            return sqlWhereList.Select(tf => new SqlParameter { ParameterName = "@" + tf.ParaName, Value = tf.Value }).ToArray();
        }


        public static List<T> ToList<T>(this IDataReader reader)
        {
            var list = new List<T>();
            if (reader.FieldCount>0)
            {
                list = EntityHelper.IGetEntityListByDataReader<T>(reader, null);
            }

            return list;
        }

        /// <summary>
        /// 转换为一个DataTable
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<TResult>(this IEnumerable<TResult> value) where TResult : class
        {
            //创建属性的集合
            List<PropertyInfo> pList = new List<PropertyInfo>();
            //获得反射的入口
            Type type = typeof(TResult);
            DataTable dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列
            Array.ForEach<PropertyInfo>(type.GetProperties(), p =>
            {
                pList.Add(p);
                dt.Columns.Add(p.Name, p.PropertyType);
            });
            foreach (var item in value)
            {
                //创建一个DataRow实例
                DataRow row = dt.NewRow();
                //给row 赋值
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                //加入到DataTable
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
