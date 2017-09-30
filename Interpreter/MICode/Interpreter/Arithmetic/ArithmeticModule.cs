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
                    if(op.name != "(" || op.name != ")") {
                        while (operators.Count > 0 && operators.Peek().precedence >= op.precedence && operators.Peek().association == Operator.Association.Left) {
                            output.Enqueue(operators.Pop());
                        }
                        operators.Push(op);
                    } else if(op.name == "(") {
                        operators.Push(op);
                    } else if(op.name == ")") {
                        while(operators.Peek().name != "(") {
                            output.Enqueue(operators.Pop());
                        }
                        operators.Pop();
                    }
                }
            }

            while(operators.Count > 0) {
                if (operators.Peek().name != "(" && operators.Peek().name != ")") {
                    output.Enqueue(operators.Pop());
                } else operators.Pop();
            }
            return output;
        }

        private static dynamic EvaluatePostFix(Queue<Token> tokens) {
            Stack<Token> numbers = new Stack<Token>();
            while(tokens.Count > 0) {
                if (!tokens.Peek().GetType().Equals(typeof(Operator))) {
                    numbers.Push(tokens.Dequeue());
                } else {
                    double n1 = ((Operand) numbers.Pop()).GetValue();
                    double n2 = ((Operand) numbers.Pop()).GetValue();
                    Operator op = (Operator) tokens.Dequeue();
                    double o = op.PerformBinaryOperation(n2, n1);
                    numbers.Push(Token.MakeToken(o.ToString()));
                }
            }
            return numbers.Peek();
        }
    }
}

