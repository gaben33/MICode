using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using static Blaze.Interpreter.Arithmetic.ArithmeticEvaluator;

namespace Blaze.Interpreter {
	public class LineInterpreter {
		public static bool Interpret(string line, out dynamic result) {
			result = null;
			//scope close operator
			if (line.Contains("}")) return false;
			//check to see if a line is a return statement
			Regex r = new Regex(@"return\s*([^;]+)?;");
			Match m = r.Match(line);
			if(m.Success) {
				result = Evaluate(m.Groups[1].Value);
				return false;//return statement should end the method
			}
			//handle conditionals and loops
			r = new Regex(@"(if|while)\s?(\([^;{}]+);?");
			m = r.Match(line);
			if(m.Success) {
				//isolate the parameter to the conditional/loop.  Also put the rest of the captured string inside excess
				string parameter = "";
				string excess = "";
				//determine if it passes the test using the arithmetic evaluator
				bool passing = Evaluate(parameter);
				//work through statement
				switch(m.Groups[1].Value) {
					case "while"://entering a while loop
						if (passing) {//if I pass, then I'm good to start looping
							//if I don't start with a scoping bracket, I need to just interpret the line
							//TODO: I need to make a stack frame that closes when it doesn't pass, and just interprets this line specifically.  
							//Consider adding an action in StackFrame that calls every time it opens 
							//(so that it can just interpret the line when it opens, and repeat the frame when it closes if the test passes)
							//this code shouldn't work.  
							if(excess.Trim(' ') != "{") if (!Interpret(line, out result)) return false;
							int l = Program.Line;
							
						}
						break;
					case "if":
						//if it passes, then interpret the result recursively
						if (passing) {
							
						}
						break;
					default:
						break;
				}
				
			}
			//run arithmetic on line
			result = Arithmetic.ArithmeticEvaluator.Evaluate(line);
			return true;
		}
	}
}
