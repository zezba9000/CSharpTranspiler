using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpTranspiler.Agnostic
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
			else if (symbol.Kind == SymbolKind.ArrayType) return GetTypeName(((IArrayTypeSymbol)symbol).ElementType);
			return symbol.ToString().Split('.').Last();
		}
		
		public static string GetFullTypeName(ITypeSymbol symbol)
		{
			if (symbol.SpecialType != SpecialType.None) return symbol.SpecialType.ToString().Replace('_', '.');
			else if (symbol.Kind == SymbolKind.ArrayType) return GetFullTypeName(((IArrayTypeSymbol)symbol).ElementType);
			return symbol.ToString();
		}

		public static string GetFullTypeNameFlat(ITypeSymbol symbol)
		{
			if (symbol.SpecialType != SpecialType.None) return symbol.SpecialType.ToString();
			else if (symbol.Kind == SymbolKind.ArrayType) return GetFullTypeNameFlat(((IArrayTypeSymbol)symbol).ElementType);
			return symbol.ToString().Replace('.', '_');
		}
	}
}
