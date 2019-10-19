using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Model.Oracle
{
    /// <summary>
    /// 炒股大赛数据模型
    /// </summary>
    public class StockContestData
    {
        /// <summary>
        /// Id, 用于sso推送修改删除操作, 此处没有用处
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户Id,用户帐号, 多个逗号分隔
        /// </summary>
        public string UserId { set; get; }
        ///// <summary>
        ///// 用户名
        ///// </summary>
        //public string UserName { set; get; }
        /// <summary>
        /// 股票代码
        /// </summary>
        public string StockCode { set; get; }
        /// <summary>
        /// 股票名称
        /// </summary>
        public string StockName { set; get; }
        /// <summary>
        /// 数量
        /// </summary>
        public string Quantity { set; get; }
        /// <summary>
        /// 成交价
        /// </summary>
        public string Price { set; get; }
        /// <summary>
        /// 操作来源高手名
        /// </summary>
        public string OperateUserName { set; get; }
        /// <summary>
        /// 操作高手头像
        /// </summary>
        public string OperateUserImg { set; get; }
        /// <summary>
        ///  操作类型: 卖出, 买入
        /// </summary>
        public string OperateType { set; get; }
        /// <summary>
        ///  操作时间
        /// </summary>
        public string OperateTime { set; get; }
        /// <summary>
        ///  3：模拟盘  4：实盘 ( 原 1：公告 2：解盘 )不变
        /// </summary>
        public string StaticTag { set; get; }
    }
}
