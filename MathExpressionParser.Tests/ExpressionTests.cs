using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace MathExpressionParser.Tests
{
    public class ExpressionTests
    {
        [Fact]
        public void TokenizerBadInputThrowsException()
        {
            var testString = "&";
            Tokenizer t;

            Assert.Throws<InvalidDataException>(() => t = new Tokenizer(new StringReader(testString)));

        }

        //[Theory]
        //[InlineData(new object[] {1, "test" }, new object[] {2, "test" })]
        //public void Test(params object[] array)
        //{
        //    foreach (var item in array)
        //    {
        //        object[] temp = (object[])item;
        //        Assert.Equal("test", temp[1]);
        //        ;
        //    }

        //}

        [Theory]
        [InlineData("5", Token.Number)]
        [InlineData("+", Token.Add)]
        [InlineData("", Token.EOF)]
        [InlineData("5-6", Token.Number, Token.Subtract,Token.Number)]

        public void TokenizerExpressions(string expr, params Token[] tokens)
        {
            var tokenizer = new Tokenizer(new StringReader(expr));
            foreach (var item in tokens)
            {
                Assert.Equal(item, tokenizer.CurrentToken);
                tokenizer.NextToken();
            }

        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.0)]
        [InlineData(1.0)]
        [InlineData(1.6)]
        [InlineData(100.6)]
        public void TokenizeDoubleNumbers(double val)
        {
            var t = new Tokenizer(new StringReader(val.ToString()));

            Assert.Equal(Token.Number, t.CurrentToken);
            Assert.Equal(val, t.Number);
        }

        [Fact]
        public void TokenizerTests()
        {
            var testString = "10 + 20 - 30.123";
            var t = new Tokenizer(new StringReader(testString));

            // "10"
            Assert.Equal(Token.Number, t.CurrentToken);
            Assert.Equal(10, t.Number);
            t.NextToken();

            // "+"
            Assert.Equal(Token.Add, t.CurrentToken);
            t.NextToken();
        }

        [Theory]
        [ClassData(typeof(NodeClassData))]
        public void BinaryNodeTests(double x, double y, double res, string op, Dictionary<string, Func<double, double, double>> funcs)
        {
            var binarynode = new BinaryNode(new NumberNode(x), new NumberNode(y), funcs[op]);

            Assert.Equal(res, binarynode.Eval());

        }

    }
}
