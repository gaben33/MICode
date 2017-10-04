using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;
using System.Linq.Expressions;

namespace MICode.Interpreter {
	public class ConditionalModule : ModuleBase {
		public SortedList<int, LineSorter> ranlines = new SortedList<int, LineSorter>();
		
		public override bool Transform(string regex) {
			int line = Program.Line;
			if (ranlines.ContainsKey(line)) return true;
			Match m = Regex.Match(regex, @"if\(([^,]+),\s(goto\s)?(\d+)\);");
			int targetLine = int.Parse(m.Groups[3].Value);
			if (targetLine - 1 < line) ranlines.Add(line, new LineSorter());
			bool result = ArithmeticModule.ArithmeticManager.Evaluate(m.Groups[1].Value);
			if (!result) {
				Program.CommandQueue.Enqueue(() => Program.Line = targetLine - 1);
				return true;
			}
			return false;
		}
	}

	public class LineSorter : IComparer<int> {
		public int Compare(int x, int y) => x.CompareTo(y);
	}
}
