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

		public virtual void Invoke(Struct signature) {
			StackFrame frame = new StackFrame(signature);
			Program.stack.Push(frame);
			for (int i = 0; i < lines.Length; i++) if (!LineInterpreter.Interpret(lines[i], out ReturnVal)) break;
			Program.stack.Pop();
		}
	}

	public struct Struct {
		public Variable[] inputs;

		public Struct(Variable[] inputs) {
			this.inputs = inputs;
		}
	}
}