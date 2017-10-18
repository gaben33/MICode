﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.Interpreter.PresetMethods {
	public class PrintMethod : Method {
		public PrintMethod(string[] lines) : base(lines, 1, 0) {
		}

		public override void Invoke(Struct signature, int line) {
			Console.WriteLine(signature.inputs[0]);
		}
	}
}
