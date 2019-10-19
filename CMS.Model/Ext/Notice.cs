using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SMVC;

namespace CMS.Model 
{
    public partial class Notice
    {
        /// <summary>
        /// 版本ID
        /// </summary>
        public int productId { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public string company { get; set; }
        /// <summary>
        /// 代理url
        /// </summary>
        public string agenturl { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 页面地址
        /// </summary>
        public string pageurl { get; set; }
        /// <summary>
        /// 课堂号
        /// </summary>
        public int roomid { get; set; }
    }
}
