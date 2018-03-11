using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTranspiler.Agnostic.Syntax.Expressions
{
	public class AssignmentExpression : Expression
	{
		

		public AssignmentExpression(AssignmentExpressionSyntax expression)
		{
			var left = expression.Left;//IdentifierNameSyntax
			var right = expression.Right;//LiteralExpressionSyntax
			var op = expression.OperatorToken;

			

		}
	}
}
