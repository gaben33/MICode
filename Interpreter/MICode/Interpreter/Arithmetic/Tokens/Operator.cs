using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICode.Interpreter.Arithmetic {
    public partial class Operator : Token {

        public enum Side { Left, Right, None };
        public int Precedence { get; private set; }
        public Side Association { get; private set; }
        public Func<dynamic, dynamic, dynamic> BinaryOperation { get; private set; }
        public Func<dynamic, dynamic> UnaryOperation { get; private set; }
        public Func<string, dynamic> AssignmentOperation { get; private set; }


        private Operator(string name, int precedence, Side association, Func<dynamic, dynamic, dynamic> function) {
            Name = name;
            Precedence = precedence;
            Association = association;
            BinaryOperation = function;
        }

        private Operator(string name, int precedence, Side association, Func<dynamic, dynamic> function) {
            Name = name; Precedence = precedence; Association = association; UnaryOperation = function;
        }

        public static bool IsOperator(string input, out Operator op) {
            foreach(Operator o in Values) {
                if (input.Equals(o.Name)) { op = o; return true; }
            } op = null; return false;
        }

        public static bool IsOperator(string input) {
            foreach (Operator o in Values) {
                if (input.Equals(o.Name)) { return true; }
            }
            return false;
        }

        public static Operand PerformOperation(ref Stack<Token> input, Operator op) {
            dynamic output;
            dynamic n1 = ((Operand)input.Pop()).Value;
            if ((output = op.BinaryOperation?.Invoke(((Operand)input.Pop()).Value, n1)) != null) return Token.MakeToken(output.ToString());
            return Token.MakeToken(op.UnaryOperation?.Invoke(n1).ToString());
        }

        public override string ToString() => Name;
    }
}
