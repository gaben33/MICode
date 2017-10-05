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
        public Func<dynamic, dynamic, dynamic> PerformBinaryOperation { get; private set; }

        private Operator(string name, int precedence, Side association, Func<dynamic, dynamic, dynamic> function) {
            Name = name;
            Precedence = precedence;
            Association = association;
            PerformBinaryOperation = function;
        }

        private Operator(string name, Func<dynamic, dynamic, dynamic> function) {
            Name = name;
            PerformBinaryOperation = function;
        }

        public static bool IsOperator(string input, out Operator op) {
            foreach(Operator o in Values) {
                if (input.Equals(o.Name)) { op = o; return true; }
            } op = null; return false;
        }

        public override string ToString() => Name;
    }
}
