using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Common
{
    public class ColumnDbType
    {
        /// <summary>
        /// 数据类型
        /// </summary>
        public SqlDbType SqlDbType
        {
            get;
            set;
        }

        /// <summary>
        /// 长度
        /// </summary>
        public int Size
        {
            get;
            set;
        }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName
        {
            get;
            set;
        }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey
        {
            get;
            set;
        }

        /// <summary>
        /// 是否标识
        /// </summary>
        public bool IsIdentity
        {
            get;
            set;
        }

        /// <summary>
        /// 是否为Null
        /// </summary>
        public bool CanBeNull
        {
            get;
            set;
        }

        public object Value
        {
            get;
            set;
        }

        public void GetDbType(string dbType)
        {
            string type = dbType.ToUpper();

            if (dbType.IndexOf("IDENTITY") > 0)
            {
                this.IsIdentity = true;
            }
            else
            {
                this.IsIdentity = false;
            }
            if (dbType.IndexOf("NOT NULL") > 0)
            {
                this.CanBeNull = false;
            }
            else
            {
                this.CanBeNull = true;
            }
            type = type.Replace("IDENTITY", "").Replace("NOT NULL", "").Trim();

            if (type.StartsWith("INT"))
            {
                this.SqlDbType = SqlDbType.Int;
                this.Size = 4;
            }
            else if (type.StartsWith("BIGINT"))
            {
                this.SqlDbType = SqlDbType.BigInt;
                this.Size = 8;
            }
            else if (type.StartsWith("DECIMAL"))
            {
                this.SqlDbType = SqlDbType.Decimal;
                this.Size = 0;
            }
            else if (type.StartsWith("MONEY"))
            {
                this.SqlDbType = SqlDbType.Money;
                this.Size = 0;
            }
            else if (type.StartsWith("DATETIME"))
            {
                this.SqlDbType = SqlDbType.DateTime;
                this.Size = 0;
            }
            else if (type.StartsWith("BIT"))
            {
                this.SqlDbType = SqlDbType.Bit;
                this.Size = 1;
            }
            else if (type.StartsWith("VARCHAR"))
            {
                this.SqlDbType = SqlDbType.VarChar;
                if (type.Contains("MAX)"))
                    this.Size = 0;
                else
                    this.Size = Convert.ToInt32(type.Replace("VARCHAR", "").Replace("(", "").Replace(")", ""));
            }
            else if (type.StartsWith("NVARCHAR"))
            {
                this.SqlDbType = SqlDbType.NVarChar;
                if (type.Contains("MAX)"))
                    this.Size = 0;
                else
                    this.Size = Convert.ToInt32(type.Replace("NVARCHAR", "").Replace("(", "").Replace(")", ""));
            }
            else if (type.StartsWith("CHAR"))
            {
                this.SqlDbType = SqlDbType.Char;
                this.Size = Convert.ToInt32(type.Replace("CHAR", "").Replace("(", "").Replace(")", ""));
            }
            else if (type.StartsWith("NCHAR"))
            {
                this.SqlDbType = SqlDbType.NChar;
                this.Size = Convert.ToInt32(type.Replace("NCHAR", "").Replace("(", "").Replace(")", ""));
            }
            else if (type.StartsWith("NTEXT"))
            {
                this.SqlDbType = SqlDbType.NText;
                this.Size = 0;
            }
            else if (type.StartsWith("TEXT"))
            {
                this.SqlDbType = SqlDbType.Text;
            }
            else if (type.StartsWith("UNIQUEIDENTIFIER"))
            {
                this.SqlDbType = SqlDbType.UniqueIdentifier;

            }
        }
    }
}
