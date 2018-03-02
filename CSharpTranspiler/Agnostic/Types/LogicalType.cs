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
		public List<PropertyDeclaration> properties;
		public List<MethodDeclaration> methods;
		
		public LogicalType(Project project, TypeDeclarationSyntax declaration, SemanticModel semanticModel)
		: base(project, declaration, semanticModel)
		{
			variables = new List<VariableDeclaration>();
			properties = new List<PropertyDeclaration>();
			methods = new List<MethodDeclaration>();
			AddMembers(declaration, semanticModel);
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
					foreach (var variable in field.Declaration.Variables)
					{
						variables.Add(new VariableDeclaration(this, variable, field, semanticModel));
					}
				}
				else if (type == typeof(PropertyDeclarationSyntax))
				{
					var property = (PropertyDeclarationSyntax)member;
					properties.Add(new PropertyDeclaration(this, property, semanticModel));
				}
				else if (type == typeof(MethodDeclarationSyntax))
				{
					var method = (MethodDeclarationSyntax)member;
					methods.Add(new MethodDeclaration(this, method, semanticModel));
				}
			}
		}
	}
}
