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
		public Expression left, right;
		public SyntaxToken op;

		public AssignmentExpression(AssignmentExpressionSyntax expression, SemanticModel semanticModel)
		{
			left = CreateExpression(expression.Left, semanticModel);
			right = CreateExpression(expression.Right, semanticModel);
			op = expression.OperatorToken;
		}
	}
}
