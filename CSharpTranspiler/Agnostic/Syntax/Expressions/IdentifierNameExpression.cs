using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace CSharpTranspiler.Agnostic.Syntax.Expressions
{
	public class IdentifierNameExpression : Expression
	{
		public IdentifierNameSyntax expression;
		public string name, fullName, fullNameFlat;
		public bool isProperty;

		public IdentifierNameExpression(IdentifierNameSyntax expression, SemanticModel semanticModel)
		{
			this.expression = expression;
			name = expression.Identifier.ValueText;

			var info = semanticModel.GetSymbolInfo(expression);
			var symbol = info.Symbol;
			if (symbol is IFieldSymbol)
			{
				var field = (IFieldSymbol)symbol;
				fullName = Tools.GetFullTypeName(field.ContainingType) + '.' + name;
				fullNameFlat = Tools.GetFullTypeNameFlat(field.ContainingType) + '_' + name;
			}
			else if (symbol is IPropertySymbol)
			{
				var property = (IPropertySymbol)symbol;
				fullName = Tools.GetFullTypeName(property.ContainingType) + '.' + name;
				fullNameFlat = Tools.GetFullTypeNameFlat(property.ContainingType) + '_' + name;
				isProperty = true;
			}
			else if (symbol is ILocalSymbol || symbol is IParameterSymbol)
			{
				fullName = name;
				fullNameFlat = name;
			}
			else
			{
				throw new NotSupportedException("Unsuported identifier symbol: " + symbol.ToString());
			}
		}
	}
}
