using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTranspiler.Agnostic.Types.MemberDeclarations
{
	public abstract class VariableDeclarationBase : Member
	{
		public bool isArray, isGeneric, isValueType;
		public string typeName, typeFullName, typeFullNameFlat;

		public VariableDeclarationBase(ITypeSymbol symbolType, SyntaxTokenList? modifiers, SyntaxList<AttributeListSyntax>? attributeList)
		: base(modifiers, attributeList)
		{
			isArray = symbolType.Kind == SymbolKind.ArrayType;
			isValueType = symbolType.IsValueType;
			typeName = Tools.GetFullTypeName(symbolType);
			typeFullName = Tools.GetFullTypeName(symbolType);
			typeFullNameFlat = Tools.GetFullTypeNameFlat(symbolType);
		}
	}

	public class VariableDeclaration : VariableDeclarationBase
	{
		public VariableDeclaratorSyntax declaration;
		public FieldDeclarationSyntax fieldDeclaration;

		public string name;
		public object constValue;

		public VariableDeclaration(VariableDeclaratorSyntax declaration, FieldDeclarationSyntax fieldDeclaration, SemanticModel semanticModel)
		: base(((IFieldSymbol)semanticModel.GetDeclaredSymbol(declaration)).Type, fieldDeclaration.Modifiers, fieldDeclaration.AttributeLists)
		{
			this.declaration = declaration;
			this.fieldDeclaration = fieldDeclaration;
			name = declaration.Identifier.ValueText;
			
			// get const value
			if (declaration.Initializer != null && declaration.Initializer.Value != null)
			{
				var value = semanticModel.GetConstantValue(declaration.Initializer.Value);
				constValue = value.HasValue ? value.Value : null;
			}
		}
	}
}
