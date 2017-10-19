﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.Interpreter.Arithmetic {
    public partial class Operator : Token {

        public enum Side { Left, Right, None };
        public int Precedence { get; private set; }
        public Side Association { get; private set; }
        public Func<dynamic, dynamic, dynamic> BinaryOperation { get; private set; }
        public Func<dynamic, dynamic> UnaryOperation { get; private set; }
        public Action<Variable, dynamic> AssignmentOperation { get; private set; }


        private Operator(string name, int precedence, Side association, Func<dynamic, dynamic, dynamic> function) {
            Name = name;
            Precedence = precedence;
            Association = association;
            BinaryOperation = function;
        }

        private Operator(string name, int precedence, Side association, Func<dynamic, dynamic> function) {
            Name = name; Precedence = precedence; Association = association; UnaryOperation = function;
        }

        private Operator(string name, Action<Variable, dynamic> function) {
            Name = name; AssignmentOperation = function;
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
            dynamic n1 = ((Operand) input.Pop()).Value;
            if (op.BinaryOperation != null) {
                dynamic n2 = ((Operand)input.Pop()).Value;
                output = op.BinaryOperation(n2, n1);
                return Token.MakeToken(output.ToString());
            } else if (op.UnaryOperation != null) return Token.MakeToken(op.UnaryOperation(n1).ToString());
            else if (op.AssignmentOperation != null) {
                op.AssignmentOperation(n1, ((Operand)input.Pop()).Value);
                return null;
            } else throw new NotImplementedException("Operation not defined");
        }

        public override string ToString() => Name;
    }
}
