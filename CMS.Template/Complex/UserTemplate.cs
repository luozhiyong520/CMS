using System;
using System.Collections.Generic;
using System.IO;

using CMS.Template.Lexer;
using CMS.Template.Parser;
using CMS.Template.Parser.AST;

namespace CMS.Template.Complex
{
    public class UserTemplate
    {
        string m_name;
        List<Element> m_elements;
        UserTemplate m_parent;

        Dictionary<string, UserTemplate> m_templates;

        public UserTemplate(string name, List<Element> elements)
        {
            this.m_name = name;
            this.m_elements = elements;
            this.m_parent = null;

            InitTemplates();
        }

        public UserTemplate(string name, List<Element> elements, UserTemplate parent)
        {
            this.m_name = name;
            this.m_elements = elements;
            this.m_parent = parent;

            InitTemplates();
        }

        /// <summary>
        /// load template from file
        /// </summary>
        /// <param name="name">name of template</param>
        /// <param name="filename">file from which to load template</param>
        /// <returns></returns>
        public static UserTemplate FromFile(string name, string filename)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                string data = reader.ReadToEnd();
                return UserTemplate.FromString(name, data);
            }
        }

        /// <summary>
        /// load template from string
        /// </summary>
        /// <param name="name">name of template</param>
        /// <param name="data">string containg code for template</param>
        /// <returns></returns>
        public static UserTemplate FromString(string name, string data)
        {
            TemplateLexer lexer = new TemplateLexer(data);
            TemplateParser parser = new TemplateParser(lexer);
            List<Element> elems = parser.Parse();

            TagParser tagParser = new TagParser(elems);
            elems = tagParser.CreateHierarchy();

            return new UserTemplate(name, elems);
        }

        /// <summary>
        /// go thru all tags and see if they are template tags and add
        /// them to this.templates collection
        /// </summary>
        private void InitTemplates()
        {
            this.m_templates = new Dictionary<string, UserTemplate>(StringComparer.InvariantCultureIgnoreCase);

            foreach (Element elem in m_elements)
            {
                if (elem is Tag)
                {
                    Tag tag = (Tag)elem;
                    if (string.Compare(tag.Name, "template", true) == 0)
                    {
                        Expression ename = tag.AttributeValue("name");
                        string tname;
                        if (ename is StringLiteral)
                            tname = ((StringLiteral)ename).Content;
                        else
                            tname = "?";

                        UserTemplate template = new UserTemplate(tname, tag.InnerElements, this);
                        m_templates[tname] = template;
                    }
                }
            }
        }

        /// <summary>
        /// gets a list of elements for this template
        /// </summary>
        public List<Element> Elements
        {
            get { return this.m_elements; }
        }

        /// <summary>
        /// gets the name of this template
        /// </summary>
        public string Name
        {
            get { return this.m_name; }
        }

        /// <summary>
        /// returns true if this template has parent template
        /// </summary>
        public bool HasParent
        {
            get { return m_parent != null; }
        }

        /// <summary>
        /// gets parent template of this template
        /// </summary>
        /// <value></value>
        public UserTemplate Parent
        {
            get { return this.m_parent; }
        }

        /// <summary>
        /// finds template matching name. If this template does not
        /// contain template called name, and parent != null then
        /// FindTemplate is called on the parent
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual UserTemplate FindTemplate(string name)
        {
            if (m_templates.ContainsKey(name))
                return m_templates[name];
            else if (m_parent != null)
                return m_parent.FindTemplate(name);
            else
                return null;
        }

        /// <summary>
        /// gets dictionary of templates defined in this template
        /// </summary>
        public System.Collections.Generic.Dictionary<string, UserTemplate> Templates
        {
            get { return this.m_templates; }
        }
    }
}
