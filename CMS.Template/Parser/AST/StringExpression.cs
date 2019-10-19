using System.Collections.Generic;

namespace CMS.Template.Parser.AST
{
    public class StringExpression : Expression
    {
        List<Expression> m_exps;

        public StringExpression(int line, int col)
            : base(line, col)
        {
            m_exps = new List<Expression>();
        }

        public int ExpCount
        {
            get { return m_exps.Count; }
        }

        public void Add(Expression exp)
        {
            m_exps.Add(exp);
        }

        public Expression this[int index]
        {
            get { return m_exps[index]; }
        }

        public IEnumerable<Expression> Expressions
        {
            get
            {
                for (int i = 0; i < m_exps.Count; i++)
                    yield return m_exps[i];
            }
        }
    }
}
