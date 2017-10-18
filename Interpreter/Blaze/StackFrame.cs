using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.Interpreter {
	public class StackFrame {
		public Dictionary<string, Variable> Vars = new Dictionary<string, Variable>();//variables executed on stack

		public StackFrame() { }
		public StackFrame(Struct vars) {
			foreach (Variable v in vars.inputs) Vars.Add(v.Name, v);
		}
	}
}
