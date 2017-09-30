using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICode.Interpreter.ArithmeticModule {

    public class ArithmeticModule : ModuleBase {


        public override void Transform(string regex) {
            throw new NotImplementedException();
        }

        public static void Evaluate(String input) {
            
        }

        private static Queue<Token> Tokenize(string input) {
            String[] tokens = input.Split(' ');
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
                    while (operators.Count > 0 && operators.Peek().precedence >= op.precedence && operators.Peek().association == Operator.Association.Left) {
                        output.Enqueue(operators.Pop());
                    } 
                    operators.Push(op);
                    }
                }

            while(operators.Count > 0) {
                output.Enqueue(operators.Pop());
            }
            return output;
        }

        private static void EvalutePostFix() {
            //Operator.Plus.PerformOperation(1, 2);
        }

        
    }
}

