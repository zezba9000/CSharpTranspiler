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
	public enum SolutionSources
	{
		Sln,
		CSProj,
		CSProjFiles
	}

	public class Solution
	{
		public Microsoft.CodeAnalysis.Solution solution {get; private set;}
		public readonly string filename;
		public readonly string[] projFileNames;
		private readonly SolutionSources source;

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
			if (ext == ".csproj") source = SolutionSources.CSProj;
			else if (ext == ".sln") source = SolutionSources.Sln;
			else throw new Exception("Invalid file type: " + filename);
		}

		public Solution(string[] projFileNames)
		{
			this.projFileNames = projFileNames;
			source = SolutionSources.CSProjFiles;
			foreach (string filename in projFileNames)
			{
				if (!File.Exists(filename)) throw new Exception("File does not exists: " + filename);
				this.filename = filename;
				if (Path.GetExtension(filename) != ".csproj") throw new Exception("Invalid file type: " + filename);
			}
		}

		public async Task Parse()
		{
			using (var workspace = MSBuildWorkspace.Create())
			{
				// load projects
				var projects = new List<Project>();
				if (source == SolutionSources.Sln)
				{
					solution = await workspace.OpenSolutionAsync(filename);
					foreach (var csProj in solution.Projects)
					{
						var proj = new Project(this, csProj);
						projects.Add(proj);
					}
				}
				else if (source == SolutionSources.CSProj)
				{
					var csProj = await workspace.OpenProjectAsync(filename);
					solution = csProj.Solution;
					var proj = new Project(this, csProj);
					projects.Add(proj);
				}
				else if (source == SolutionSources.CSProjFiles)
				{
					foreach (string projFileName in projFileNames)
					{
						var csProj = await workspace.OpenProjectAsync(projFileName);
						solution = csProj.Solution;
						var proj = new Project(this, csProj);
						projects.Add(proj);
					}
				}
				else
				{
					throw new Exception("Unsuported source type: " + source);
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
