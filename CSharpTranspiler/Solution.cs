using CSharpTranspiler.Types;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSharpTranspiler
{
	public class Solution
	{
		public Microsoft.CodeAnalysis.Solution solution;
		public string filename;
		private bool isProjFilename;

		public List<Project> projects;

		public Solution(string filename)
		{
			this.filename = filename;
			if (Path.GetExtension(filename) == ".csproj") isProjFilename = true;
		}

		public async Task Parse()
		{
			using (var workspace = MSBuildWorkspace.Create())
			{
				projects = new List<Project>();
				if (!isProjFilename)
				{
					solution = await workspace.OpenSolutionAsync(filename);
					foreach (var csProj in solution.Projects)
					{
						var proj = new Project(csProj.FilePath);
						await proj.Parse(csProj);
						projects.Add(proj);
					}
				}
				else
				{
					var csProj = await workspace.OpenProjectAsync(filename);
					solution = csProj.Solution;
					var proj = new Project(csProj.FilePath);
					await proj.Parse(csProj);
					projects.Add(proj);
				}
			}
		}
	}
}
