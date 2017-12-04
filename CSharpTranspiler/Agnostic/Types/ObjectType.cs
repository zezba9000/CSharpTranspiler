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
using CSharpTranspiler.Agnostic.Syntax;

namespace CSharpTranspiler.Agnostic.Types
{
	public class BaseObject
	{
		public BaseTypeSyntax baseType;
		public TypeInfo typeInfo;
		public string fullName;
		public bool isInterface;
	}

	public abstract class ObjectType : Member
	{
		public List<BaseTypeDeclarationSyntax> declarationSyntaxes;

		public string name, fullName, fullNameFlat;
		public List<BaseObject> baseObjects;
		
		public ObjectType(BaseTypeDeclarationSyntax declaration, SemanticModel semanticModel)
		: base(declaration.Modifiers, declaration.AttributeLists)
		{
			// get name
			var symbol = semanticModel.GetDeclaredSymbol(declaration);
			name = Tools.GetTypeName(symbol);
			fullName = Tools.GetFullTypeName(symbol);
			fullNameFlat = Tools.GetFullTypeNameFlat(symbol);

			// add current declaration
			declarationSyntaxes = new List<BaseTypeDeclarationSyntax>();
			declarationSyntaxes.Add(declaration);

			// parse base types (TODO: link to actual ObjectTypes even if contained in reference project)
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
					var obj = new BaseObject()
					{
						baseType = baseType,
						typeInfo = typeInfo,
						fullName = Tools.GetFullTypeName(typeSymbol),
						isInterface = typeSymbol.TypeKind == TypeKind.Interface
					};
					
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
	}
}
