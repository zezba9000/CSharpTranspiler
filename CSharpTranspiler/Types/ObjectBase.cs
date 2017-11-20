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

namespace CSharpTranspiler.Types
{
	public abstract class ObjectBase
	{
		public List<TypeDeclarationSyntax> typeDeclarationSyntaxes;
		public string name, fullName, fullNameFlat;
		public List<SyntaxToken> modifiers;
		public List<BaseTypeSyntax> baseTypes;

		public ObjectBase(string name, string fullName, TypeDeclarationSyntax typeDeclarationSyntax)
		{
			this.name = name;
			this.fullName = fullName;
			fullNameFlat = fullName.Replace('.', '_');
			typeDeclarationSyntaxes = new List<TypeDeclarationSyntax>();
			typeDeclarationSyntaxes.Add(typeDeclarationSyntax);

			modifiers = new List<SyntaxToken>(typeDeclarationSyntax.Modifiers);
			if (typeDeclarationSyntax.BaseList != null) baseTypes = new List<BaseTypeSyntax>(typeDeclarationSyntax.BaseList.Types);
			else baseTypes = new List<BaseTypeSyntax>();
		}

		public void MergePartial(TypeDeclarationSyntax typeDeclarationSyntax, SemanticModel semanticModel)
		{
			typeDeclarationSyntaxes.Add(typeDeclarationSyntax);
			
			// merge modifiers
			foreach (var modifier in typeDeclarationSyntax.Modifiers)
			{
				bool found = false;
				foreach (var currentModifier in modifiers)
				{
					if (modifier.Kind() == currentModifier.Kind())
					{
						found = true;
						break;
					}
				}
				
				if (!found) modifiers.Add(modifier);
			}
			
			// merge base types
			foreach (var baseType in typeDeclarationSyntax.BaseList.Types)
			{
				var namedType = semanticModel.GetTypeInfo(baseType.Type).Type;
				var fullName = namedType.ToDisplayString();
				bool found = false;
				foreach (var currentBaseType in baseTypes)
				{
					namedType = semanticModel.GetTypeInfo(currentBaseType.Type).Type;
					var fullName2 = namedType.ToDisplayString();
					if (fullName == fullName2)
					{
						found = true;
						break;
					}
				}

				if (!found) baseTypes.Add(baseType);
			}
		}
	}
}
