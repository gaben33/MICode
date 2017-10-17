using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blaze.Interpreter.Arithmetic {

    public class ArithmeticEvaluator {
        
        public static dynamic Evaluate(string input) {
            Match m = Regex.Match(input, @"([A-Za-z0-9]+)?\s([A-Za-z0-9]+)\s?=\s?([A-Za-z0-9]+);");
            if(m.Success) {
                m.Groups[1]
            }
            return EvaluatePostFix(ToPostFix(Tokenize(input)));
        }

        private static List<string> Tokenize(string input) {
            List<string> output = Regex.Split(input.Replace(" ", ""), @"(&{2}|\|{2}|={2}|!=|>=|<=|-|\+=|[!+^*/%()<>])").ToList(); // Todo don't remove empty space until after tokenizing
            output.RemoveAll(i => i == ""); // TODO split the string such that no empty space tokens are created in the first place
            return output;
        }

        private static List<Token> ToPostFix(List<string> tokens) {
            List<Token> output = new List<Token>();
            Stack<Token> operators = new Stack<Token>(); // TODO rename operators because it can also hold ( which isn't an operator

            for (int i = 0; i < tokens.Count; i++) {
                Token token = Token.MakeToken(tokens[i]);
                if (token is Operand) output.Add(token);
                if (token is OpeningBracket) operators.Push(token);
                if (token is ClosingBracket) {
                    while (!(operators.Peek() is OpeningBracket))  output.Add(operators.Pop());
                    operators.Pop();
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
                if (tokens[i] is Operand) {
                    numbers.Push(tokens[i]);
                } else {
                    Operand result = Operator.PerformOperation(ref numbers, (Operator) tokens[i]);
                    numbers.Push(result);
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