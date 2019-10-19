
namespace CMS.Template.Parser.AST
{
    public class IntLiteral : Expression
    {
        int m_value;

        public IntLiteral(int line, int col, int value)
            : base(line, col)
        {
            this.m_value = value;
        }

        public int Value
        {
            get { return this.m_value; }
        }
    }
}
