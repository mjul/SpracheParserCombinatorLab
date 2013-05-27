using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserCombinatorLab
{
    public class Form
    {
        public Literal Literal { get; private set; }
        public Form(Literal literal)
        {
            Literal = literal;
        }
    }

    public abstract class Literal
    {
    }
    public abstract class Literal<T> : Literal
    {
        protected Literal(T value)
        {
            Value = value;
        }
        public T Value { get; private set; }
    }

    public class StringLiteral : Literal<string>
    {
        public StringLiteral(string value) : base(value)
        {
        }
    }

    public class CharLiteral : Literal<char>
    {
        public CharLiteral(char value) : base(value)
        {
        }
    }

    public class IntLiteral : Literal<int>
    {
        public IntLiteral(int value)
            : base(value)
        {
        }
    }


    public class ListExpression
    {
        public IEnumerable<Form> Forms { get; set; }
        public ListExpression(IEnumerable<Form> forms)
        {
            Forms = forms;
        }
    }
}
