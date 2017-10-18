using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.Interpreter.Arithmetic {
    public class ClosingBracket : Token {

        public static readonly ClosingBracket RightParentheses = new ClosingBracket(")");
        public static readonly ClosingBracket Comma = new ClosingBracket(",");

        public static IEnumerable<ClosingBracket> Values {
            get { yield return RightParentheses; }
        }

        private ClosingBracket(string name) {
            Name = name;
        }
    }
}
