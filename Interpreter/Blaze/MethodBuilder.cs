using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Blaze.Interpreter {
	internal class MethodBuilder {
		public static Dictionary<string, Method> CreateDictionary(string[] text) {
			Dictionary<string, Method> methodDict = new Dictionary<string, Method>() {//default methods
				
			};
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
								lines.Dequeue();
								if (lines.Count == 1) {
									closingLine = lines.Dequeue();
									break;
								}
							}
						}
					}
					//find the name and type
					string name = curMatch.Groups[2].Value;
					string type = curMatch.Groups[1].Value;
					//create a code block for it, then encapsulate with a Method
					Method newMethod = new Method(text.Skip(openingLine).Take(closingLine - openingLine).ToArray());
					if (type != "void") newMethod = new FunctionalMethod(text.Skip(openingLine).Take(closingLine - openingLine).ToArray());
					//add the method to the dictionary
					methodDict.Add(name, newMethod);
				}
			}
			return methodDict;
		}
	}
}
