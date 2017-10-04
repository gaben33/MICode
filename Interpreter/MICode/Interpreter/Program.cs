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
			new CommentModule(),
			new VariableModule(),
			new PrintModule(),
			new GotoModule()
		};

		public static int Line { get; set; } = 0;
		public static Dictionary<string, int> lineLabels = new Dictionary<string, int>();
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
			string newPath = args[0].Replace(".blaze", ".burn");
			Preprocessor.Preprocessor.Fix(args[0], newPath);
			List<string> lines = File.ReadAllLines(newPath).ToList();
			while (running && Line < lines.Count) {
				while (CommandQueue.Count > 0) CommandQueue.Dequeue()();
				for (int i = 0; i < Modules.Length; i++) {
					bool forceContinue = Modules[i].Transform(lines[Line]);
					while (CommandQueue.Count > 0) CommandQueue.Dequeue()();
					if (forceContinue) break;
				}
				Line++;
			}
			while (true) ;
		}

		public static Queue<Action> CommandQueue = new Queue<Action>();

		public static void Stop() => running = false;
	}
}
