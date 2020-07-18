using System;
using System.Collections.Generic;
using System.IO;

namespace MathExpressionParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var expression = "12 ^ 10";

           

            Console.WriteLine(Parser.Parse(expression).Eval());
        }
    }
}
