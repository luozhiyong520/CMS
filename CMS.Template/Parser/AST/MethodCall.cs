
namespace CMS.Template.Parser.AST
{
    public class MethodCall : Expression
    {
        string m_name;
        Expression m_obj;
        Expression[] m_args;

        public MethodCall(int line, int col, Expression obj, string name, Expression[] args)
            : base(line, col)
        {
            this.m_name = name;
            this.m_args = args;
            this.m_obj = obj;
        }

        public Expression CallObject
        {
            get { return this.m_obj; }
        }

        public Expression[] Args
        {
            get { return this.m_args; }
        }

        public string Name
        {
            get { return this.m_name; }
        }
    }
}
