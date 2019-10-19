
namespace CMS.Template.Parser.AST
{
    public class TagIf : Tag
    {
        Tag m_falseBranch;
        Expression m_test;

        public TagIf(int line, int col, Expression test)
            : base(line, col, "if")
        {
            this.m_test = test;
        }

        public Tag FalseBranch
        {
            get { return this.m_falseBranch; }
            set { this.m_falseBranch = value; }
        }

        public Expression Test
        {
            get { return m_test; }
        }
    }
}
