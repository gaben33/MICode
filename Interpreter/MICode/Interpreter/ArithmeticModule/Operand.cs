using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICode.Interpreter.ArithmeticModule {
    class Operand : Token {

        dynamic value;

        public Operand(string input) {
            this.value = VariableManager.VarValues[input];
        }

        public Operand(double input) {
            this.value = input;
        }

        public dynamic GetValue() {
            return value;
        }

        public override string ToString() {
            return value.ToString();
        }
    }
}
