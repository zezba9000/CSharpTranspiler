using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpTranspiler.Agnostic.Syntax.Statements;

namespace CSharpTranspiler.Agnostic.Syntax
{
	public class LogicalBody
	{
		public BlockSyntax block;
		public Member member;

		public List<Statement> statements;

		public LogicalBody(Member member, BlockSyntax block)
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
					case SyntaxKind.ExpressionStatement: statement = new ExpressionStatement((ExpressionStatementSyntax)s); break;
					default: throw new NotImplementedException("Unsuported Statement SyntaxKind: " + kind);
				}

				statements.Add(statement);
			}
		}
	}
}
