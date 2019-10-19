
namespace CMS.Template.Parser.AST
{
    public class StringLiteral : Expression
    {
        string m_content;

        public StringLiteral(int line, int col, string content)
            : base(line, col)
        {
            this.m_content = content;
        }

        public string Content
        {
            get { return this.m_content; }
        }
    }
}
