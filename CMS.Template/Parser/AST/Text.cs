
namespace CMS.Template.Parser.AST
{
    public class Text : Element
    {
        string m_data;

        public Text(int line, int col, string data)
            : base(line, col)
        {
            this.m_data = data;
        }

        public string Data
        {
            get { return this.m_data; }
        }
    }
}
