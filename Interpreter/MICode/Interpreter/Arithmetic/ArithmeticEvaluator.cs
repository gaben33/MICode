using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MICode.Interpreter.Arithmetic {

    public class ArithmeticEvaluator {
        
        public static dynamic Evaluate(string input) {
            return EvaluatePostFix(ToPostFix(Tokenize(input)));
        }

        private static Queue<Token> Tokenize(string input) {
            string[] tokens = Regex.Split(input.Replace(" ", ""), @"(&{2}|\|{2}|={2}|!=|>=|<=|-|[+^*/%()<>])");
            Queue<Token> output = new Queue<Token>();
            foreach (string t in tokens) {
                if (t != "") output.Enqueue(Token.MakeToken(t));
            }
            return output;
        }

        private static Queue<Token> ToPostFix(Queue<Token> tokens) {
            Queue<Token> output = new Queue<Token>();
            Stack<Operator> operators = new Stack<Operator>();

            foreach (Token token in tokens) {
                if (!token.GetType().Equals(typeof(Operator))) {
                    output.Enqueue(token);
                } else {
                    Operator op = (Operator) token;
                    if(op.Name != "(" || op.Name != ")") {
                        while (operators.Count > 0 && operators.Peek().Precedence >= op.Precedence && operators.Peek().Association == Operator.Side.Left) {
                            output.Enqueue(operators.Pop());
                        }
                        operators.Push(op);
                    } else if(op.Name == "(") {
                        operators.Push(op);
                    } else if(op.Name == ")") {
                        while(operators.Peek().Name != "(") {
                            output.Enqueue(operators.Pop());
                        }
                        operators.Pop();
                    }
                }
            }

            while(operators.Count > 0) {
                if (operators.Peek().Name != "(" && operators.Peek().Name != ")") {
                    output.Enqueue(operators.Pop());
                } else operators.Pop();
            }
            
            return output;
        }

        private static dynamic EvaluatePostFix(Queue<Token> tokens) {
            Stack<Token> numbers = new Stack<Token>();
            
            while (tokens.Count > 0) {
                if (!tokens.Peek().GetType().Equals(typeof(Operator))) {
                    numbers.Push(tokens.Dequeue());
                } else {
                    dynamic n1 = ((Operand) numbers.Pop()).Value;
                    dynamic n2 = ((Operand) numbers.Pop()).Value;
                    Operator op = (Operator) tokens.Dequeue();
                    try {
                        dynamic o = op.PerformBinaryOperation(n2, n1);
                        numbers.Push(Token.MakeToken(o.ToString()));
                    } catch (Exception) {
                        throw new FormatException("Mixed Types at line: " + (Program.Line + 1));
                    }
                }
            }
            return numbers.Peek();
        }
    }
}

