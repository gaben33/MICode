using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MICode.Interpreter.Arithmetic {

    public class ArithmeticEvaluator {
        
        public static dynamic Evaluate(string input) {
            return EvaluatePostFix2(ToPostFix2(Tokenize2(input)));
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

        private static List<string> Tokenize2(string input) {
            List<Token> output = new List<Token>();
            string[] tokens = Regex.Split(input.Replace(" ", ""), @"(&{2}|\|{2}|={2}|!=|>=|<=|-|[+^*/%()<>])");
            //foreach(string token in tokens) if(token != "") output.Add(Token.MakeToken(token));
            return tokens.ToList();
        }

        private static List<Token> ToPostFix2(List<string> tokens) {
            List<Token> output = new List<Token>();
            Stack<Operator> operators = new Stack<Operator>();
            tokens.RemoveAll(i => i == "");
            for(int i = 0; i < tokens.Count; i++) {
                if (!Operator.IsOperator(tokens[i])) output.Add(Token.MakeToken(tokens[i]));
                else if(Operator.IsOperator(tokens[i], out Operator op)){
                    if (op.Name == "-") if (i == 0 || tokens[i-1] == "(" || (Operator.IsOperator(tokens[i-1])) && tokens[i-1] != ")") op = Operator.Negative;

                    if (op.Name == "(") operators.Push(op);
                    else if (op.Name == ")") {
                        while (operators.Peek().Name != "(") {
                            output.Add(operators.Pop());
                        }
                        operators.Pop();
                    } else {
                        while (operators.Count > 0 && operators.Peek().Precedence >= op.Precedence && operators.Peek().Association == Operator.Side.Left) {
                            output.Add(operators.Pop());
                        }
                        operators.Push(op);
                    }
                }
            }

            while (operators.Count > 0) {
                if (operators.Peek().Name != "(" && operators.Peek().Name != ")") {
                    output.Add(operators.Pop());
                } else operators.Pop();
            }

            return output;
        }

        private static dynamic EvaluatePostFix2(List<Token> tokens) {
            Stack<Token> numbers = new Stack<Token>();

            for(int i = 0; i < tokens.Count; i++) {
                if (tokens[i] is Operand) {
                    numbers.Push(tokens[i]);
                } else {
                    numbers = Operator.PerformOperation(numbers, (Operator)tokens[i], out dynamic result);
                    numbers.Push(Token.MakeToken(result.ToString()));
                }
            }
            return numbers.Peek();
        }

        private static Queue<Token> ToPostFix(Queue<Token> tokens) {
            Queue<Token> output = new Queue<Token>();
            Stack<Operator> operators = new Stack<Operator>();

            foreach (Token token in tokens) {
                if (token is Operand) {
                    output.Enqueue(token);
                } else {
                    Operator op = (Operator) token;
                    if(op.Name != "(" && op.Name != ")") {
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
                if (tokens.Peek() is Operand) {
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

