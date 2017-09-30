using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MICode.Interpreter {
	public class VariableModule : ModuleBase {
		public override bool Transform(string regex) {
			Match m = Regex.Match(regex, @"(int|char|float|bool)\s?([A-Za-z]+)\s?(=\s?([^;]+))?;");
			if (m.Success) {
				bool hasInitialVal = m.Groups[4].Length > 0;
				Type t = ToType(m.Groups[1].Value);
				dynamic initialVal = null;
				if (hasInitialVal) initialVal = VariableManager.parsers[t](m.Groups[4].Value);
				VariableManager.CreateVariable<dynamic>(m.Groups[2].Value, initialVal);
			}
			return true;
		}

		private Type ToType (string type) {
			switch(type) {
				case "int": return typeof(int);
				case "char": return typeof(char);
				case "float": return typeof(float);
				case "bool": return typeof(bool);
				default: return typeof(int);
			}
		}
	}
}
