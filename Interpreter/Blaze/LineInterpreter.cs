using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Blaze.Interpreter {
	public class LineInterpreter {
		public static bool Interpret (string line) {
			Arithmetic.ArithmeticEvaluator.Evaluate(line);
			return true;
		}

		public static bool Interpret(string line, out dynamic result) {
			result = Arithmetic.ArithmeticEvaluator.Evaluate(line);
			return true;
		}
	}
}
