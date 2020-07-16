using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MathExpressionParser
{
    public class NodeClassData : IEnumerable<object[]>
    {
        static Dictionary<string, Func<double, double, double>> funcs = new Dictionary<string, Func<double, double, double>>()
        {
            {"add", (x, y) => x + y },
            {"sub", (x, y) => x - y },
            {"mult", (x, y) => x * y },
            {"div", (x, y) => x / y },
            
        };

        static readonly List<object[]> data = new List<object[]>()
        {
            new object[] {2, 2, 4, "add", funcs},
            new object[] { 2, 3, 6, "mult", funcs},
            new object[] { 2, 2, 1, "div", funcs},
            new object[] {2, 2, 0, "sub", funcs},

        };
        public IEnumerator<object[]> GetEnumerator() => data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
