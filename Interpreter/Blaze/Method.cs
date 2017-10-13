using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.Interpreter {
	public class Method {
		public CodeBlock Block;

		public Method(CodeBlock block) {
			Block = block;
		}
		
		public virtual void Invoke () {
			Block.Execute();
		}
	}

	public class FunctionalMethod : Method {
		public dynamic ReturnVal { get; set; }
		public FunctionalMethod(CodeBlock block) : base(block) {
		}
		public override void Invoke() {
			
		}
	}
}
