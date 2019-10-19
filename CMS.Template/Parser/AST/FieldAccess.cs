
namespace CMS.Template.Parser.AST
{
    public class FieldAccess : Expression
    {
        private Expression m_exp;
        private string m_field;

        public FieldAccess(int line, int col, Expression exp, string field)
            : base(line, col)
        {
            this.m_exp = exp;
            this.m_field = field;
        }

        public Expression Exp
        {
            get { return this.m_exp; }
        }

        public string Field
        {
            get { return this.m_field; }
        }
    }
}
