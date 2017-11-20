using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTranspiler.Types
{
	public class StructObject : ObjectBase
	{
		public StructObject(string name, string fullName, ClassDeclarationSyntax classDeclarationSyntax)
		: base(name, fullName, classDeclarationSyntax)
		{

		}
	}
}
