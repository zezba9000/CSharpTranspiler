﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
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
		public string name;

		public IdentifierNameExpression(IdentifierNameSyntax expression)
		{
			this.expression = expression;
			name = expression.Identifier.ValueText;
		}
	}
}