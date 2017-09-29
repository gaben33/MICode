using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICode.Interpreter {
	public class VariableManager {
		private static readonly int varCount = 200;

		private static List<string> variableNames = new List<string>(varCount * 4);

		public static Dictionary<string, int> intVars = new Dictionary<string, int>(varCount);
		public static Dictionary<string, char> charVars = new Dictionary<string, char>(varCount);
		public static Dictionary<string, bool> boolVars = new Dictionary<string, bool>(varCount);
		public static Dictionary<string, float> floatVars = new Dictionary<string, float>(varCount);


	}
}
