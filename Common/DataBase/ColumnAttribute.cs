using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ColumnAttribute : Attribute
    {
        private string dbType;

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DbType
        {
            get
            {
                return this.dbType;
            }
            set
            {
                this.dbType = value;
            }
        }

        private bool isPrimaryKey;
        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey
        {
            get
            {
                return this.isPrimaryKey;
            }
            set
            {
                this.isPrimaryKey = value;
            }
        }

        private bool canBeNull = true;
        /// <summary>
        /// 是否为Null
        /// </summary>
        public bool CanBeNull
        {
            get
            {
                return this.canBeNull;
            }
            set
            {
                this.canBeNull = value;
            }
        }
    }
}
