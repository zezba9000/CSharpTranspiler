using CSharpTranspiler.Agnostic;
using CSharpTranspiler.Transpilers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CSharpTranspiler
{
	class Program
	{
		static void Main(string[] args = null)
		{
			var task = Compile();
			task.Wait();
		}

		static async Task Compile()
		{
			try
			{
				string path = Path.Combine(Environment.CurrentDirectory, @"..\..\..\");
				var solution = new Solution(Path.Combine(path, @"TestApp\TestApp.csproj"));
				await solution.Parse();
				TranspilerC.CompileSolution(solution, TranspilerC.TargetTypes.VCPP, Path.Combine(path, @"TestOutput\"));
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
			}
		}
	}
}
