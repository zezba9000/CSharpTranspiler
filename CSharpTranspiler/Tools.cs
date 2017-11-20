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
			foreach (var token in syntaxTokenList)
			{
				if (token.IsKind(kind)) return true;
			}

			return false;
		}
	}
}
