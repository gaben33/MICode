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
            Queue<Token> output = new Queue<Token>();
            string[] tokens = Regex.Split(input.Replace(" ", "").Replace("--", "+"), @"(&{2}|\|{2}|={2}|!=|>=|<=|[+^*/%()<>])");

            for (int i = 0; i < tokens.Length; i++) {
                string t = tokens[i];
                if (t != "") {
                    int count = t.Count(f => f == '-');
                    if ((count > 0 && t[0] != '-') || (count > 1 && t[0] == '-')) {
                        for (int j = 1; j < t.Length; j++) {
                            if (t[j] == '-' && !Operator.IsOperator(t[j - 1].ToString())) {
                                t = t.Insert(j, "+");
                            }
                        }
                        foreach (Token tempToken in Tokenize(t)) output.Enqueue(tempToken);
                    } else output.Enqueue(Token.MakeToken(t));
                }
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
                    numbers = Operator.PerformOperation(numbers, (Operator)tokens.Dequeue(), out dynamic result);
                    numbers.Push(Token.MakeToken(result.ToString()));
                }
            }
            return numbers.Peek();
        }
    }
}

