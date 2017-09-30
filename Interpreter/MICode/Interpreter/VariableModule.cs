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
			bool hasInitialVal = m.Groups[4].Length > 0;
			//switch(m.Groups[1].Value) {
			//	case "int":
			//		if (hasInitialVal) VariableManager.intVars.Add(m.Groups[2].Value, int.Parse(m.Groups[4].Value));
			//		else VariableManager.intVars.Add(m.Groups[2].Value, 0);
			//		break;
			//	case "char":
			//		if (hasInitialVal) VariableManager.charVars.Add(m.Groups[2].Value, char.Parse(m.Groups[4].Value));
			//		else VariableManager.charVars.Add(m.Groups[2].Value, (char)0);
			//		break;
			//	case "float":
			//		if (hasInitialVal) VariableManager.floatVars.Add(m.Groups[2].Value, float.Parse(m.Groups[4].Value));
			//		else VariableManager.floatVars.Add(m.Groups[2].Value, 0);
			//		break;
			//	case "bool":
			//		if (hasInitialVal) VariableManager.boolVars.Add(m.Groups[2].Value, bool.Parse(m.Groups[4].Value));
			//		else VariableManager.boolVars.Add(m.Groups[2].Value, false);
			//		break;
			//}
			Type t = Type.GetType(m.Groups[1].Value);
			if(hasInitialVal) 
			return true;
		}
	}
}
