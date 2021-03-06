﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace MICode.Preprocessor {
	public class Preprocessor {
		//preprocesses the string and puts the replacement at output
		public static void Fix (string path, string output) {
			string template = File.ReadAllText(path);
			EraseComments(ref template);
			DoDefines(ref template);
			DoIncrement(ref template);
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
			MatchCollection mc = Regex.Matches(text, @"label\s?([^\s;]+);");
			foreach(Match m in mc) {
				Interpreter.Program.lineLabels.Add(m.Groups[1].Value, 0);
			}
		}
	}
}
