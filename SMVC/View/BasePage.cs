using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.CodeDom;
using System.Web;

namespace SMVC
{
    [FileLevelControlBuilder(typeof(ViewPageControlBuilder))]
    public class BasePage : Page
    {
        private string requestEncodeUrlRawUrl;

        public string RequestEncodeUrlRawUrl
        {
            get
            {
                if (string.IsNullOrEmpty(requestEncodeUrlRawUrl))
                {
                    requestEncodeUrlRawUrl = Server.UrlEncode(HttpContextHelper.RequestRawUrl);
                }
                return requestEncodeUrlRawUrl;
            }
        }

        public virtual void SetModel(object model) { }
    }


    internal sealed class ViewPageControlBuilder : FileLevelPageControlBuilder
    {
        public string PageBaseType { get; set; }

        public override void ProcessGeneratedCode(
            CodeCompileUnit codeCompileUnit,
            CodeTypeDeclaration baseType,
            CodeTypeDeclaration derivedType,
            CodeMemberMethod buildMethod,
            CodeMemberMethod dataBindingMethod)
        {
            //如果分析器找到一个有效的类型，就使用它
            if (!string.IsNullOrEmpty(PageBaseType))
                derivedType.BaseTypes[0] = new CodeTypeReference(PageBaseType);
        }
    }
}
