using CS2X.Core.Agnostic.Types;
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
using Microsoft.Build.Locator;

namespace CS2X.Core.Agnostic
{
	public class Solution
	{
		public Microsoft.CodeAnalysis.Solution solution {get; private set;}
		public readonly string filename;
		private readonly bool isProjFileName;

		public IReadOnlyList<Project> projects {get; private set;}

		static Solution()
		{
			if (MSBuildLocator.CanRegister) MSBuildLocator.RegisterDefaults();
		}

		public Solution(string filename)
		{
			this.filename = filename;
			if (!File.Exists(filename)) throw new Exception("File does not exists: " + filename);
			string ext = Path.GetExtension(filename);
			if (ext == ".csproj") isProjFileName = true;
			else if (ext == ".sln") isProjFileName = false;
			else throw new Exception("Invalid file type: " + filename);
		}

		public async Task Parse()
		{
			using (var workspace = MSBuildWorkspace.Create())
			{
				// load projects
				if (!isProjFileName)
				{
					solution = await workspace.OpenSolutionAsync(filename);
				}
				else
				{
					var csProj = await workspace.OpenProjectAsync(filename);
					solution = csProj.Solution;
				}

				var projects = new List<Project>();
				foreach (var csProj in solution.Projects)
				{
					if (csProj.Name == "CoreLib") continue;// HACK: skip until we can parse CoreLib
					var proj = new Project(this, csProj);
					projects.Add(proj);
				}

				this.projects = projects;

				// parse projects
				foreach (var project in projects)
				{
					if (!project.isParsed) await project.Parse();
				}
			}
		}
	}
}
