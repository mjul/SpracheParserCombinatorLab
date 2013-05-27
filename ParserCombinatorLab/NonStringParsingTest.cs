using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Sprache;

namespace ParserCombinatorLab
{
    [TestFixture]
    public class NonStringParsingTest
    {
        public static Parser<Literal> Literal = GetNonStringInput;

        public static Parser<TLiteral> LiteralType<TLiteral>() where TLiteral : Literal
        {
            return (from l in Literal 
                    where (l is TLiteral) 
                    select ((TLiteral)l));
        }

        private static Parser<IntLiteral> IntLiteral = LiteralType<IntLiteral>();

        private static Parser<StringLiteral> StringLiteral = LiteralType<StringLiteral>();

        private static Parser<CharLiteral> CharLiteral = LiteralType<CharLiteral>();

        private static Parser<int> IntParser =
            (from i in IntLiteral
             select i.Value);

        private static Parser<string> SourceFormat =
            (from i in IntLiteral select String.Format(@"{0}", i.Value))
            .Or(from s in StringLiteral select String.Format("{0}", s.Value))
            .Or(from c in CharLiteral select String.Format("{0}", c.Value));

        private static NonStringInput Input = new NonStringInput(new IntLiteral(42));

        private static IResult<Literal> GetNonStringInput(Input input)
        {
            return Input;
        }

        [Test]
        public void SourceFormat_FromNonStringInputIntLiteral_ShouldParse()
        {
            Input = new NonStringInput(new IntLiteral(42));
            var actual = SourceFormat.Parse(String.Empty);
            Assert.That(actual, Is.EqualTo("42"));
        }

        [Test]
        public void SourceFormat_FromNonStringInputStringLiteral_ShouldParse()
        {
            Input = new NonStringInput(new StringLiteral("Hello, Combinator"));
            var actual = SourceFormat.Parse(String.Empty);
            Assert.That(actual, Is.EqualTo("Hello, Combinator"));
        }

    
    }

    internal class NonStringInput : IResult<Literal>
    {
        public NonStringInput(Literal value)
        {
            Value = value;
            WasSuccessful = true;
            Message = "NonStringInput";
            Expectations = new string[]{};
            Remainder = null;
        }

        public Literal Value { get; private set; }
        public bool WasSuccessful { get; private set; }
        public string Message { get; private set; }
        public IEnumerable<string> Expectations { get; private set; }
        public Input Remainder { get; private set; }
    }
}
