using System;

namespace CMS.Template.Complex
{
    public class StaticTypeReference
    {
        readonly Type m_type;

        public StaticTypeReference(Type type)
        {
            this.m_type = type;
        }

        public Type Type
        {
            get { return m_type; }
        }
    }
}
