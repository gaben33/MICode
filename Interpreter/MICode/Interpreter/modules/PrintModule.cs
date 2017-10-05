using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MICode.Interpreter {
	class PrintModule : ModuleBase {
		public override bool Transform(string regex) {
			Match m = Regex.Match(regex, @"print\((.*)\);");
			if(m.Success) {
                dynamic val = Arithmetic.ArithmeticEvaluator.Evaluate(m.Groups[1].Value);
                Console.WriteLine(val);
			}
			return false;
		}
	}
}
