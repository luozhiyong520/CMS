
namespace CMS.Template.Parser.AST
{
    public class Name : Expression
    {
        string m_id;

        public Name(int line, int col, string id)
            : base(line, col)
        {
            this.m_id = id;
        }

        public string Id
        {
            get { return this.m_id; }
        }
    }
}
