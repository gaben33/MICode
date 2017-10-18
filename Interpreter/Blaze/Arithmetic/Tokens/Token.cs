using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.Interpreter.Arithmetic {
    public class Token {

        public string Name { get; protected set; }

        public static Token MakeToken(string input) {
            if (input == "(") return OpeningBracket.LeftParentheses;
            if (input == ")") return ClosingBracket.RightParentheses;
            if (input == ",") return ClosingBracket.Comma;
            if (Operator.IsOperator(input, out Operator op)) return op;
            return new Operand(input);
        }

        public static bool IsUnary(int index, List<string> tokens) {
            return index == 0 || tokens[index - 1] == "(" || (Operator.IsOperator(tokens[index - 1]));
        }
    }

    
}
