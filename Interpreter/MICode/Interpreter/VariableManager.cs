using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICode.Interpreter {
	public static class VariableManager {
		public static Dictionary<Type, Func<string, dynamic>> parsers = new Dictionary<Type, Func<string, dynamic>> {
			{ typeof(int), s => int.Parse(s) },
			{ typeof(float), s => float.Parse(s) },
			{ typeof(bool), s => bool.Parse(s) },
			{ typeof(char), s => char.Parse(s) }
		};

		public static Dictionary<string, Type> VarTypes = new Dictionary<string, Type>(500);

		public static Dictionary<string, dynamic> VarValues = new Dictionary<string, dynamic>(500);

		public static string GetValue(string variable) => VarValues[variable].ToString();

		public static void CreateVariable<T>(string name, T initialValue) {
			if(!VarTypes.ContainsKey(name)) {
				VarTypes.Add(name, typeof(T));
				VarValues.Add(name, initialValue);
			}
		}

		public static void CreateVariable<T>(string name) => CreateVariable<T>(name, default(T));
	}
}