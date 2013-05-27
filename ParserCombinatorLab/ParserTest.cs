using System.Linq;
using NUnit.Framework;
using Sprache;

namespace ParserCombinatorLab
{
    [TestFixture]
    public class ParserTest
    {
        [Test]
        public void CharLiteral_Valid_ShouldParse()
        {
            var actual = Grammar.CharLiteral.Parse("'x'");
            Assert.That(actual.Value, Is.EqualTo('x'));
        }

        [Test]
        public void StringLiteral_Valid_ShouldParse()
        {
            var actual = Grammar.StringLiteral.Parse("\"abc\"");
            Assert.That(actual.Value, Is.EqualTo("abc"));
        }

        [Test]
        public void IntLiteral_Valid_ShouldParse()
        {
            var actual = Grammar.IntLiteral.Parse("123");
            Assert.That(actual.Value, Is.EqualTo(123));
        }

        [Test]
         public void ListExpression_EmptyList_ShouldParse()
         {
             var result = Grammar.ListExpression.Parse("()");
             Assert.That(result.Forms, Is.Empty);
         }

        [Test]
        public void ListExpression_SingleForm_ShouldParse()
        {
            var result = Grammar.ListExpression.Parse("('x')");
            Assert.That(result.Forms, Has.Count.EqualTo(1));
        }

    }
}