using System.Collections.Generic;

namespace CMS.Template.Parser.AST
{
    public class Tag : Element
    {
        string m_name;
        List<TagAttribute> m_attribs;
        List<Element> m_innerElements;
        TagClose m_closeTag;
        bool m_isClosed;	// set to true if tag ends with />

        public Tag(int line, int col, string name)
            : base(line, col)
        {
            this.m_name = name;
            this.m_attribs = new List<TagAttribute>();
            this.m_innerElements = new List<Element>();
        }

        public List<TagAttribute> Attributes
        {
            get { return this.m_attribs; }
        }

        public Expression AttributeValue(string name)
        {
            foreach (TagAttribute attrib in m_attribs)
                if (string.Compare(attrib.Name, name, true) == 0)
                    return attrib.Expression;

            return null;
        }

        public List<Element> InnerElements
        {
            get { return this.m_innerElements; }
        }

        public string Name
        {
            get { return this.m_name; }
        }

        public TagClose CloseTag
        {
            get { return this.m_closeTag; }
            set { this.m_closeTag = value; }
        }

        public bool IsClosed
        {
            get { return this.m_isClosed; }
            set { this.m_isClosed = value; }
        }
    }
}
