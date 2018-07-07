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
	public abstract class Expression
	{
		public static Expression CreateExpression(ExpressionSyntax e, SemanticModel semanticModel)
		{
			Expression expression;
			var kind = e.Kind();
			switch (kind)
			{
				case SyntaxKind.SimpleAssignmentExpression: expression = new AssignmentExpression((AssignmentExpressionSyntax)e, semanticModel); break;
				case SyntaxKind.IdentifierName: expression = new IdentifierNameExpression((IdentifierNameSyntax)e); break;

				case SyntaxKind.NumericLiteralExpression:
				case SyntaxKind.NullLiteralExpression:
					expression = new LiteralExpression((LiteralExpressionSyntax)e, semanticModel);
					break;

				case SyntaxKind.CastExpression: expression = new CastExpression((CastExpressionSyntax)e, semanticModel); break;
				case SyntaxKind.SimpleMemberAccessExpression: expression = new AccessExpression((MemberAccessExpressionSyntax)e, semanticModel); break;
				default: throw new NotImplementedException("Unsuported ExpressionStatement SyntaxKind: " + kind);
			}

			return expression;
		}
	}
}
