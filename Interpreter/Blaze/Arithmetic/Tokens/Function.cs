using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.Interpreter;

namespace Blaze.Interpreter.Arithmetic {
    class Function : Token {

        public int ArgCount { get; private set; }
        public Variable[] Args { get; set; }
        private Method method;
        
        public Function(Method method) {
            this.method = method;
            ArgCount = method.ParamCount;
            Args = new Variable[ArgCount];

        }

        public dynamic Execute() {
            Struct p = new Struct(Args);
            method.Invoke(p);
            return method.ReturnVal;
        }
    }
}
