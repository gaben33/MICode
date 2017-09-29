using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICode.Interpreter {
	public class Program {
		#region variables
		private static bool running = true;
		#endregion

		static void Main(string[] args) {
			while (running) {
				if (CommandQueue.Count > 0) CommandQueue.Dequeue()();
			}
		}

		public static Queue<Action> CommandQueue = new Queue<Action>();

		public static void Stop() => running = false;
	}
}
