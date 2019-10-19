using System.Runtime.Serialization;

namespace CMS.Model
{
    [DataContract]
    public class TemplateBModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        [DataMember]
        public int TemplateId { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        [DataMember]
        public string FileName { get; set; }



        /// <summary>
        /// 目录路径
        /// </summary>
        [DataMember]
        public string Path { get; set; }


        /// <summary>
        /// 编码
        /// </summary>
        [DataMember]
        public string CharSet { get; set; }

        /// <summary>
        /// 模板类型1:列表页,2:终极页面
        /// </summary>
        [DataMember]
        public int? TempleteType { get; set; }

    }
}
