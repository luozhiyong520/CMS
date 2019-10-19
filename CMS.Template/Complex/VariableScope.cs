using System;
using System.Collections.Generic;

namespace CMS.Template.Complex
{
    public class VariableScope
    {
        VariableScope m_parent;
        Dictionary<string, object> m_values;

        public VariableScope()
            : this(null)
        {
        }

        public VariableScope(VariableScope parent)
        {
            this.m_parent = parent;
            this.m_values = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// clear all variables from this scope
        /// </summary>
        public void Clear()
        {
            m_values.Clear();
        }

        /// <summary>
        /// gets the parent scope for this scope
        /// </summary>
        public VariableScope Parent
        {
            get { return m_parent; }
        }

        /// <summary>
        /// returns true if variable name is defined
        /// otherwise returns parents isDefined if parent != null
        /// </summary>
        public bool IsDefined(string name)
        {
            if (m_values.ContainsKey(name))
                return true;
            else if (m_parent != null)
                return m_parent.IsDefined(name);
            else
                return false;
        }

        /// <summary>
        /// returns value of variable name
        /// If name is not in this scope and parent != null
        /// parents this[name] is called
        /// </summary>
        public object this[string name]
        {
            get
            {
                if (!m_values.ContainsKey(name))
                {
                    if (m_parent != null)
                        return m_parent[name];
                    else
                        return null;
                }
                else
                    return m_values[name];
            }
            set { m_values[name] = value; }
        }
    }
}
