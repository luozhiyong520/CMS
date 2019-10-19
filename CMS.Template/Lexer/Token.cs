
using CMS.Template.Enum;

namespace CMS.Template.Lexer
{
    /// <summary>
    /// 词素
    /// </summary>
    public class Token
    {
        int m_line;
        int m_col;
        string m_data;
        TokenKind m_tokenKind;

        public Token(TokenKind kind, string data, int line, int col)
        {
            m_tokenKind = kind;
            m_line = line;
            m_col = col;
            m_data = data;
        }

        /// <summary>
        /// 所在列
        /// </summary>
        public int Col
        {
            get { return m_col; }
        }

        /// <summary>
        /// Token数据
        /// </summary>
        public string Data
        {
            get { return m_data; }
            set { this.m_data = value; }
        }

        /// <summary>
        /// 所在行
        /// </summary>
        public int Line
        {
            get { return m_line; }
        }

        /// <summary>
        /// Token类型
        /// </summary>
        public TokenKind TokenKind
        {
            get { return m_tokenKind; }
            set { m_tokenKind = value; }
        }
    }
}
