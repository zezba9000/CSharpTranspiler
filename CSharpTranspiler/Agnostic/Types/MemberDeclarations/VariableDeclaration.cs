using CSharpTranspiler.Agnostic.Syntax.Expressions;
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
	public abstract class VariableDeclarationBase : Member
	{
		public bool isArray, isGeneric, isValueType;
		public string typeName, typeFullName, typeFullNameFlat;

		public VariableDeclarationBase(ITypeSymbol symbolType)
		: this(symbolType, null, null)
		{}

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
		public Expression initializeExpression;

		public ObjectType objectType;
		public string name, fullName, fullNameFlat;
		public bool isLocalScope;

		public VariableDeclaration(VariableDeclaratorSyntax declaration, SemanticModel semanticModel, ITypeSymbol type)
		: base(type, null, null)
		{
			isLocalScope = true;
			Init(null, declaration, null, semanticModel, type);
		}

		public VariableDeclaration(ObjectType objectType, VariableDeclaratorSyntax declaration, FieldDeclarationSyntax fieldDeclaration, SemanticModel semanticModel, ITypeSymbol type)
		: base(type, fieldDeclaration.Modifiers, fieldDeclaration.AttributeLists)
		{
			Init(objectType, declaration, fieldDeclaration, semanticModel, type);
		}

		private void Init(ObjectType objectType, VariableDeclaratorSyntax declaration, FieldDeclarationSyntax fieldDeclaration, SemanticModel semanticModel, ITypeSymbol type)
		{
			this.objectType = objectType;
			this.declaration = declaration;
			this.fieldDeclaration = fieldDeclaration;

			// get name
			name = declaration.Identifier.ValueText;
			if (!isLocalScope)
			{
				var symbol = semanticModel.GetDeclaredSymbol((BaseTypeDeclarationSyntax)declaration.Parent.Parent.Parent);
				fullName = Tools.GetFullTypeName(symbol) + '.' + name;
				fullNameFlat = Tools.GetFullTypeNameFlat(symbol) + '_' + name;
			}
			else
			{
				fullName = name;
				fullNameFlat = name;
			}

			// get initialized value
			if (declaration.Initializer != null && declaration.Initializer.Value != null)
			{
				initializeExpression = Expression.CreateExpression(declaration.Initializer.Value, semanticModel);
			}
		}
	}
}
