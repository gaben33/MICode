using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICode.Interpreter.ArithmeticModule {

    public class ArithmeticManager {

        public static dynamic Evaluate(string input) {
            return EvaluatePostFix(ToPostFix(Tokenize(input)));
        }

        private static Queue<Token> Tokenize(string input) {
            string[] tokens = input.Split(' ');
            Queue<Token> output = new Queue<Token>(tokens.Select(s => Token.MakeToken(s)));
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
                    if(op != Operator.LeftParentheses || op != Operator.RightParentheses) {
                        while (operators.Count > 0 && operators.Peek().precedence >= op.precedence && operators.Peek().association == Operator.Association.Left) {
                            output.Enqueue(operators.Pop());
                        }
                        operators.Push(op);
                    } else if(op == Operator.LeftParentheses) {
                        operators.Push(op);
                    } else if(op == Operator.RightParentheses) {
                        while(operators.Peek() != Operator.LeftParentheses) {
                            output.Enqueue(operators.Pop());
                        }
                        operators.Pop();
                    }
                }
            }

            while(operators.Count > 0) {
                output.Enqueue(operators.Pop());
            }
            return output;
        }

        private static dynamic EvaluatePostFix(Queue<Token> tokens) {
            Stack<Token> numbers = new Stack<Token>();
            while(tokens.Count > 0) {
                Token token = tokens.Dequeue();
                if (!token.GetType().Equals(typeof(Operator))) {
                    numbers.Push(token);
                } else {
                    Operand op1 = (Operand) numbers.Pop();
                    Operand op2 = (Operand) numbers.Pop();
                    Operator op = (Operator) token;
                    double n1 = op1.GetValue();
                    double n2 = op2.GetValue();
                    double o = op.PerformBinaryOperation(n2, n1);
                    numbers.Push(Token.MakeToken(o.ToString()));
                }
            }
            return numbers.Peek();
        }
    }
}

