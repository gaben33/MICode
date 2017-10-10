using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICode.Interpreter.Arithmetic {
    public class Token {

        public static Token MakeToken(string input) {
            dynamic val;
            if (Operator.IsOperator(input, out Operator op)) return op;
            else if (VariableManager.HasVariable(input, out val) || (input[0] == '-' && VariableManager.HasVariable(input.Substring(1), out val))) {
                if (input[0] != '-') return new Operand(val.ToString());
                else return new Operand(val.ToString(), true);
            } else return new Operand(input);
        }
    }

    
}
