using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Blaze.Preprocessor;

namespace Blaze.Interpreter {
	class Program {
		#region static access for interpretation
		public static Dictionary<string, Variable> heap = new Dictionary<string, Variable>();
		public static Stack<StackFrame> stack = new Stack<StackFrame>();
		public static Dictionary<string, Method> methods;
		public static int Line { get; set; }
		public static List<string> lines = new List<string>();
		#endregion

		[STAThread]
		public static void Main(string[] args) {
			string path = Open(ref args);
			//build method dictionary after preprocessing, so that line counts are corrected, and main method is fixed
			methods = MethodBuilder.CreateDictionary(args);
			StackFrame root = new StackFrame(0);
			stack.Push(root);
			string uneditedPath = args[0];//path of file without preprocessing done
			Interpret(methods["Main"]);
		}

		private static string Open (ref string[] args) {
			if (args.Count() == 0) {
				OpenFileDialog ofd = new OpenFileDialog() {
					InitialDirectory = "c:\\",
					Filter = "Blaze Files (*.blaze)|*.blaze"
				};
				if (ofd.ShowDialog() == DialogResult.OK) {
					args = new string[] { ofd.FileName };
				}
			}
			string newPath = args[0].Replace(".blaze", ".burn");
			lines = File.ReadAllLines(args[0]).ToList();
			Preprocessor.Preprocessor.Fix(args[0], newPath);
			return newPath;
		}

		public static void Interpret (Method method) {//interprets given method
			
		}
	}
}