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
		public int Line;

		public Method(string[] lines, int paramCount, int line) {
			this.lines = lines;
			ParamCount = paramCount;
			Line = line;
		}

		public virtual void Invoke(Struct signature) {
			StackFrame frame = new StackFrame(signature, Line);
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