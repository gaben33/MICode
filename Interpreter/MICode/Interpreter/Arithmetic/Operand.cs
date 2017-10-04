using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICode.Interpreter.ArithmeticModule {
    class Operand : Token {

        dynamic value;

        public Operand(string input) {
            if (input == "") return;
            if (bool.TryParse(input, out bool boolResult)) value = boolResult;
            else if (int.TryParse(input, out int intResult)) value = intResult;
            else if (float.TryParse(input, out float floatResult)) value = floatResult;
            else if (char.TryParse(input, out char charResult)) value = charResult;
            else {
                Console.WriteLine(Program.Line);
                throw new NotImplementedException();
            }
        }

        public dynamic GetValue() {
            return value;
        }

        public override string ToString() {
            return value.ToString();
        }
    }
}
