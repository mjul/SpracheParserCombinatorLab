using System;
using Sprache;

namespace ParserCombinatorLab
{
    public class Grammar
    {
        public static Parser<StringLiteral> StringLiteral =
            (from openQuote in Parse.Char('"')
             from text in Parse.CharExcept('"').Many().Text()
             from closeQuote in Parse.Char('"')
             select new StringLiteral(text));

        public static Parser<CharLiteral> CharLiteral =
            (from openQuote in Parse.Char('\'')
             from ch in Parse.CharExcept('\'')
             from closeQuote in Parse.Char('\'')
             select new CharLiteral(ch));

        public static Parser<IntLiteral> IntLiteral =
            (from digits in Parse.Digit.AtLeastOnce().Text()
             select new IntLiteral(Int32.Parse(digits)));

        public static Parser<Literal> Literal =
            CharLiteral
            .Or<Literal>(StringLiteral)
            .Or<Literal>(IntLiteral);

        public static Parser<Form> Form =
            (from l in Literal.Token()
             select new Form(l));
        
        // list : OPEN_PAREN ( form )* CLOSE_PAREN
        public static Parser<ListExpression> ListExpression =
            (from openParen in Parse.Char('(')
             from forms in Form.Many().Token()
             from closeParen in Parse.Char(')')
             select new ListExpression(forms));

    }
}