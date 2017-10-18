using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.Interpreter {
	public class Method {
		public string[] lines;
		public int ParamCount;
		public dynamic ReturnVal;

		public Method(string[] lines, int paramCount) {
			this.lines = lines;
			ParamCount = paramCount;
		}

		public virtual void Invoke(Struct signature, int line) {
			StackFrame frame = new StackFrame(signature, line);
			Program.stack.Push(frame);
			MethodOpen();
			for (int i = 0; i < lines.Length; i++) if (!LineInterpreter.Interpret(lines[i], out ReturnVal)) break;
			MethodClose();
		}

		public virtual void MethodOpen () {
		}

		public virtual void MethodClose () => Program.stack.Pop();
	}

	public class Struct {
		public Variable[] inputs;

		public Struct() {
			inputs = new Variable[0];
		}

		public Struct(Variable[] inputs) {
			this.inputs = inputs;
		}
	}
}