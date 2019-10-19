using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Model.Oracle
{
    /// <summary>
    /// 云秒杀实体
    /// </summary>
    public class DsYmsUserModel
    {
        /// <summary>
        /// 用户名
        /// </summary>

        public string UserName { get; set; }

        /// <summary>
        /// 显示的名称
        /// </summary>

        public string ShowName { get; set; }

        /// <summary>
        /// 上个月的战绩
        /// </summary>

        public decimal LastResults { get; set; }

        /// <summary>
        /// 排名
        /// </summary>

        public int Sort { get; set; }

        /// <summary>
        /// 套餐类型（A,B）
        /// </summary>

        public string Package { get; set; }

        /// <summary>
        /// 总资产
        /// </summary>

        public decimal Capital { get; set; }

        /// <summary>
        /// 仓位
        /// </summary>

        public decimal PosRatio { get; set; }
    }
}
