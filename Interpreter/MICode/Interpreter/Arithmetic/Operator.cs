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
        public static readonly Operator LogicalEquals = new Operator("==", (i1, i2) => i1 == i2);
        public static readonly Operator LogicalAnd = new Operator("&&", (i1, i2) => i1 && i2);
        public static readonly Operator LogicalOr = new Operator("||", (i1, i2) => i1 || i2);
        public static readonly Operator LogicalGreaterThan = new Operator(">", (i1, i2) => i1 > i2);
        public static readonly Operator LogicalLessThan = new Operator("<", (i1, i2) => i1 < i2);
        public static readonly Operator LogicalGreaterOrEqualTo = new Operator(">=", (i1, i2) => i1 >= i2);
        public static readonly Operator LogicalLessOrEqualTo = new Operator("<=", (i1, i2) => i1 <= i2);

        public static IEnumerable<Operator> Values {
            get {
                yield return Plus;
                yield return Minus;
                yield return Times;
                yield return Divide;
                yield return Modulus;
                yield return Power;
                yield return LeftParentheses;
                yield return RightParentheses;
                yield return LogicalEquals;
                yield return LogicalAnd;
                yield return LogicalOr;
                yield return LogicalGreaterThan;
                yield return LogicalLessThan;
                yield return LogicalGreaterOrEqualTo;
                yield return LogicalLessOrEqualTo;
            }
        }

        public enum Association { Left, Right, None };

        public readonly string name;
        public readonly int precedence;
        public readonly Association association;
        public readonly Func<dynamic, dynamic, dynamic> PerformBinaryOperation;

        private Operator(string name, int precedence, Association association, Func<dynamic, dynamic, dynamic> PerformOperation) {
            this.name = name;
            this.precedence = precedence;
            this.association = association;
            this.PerformBinaryOperation = PerformOperation;
        }

        private Operator(string name, Func<dynamic, dynamic, dynamic> PerformOperation) {
            this.name = name;
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
