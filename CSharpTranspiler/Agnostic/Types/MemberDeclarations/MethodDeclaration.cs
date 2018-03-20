using CSharpTranspiler.Agnostic.Syntax;
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
	public class MethodParameter : VariableDeclarationBase
	{
		public ParameterSyntax syntax;

		public MethodDeclaration method;
		public string name;
		//public object defaultValue;// Not supported in C# 3

		public MethodParameter(MethodDeclaration method, ParameterSyntax syntax, SemanticModel semanticModel)
		: base(semanticModel.GetDeclaredSymbol(syntax).Type, syntax.Modifiers, syntax.AttributeLists)
		{
			this.method = method;
			this.syntax = syntax;
			name = syntax.Identifier.ValueText;
			
			// get default value (Not supported in C# 3)
			/*if (declaration.Default != null && declaration.Default.Value != null)
			{
				var value = semanticModel.GetConstantValue(declaration.Default.Value);
				defaultValue = value.HasValue ? value.Value : null;
			}*/
		}
	}

	public class MethodReturn : VariableDeclarationBase
	{
		public TypeSyntax syntax;
		
		public MethodReturn(TypeSyntax syntax, SemanticModel semanticModel)
		: base((ITypeSymbol)semanticModel.GetSymbolInfo(syntax).Symbol, null, null)// TODO: pass in return attribute from method declaration
		{
			this.syntax = syntax;
		}
	}

	public class MethodDeclaration : Member
	{
		public MethodDeclarationSyntax declaration;
		
		public ObjectType objectType;
		public string name, fullName, fullNameFlat;
		public List<MethodParameter> parameters;
		public MethodReturn returnType;
		public LogicalBody body;

		public MethodDeclaration(ObjectType objectType, MethodDeclarationSyntax declaration, SemanticModel semanticModel)
		: base(declaration.Modifiers, declaration.AttributeLists)
		{
			this.objectType = objectType;
			this.declaration = declaration;

			// get name
			name = declaration.Identifier.ValueText;
			var symbol = semanticModel.GetDeclaredSymbol((BaseTypeDeclarationSyntax)declaration.Parent);
			fullName = Tools.GetFullTypeName(symbol) + '.' + name;
			fullNameFlat = Tools.GetFullTypeNameFlat(symbol) + '_' + name;
			
			// parse method parameters
			parameters = new List<MethodParameter>();
			foreach (var parameter in declaration.ParameterList.Parameters)
			{
				parameters.Add(new MethodParameter(this, parameter, semanticModel));
			}

			// parse return type
			returnType = new MethodReturn(declaration.ReturnType, semanticModel);

			// parse method body
			body = new LogicalBody(this, declaration.Body, semanticModel);
		}
	}
}
