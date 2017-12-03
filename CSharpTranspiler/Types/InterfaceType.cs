using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace CSharpTranspiler.Types
{
	public class InterfaceType : LogicalType
	{
		public InterfaceType(InterfaceDeclarationSyntax declarationSyntax, SemanticModel semanticModel)
		: base(declarationSyntax, semanticModel)
		{
		}
	}
}
