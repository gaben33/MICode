using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.Interpreter {
	public class StackFrame {
		public Dictionary<string, Variable> Vars = new Dictionary<string, Variable>();//variables executed on stack
		public int Line;//line that stack frame starts on.  Used for looping purposes
		public Action<int> OnStackOpen, OnStackClose;

		public StackFrame (int lineIndex) {
			Line = lineIndex;
		}

		public StackFrame (Struct vars, int lineIndex) : this(lineIndex) {
			foreach (Variable v in vars.inputs) Vars.Add(v.Name, v);
		}

		public void Open() => OnStackOpen?.Invoke(Line);
		public void Close() => OnStackClose?.Invoke(Line);
	}
}
