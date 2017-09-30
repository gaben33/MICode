using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MICode.Interpreter {
	public class GotoModule : ModuleBase {
		public override bool Transform(string regex) {
			Match m = Regex.Match(regex, @"goto (\d+)");
			if(m.Success) Program.CommandQueue.Enqueue(() => Program.SetLine(int.Parse(m.Groups[1].Value) - 2));
			return true;
		}
	}
}
