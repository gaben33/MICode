using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICode.Interpreter.Arithmetic {
    public partial class Operator : Token {

        public enum Side { Left, Right, None };
        public string Name { get; private set; }
        public int Precedence { get; private set; }
        public Side Association { get; private set; }
        public Func<dynamic, dynamic, dynamic> BinaryOperation { get; private set; }
        public Func<dynamic, dynamic> UnaryOperation { get; private set; }

        private Operator(string name, int precedence, Side association, Func<dynamic, dynamic, dynamic> function) {
            Name = name;
            Precedence = precedence;
            Association = association;
            BinaryOperation = function;
        }

        private Operator(string name, Func<dynamic, dynamic, dynamic> function) {
            Name = name;
            BinaryOperation = function;
        }

        private Operator(string name, int precedence, Side association, Func<dynamic, dynamic> function) {
            Name = name;  Precedence = precedence; Association = association; UnaryOperation = function;
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

        public static Stack<Token> PerformOperation(Stack<Token> input, Operator op, out dynamic output) {
            dynamic n1 = ((Operand)input.Pop()).Value;
            if((output = op.BinaryOperation?.Invoke(((Operand)input.Pop()).Value, n1)) != null) return input;
            else output = op.UnaryOperation?.Invoke(n1);
            return input;
        }

        public override string ToString() => Name;
    }
}
