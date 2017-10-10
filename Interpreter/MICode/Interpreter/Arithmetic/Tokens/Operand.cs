using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICode.Interpreter.Arithmetic {
    class Operand : Token {

        public dynamic Value { get; private set; }
       

        public Operand(string input) {
			if (bool.TryParse(input, out bool boolResult)) Value = boolResult;
			else if (int.TryParse(input, out int intResult)) Value = intResult;
			else if (float.TryParse(input, out float floatResult)) Value = floatResult;
			else if (char.TryParse(input, out char charResult)) Value = charResult;
			else throw new NotImplementedException("Cannot identify type at line: " + (Program.Line+1));
        }

        public override string ToString() => Value.ToString();
    }
}
