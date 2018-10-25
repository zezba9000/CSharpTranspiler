using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.CodeAnalysis.CSharp.Extensions;
using CS2X.Core.Agnostic.Syntax;

namespace CS2X.Core.Agnostic.Types
{
	public class BaseObject
	{
		public BaseTypeSyntax baseType;

		public ObjectType childObjectType;
		public TypeInfo typeInfo;
		public string name, fullName, fullNameFlat;

		public bool isPrimitive;
		public ObjectType objectType;

		public BaseObject(ObjectType childObjectType, BaseTypeSyntax baseType, SemanticModel semanticModel)
		{
			this.childObjectType = childObjectType;
			this.baseType = baseType;

			typeInfo = semanticModel.GetTypeInfo(baseType.Type);
			var symbol = typeInfo.Type;
			name = Tools.GetTypeName(symbol);
			fullName = Tools.GetFullTypeName(symbol);
			fullNameFlat = Tools.GetFullTypeNameFlat(symbol);

			isPrimitive = symbol.SpecialType != SpecialType.None;
		}

		public void Resolve()
		{
			if (isPrimitive) return;

			foreach (var project in childObjectType.project.solution.projects)
			foreach (var obj in project.allObjects)
			{
				if (obj.fullName == fullName)
				{
					objectType = obj;
					return;
				}
			}

			throw new Exception("Failed to resolve base type: " + fullName);
		}
	}

	public abstract class ObjectType : Member
	{
		public List<BaseTypeDeclarationSyntax> declarationSyntaxes;
		
		public Project project;
		public string name, fullName, fullNameFlat;
		public List<BaseObject> baseObjects;
		
		public ObjectType(Project project, BaseTypeDeclarationSyntax declaration, SemanticModel semanticModel)
		: base(declaration.Modifiers, declaration.AttributeLists)
		{
			this.project = project;

			// get name
			var symbol = semanticModel.GetDeclaredSymbol(declaration);
			name = Tools.GetTypeName(symbol);
			fullName = Tools.GetFullTypeName(symbol);
			fullNameFlat = Tools.GetFullTypeNameFlat(symbol);

			// add current declaration
			declarationSyntaxes = new List<BaseTypeDeclarationSyntax>();
			declarationSyntaxes.Add(declaration);

			// add base types
			baseObjects = new List<BaseObject>();
			AddBaseObjects(declaration, semanticModel);
		}

		private void AddBaseObjects(BaseTypeDeclarationSyntax declarationSyntax, SemanticModel semanticModel)
		{
			if (declarationSyntax.BaseList == null) return;
			foreach (var baseType in declarationSyntax.BaseList.Types)
			{
				var typeInfo = semanticModel.GetTypeInfo(baseType.Type);
				var typeSymbol = typeInfo.Type;
				if (!baseObjects.Exists(x => x.typeInfo.Type == typeSymbol))
				{
					var obj = new BaseObject(this, baseType, semanticModel);
					baseObjects.Add(obj);
				}
			}
		}

		public virtual void MergePartial(BaseTypeDeclarationSyntax declarationSyntax, SemanticModel semanticModel)
		{
			if (declarationSyntaxes.Contains(declarationSyntax)) throw new Exception("Error: Partial already merged!");
			declarationSyntaxes.Add(declarationSyntax);
			
			AddModifiers(declarationSyntax.Modifiers);
			AddBaseObjects(declarationSyntax, semanticModel);
		}

		public void Resolve()
		{
			// parse base types
			foreach (var baseObject in baseObjects)
			{
				baseObject.Resolve();
			}
		}
	}
}
