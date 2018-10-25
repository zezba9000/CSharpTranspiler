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
			var projFileNames = new string[]
			{
				Path.Combine(path, @"TestLib\TestLib.csproj"),
				Path.Combine(path, @"TestApp\TestApp.csproj")
			};
			var solution = new Solution(projFileNames);
			var task = solution.Parse();
			task.Wait();
			TranspilerC.CompileSolution(solution, TranspilerC.TargetTypes.VCPP, Path.Combine(path, @"TestOutput\"));
		}
	}
}
