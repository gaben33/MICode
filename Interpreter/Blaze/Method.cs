using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.Interpreter {
	public class Method {
		public CodeBlock Block;
		public Struct Signature;
		//needs some notion of a signature

		public Method(CodeBlock block, Struct signature) {
			Block = block;
			Signature = signature;
		}
		
		public virtual void Invoke () {
			Block.Execute();
		}
	}

	public class FunctionalMethod : Method {
		public dynamic ReturnVal { get; set; }
		public FunctionalMethod(CodeBlock block, Struct signature) : base(block, signature) {
		}
	}

	public struct Struct {
		public Variable[] inputs;

		public Struct(Variable[] inputs) {
			this.inputs = inputs;
		}
	}
}
