using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace CSharpTranspiler.Types
{
	public class EnumType : ObjectType
	{
		public EnumType(string name, string fullName, EnumDeclarationSyntax declarationSyntax, SemanticModel semanticModel)
		: base(name, fullName, declarationSyntax, semanticModel)
		{
		}
	}
}
