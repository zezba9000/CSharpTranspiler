using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using CS2X.Core.Agnostic.Syntax.Expressions;
using Microsoft.CodeAnalysis;
using CS2X.Core.Agnostic.Types.MemberDeclarations;

namespace CS2X.Core.Agnostic.Syntax.Statements
{
	public class LocalDeclarationStatement : Statement
	{
		public LocalDeclarationStatementSyntax statement;
		public List<VariableDeclaration> variables;

		public LocalDeclarationStatement(LocalDeclarationStatementSyntax statement, SemanticModel semanticModel)
		{
			variables = new List<VariableDeclaration>();
			var typeInfo = semanticModel.GetTypeInfo(statement.Declaration.Type);
			foreach (var variable in statement.Declaration.Variables)
			{
				variables.Add(new VariableDeclaration(variable, semanticModel, typeInfo.Type));
			}
		}
	}
}
