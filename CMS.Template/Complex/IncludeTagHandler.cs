using System.IO;

using CMS.Template.Parser.AST;
using CMS.Template.Except;
using System.Text;
using System.Configuration;


namespace CMS.Template.Complex
{
    /// <summary>
    /// 文件引用模板标签,要嵌入的文件必须首先调用引擎解释完毕
    /// <#include file="master/index.htm" source="file/web" charset="utf-8" />
    /// </summary>
    public class IncludeTagHandler : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements,
                                    ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = false;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp,exp2;
            string strFile, strResult;
            //获取文件路径参数
            exp = tag.AttributeValue("virtual");
            exp2 = tag.AttributeValue("file");
            if (exp == null && exp2 == null)
                throw new TemplateRuntimeException("include 标签必须有 virtual或file属性.", tag.Line, tag.Col);
            strFile = manager.EvalExpression(exp).ToString();
            if (string.IsNullOrEmpty(strFile))
                strFile = manager.EvalExpression(exp2).ToString();
            if(!strFile.Contains(":"))
            {
                strFile = ConfigurationManager.AppSettings["webpath"] + strFile;
            }
            //读取文件内容并插入到模板中

            //读取本地文件内容
            if (File.Exists(strFile))
                strResult = System.IO.File.ReadAllText(strFile,Encoding.Default);
            else
                strResult = string.Format("[{0}文件不存在.]", strFile);
            manager.WriteValue(strResult);
        }
    }
}
