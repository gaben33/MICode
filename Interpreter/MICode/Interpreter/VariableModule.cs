using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MICode.Interpreter {
	public class VariableModule : ModuleBase {
		public override bool Transform(string regex) {
			Match m = Regex.Match(regex, @"(int|char|float|bool)\s([A-Za-z]+)\s?(=\s?([^;]+))?;");
			if (m.Success) {
				bool hasInitialVal = m.Groups[4].Length > 0;
				Type t = ToType(m.Groups[1].Value);
				dynamic initialVal = null;
                dynamic val = ArithmeticModule.ArithmeticManager.Evaluate(m.Groups[4].Value);
				if (hasInitialVal) initialVal = VariableManager.parsers[t](val.ToString());
				switch(m.Groups[1].Value) {
					case "int": VariableManager.CreateVariable<int>(m.Groups[2].Value, initialVal);
						break;
					case "char": VariableManager.CreateVariable<char>(m.Groups[2].Value, initialVal);
						break;
					case "float": VariableManager.CreateVariable<float>(m.Groups[2].Value, initialVal);
						break;
					case "bool": VariableManager.CreateVariable<bool>(m.Groups[2].Value, initialVal);
						break;
					default: VariableManager.CreateVariable<dynamic>(m.Groups[2].Value, initialVal);
						break;
				}
			} else {
				m = Regex.Match(regex, @"([A-Za-z]+)\s?=[^=](\s?([^;]*))?;");
				//Console.WriteLine(m.Success);
				if(m.Success) {
					string varName = m.Groups[1].Value;
					string varValue = m.Groups[3].Value;
                    //Console.WriteLine($"Value: {varValue}");
                    if (VariableManager.HasVariable(varName, out dynamic val)) {
                        VariableManager.SetValue(varName, varValue);
                    } else throw new NotImplementedException();
					
				}
			}

			return false;
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
