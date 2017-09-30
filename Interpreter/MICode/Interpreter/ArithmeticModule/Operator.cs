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

        public static IEnumerable<Operator> Values {
            get {
                yield return Plus;
                yield return Minus;
                yield return Times;
                yield return Divide;
            }
        }

        public enum Association {Left, Right};

        public readonly string name;
        public readonly int precedence;
        public readonly Association association;
        public Func<int, int, int> PerformOperation;

        private Operator(string name, int precedence, Association association, Func<int, int, int> PerfromOperation) {
            this.name = name;
            this.precedence = precedence;
            this.association = association;
            this.PerformOperation = PerformOperation;
        }
        

    }
}
