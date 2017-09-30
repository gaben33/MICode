using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICode.Interpreter.ArithmeticModule {
    class Operator : Token{

        public static readonly Operator Plus = new Operator("+", 2, Association.Left, (i1,i2) => i1 + i2);
        public static readonly Operator Minus = new Operator("-", 2, Association.Left, (i1, i2) => i1 - i2);
        public static readonly Operator Times = new Operator("*", 3, Association.Left, (i1, i2) => i1 * i2);
        public static readonly Operator Divide = new Operator("/", 3, Association.Left, (i1, i2) => i1 / i2);
        public static readonly Operator Modulus = new Operator("%", 3, Association.Left, (i1, i2) => i1 % i2);
        public static readonly Operator Power = new Operator("^", 3, Association.Right, (i1, i2) => (int) Math.Pow(i1, i2));
        public static readonly Operator Factorial = new Operator("!", 3, Association.Left, (i1) => i1);

        public static IEnumerable<Operator> Values {
            get {
                foreach(Operator o in Operator.Values) {
                    yield return o;
                }
            }
        }

        public enum Association {Left, Right};

        public readonly string name;
        public readonly int precedence;
        public readonly Association association;
        public readonly Func<double, double, double> PerformBinaryOperation;
        public readonly Func<int, int> PerformUnaryOperation;

        private Operator(string name, int precedence, Association association, Func<double, double, double> PerformOperation) {
            this.name = name;
            this.precedence = precedence;
            this.association = association;
            this.PerformBinaryOperation = PerformOperation;
        }

        private Operator(string name, int precedence, Association association, Func<int, int> PerformOperation) {
            this.name = name;
            this.precedence = precedence;
            this.association = association;
            this.PerformUnaryOperation = PerformOperation;
        }

        public override string ToString() {
            return name;
        }
    }
}
