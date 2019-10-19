using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.CodeDom;
using System.ComponentModel;

namespace SMVC
{
    [FileLevelControlBuilder(typeof(ViewTypeControlBuilder))]
    [NonVisualControl]
    public class ViewType : Control
    {
        private string typeName;

        [DefaultValue("")]
        public string TypeName
        {
            get
            {
                return typeName ?? string.Empty;
            }
            set
            {
                typeName = value;
            }
        }
    }

    internal sealed class ViewTypeControlBuilder : ControlBuilder
    {
        string typeName;

        public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string id, System.Collections.IDictionary attribs)
        {
            base.Init(parser, parentBuilder, type, tagName, id, attribs);
            typeName = attribs["typename"] as string;
        }

        public override void ProcessGeneratedCode(
            CodeCompileUnit codeCompileUnit, 
            CodeTypeDeclaration baseType, 
            CodeTypeDeclaration derivedType, 
            CodeMemberMethod buildMethod, 
            CodeMemberMethod dataBindingMethod)
        {
            // Override the view's base type with the explicit base type
            if(!string.IsNullOrEmpty(typeName))
                derivedType.BaseTypes[0] = new CodeTypeReference(typeName);
        }
    }
}
