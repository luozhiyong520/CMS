using System;

namespace CMS.Template.Except
{
    /// <summary>
    /// 词法分析异常
    /// </summary>
    public class ParseException : Exception
    {
        int m_line;
        int m_col;

        public ParseException(string msg, int line, int col)
            : base(msg)
        {
            this.m_line = line;
            this.m_col = col;

        }

        public int Col
        {
            get { return this.m_col; }
        }

        public int Line
        {
            get { return this.m_line; }
        }
    }
}
