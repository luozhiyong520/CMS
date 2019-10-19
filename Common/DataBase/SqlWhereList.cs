using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Common
{

    public class SqlWhereList : List<TableField>
    {
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="paraName"></param>
        /// <param name="tableColumn"></param>
        /// <param name="value"></param>
        public void Add(string paraName, string tableColumn, object value)
        {
            TableField tf = new TableField();
            tf.ParaName = paraName;
            tf.TableColumn = tableColumn;
            tf.Value = value;

            this.Add(tf);
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="paraName"></param>
        /// <param name="value"></param>
        public void Add(string paraName, object value)
        {
            this.Add(paraName, "", value);
        }
    }

    /// <summary>
    /// 表字段
    /// </summary>
    public class TableField
    {
        public string ParaName
        {
            get;
            set;
        }

        public string TableColumn
        {
            get;
            set;
        }

        public object Value
        {
            get;
            set;
        }
    }
}
