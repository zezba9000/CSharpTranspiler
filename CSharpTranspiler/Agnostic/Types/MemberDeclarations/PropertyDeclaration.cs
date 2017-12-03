using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTranspiler.Agnostic.Types.MemberDeclarations
{
	public class PropertyDeclaration : Member
	{
		public PropertyDeclarationSyntax declaration;

		public PropertyDeclaration(PropertyDeclarationSyntax declaration, SemanticModel semanticModel)
		: base(declaration.Modifiers, declaration.AttributeLists)
		{
			
		}
	}
}
