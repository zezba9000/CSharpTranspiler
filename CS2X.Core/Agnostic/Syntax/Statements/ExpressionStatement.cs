using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using CS2X.Core.Agnostic.Syntax.Expressions;
using Microsoft.CodeAnalysis;

namespace CS2X.Core.Agnostic.Syntax.Statements
{
	public class ExpressionStatement : Statement
	{
		public Expression expression;

		public ExpressionStatement(ExpressionStatementSyntax statement, SemanticModel semanticModel)
		{
			expression = Expression.CreateExpression(null, statement.Expression, semanticModel);
		}
	}
}
