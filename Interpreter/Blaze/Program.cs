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
		public static Method Method { get; private set; }//method enclosing instruction currently being executed
		public static bool Running = true;
		#endregion

		[STAThread]
		public static void Main(string[] args) {
			string path = Open(ref args);
			if (path == "null") return;
			//build method dictionary after preprocessing, so that line counts are corrected, and main method is fixed
			methods = MethodBuilder.CreateDictionary(File.ReadAllLines(args[0]));
			string uneditedPath = args[0];//path of file without preprocessing done
			methods["Main"].Invoke(new Struct());
			while (Running);
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
			if (args.Length == 0) return "null";
			string newPath = args[0].Replace(".blaze", ".burn");
			lines = File.ReadAllLines(args[0]).ToList();
			Preprocessor.Preprocessor.Fix(args[0], newPath);
			return newPath;
		}

		public static bool HasVariable(string name, out Variable var) {
			var = null;
			if (heap.ContainsKey(name)) { var = heap[name]; return true; }
			StackFrame sf = stack.Peek();
			if (sf.Vars.ContainsKey(name)) { var = sf.Vars[name]; return true; };
			return false;
		}

		public static Variable CreateVariable (string name, Type type, dynamic initialVal) {
			Variable v = new Variable(name, initialVal, type);
			stack.Peek().Vars.Add(name, v);
			return v;
		}

		public static bool HasMethod(string name, out Method method) {
			bool hasMethod = methods.ContainsKey(name);
			if (hasMethod) method = methods[name];
			else method = null;
			return hasMethod;
		}
	}
}