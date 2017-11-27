using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace CSharpTranspiler.Types
{
	public abstract class LogicalObject : ObjectBase
	{
		public List<FieldDeclarationSyntax> fields;
		public List<PropertyDeclarationSyntax> properties;
		public List<MethodDeclarationSyntax> methods;
		
		public LogicalObject(string name, string fullName, TypeDeclarationSyntax declarationSyntax, SemanticModel semanticModel)
		: base(name, fullName, declarationSyntax, semanticModel)
		{
			fields = new List<FieldDeclarationSyntax>();
			properties = new List<PropertyDeclarationSyntax>();
			methods = new List<MethodDeclarationSyntax>();
			AddMembers(declarationSyntax);
		}

		public override void MergePartial(BaseTypeDeclarationSyntax declarationSyntax, SemanticModel semanticModel)
		{
			base.MergePartial(declarationSyntax, semanticModel);
			AddMembers((TypeDeclarationSyntax)declarationSyntax);
		}

		private void AddMembers(TypeDeclarationSyntax declarationSyntax)
		{
			foreach (var member in declarationSyntax.Members)
			{
				var type = member.GetType();
				if (type == typeof(FieldDeclarationSyntax)) fields.Add((FieldDeclarationSyntax)member);
				else if (type == typeof(PropertyDeclarationSyntax)) properties.Add((PropertyDeclarationSyntax)member);
				else if (type == typeof(MethodDeclarationSyntax)) methods.Add((MethodDeclarationSyntax)member);
			}
		}
	}
}
