using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.Interpreter {
	public interface IExecutable {
		void Execute();
	}

	public abstract class Instruction : IExecutable {
		public abstract void Execute();
	}

	public class CodeBlock : IExecutable {
		int StartLine, StopLine;
		public Instruction[] Instructions;

		public CodeBlock(int startLine, int stopLine) {
			StartLine = startLine;
			StopLine = stopLine;
		}

		public void Execute() {
			for (int i = 0; i < Instructions.Length; i++) Instructions[i].Execute();
		}

	}

	public abstract class StackBuilder {
		public abstract StackFrame BuildFrame();
	}
}
