using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze {
	public class Variable {
		public string Name;
		public dynamic Value;
		public Type Type;
		private static Dictionary<Type, Func<string, dynamic>> parsers = new Dictionary<Type, Func<string, dynamic>> {
			{ typeof(int), s => int.Parse(s) },
			{ typeof(float), s => float.Parse(s) },
			{ typeof(bool), s => bool.Parse(s) },
			{ typeof(char), s => char.Parse(s) }
		};

		public Variable(string name, dynamic value, Type type) {
			Name = name;
			Value = value;
			Type = type;
		}

		public void Parse(string value) {
			Value = parsers[Type](value);
		}
	}
}
