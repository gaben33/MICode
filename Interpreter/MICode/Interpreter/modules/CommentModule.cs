using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICode.Interpreter {
	public class CommentModule : ModuleBase {
		public override bool Transform(string regex) {
			if (regex.StartsWith("//")) return true;
			return false;
		}
	}
}
