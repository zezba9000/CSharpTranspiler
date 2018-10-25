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
	static class TestTools
	{
		public static void CompileToAllLangs(Solution solution)
		{
			string path = Path.Combine(Environment.CurrentDirectory, @"..\..\..\");
			TranspilerC.CompileSolution(solution, TranspilerC.TargetTypes.VCPP, Path.Combine(path, @"TestOutput\"));
		}
	}
}
