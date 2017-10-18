﻿using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace Blaze.Interpreter.Arithmetic {

    public static class ArithmeticEvaluator {
        
        public static dynamic Evaluate(string input) {
            Variable variable = null;
            Match m = Regex.Match(input, @"([A-Za-z0-9]+)?\s([A-Za-z0-9]+)\s?=([^=;]+);");
            if(m.Success) {
                string type = m.Groups[1].Value;
                string name = m.Groups[2].Value;
                string right = m.Groups[3].Value;
                if(type == "") {
                    dynamic result = EvaluatePostFix(ToPostFix(Tokenize(input)));
                    variable = Program.CreateVariable(name, Variable.ParseType(type), result);
                } else {
                    if (Program.HasVariable(name, out Variable var)) variable = var;
                    else throw new NotImplementedException("The variable: " + name + " doesn't exist in the current context");
                    variable.Value = EvaluatePostFix(ToPostFix(Tokenize(input)));
                }
            } else return EvaluatePostFix(ToPostFix(Tokenize(input)));
            return variable.Value;
        }

        private static List<string> Tokenize(string input) {
            List<string> output = Regex.Split(input.Replace(" ", ""), @"(&&|\|\||==|!=|>=|<=|\+=|-=|\*=|\/=|%=|-|[=!+^*/%()<>])").ToList(); // Todo don't remove empty space until after tokenizing
            output.RemoveAll(i => i == ""); // TODO split the string such that no empty space tokens are created in the first place
            return output;
        }

        private static List<Token> ToPostFix(List<string> tokens) {
            List<Token> output = new List<Token>();
            Stack<Token> operators = new Stack<Token>(); // TODO rename operators because it can also hold ( which isn't an operator

            for (int i = 0; i < tokens.Count; i++) {
                Token token = Token.MakeToken(tokens[i]);
                if (token is Operand) output.Add(token);
                if (token is Function) operators.Push(token);
                if (token is OpeningBracket) operators.Push(token);
                if (token is ClosingBracket) {
                    while (!(operators.Peek() is OpeningBracket))  output.Add(operators.Pop());
                    if (token.Name == ")") {
                        operators.Pop();
                        if (operators.Peek() is Function) output.Add(operators.Pop());
                    }
                }
                if (token is Operator) {
                    if (token.Name == "-" && Token.IsUnary(i, tokens)) token = Operator.Negative; // determines if binary minus or unary negative
                    if (token.Name == "+" && Token.IsUnary(i, tokens)) continue; // filters out redundant +'s eg (x = +5)

                    while (KeepPushingOperators(token, operators)) output.Add(operators.Pop());
                    operators.Push(token);
                }
            }
            while (operators.Count > 0) {
                if (operators.Peek() is Operator) output.Add(operators.Pop());
                else operators.Pop();
            }
            return output;
        }

        private static dynamic EvaluatePostFix(List<Token> tokens) {
            Stack<Token> numbers = new Stack<Token>();

            for(int i = 0; i < tokens.Count; i++) {
                if (tokens[i] is Operand) numbers.Push(tokens[i]);
                else if(tokens[i] is Operator){
                    Operand result = Operator.PerformOperation(ref numbers, (Operator) tokens[i]);
                    numbers.Push(result);
                } else if(tokens[i] is Function) {
                    Function f = (Function) tokens[i];
                    for(int j = 0; j < f.ArgCount; j++) {
                        f.Args[j] = new Variable("temp", numbers.Pop(), ((Operand) tokens[i]).Type);
                    }
                    f.Execute();
                }
            }
            return numbers.Peek();
        }

        private static bool KeepPushingOperators(Token op, Stack<Token> operators) {
            if (operators.Count == 0 || operators.Peek() is OpeningBracket) return false;
            return ((Operator) operators.Peek()).Precedence >= ((Operator) op).Precedence && ((Operator) operators.Peek()).Association == Operator.Side.Left;
        }
    }
}