using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2X.Core.Agnostic.Types
{
	public class StructType : LogicalType
	{
		public StructType(Project project, StructDeclarationSyntax declaration, SemanticModel semanticModel)
		: base(project, declaration, semanticModel)
		{

		}
	}
}
