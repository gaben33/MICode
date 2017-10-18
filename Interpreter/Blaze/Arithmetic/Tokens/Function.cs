using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.Interpreter.Arithmetic {
    class Function : Token {

        public int ArgCount { get; private set; }
        public Token[] Args { get; set; }
        
        public Function(string name, int argCount) {
            ArgCount = argCount;
            Args = new Token[argCount];

        }

        public dynamic Execute() {
            throw new NotImplementedException();
        }
    }
}
