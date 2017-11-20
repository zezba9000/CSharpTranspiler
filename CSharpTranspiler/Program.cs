using CSharpTranspiler.Transpilers;
using System;
using System.Threading.Tasks;

namespace CSharpTranspiler
{
	class Program
	{
		static void Main(string[] args)
		{
			var task = Compile();
			task.Wait();
		}

		static async Task Compile()
		{
			try
			{
				var solution = new Solution(@"D:\Dev\Reign\CSharpTranspiler\TestApp\TestApp.csproj");
				await solution.Parse();
				TranspilerC.CompileSolution(solution, TranspilerC.TargetTypes.VCPP, @"D:\Dev\Reign\CSharpTranspiler\TestOutput\");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
