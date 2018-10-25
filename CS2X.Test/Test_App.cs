using System;
using System.IO;
using CS2X.Core.Agnostic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CS2X.Test
{
	[TestClass]
	public class Test_App
	{
		[TestMethod]
		public void Compile()
		{
			//string path = Path.Combine(Environment.CurrentDirectory, @"..\..\..\");
			//var solution = new Solution(Path.Combine(path, @"TestApp\TestApp.csproj"));
			string path = @"C:\Users\zezba9000\Dev\Andrew\CSharpTranspiler\TestApp\TestApp.csproj";
			var solution = new Solution(path);
			var task = solution.Parse();
			task.Wait();
			TestTools.CompileToAllLangs(null);
			Assert.IsTrue(true);
		}
	}
}
