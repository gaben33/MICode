using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.Interpreter.Arithmetic {
    public class OpeningBracket : Token {

        public static readonly OpeningBracket LeftParentheses = new OpeningBracket("(");

        public static IEnumerable<OpeningBracket> Values {
            get { yield return LeftParentheses; }
        }

        private OpeningBracket(string name) {
            Name = name;
        }
    }
}
