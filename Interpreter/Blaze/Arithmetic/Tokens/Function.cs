using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.Interpreter;

namespace Blaze.Interpreter.Arithmetic {
    class Function : Token {

        private int count;
        public int ArgCount { get { return count; } set { args = new Variable[value]; count = value; } }
        private Variable[] args;
        public Variable[] Args { get { return args; } }
        private Method method;
        
        public Function(Method method) {
            this.method = method;
            //Name = method.Name;
            //ArgCount = method.ParamCount;
            //Args = new Variable[ArgCount];

        }

        public dynamic Execute() {
            method.Invoke(new Struct(Args));
            return method.ReturnVal;
        }
    }
}
