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
	public class CastExpression : Expression
	{
		public CastExpressionSyntax experesion;
		public string typeName, typeFullName, typeFullNameFlat;

		public CastExpression(CastExpressionSyntax experesion, SemanticModel semanticModel)
		{
			this.experesion = experesion;

			var symbolType = (ITypeSymbol)semanticModel.GetSymbolInfo(experesion.Type).Symbol;
			typeName = Tools.GetFullTypeName(symbolType);
			typeFullName = Tools.GetFullTypeName(symbolType);
			typeFullNameFlat = Tools.GetFullTypeNameFlat(symbolType);
		}
	}
}
