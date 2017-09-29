using System;

public class ArithmeticModule : ModuleBase {

    public static void Evaluate() {

    }

    private static Queue<Token> Tokenize(string input) {

    }

    private static Queue<string> ToPostFix(Queue<Token> tokens) {
        Queue<Token> output = new Queue<Token>();
        Stack<Operator> operators = new Stack<Operator>();

        foreach(Token token in tokens) {
            
        }

    }

    private static void EvalutePostFix() {

    }


}
