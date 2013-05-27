# Sprache Parser Combinator Lab

Demonstration of the Sprache parser combinator for .NET.


# Parsing Text is Simple
Parser combinators are a clever way to express the grammar of the input language directly as
higher-order functions in the host language, eliminating the need for our old friends, Lex and Yacc.

With Sprache the parsing is defined in Linq expressions.

For example, consider the following EBNF production:

    StringLiteral ::= QUOTE-CHAR ( NONQUOTE-CHAR )* QUOTE-CHAR ;

Here the terminals QUOTE-CHAR is the double-quote ("), and NONQUOTE-CHAR is any other character.
Translated to Sprache, it looks like this:

```csharp

        public static Parser<StringLiteral> StringLiteral =
            (from openQuote in Parse.Char('"')
             from text in Parse.CharExcept('"').Many().Text()
             from closeQuote in Parse.Char('"')
             select new StringLiteral(text));

```

`Many` denotes zero or more, `Text` folds the resulting sequence of chars from _Many_ of `Parse.CharExcept` into a single string.
`StringLiteral` is the class we have defined to represent a StringLiteral in our Abstract Syntax Tree, so the result
of the above code is mapping directly from text input to objects.

That's pretty cool for a few lines of code.

For a larger example see `ParserTest.cs` and the files defining the parser (`Grammar.cs`) and the Abstract Syntax Tree types (`AstTypes.cs`).

# Parser Non-Textual Input is Possible
With a twist of the arm, Sprache also parse object graphs not just text input.
The key is to define an IResult<T>, the return type of the parser.
For example, if we want to parse `Literal` subclasses we can define an instance like so: 

```csharp

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

``` 

The `NonStringParsingTest.cs` shows and example of how we can specify a grammar that allows us to use Sprache to 
navigate the object graph defined by this _IResult_. 

It could be useful for defining for example input validation or data mappings on the boundary of an application.

# Consider Using Formal Parsers More Often
It is my observation that writing a compiler is something that only graduates fresh out of university can do.
After a couple of years, many programmers degrade to a daily routine of building smal hand-written, 
_ad hoc_ parsers, possibly because they remember parser generators and compiler construction as complex activities.

Parser combinators change all that.

Now it is easy to express for example protocol or data exchange formats in a formal grammar, and parse them accordingly.

This is a good way to reduce security holes or bugs as you can rely on the parser library and grammar to verify that 
you only accept correct input.

With a formal grammar you also have a clear contract, and it has the added benefit that you get better 
error messages for invalid input, which may also be useful.

Whether you need to convert a bunch of old scripts to the new version of your ERP system, or migrate data from a
legacy system with poor data quality, or just decode some control messages, formal parsers should be one of the
first options to consider.

Happy parsing!
Martin

# License

This lab was created by Martin Jul and released under the MIT license, see the file [LICENSE.txt].


Sprache has the following license:

    The MIT License

    Copyright (c) 2011 Nicholas Blumhardt

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.

