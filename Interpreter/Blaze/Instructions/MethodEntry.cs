using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Blaze.Interpreter.Arithmetic;

namespace Blaze.Interpreter.Instructions {
	public class MethodEntry : Instruction {
		public MethodEntry(Method enclosingMethod, Struct signature) : base(enclosingMethod, signature) {}

		public override void Execute() {
			
			EnclosingMethod.Invoke();

		}
	}

	//assumes variable has been created as an object and filled with proper value.  This adds it to the stack frame
	public class VariableCreation : Instruction {
		public Variable var;
		public VariableCreation(Method enclosingMethod, Variable variable) : base(enclosingMethod, new Struct()) => var = variable;

		public override void Execute() => Program.stack.Peek().Vars.Add(var.Name, var);
	}

	public class ArithmeticParse : Instruction {
		string Line;
		public ArithmeticParse(Method enclosingMethod, Struct signature, string line) : base(enclosingMethod, signature) {
			Line = line;
		}

		public override void Execute() {
			
		}
	}
}
