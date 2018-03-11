using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using CSharpTranspiler.Agnostic.Syntax.Expressions;

namespace CSharpTranspiler.Agnostic.Syntax.Statements
{
	public class ExpressionStatement : Statement
	{
		public Expression expression;

		public ExpressionStatement(ExpressionStatementSyntax statement)
		{
			var e = statement.Expression;
			var kind = e.Kind();
			switch (kind)
			{
				case SyntaxKind.SimpleAssignmentExpression: expression = new AssignmentExpression((AssignmentExpressionSyntax)e); break;
				default: throw new NotImplementedException("Unsuported ExpressionStatement SyntaxKind: " + kind);
			}
		}
	}
}
