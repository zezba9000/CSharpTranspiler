using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTranspiler.Types
{
	public class ClassObject : ObjectBase
	{
		public ClassObject(string name, string fullName, ClassDeclarationSyntax declarationSyntax)
		: base(name, fullName, declarationSyntax)
		{
			
		}
	}
}
