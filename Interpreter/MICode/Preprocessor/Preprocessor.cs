using System;
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
			
			using (StreamWriter swr = File.CreateText(output)) {
				swr.Write(template);
			}
		}

		static void EraseComments (ref string text) {
			text = Regex.Replace(text, @"\/\/.*\n", "");//line comments
			text = Regex.Replace(text, @"\/\*(.|\n)*\*\/", "");//block comments
		}

		static void DoDefines (ref string text) {
			MatchCollection mc = Regex.Matches(text, "#DEFINE (.+) (.+)");
			foreach(Match m in mc) {
				if(m.Success) {
					string definition = m.Groups[1].Value;
					string replacement = m.Groups[2].Value;
					text = text.Replace(definition, replacement);
				}
			}
			text = text.Replace("#DEFINE .+", "");
		}
	}
}
