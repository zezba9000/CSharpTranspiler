using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTranspiler.Agnostic.Syntax
{
	public class LogicalBody
	{
		public BlockSyntax block;
		public Member member;

		public LogicalBody(Member member, BlockSyntax block)
		{
			this.member = member;
			this.block = block;
		}
	}
}
