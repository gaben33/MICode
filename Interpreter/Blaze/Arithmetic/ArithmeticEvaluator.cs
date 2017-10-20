using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace Blaze.Interpreter.Arithmetic {

    public static class ArithmeticEvaluator {

        private static string TokenRegex = @"(&&|\|\||==|!=|>=|<=|\+=|-=|\*=|\/=|%=|\+\+|-|[,=!+^*/%()<>])";

        public static dynamic Evaluate(string input) {
            Variable variable = null;
            Match m = Regex.Match(input, @"([A-Za-z0-9]+)?\s?([A-Za-z0-9]+)\s?=([^=;]+);");
            if(m.Success) {
                string type = m.Groups[1].Value;
                string name = m.Groups[2].Value;
                dynamic result = EvaluatePostFix(ToPostFix(Tokenize(m.Groups[3].Value)));
                if (type != "") {
                    variable = Program.CreateVariable(name, Variable.ParseType(type), result);
                } else {
                    if (Program.HasVariable(name, out Variable var)) {
                        var.Value = result;
                    } else throw new NotImplementedException("The variable: " + name + " doesn't exist in the current context");
                }
            } else return EvaluatePostFix(ToPostFix(Tokenize(input)));
            return null;
        }

        private static List<string> Tokenize(string input) {
            List<string> output = Regex.Split(input.Replace(" ", ""), TokenRegex).ToList();
            output.RemoveAll(i => i == ""); // TODO split the string such that no empty space tokens are created in the first place
            return output;
        }

        private static List<Token> ToPostFix(List<string> tokens) {
            List<Token> output = new List<Token>();
            Stack<Token> stack = new Stack<Token>();
            Stack<int> arity = new Stack<int>();

            for (int i = 0; i < tokens.Count; i++) {
                Token token = Token.MakeToken(tokens[i]);
                if (token is Operand) output.Add(token);
                else if (token is Function) { stack.Push(token); arity.Push(1); }
                else if (token is OpeningBracket) stack.Push(token);
                else if (token is ClosingBracket) {
                    while (!(stack.Peek() is OpeningBracket))  output.Add(stack.Pop());
                    if (token.Name == ")") {
                        stack.Pop();
                        if (stack.Count > 0 && stack.Peek() is Function) {
                            Function f = (Function) stack.Pop();
                            f.ArgCount = arity.Pop();
                            output.Add(f); 
                        };
                    }
                    if(token.Name == ",") { arity.Push(arity.Pop() + 1); }
                } 
                else if (token is Operator) {
                    if (token.Name == "-" && Token.IsUnary(i, tokens)) token = Operator.Negative; // determines if binary minus or unary negative
                    if (token.Name == "+" && Token.IsUnary(i, tokens)) continue; // filters out redundant +'s eg (x = +5)

                    while (KeepPushingOperators(token, stack)) output.Add(stack.Pop());
                    stack.Push(token);
                }
            }
            while (stack.Count > 0) {
                if (stack.Peek() is Operator) output.Add(stack.Pop());
                else stack.Pop();
            }
            return output;
        }

        private static dynamic EvaluatePostFix(List<Token> tokens) {
            Stack<Token> numbers = new Stack<Token>();

            for(int i = 0; i < tokens.Count; i++) {
                if (tokens[i] is Operand o) numbers.Push(o);
                else if(tokens[i] is Operator curOp){
                    Operand result = Operator.PerformOperation(ref numbers, curOp);
                    numbers.Push(result);
                } else if(tokens[i] is Function f) {
                    for(int j = 0; j < f.ArgCount; j++) {
                        Operand operand = (Operand) numbers.Pop();
                        f.Args[j] = operand.Variable;
                    }
                    dynamic result;
                    if ((result = f.Execute()) != null) {
                        numbers.Push(Token.MakeToken(result.ToString()));
                    }
                }
            }
            return ((Operand) numbers.Peek()).Value;
        }

        private static bool KeepPushingOperators(Token op, Stack<Token> operators) {
            if (operators.Count == 0 || operators.Peek() is OpeningBracket) return false;
            return ((Operator) operators.Peek()).Precedence >= ((Operator) op).Precedence && ((Operator) operators.Peek()).Association == Operator.Side.Left;
        }
    }
}