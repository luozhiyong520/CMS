
namespace CMS.Template.Parser.AST
{
    public class ArrayAccess : Expression
    {
        Expression m_exp;
        Expression m_index;

        public ArrayAccess(int line, int col, Expression exp, Expression index)
            : base(line, col)
        {
            this.m_exp = exp;
            this.m_index = index;
        }

        public Expression Exp
        {
            get { return this.m_exp; }
        }

        public Expression Index
        {
            get { return this.m_index; }
        }
    }
}
