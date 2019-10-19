using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SMVC;
namespace CMS.Model
{
    public partial class Analyst
    {
        /// <summary>
        /// 成功率
        /// </summary>
        public decimal SuccessRate { get; set; }

        /// <summary>
        /// 数据连接
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// 关注时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public int CustomerId { get; set; }
    }
}
