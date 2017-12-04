using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTranspiler.Agnostic.Types.MemberDeclarations
{
	public class PropertyDeclaration : VariableDeclarationBase
	{
		public PropertyDeclarationSyntax declaration;

		public string name;
		//public object initializedValue;// Not supported in C# 3

		public PropertyDeclaration(PropertyDeclarationSyntax declaration, SemanticModel semanticModel)
		: base((semanticModel.GetDeclaredSymbol(declaration)).Type, declaration.Modifiers, declaration.AttributeLists)
		{
			this.declaration = declaration;
			name = declaration.Identifier.ValueText;
			
			// get initialized value (Not supported in C# 3)
			/*if (declaration.Initializer != null && declaration.Initializer.Value != null)
			{
				var value = semanticModel.GetConstantValue(declaration.Initializer.Value);
				initializedValue = value.HasValue ? value.Value : null;
			}*/

			// parse method set/get
			foreach (var accessor in declaration.AccessorList.Accessors)
			{
				if (accessor.Keyword.IsKind(SyntaxKind.GetKeyword))
				{
					// TODO: parse body
				}
				else if (accessor.Keyword.IsKind(SyntaxKind.SetKeyword))
				{
					// TODO: parse body
				}
				else
				{
					throw new Exception("Unsuported property accessor: " + accessor.Keyword);
				}
			}
		}
	}
}
