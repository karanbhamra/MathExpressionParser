using System;
using System.Collections.Generic;
using System.Text;

namespace MathExpressionParser
{
    public class NumberNode : Node
    {
        double number;
        public NumberNode(double number)
        {
            this.number = number;
        }

        public override double Eval()
        {
            return number;
        }
    }
}
