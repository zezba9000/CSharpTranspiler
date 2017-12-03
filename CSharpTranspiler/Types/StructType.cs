using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTranspiler.Types
{
	public class StructType : LogicalType
	{
		public StructType(StructDeclarationSyntax declarationSyntax, SemanticModel semanticModel)
		: base(declarationSyntax, semanticModel)
		{

		}
	}
}
