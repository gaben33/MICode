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
            // Todo add name
            ArgCount = method.ParamCount;
            Args = new Variable[ArgCount];

        }

        public dynamic Execute() {
            method.Invoke(new Struct(Args));
            return method.ReturnVal;
        }
    }
}
