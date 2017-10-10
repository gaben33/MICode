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

        private static List<string> Tokenize(string input) {
            List<string> output = new List<string>();
            string[] tokens = Regex.Split(input.Replace(" ", ""), @"(&{2}|\|{2}|={2}|!=|>=|<=|-|[!+^*/%()<>])");
            output = tokens.ToList();
            output.RemoveAll(i => i == ""); // TODO split the string such that no empty space tokens are created in the first place
            return output;
        }

        private static List<Token> ToPostFix(List<string> tokens) {
            List<Token> output = new List<Token>();
            Stack<Operator> operators = new Stack<Operator>();
            
            for(int i = 0; i < tokens.Count; i++) {
                if (!Operator.IsOperator(tokens[i])) output.Add(Token.MakeToken(tokens[i])); //TODO add an IsOperand method
                else if(Operator.IsOperator(tokens[i], out Operator op)){
                    if (op.Name == "-" && Token.IsUnary(i, tokens)) op = Operator.Negative; // determines if binary minus or unary negative
                    if (op.Name == "+" && Token.IsUnary(i, tokens)) continue; // filters out redundant +'s eg (x = +5)

                    if (op.Name == "(") operators.Push(op);
                    else if (op.Name == ")") {
                        while (operators.Peek().Name != "(") {
                            output.Add(operators.Pop());
                        }
                        operators.Pop();
                    } else {
                        while (KeepPushingOperators(op, operators)) {
                            output.Add(operators.Pop());
                        }
                        operators.Push(op);
                    }
                }
            }

            while (operators.Count > 0) {
                if (operators.Peek().Name != "(" && operators.Peek().Name != ")") output.Add(operators.Pop());
                else operators.Pop();
            }

            return output;
        }

        private static dynamic EvaluatePostFix(List<Token> tokens) {
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

        private static bool KeepPushingOperators(Operator op, Stack<Operator> operators) {
            return operators.Count > 0 && operators.Peek().Precedence >= op.Precedence && operators.Peek().Association == Operator.Side.Left;
        }
    }
}

