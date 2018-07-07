using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;

namespace CSharpTranspiler.Agnostic.Syntax.Expressions
{
	public class LiteralExpression : Expression
	{
		public LiteralExpressionSyntax expression;
		public object value;

		public LiteralExpression(LiteralExpressionSyntax expression, SemanticModel semanticModel)
		{
			this.expression = expression;

			var value = semanticModel.GetConstantValue(expression);
			this.value = value.HasValue ? value.Value : null;
		}
	}
}
