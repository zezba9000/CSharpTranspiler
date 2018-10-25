using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2X.Core.Agnostic.Syntax.Expressions
{
	public class AccessExpression : Expression
	{
		public MemberAccessExpressionSyntax expression;

		public string name, fullName, fullNameFlat;
		public bool hasThisPrefix;

		public AccessExpression(MemberAccessExpressionSyntax expression, SemanticModel semanticModel)
		{
			this.expression = expression;

			name = expression.Name.Identifier.ValueText;
			if (expression.Expression is ThisExpressionSyntax)
			{
				hasThisPrefix = true;
				fullName = name;
				fullNameFlat = name;
			}
			else
			{
				TypeInfo info;
				if (expression.Expression is MemberAccessExpressionSyntax) info = semanticModel.GetTypeInfo(((MemberAccessExpressionSyntax)expression.Expression).Name);
				else info = semanticModel.GetTypeInfo(expression.Expression);
				fullName = Tools.GetFullTypeName(info.Type) + '.' + name;
				fullNameFlat = Tools.GetFullTypeNameFlat(info.Type) + '_' + name;
			}
		}
	}
}
