using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpTranspiler
{
	public static class Tools
	{
		public static bool HasKind(SyntaxTokenList syntaxTokenList, SyntaxKind kind)
		{
			return syntaxTokenList.Any(x => x.IsKind(kind));
		}

		public static string GetTypeName(ITypeSymbol symbol)
		{
			if (symbol.SpecialType != SpecialType.None) return symbol.SpecialType.ToString().Split('_').Last();
			return symbol.ToString().Split('.').Last();
		}
		
		public static string GetFullTypeName(ITypeSymbol symbol)
		{
			if (symbol.SpecialType != SpecialType.None) return symbol.SpecialType.ToString().Replace('_', '.');
			return symbol.ToString();
		}

		public static string GetFullTypeNameFlat(ITypeSymbol symbol)
		{
			if (symbol.SpecialType != SpecialType.None) return symbol.SpecialType.ToString();
			return symbol.ToString().Replace('.', '_');
		}
	}
}
