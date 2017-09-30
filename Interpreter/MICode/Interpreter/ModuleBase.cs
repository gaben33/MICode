using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MICode.Interpreter {
	public abstract class ModuleBase {
		//whether or not to advance to the next line (might just modify the current line without building a command
		public abstract bool Transform(string regex);
		
	}
}
