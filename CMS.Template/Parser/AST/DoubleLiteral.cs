
namespace CMS.Template.Parser.AST
{
    public class DoubleLiteral : Expression
    {
        double m_value;

        public DoubleLiteral(int line, int col, double value)
            : base(line, col)
        {
            this.m_value = value;
        }

        public double Value
        {
            get { return this.m_value; }
        }
    }
}
