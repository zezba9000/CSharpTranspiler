using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS2X.Core.Agnostic.Syntax.Statements;
using Microsoft.CodeAnalysis;

namespace CS2X.Core.Agnostic.Syntax
{
	public class LogicalBody
	{
		public BlockSyntax block;
		public Member member;

		public List<Statement> statements;

		public LogicalBody(Member member, BlockSyntax block, SemanticModel semanticModel)
		{
			this.member = member;
			this.block = block;

			// parse statments
			statements = new List<Statement>();
			foreach (var s in block.Statements)
			{
				var kind = s.Kind();
				Statement statement;
				switch (kind)
				{
					case SyntaxKind.ExpressionStatement: statement = new ExpressionStatement((ExpressionStatementSyntax)s, semanticModel); break;
					case SyntaxKind.ReturnStatement: statement = new ReturnStatement((ReturnStatementSyntax)s, semanticModel); break;
					case SyntaxKind.LocalDeclarationStatement: statement = new LocalDeclarationStatement((LocalDeclarationStatementSyntax)s, semanticModel); break;
					default: throw new NotImplementedException("Unsuported Statement SyntaxKind: " + kind);
				}

				statements.Add(statement);
			}
		}
	}
}
