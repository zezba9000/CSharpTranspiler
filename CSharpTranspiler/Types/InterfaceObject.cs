using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpTranspiler.Types
{
	public class InterfaceObject : ObjectBase
	{
		public InterfaceObject(string name, string fullName, TypeDeclarationSyntax typeDeclarationSyntax) : base(name, fullName, typeDeclarationSyntax)
		{
		}
	}
}
