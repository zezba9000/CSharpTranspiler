using CSharpTranspiler.Agnostic.Types;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSharpTranspiler.Agnostic
{
	public enum ProjectTypes
	{
		Exe,
		Dll
	}

	public class Project
	{
		public Solution solution;
		public Microsoft.CodeAnalysis.Project project;

		public ProjectTypes type;
		public bool isReleaseBuild;
		public string filename, assemblyName;
		public List<string> references;

		public List<ObjectType> allObjects;
		public List<ClassType> classObjects;
		public List<StructType> structObjects;
		public List<InterfaceType> interfaceObjects;
		public List<EnumType> enumObjects;

		public Project(Solution solution, Microsoft.CodeAnalysis.Project project)
		{
			this.solution = solution;
			this.project = project;
			this.filename = project.FilePath;
		}

		public async Task Parse()
		{
			// init main objects
			assemblyName = project.AssemblyName;
			allObjects = new List<ObjectType>();
			classObjects = new List<ClassType>();
			structObjects = new List<StructType>();
			interfaceObjects = new List<InterfaceType>();
			enumObjects = new List<EnumType>();

			// validate compiler options
			var parseOptions = (CSharpParseOptions)project.ParseOptions;
			if (parseOptions.LanguageVersion != LanguageVersion.CSharp3) throw new Exception("Project lang version must be 3.0: " + project.FilePath);

			var compilationOptions = project.CompilationOptions;
			if (compilationOptions.Platform != Platform.AnyCpu) throw new Exception("Project platform must be AnyCpu: " + project.FilePath);
			
			// get project type
			var kind = compilationOptions.OutputKind;
			if (kind == OutputKind.DynamicallyLinkedLibrary) type = ProjectTypes.Dll;
			else if (kind == OutputKind.ConsoleApplication || kind == OutputKind.WindowsApplication) type = ProjectTypes.Exe;
			else throw new Exception("Unsuported project kind: " + project.FilePath);

			// check optimization level
			isReleaseBuild = compilationOptions.OptimizationLevel == OptimizationLevel.Release;

			// gather references
			references = new List<string>();
			var sln = project.Solution;
			foreach (var reference in project.AllProjectReferences)
			{
				var p = sln.GetProject(reference.ProjectId);
				references.Add(p.AssemblyName);
			}

			// parse syntax tree
			var compilation = await project.GetCompilationAsync();
			foreach (var doc in project.Documents)
			{
				var syntaxTree = await doc.GetSyntaxTreeAsync() as CSharpSyntaxTree;
				if (syntaxTree == null) throw new Exception("Not a C# file: " + doc.FilePath);
				var semanticModel = await doc.GetSemanticModelAsync();
				AddObjects(syntaxTree.GetRoot().ChildNodes(), syntaxTree, semanticModel);
			}

			// add all objects to all list
			allObjects.AddRange(enumObjects);
			allObjects.AddRange(interfaceObjects);
			allObjects.AddRange(structObjects);
			allObjects.AddRange(classObjects);

			// resolve objects
			foreach (var obj in allObjects)
			{
				obj.Resolve();
			}
		}

		private bool DoesPartialObjectExist(string fullName, out ObjectType objBase)
		{
			objBase = classObjects.FirstOrDefault(x => x.fullName == fullName);
			if (objBase != null) return true;

			objBase = interfaceObjects.FirstOrDefault(x => x.fullName == fullName);
			if (objBase != null) return true;

			objBase = structObjects.FirstOrDefault(x => x.fullName == fullName);
			if (objBase != null) return true;

			objBase = null;
			return false;
		}

		private delegate void AddObjectCallbackMethod();
		private void AddObject(TypeDeclarationSyntax node, CSharpSyntaxTree syntaxTree, SemanticModel semanticModel, SyntaxTokenList modifiers, AddObjectCallbackMethod callback)
		{
			var symbol = semanticModel.GetDeclaredSymbol(node);
			string fullname = Tools.GetFullTypeName(symbol);

			bool addNew = false;
			if (Tools.HasKind(modifiers, SyntaxKind.PartialKeyword))
			{
				ObjectType obj;
				if (DoesPartialObjectExist(fullname, out obj)) obj.MergePartial(node, semanticModel);
				else addNew = true;
			}
			else
			{
				addNew = true;
			}
					
			if (addNew) callback?.Invoke();
			AddObjects(node.ChildNodes(), syntaxTree, semanticModel);
		}

		private void AddObjects(IEnumerable<SyntaxNode> syntaxNodes, CSharpSyntaxTree syntaxTree, SemanticModel semanticModel)
		{
			foreach (var node in syntaxNodes)
			{
				var type = node.GetType();
				if (type == typeof(NamespaceDeclarationSyntax))
				{
					var namespaceNode = (NamespaceDeclarationSyntax)node;
					AddObjects(namespaceNode.ChildNodes(), syntaxTree, semanticModel);
				}
				else if (type == typeof(ClassDeclarationSyntax))
				{
					var classNode = (ClassDeclarationSyntax)node;
					void AddObjectCallback()
					{
						string name = classNode.Identifier.ValueText;
						classObjects.Add(new ClassType(this, classNode, semanticModel));
					}

					AddObject(classNode, syntaxTree, semanticModel, classNode.Modifiers, AddObjectCallback);
				}
				else if (type == typeof(StructDeclarationSyntax))
				{
					var structNode = (StructDeclarationSyntax)node;
					void AddObjectCallback()
					{
						string name = structNode.Identifier.ValueText;
						structObjects.Add(new StructType(this, structNode, semanticModel));
					}

					AddObject(structNode, syntaxTree, semanticModel, structNode.Modifiers, AddObjectCallback);
				}
				else if (type == typeof(InterfaceDeclarationSyntax))
				{
					var interfaceNode = (InterfaceDeclarationSyntax)node;
					void AddObjectCallback()
					{
						string name = interfaceNode.Identifier.ValueText;
						interfaceObjects.Add(new InterfaceType(this, interfaceNode, semanticModel));
					}

					AddObject(interfaceNode, syntaxTree, semanticModel, interfaceNode.Modifiers, AddObjectCallback);
				}
				else if (type == typeof(EnumDeclarationSyntax))
				{
					var enumNode = (EnumDeclarationSyntax)node;
					string name = enumNode.Identifier.ValueText;
					string fullName = semanticModel.GetDeclaredSymbol(node).ToString();
					enumObjects.Add(new EnumType(this, enumNode, semanticModel));
				}
			}
		}
	}
}
