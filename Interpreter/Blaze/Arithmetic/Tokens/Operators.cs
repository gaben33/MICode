using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.Interpreter.Arithmetic {
    public partial class Operator {
        //math
        public static readonly Operator Plus = new Operator("+", 2, Side.Left, (i1, i2) => i1 + i2);
        public static readonly Operator Minus = new Operator("-", 2, Side.Left, (i1, i2) => i1 - i2);
        public static readonly Operator Times = new Operator("*", 3, Side.Left, (i1, i2) => i1 * i2);
        public static readonly Operator Divide = new Operator("/", 3, Side.Left, (i1, i2) => i1 / i2);
        public static readonly Operator Modulus = new Operator("%", 3, Side.Left, (i1, i2) => i1 % i2);
        public static readonly Operator Power = new Operator("^", 4, Side.Right, (i1, i2) => Math.Pow(i1, i2));
        //logic
        public static readonly Operator LogicalEquals = new Operator("==", 1, Side.Left, (i1, i2) => i1 == i2);
        public static readonly Operator LogicalNotEquals = new Operator("!=", 1, Side.Left, (i1, i2) => i1 != i2);
        public static readonly Operator LogicalAnd = new Operator("&&", 1, Side.None, (i1, i2) => i1 && i2);
        public static readonly Operator LogicalOr = new Operator("||", 1, Side.None, (i1, i2) => i1 || i2);
        public static readonly Operator LogicalGreaterThan = new Operator(">", 1, Side.Left, (i1, i2) => i1 > i2);
        public static readonly Operator LogicalLessThan = new Operator("<", 1, Side.Left, (i1, i2) => i1 < i2);
        public static readonly Operator LogicalGreaterOrEqualTo = new Operator(">=", 1, Side.Left, (i1, i2) => i1 >= i2);
        public static readonly Operator LogicalLessOrEqualTo = new Operator("<=", 1, Side.Left, (i1, i2) => i1 <= i2);
        //unary
        public static readonly Operator Negative = new Operator("–", 5, Side.Left, (i) => -i);
        public static readonly Operator Not = new Operator("!", 2, Side.Left, (i) => !i);
        public static readonly Operator Increment = new Operator("++", 5, Side.Left, (i) => i+1);
        public static readonly Operator Decrement = new Operator("--", 2, Side.Left, (i) => i--);
        //assignment
        public static readonly Operator AssignmentEquals = new Operator("=", (i1, i2) => i1.Value = i2);
        public static readonly Operator PlusEquals = new Operator("+=", (i1, i2) => i1.Value += i2);
        public static readonly Operator MinusEquals = new Operator("-=", (i1, i2) => i1.Value -= i2);
        public static readonly Operator TimesEquals = new Operator("*=", (i1, i2) => i1.Value *= i2);
        public static readonly Operator DividedEquals = new Operator("/=", (i1, i2) => i1.Value /= i2);
        public static readonly Operator ModulusEquals = new Operator("%=", (i1, i2) => i1.Value %= i2);

        public static IEnumerable<Operator> Values {
            get {
                yield return Plus;
                yield return Minus;
                yield return Times;
                yield return Divide;
                yield return Modulus;
                yield return Power;
                yield return LogicalEquals;
                yield return LogicalNotEquals;
                yield return LogicalAnd;
                yield return LogicalOr;
                yield return LogicalGreaterThan;
                yield return LogicalLessThan;
                yield return LogicalGreaterOrEqualTo;
                yield return LogicalLessOrEqualTo;
                yield return Negative;
                yield return Not;
                yield return Increment;
                yield return Decrement;
                yield return AssignmentEquals;
                yield return PlusEquals;
                yield return MinusEquals;
                yield return TimesEquals;
                yield return DividedEquals;
                yield return ModulusEquals;
            }
        }
    }
}
