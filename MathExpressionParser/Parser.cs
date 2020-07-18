using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace MathExpressionParser
{
    public class Parser
    {
        private readonly Tokenizer tokenizer;

        public static Node Parse(string exp)
        {
            return new Parser(new Tokenizer(new StringReader(exp))).ParseExpression();
        }

        public static Node Parse(TextReader reader)
        {
            return new Parser(new Tokenizer(reader)).ParseExpression();
        }

        private Parser(Tokenizer tokenizer)
        {
            this.tokenizer = tokenizer;
        }

        // parse an entire expression until EOF
        private Node ParseExpression()
        {
            // for the moment do basic math symbols
            var expr = ParseMathSymbols();

            if (tokenizer.CurrentToken != Token.EOF)
            {
                throw new SyntaxErrorException("Unexpected characters at the end of the string.");
            }

            return expr;
        }

        // parse sequence of math symbols
        private Node ParseMathSymbols()
        {
            // get the left hand side
            var lhsNode = ParseLeaf();

            while(true)
            {
                // match the operator
                Func<double, double, double> op = null;

                switch (tokenizer.CurrentToken)
                {
                    case Token.Add:
                        {
                            op = (a, b) => a + b;
                            break;
                        }

                    case Token.Subtract:
                        {
                            op = (a, b) => a - b;
                            break;
                        }
                    case Token.Multiply:
                        {
                            op = (a, b) => a * b;
                            break;
                        }
                    case Token.Divide:
                        {
                            op = (a, b) => a / b;
                            break;
                        }
                    case Token.Pow:
                        {
                            op = (a, b) => Math.Pow(a, b);
                            break;
                        }
                    
                }

                if (op == null)
                {
                    // return lhs if wrong operator
                    return lhsNode;
                }

                //move to next token
                tokenizer.NextToken();

                var rhsNode = ParseLeaf();

                // create a binary node and its gonna become the lhs from now
                lhsNode = new BinaryNode(lhsNode, rhsNode, op);

            }
        }

        // parse a leaf node
        private Node ParseLeaf()
        {
            // if number
            if (tokenizer.CurrentToken == Token.Number)
            {
                var numberNode = new NumberNode(tokenizer.Number);

                // consumed the token, move it to next one
                tokenizer.NextToken();

                return numberNode;
            }

            // something else
            throw new SyntaxErrorException($"Unexpected token: {tokenizer.CurrentToken}");
            
        }
    }
}
