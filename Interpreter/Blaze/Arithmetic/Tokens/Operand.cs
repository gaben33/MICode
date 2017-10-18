using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.Interpreter.Arithmetic {
    public class Operand : Token {

        public dynamic Value { get; private set; }
        public Type Type { get; private set; }

        public Operand(string input) {
            Name = input;
            if (Program.HasVariable(input, out Variable variable)) {
                Value = variable.Value; Type = variable.Type;
            } else if (input[0] == '"' && input[input.Length - 1] == '"') Value = input.Trim('"');
            else if (bool.TryParse(input, out bool boolResult)) { Value = boolResult; Type = typeof(bool); }
            else if (int.TryParse(input, out int intResult)) { Value = intResult; Type = typeof(int); }
            else if (float.TryParse(input, out float floatResult)) { Value = floatResult; Type = typeof(int); }
            else if (char.TryParse(input, out char charResult)) { Value = charResult; Type = typeof(char); }
            else Value = input;//throw new NotImplementedException("Cannot identify type at line: " + (Program.Line + 1));
        }

        public override string ToString() => Value.ToString();
    }
}
