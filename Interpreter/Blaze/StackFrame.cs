using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.Interpreter {
	public class StackFrame {
		public Dictionary<string, Variable> Vars = new Dictionary<string, Variable>();//variables executed on stack

		public int ExecutionLine { get; private set; }

		/// <summary>
		/// Creates a stack frame, to be added to the runtime stack.  
		/// </summary>
		/// <param name="line">The line that the stack frame opens on.  Is used to adjust execution location after stack completes</param>
		public StackFrame(int line) => ExecutionLine = line;


	}
}
