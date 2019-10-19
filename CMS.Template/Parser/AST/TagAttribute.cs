
namespace CMS.Template.Parser.AST
{
   public class TagAttribute
    {
        string m_name;
        Expression m_expression;

        public TagAttribute(string name, Expression expression)
        {
            this.m_name = name;
            this.m_expression = expression;
        }

        public Expression Expression
        {
            get { return this.m_expression; }
        }

        public string Name
        {
            get { return this.m_name; }
        }
    }
}
