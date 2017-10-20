using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.Interpreter.Arithmetic {
    public class Operand : Token {

        public dynamic Value { get { return Variable.Value; }  set { Variable.Value = value; } }
        public Type Type { get { return Variable.Type; } }
        public Variable Variable { get; set; }

        public Operand(string input) {
            Name = input;
            if (Program.HasVariable(input, out Variable variable)) Variable = variable;
            else if (bool.TryParse(input, out bool boolResult)) Variable = new Variable("", boolResult, typeof(bool));
            else if (int.TryParse(input, out int intResult)) Variable = new Variable("", intResult, typeof(int));
            else if (float.TryParse(input, out float floatResult)) Variable = new Variable("", floatResult, typeof(float));
            else if (char.TryParse(input, out char charResult)) Variable = new Variable("", charResult, typeof(char));
            else Variable = new Variable("", input.Replace("\"", ""), typeof(string));
        }

        public override string ToString() => Value.ToString();
    }
}
