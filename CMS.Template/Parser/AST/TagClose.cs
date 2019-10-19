
namespace CMS.Template.Parser.AST
{
    public class TagClose : Element
    {
        string m_name;

        public TagClose(int line, int col, string name)
            : base(line, col)
        {
            this.m_name = name;
        }

        public string Name
        {
            get { return this.m_name; }
        }
    }
}
