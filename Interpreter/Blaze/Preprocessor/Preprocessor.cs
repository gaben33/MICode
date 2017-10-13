using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Blaze.Preprocessor {
	public class Preprocessor {
		//preprocesses the string and puts the replacement at output
		public static void Fix (string path, string output) {
			string template = File.ReadAllText(path);
			BuildLabels(ref template);
			//do line changing changes after this point
			EraseComments(ref template);
			DoDefines(ref template);
			//DoIncrement(ref template);
			DoMainMethodCase(ref template);
			//do things that rely on post-line changes here

			
			using (StreamWriter swr = File.CreateText(output)) {
				swr.Write(template);
			}
		}

		static void EraseComments (ref string text) {
			text = Regex.Replace(text, @"(.+)?\/\/.*", "$1");//line comments
			text = Regex.Replace(text, @"\/\*(.|\n)*\*\/", "");//block comments
		}

		static void DoDefines (ref string text) {
			MatchCollection mc = Regex.Matches(text, "#DEFINE (.+) (.+)");
			foreach(Match m in mc) {
				if(m.Success) {
					string definition = m.Groups[1].Value;
					string replacement = m.Groups[2].Value;
					text = Regex.Replace(text, definition, replacement);
				}
			}
			text = Regex.Replace(text, "#DEFINE .+", "");
		}

		static void DoIncrement (ref string text) {
			text = Regex.Replace(text, @"([a-zA-Z]+)(\+|-){2}", @"$1 = $1 $2 1");
		}

		static void BuildLabels (ref string text) {
			//we need to create labels for goto numbers
			Regex r = new Regex(@"goto\s(\d+)");
			List<string> lines = text.Split('\n').ToList();
			Dictionary<int, string> newLines = new Dictionary<int, string>();
			List<int> locations = new List<int>();
			for (int i = 0; i < lines.Count; i++) {
				string s = lines[i];
				Match m = r.Match(s);
				if (m.Success) {
					int gotoLine = int.Parse(m.Groups[1].Value) - 1;
					string name = "line_" + (gotoLine + 1);
					newLines.Add(gotoLine, $"\tlabel {name};\n\r");
					if (!locations.Contains(gotoLine)) {
						locations.Add(gotoLine);
						lines[i] = r.Replace(s, @"goto " + name);
					}
				}
			}
			locations.Sort();
			//locations.Reverse();
			for(int i = locations.Count - 1; i >= 0; i--) {
				lines.Insert(locations[i], newLines[locations[i]]);
			}
			text = string.Join("\n", lines);
		}

		static void DoMainMethodCase (ref string text) {
			text = Regex.Replace(text, @"(void|int|char|bool|string)\smain", @"$1 Main");
		}
	}
}
