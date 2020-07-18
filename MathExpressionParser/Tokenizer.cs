using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace MathExpressionParser
{
    public class Tokenizer
    {
        private readonly StringBuilder stringBuilder;
        private readonly TextReader reader;
        public char CurrentChar { get; private set; }
        public Token CurrentToken { get; private set; }
        public double Number { get; private set; }


        public Tokenizer(TextReader textReader)
        {
            stringBuilder = new StringBuilder();
            reader = textReader;
            // move to starting pos
            NextChar();
            NextToken();
        }

        private void NextChar()
        {
            int val = reader.Read();

            CurrentChar = val == -1 ? '\0' : (char)val;

        }

        public Token NextToken()
        {
            // lets skip whitespace
            while (char.IsWhiteSpace(CurrentChar))
            {
                NextChar();
            }

            // our special character tokens
            switch (CurrentChar)
            {
                case '\0':
                    CurrentToken = Token.EOF;
                    return CurrentToken;
                case '+':
                    NextChar();
                    CurrentToken = Token.Add;
                    return CurrentToken;
                case '-':
                    NextChar();
                    CurrentToken = Token.Subtract;
                    return CurrentToken;
                case '*':
                    NextChar();
                    CurrentToken = Token.Multiply;
                    return CurrentToken;
                case '/':
                    NextChar();
                    CurrentToken = Token.Divide;
                    return CurrentToken;
                case '^':
                    NextChar();
                    CurrentToken = Token.Pow;
                    return CurrentToken;
            }

            // if a number
            if (CurrentChar == '.' || char.IsDigit(CurrentChar))
            {
                stringBuilder.Clear();
                bool isDecimal = false;
                while (char.IsDigit(CurrentChar) || (!isDecimal && CurrentChar == '.'))
                {
                    stringBuilder.Append(CurrentChar);

                    isDecimal = CurrentChar == '.';

                    NextChar();
                }

                // parse it
                Number = double.Parse(stringBuilder.ToString(), CultureInfo.InvariantCulture);
                CurrentToken = Token.Number;
                return CurrentToken;
            }

            // if unrecognized
            throw new InvalidDataException($"Invalid character found: {CurrentChar}");
        }


    }
}
