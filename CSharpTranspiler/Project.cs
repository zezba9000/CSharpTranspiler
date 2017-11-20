using CSharpTranspiler.Types;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSharpTranspiler
{
	public class Project
	{
		public string filename;
		public Microsoft.CodeAnalysis.Project project;
		public List<ObjectBase> allObjects;
		public List<ClassObject> classObjects;
		public List<StructObject> structObjects;
		public List<InterfaceObject> interfaceObjects;
		public List<EnumObject> enumObjects;

		public Project(string filename)
		{
			this.filename = filename;
		}

		public async Task Parse(Microsoft.CodeAnalysis.Project project)
		{
			this.project = project;
			allObjects = new List<ObjectBase>();
			classObjects = new List<ClassObject>();
			structObjects = new List<StructObject>();
			interfaceObjects = new List<InterfaceObject>();
			enumObjects = new List<EnumObject>();

			var compilation = await project.GetCompilationAsync();
			foreach (var doc in project.Documents)
			{
				var syntaxTree = await doc.GetSyntaxTreeAsync() as CSharpSyntaxTree;
				if (syntaxTree == null) throw new Exception("Not a C# file: " + doc.FilePath);
				var semanticModel = await doc.GetSemanticModelAsync();
				AddObjects(syntaxTree.GetRoot().ChildNodes(), syntaxTree, semanticModel);
			}
		}

		private bool DoesObjectExist(string fullName, out ObjectBase objBase)
		{
			foreach (var obj in allObjects)
			{
				if (obj.fullName == fullName)
				{
					objBase = obj;
					return true;
				}
			}

			objBase = null;
			return false;
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
					string name = classNode.Identifier.ValueText;
					var fullName = semanticModel.GetDeclaredSymbol(node).ToString();
					bool addNew = false;
					if (Tools.HasKind(classNode.Modifiers, SyntaxKind.PartialKeyword))
					{
						ObjectBase obj;
						if (DoesObjectExist(fullName, out obj))
						{
							obj.MergePartial(classNode, semanticModel);
						}
						else
						{
							addNew = true;
						}
					}
					else
					{
						addNew = true;
					}
					
					if (addNew)
					{
						var classType = new ClassObject(name, fullName, classNode);
						classObjects.Add(classType);
						allObjects.Add(classType);
					}

					AddObjects(classNode.ChildNodes(), syntaxTree, semanticModel);
				}
			}
		}
	}
}
