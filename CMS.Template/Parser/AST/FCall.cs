
namespace CMS.Template.Parser.AST
{
    public class FCall : Expression
    {
        string m_name;
        Expression[] M_args;

        public FCall(int line, int col, string name, Expression[] args)
            : base(line, col)
        {
            this.m_name = name;
            this.M_args = args;
        }

        public Expression[] Args
        {
            get { return this.M_args; }
        }

        public string Name
        {
            get { return this.m_name; }
        }
    }
}
