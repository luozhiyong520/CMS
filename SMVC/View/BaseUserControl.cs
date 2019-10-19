using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.CodeDom;

namespace SMVC
{
    [FileLevelControlBuilder(typeof(ViewUserControlControlBuilder))]
    public class BaseUserControl : UserControl
    {
        public virtual void SetModel(object model) { }
    }

    internal sealed class ViewUserControlControlBuilder : FileLevelUserControlBuilder
    {
        public string UserControlBaseType
        {
            get;
            set;
        }

        public override void ProcessGeneratedCode(CodeCompileUnit codeCompileUnit, CodeTypeDeclaration baseType, CodeTypeDeclaration derivedType, CodeMemberMethod buildMethod, CodeMemberMethod dataBindingMethod)
        {
            if (!string.IsNullOrEmpty(UserControlBaseType))
                derivedType.BaseTypes[0] = new CodeTypeReference(UserControlBaseType);
        }
    }
}
