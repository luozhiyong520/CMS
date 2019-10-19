using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Model.Oracle
{
    public class TB_PUSH_INFO
    {
        /// <summary>
        /// 自增量
        /// </summary>
        public int FID { get; set; }
        /// <summary>
        /// 新闻Id
        /// </summary>
        public int INFOID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public string INFOABSTRACT { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string AUTHOR { get; set; }
        /// <summary>
        /// 新闻时间
        /// </summary>
        public DateTime CREATEDTIME { get; set; }
        /// <summary>
        /// 新闻类别
        /// </summary>
        public string INFOTYPE { get; set; }
        /// <summary>
        /// 新闻内容
        /// </summary>
        public string INFOCONTENT { get; set; }
        /// <summary>
        /// 写入时间
        /// </summary>
        public DateTime OPERATEDATE { get; set; }
        /// <summary>
        /// 计划接收数
        /// </summary>
        public int PLANCOUNT { get; set; }
        /// <summary>
        /// 实际接收数
        /// </summary>
        public int REALCOUNT { get; set; }
        /// <summary>
        /// 点击数
        /// </summary>
        public int CLICKCOUNT { get; set; }
        /// <summary>
        /// 编辑人员
        /// </summary>
        public string EDITOR { get; set; }
        /// <summary>
        /// 退送平台
        /// </summary>
        public string PLATFORM { get; set; }

    }
}
