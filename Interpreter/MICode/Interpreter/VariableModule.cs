using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MICode.Interpreter {
	public class VariableModule : ModuleBase {
		public override void Transform(string regex) {
			Match m = Regex.Match(regex, @"(int|char|float|bool)\s?([A-Za-z]+)\s?(=\s?([^;]+))?;");
			bool hasInitialVal = m.Groups[4].Length > 0;
			switch(m.Groups[1].Value) {
				case "int":
					if (hasInitialVal) VariableManager.intVars.Add(m.Groups[2].Value, int.Parse(m.Groups[4].Value));
					else VariableManager.intVars.Add(m.Groups[2].Value, 0);
					break;
			}
		}
	}
}
