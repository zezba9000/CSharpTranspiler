using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpTranspiler.Types
{
	public class EnumObject : ObjectBase
	{
		public EnumObject(string name, string fullName, TypeDeclarationSyntax typeDeclarationSyntax)
		: base(name, fullName, typeDeclarationSyntax)
		{
		}
	}
}
