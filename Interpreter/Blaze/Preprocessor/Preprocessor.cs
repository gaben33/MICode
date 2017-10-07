﻿using System;
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
			EraseComments(ref template);
			DoDefines(ref template);
			DoIncrement(ref template);
			UpperCaseMainMethod(ref template);
			//after this point needs to be done after all line numbers have been changed
			
			BuildLabels(ref template);
			using (StreamWriter swr = File.CreateText(output)) {
				swr.Write(template);
			}
		}

		static void EraseComments (ref string text) {
			text = Regex.Replace(text, @"(.+)\/\/.*", "$1");//line comments
			//text = Regex.Replace(text, @"\/\*(.|\n)*\*\/", "");//block comments
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
			List<string> lines = Interpreter.Program.lines.ToList();
			int labelCount = 0;
			List<int> addedLines = new List<int>();
			for (int i = 0; i < lines.Count; i++) {
				string s = lines[i];
				Match m = r.Match(s);
				if (m.Success) {
					int gotoLine = int.Parse(m.Groups[1].Value) - 1;
					addedLines.Add(gotoLine);
					int offset = addedLines.Count(l => l < gotoLine);
					gotoLine += offset;
					if (gotoLine < i) i++;//if adding a line before current marker, go down a line
					string name = "lbl" + labelCount;
					labelCount++;
					lines[i] = r.Replace(s, @"goto " + name);
					lines.Insert(gotoLine, "label " + name);
				}
			}
		}

		static void UpperCaseMainMethod (ref string text) {
			text = Regex.Replace(text, @"(void|int|char|bool|string)\smain", @"$1 Main");
		}
	}
}
