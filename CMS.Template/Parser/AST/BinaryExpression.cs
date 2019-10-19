using CMS.Template.Enum;

namespace CMS.Template.Parser.AST
{
    public class BinaryExpression : Expression
    {
        Expression m_lhs;
        Expression m_rhs;

        TokenKind op;

        public BinaryExpression(int line, int col, Expression lhs, TokenKind op, Expression rhs)
            : base(line, col)
        {
            this.m_lhs = lhs;
            this.m_rhs = rhs;
            this.op = op;
        }

        public Expression Lhs
        {
            get { return this.m_lhs; }
        }

        public Expression Rhs
        {
            get { return this.m_rhs; }
        }

        public TokenKind Operator
        {
            get { return this.op; }
        }
    }
}
