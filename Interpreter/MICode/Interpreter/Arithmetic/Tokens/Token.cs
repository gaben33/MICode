﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICode.Interpreter.Arithmetic {
    public class Token {

        public static Token MakeToken(string input) {
            dynamic val;
            if (Operator.IsOperator(input, out Operator op)) return op;
            else if (VariableManager.HasVariable(input, out val)) {
                return new Operand(val.ToString());
            } else return new Operand(input);
        }
    }

    
}
