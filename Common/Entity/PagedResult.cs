using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Common
{
    /// <summary>
    /// 数据分页实体
    /// </summary>
    public class PagedResult
    {
        private  DataTable result;
        private int total;

        public PagedResult(DataTable result, int total)
        {
            this.result = result;
            this.total = total;            
        }

        public PagedResult()
            : this(new DataTable(), 0)
        {
        }

        /// <summary>
        /// 结果集，即为一页数据
        /// </summary>
        public DataTable Result
        {
            get
            {
                return result;
            }
            set
            {
                result = value;
            }
        }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int Total
        {
            get
            {
                return total;
            }
            set
            {
                total = value;
            }
        }

        /// <summary>
        /// 标识结果集是否为空
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return result.Rows.Count == 0;
            }
        }
    }

    public class PagedResult<T>
    {
        public List<T> Result { get; set; }

        public int Total { get; set; }

        public PagedResult(List<T> list, int total)
        {
            this.Result = list;
            this.Total = total;
        }

        public PagedResult() { }

    }
}
