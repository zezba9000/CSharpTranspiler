using CS2X.Core.Agnostic;
using CS2X.Core.Transpilers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2X.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = Path.Combine(Environment.CurrentDirectory, @"..\..\..\");
			var solution = new Solution(Path.Combine(path, @"TestApp\TestApp.csproj"));
			var task = solution.Parse();
			task.Wait();
			TranspilerC.CompileSolution(solution, TranspilerC.TargetTypes.VCPP, Path.Combine(path, @"TestOutput\"));
		}
	}
}
