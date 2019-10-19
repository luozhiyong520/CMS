using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;

namespace CMS.Model
{
    public partial class Topic
    {
        /// <summary>
        /// 专题类型名称
        /// </summary>
        public string TopicTypeName { get; set; }
        public string TopicTypeGenerate { get; set; }
        public int TemplateId { get; set; }

    }
}
