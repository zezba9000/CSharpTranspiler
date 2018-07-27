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
	public class AssignmentExpression : Expression
	{
		public AssignmentExpressionSyntax expression;

		public Expression left, right;
		public SyntaxToken op;

		public AssignmentExpression(AssignmentExpressionSyntax expression, SemanticModel semanticModel)
		{
			this.expression = expression;

			left = CreateExpression(this, expression.Left, semanticModel);
			right = CreateExpression(this, expression.Right, semanticModel);
			op = expression.OperatorToken;
		}
	}
}
