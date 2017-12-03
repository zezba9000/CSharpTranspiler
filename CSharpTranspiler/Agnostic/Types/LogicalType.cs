using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using CSharpTranspiler.Agnostic.Types.MemberDeclarations;

namespace CSharpTranspiler.Agnostic.Types
{
	public abstract class LogicalType : ObjectType
	{
		public List<VariableDeclaration> variables;
		public List<PropertyDeclarationSyntax> properties;
		public List<MethodDeclarationSyntax> methods;
		
		public LogicalType(TypeDeclarationSyntax declarationSyntax, SemanticModel semanticModel)
		: base(declarationSyntax, semanticModel)
		{
			variables = new List<VariableDeclaration>();
			properties = new List<PropertyDeclarationSyntax>();
			methods = new List<MethodDeclarationSyntax>();
			AddMembers(declarationSyntax, semanticModel);
		}

		public override void MergePartial(BaseTypeDeclarationSyntax declarationSyntax, SemanticModel semanticModel)
		{
			base.MergePartial(declarationSyntax, semanticModel);
			AddMembers((TypeDeclarationSyntax)declarationSyntax, semanticModel);
		}

		private void AddMembers(TypeDeclarationSyntax declarationSyntax, SemanticModel semanticModel)
		{
			foreach (var member in declarationSyntax.Members)
			{
				var type = member.GetType();
				if (type == typeof(FieldDeclarationSyntax))
				{
					var field = (FieldDeclarationSyntax)member;
					var fieldType = field.Declaration.Type;
					foreach (var variable in field.Declaration.Variables)
					{
						variables.Add(new VariableDeclaration(fieldType, variable, semanticModel));
					}
				}
				else if (type == typeof(PropertyDeclarationSyntax))
				{
					properties.Add((PropertyDeclarationSyntax)member);
				}
				else if (type == typeof(MethodDeclarationSyntax))
				{
					methods.Add((MethodDeclarationSyntax)member);
				}
			}
		}
	}
}
