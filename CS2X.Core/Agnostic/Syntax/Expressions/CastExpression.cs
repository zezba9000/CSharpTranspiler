using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace CS2X.Core.Agnostic.Syntax.Expressions
{
	public class CastExpression : Expression
	{
		public CastExpressionSyntax expression;
		public string typeName, typeFullName, typeFullNameFlat;
		public Expression castFromExpression;

		public CastExpression(CastExpressionSyntax expression, SemanticModel semanticModel)
		{
			this.expression = expression;

			var symbolType = (ITypeSymbol)semanticModel.GetSymbolInfo(expression.Type).Symbol;
			typeName = Tools.GetFullTypeName(symbolType);
			typeFullName = Tools.GetFullTypeName(symbolType);
			typeFullNameFlat = Tools.GetFullTypeNameFlat(symbolType);

			castFromExpression = CreateExpression(this, expression.Expression, semanticModel);
		}
	}
}
