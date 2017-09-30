﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICode.Interpreter.ArithmeticModule {
    class Token {

        public static Token MakeToken(string input) {
            if (input.Equals("+")) return Operator.Plus;
            if (input.Equals("-")) return Operator.Minus;
            if (input.Equals("*")) return Operator.Times;
            if (input.Equals("/")) return Operator.Divide;
            if (VariableManager.HasVariable(input, out dynamic val)) return new Operand(val);
            return new Operand(Double.Parse(input));

        }
    }

    
}
