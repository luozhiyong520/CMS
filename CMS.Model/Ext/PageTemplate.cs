
namespace CMS.Model
{
    public partial class PageTemplate
    {
        public TemplateBModel ToBModel()
        {
            return new TemplateBModel()
                {
                    TemplateId = this.TemplateId,
                    FileName = this.TemplateFileName,
                    Path = this.HtmlPath,
                    CharSet = "utf-8",
                    TempleteType = this.TempleteType
                };
        }
        public PageTemplate(int templateId, string templateName, string templateFileName)
        {
            this.TemplateId = templateId;
            this.TemplateName = templateName;
            this.TemplateFileName = templateFileName;
        }

        /// <summary>
        /// 频道Id
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// 所属关系，1-涉及；2-模板
        /// </summary>
        public int RelationType { get; set; }
    }
}
