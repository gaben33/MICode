using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Blaze.Interpreter.PresetMethods;

namespace Blaze.Interpreter {
	internal class MethodBuilder {
		public static Dictionary<string, Method> CreateDictionary(string[] text, out Dictionary<string, int> methodIndices) {
			Dictionary<string, Method> methodDict = new Dictionary<string, Method>() {//default methods
				{"print", new PrintMethod(new string[] { }) }
			};
			methodIndices = new Dictionary<string, int>();
			Regex methodSpotter = new Regex(@"(void|int|char|bool|string)\s([A-z]+)\s?\(([A-z,\s]+)?\)");
			for (int i = 0; i < text.Length; i++) {
				Match curMatch;
				if ((curMatch = methodSpotter.Match(text[i])).Success) {
					int openingLine = i, closingLine = i;
					//find closing line
					Queue<int> lines = new Queue<int>();
					for (int j = 0; j < text.Length; j++) {
						for (int k = 0; k < text[j].Length; k++) {
							if (text[j][k] == '{') lines.Enqueue(j);
							else if (text[j][k] == '}') {
								if (lines.Count == 1) {
									closingLine = j;
									break;
								} else lines.Dequeue();
							}
						}
					}
					//find the name and type, and param count
					string name = curMatch.Groups[2].Value;
					string type = curMatch.Groups[1].Value;
					int paramCount = curMatch.Groups[3].Value.Split(',').Select(s => s.Length > 1).Count();
					//create a code block for it, then encapsulate with a Method
					Method newMethod = new Method(text.Skip(openingLine).Take(closingLine - openingLine).ToArray(), paramCount);
					//add the method to the dictionary
					methodDict.Add(name, newMethod);
					//and also add the index
					methodIndices.Add(name, openingLine);
				}
			}
			return methodDict;
		}
	}
}
