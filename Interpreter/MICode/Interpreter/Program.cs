using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace MICode.Interpreter {
	public class Program {
		#region variables
		private static bool running = true;
		private static ModuleBase[] Modules = new ModuleBase[] {
			new VariableModule(),
			new PrintModule(),
			new GotoModule()
		};

		private static int line = 0;
		#endregion

		[STAThread]
		static void Main(string[] args) {
			if(args.Count() == 0) {
				OpenFileDialog ofd = new OpenFileDialog() {
					InitialDirectory = "c:\\",
					Filter = "Blaze Files (*.blaze)|*.blaze"
				};
				if (ofd.ShowDialog() == DialogResult.OK) {
					args = new string[] { ofd.FileName };
				} else return;
			}
			List<string> lines = File.ReadAllLines(args[0]).ToList();
			while (running && line < lines.Count) {
				while (CommandQueue.Count > 0) CommandQueue.Dequeue()();
				for (int i = 0; i < Modules.Length; i++) {
					bool forceContinue = Modules[i].Transform(lines[line]);
					while (CommandQueue.Count > 0) CommandQueue.Dequeue()();
					if (forceContinue) break;
				}
				line++;
			}
			while (true) ;
		}

		public static Queue<Action> CommandQueue = new Queue<Action>();

		public static void Stop() => running = false;
		public static void SetLine(int lineNumber) => line = lineNumber;
	}
}
