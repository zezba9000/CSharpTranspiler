using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTranspiler.Types.MemberDeclarations
{
	public class VariableDeclaration
	{
		public string name, typeName, typeFullName, typeFullNameFlat;
		public object constValue;
		public TypeSyntax type;
		public VariableDeclaratorSyntax declaration;

		public VariableDeclaration(TypeSyntax type, VariableDeclaratorSyntax declaration, SemanticModel semanticModel)
		{
			this.type = type;
			this.declaration = declaration;
			name = declaration.Identifier.ValueText;

			// get type name
			var symbol = (IFieldSymbol)semanticModel.GetDeclaredSymbol(declaration);
			var symbolType = symbol.Type;
			typeName = Tools.GetFullTypeName(symbolType);
			typeFullName = Tools.GetFullTypeName(symbolType);
			typeFullNameFlat = Tools.GetFullTypeNameFlat(symbolType);

			// get const value
			if (declaration.Initializer != null && declaration.Initializer.Value != null)
			{
				var value = semanticModel.GetConstantValue(declaration.Initializer.Value);
				constValue = value.HasValue ? value.Value : null;
			}
		}
	}
}
