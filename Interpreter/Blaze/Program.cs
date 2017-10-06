using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.Interpreter {
	class Program {
		public static Dictionary<string, Variable> heap = new Dictionary<string, Variable>();
		public static Stack<StackFrame> stack = new Stack<StackFrame>();
		
		public static void Main(string[] args) {
			StackFrame root = new StackFrame(0);
			stack.Push(root);
			
		}
	}
}