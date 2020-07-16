using System;
using System.Collections.Generic;
using System.Text;

namespace MathExpressionParser
{
    public class BinaryNode : Node
    {
        Node left;
        Node right;
        Func<double, double, double> op;
        public BinaryNode(Node left, Node right, Func<double, double, double> op)
        {
            this.left = left;
            this.right = right;
            this.op = op;
        }

        public override double Eval()
        {
            return op(left.Eval(), right.Eval());
        }
    }
}
