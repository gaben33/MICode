using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICode.Interpreter.ArithmeticModule {
    class Operator : Token {

        public static readonly Operator Plus = new Operator("+", 2, Association.Left, (i1, i2) => i1 + i2);
        public static readonly Operator Minus = new Operator("-", 2, Association.Left, (i1, i2) => i1 - i2);
        public static readonly Operator Times = new Operator("*", 3, Association.Left, (i1, i2) => i1 * i2);
        public static readonly Operator Divide = new Operator("/", 3, Association.Left, (i1, i2) => i1 / i2);
        public static readonly Operator Modulus = new Operator("%", 3, Association.Left, (i1, i2) => i1 % i2);
        public static readonly Operator Power = new Operator("^", 4, Association.Right, (i1, i2) => (int)Math.Pow(i1, i2));
        public static readonly Operator LeftParentheses = new Operator("(", 0, Association.None, null);
        public static readonly Operator RightParentheses = new Operator(")", 0, Association.None, null);

        public static IEnumerable<Operator> Values {
            get {
                foreach (Operator o in Operator.Values) {
                    yield return o;
                }
            }
        }

        public enum Association { Left, Right, None };

        public readonly string name;
        public readonly int precedence;
        public readonly Association association;
        public readonly Func<double, double, double> PerformBinaryOperation;

        private Operator(string name, int precedence, Association association, Func<double, double, double> PerformOperation) {
            this.name = name;
            this.precedence = precedence;
            this.association = association;
            this.PerformBinaryOperation = PerformOperation;
        }

        public static bool IsOperator(string input, out Operator op) {
            foreach(Operator o in Values) {
                if (input.Equals(o.name)) { op = o; return true; }
            } op = null; return false;
        }

        public override string ToString() {
            return name;
        }
    }
}
