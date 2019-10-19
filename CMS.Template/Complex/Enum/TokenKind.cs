
namespace CMS.Template.Enum
{
   public enum TokenKind
    {
        EOF,
        Comment,
        // common tokens
        ID,				// (alpha)+

        // text specific tokens
        TextData,

        // tag tokens
        TagStart,		// <zb: 
        TagEnd,			// > 
        TagEndClose,	// />
        TagClose,		// </zb:
        TagEquals,		// =


        // expression
        ExpStart,		// $ at the beginning
        ExpEnd,			// $ at the end
        LParen,			// (
        RParen,			// )
        Dot,			// .
        Comma,			// ,
        Integer,		// integer number
        Double,			// double number
        LBracket,		// [
        RBracket,		// ]

        // operators
        OpOr,           // "or" keyword
        OpAnd,          // "and" keyword
        OpIs,			// "is" keyword
        OpIsNot,		// "isnot" keyword
        OpLt,			// "lt" keyword
        OpGt,			// "gt" keyword
        OpLte,			// "lte" keyword
        OpGte,			// "gte" keyword

        // string tokens
        StringStart,	// "
        StringEnd,		// "
        StringText		// text within the string
    }
}
