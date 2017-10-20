using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.Interpreter.PresetMethods {
	public class PrintMethod : Method {
		public PrintMethod(string[] lines) : base(lines, 0) {}

		public override void Invoke(Struct signature) {
            for(int i = 0; i < signature.inputs.Length; i++) {
                Console.Write(signature.inputs[signature.inputs.Length-i-1]);
            }
		}
	}
    public class PrintlnMethod : Method {
        public PrintlnMethod(string[] lines) : base(lines, 0) {}

        public override void Invoke(Struct signature) {
            for (int i = 0; i < signature.inputs.Length; i++) {
                Console.WriteLine(signature.inputs[signature.inputs.Length - i - 1]);
            }
        }
    }

    public class Max : Method {
        public Max(string[] lines) : base(lines, 0) { }
        public override void Invoke(Struct signature) {
            int max = signature.inputs[0].Value;
            for (int i = 0; i < signature.inputs.Length; i++) {
                if (signature.inputs[i].Value > max) max = signature.inputs[i].Value;
            }
            ReturnVal = max;
        }
    }

	public class Random : Method {
		public Random(string[] lines) : base(lines, 0) {
		}
		public override void Invoke(Struct signature) {
			ReturnVal = (float)(new System.Random().NextDouble());
		}
	}

	public class RandomInt : Method {
		public RandomInt(string[] lines) : base(lines, 0) {
		}
		public override void Invoke(Struct signature) {
			ReturnVal = new System.Random().Next();
		}
	}
}
