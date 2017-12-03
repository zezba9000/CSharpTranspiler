using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace CSharpTranspiler.Agnostic.Types
{
	public class InterfaceType : LogicalType
	{
		public InterfaceType(InterfaceDeclarationSyntax declaration, SemanticModel semanticModel)
		: base(declaration, semanticModel)
		{
		}
	}
}
