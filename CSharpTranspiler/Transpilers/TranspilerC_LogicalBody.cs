using CSharpTranspiler.Agnostic;
using CSharpTranspiler.Agnostic.Syntax;
using CSharpTranspiler.Agnostic.Syntax.Expressions;
using CSharpTranspiler.Agnostic.Syntax.Statements;
using CSharpTranspiler.Agnostic.Types;
using CSharpTranspiler.Agnostic.Types.MemberDeclarations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTranspiler.Transpilers
{
	public static partial class TranspilerC
	{
		private static void WriteLogicalBody(LogicalBody body, StreamWriter writer)
		{
			StreamWriterEx.prefix = "\t";
			foreach (var statement in body.statements)
			{
				if (statement is ExpressionStatement) WriteExpressionStatement((ExpressionStatement)statement, writer);
				else if (statement is ReturnStatement) WriteReturnStatement((ReturnStatement)statement, writer);
				else if (statement is LocalDeclarationStatement) WriteLocalDeclarationStatement((LocalDeclarationStatement)statement, writer);
				else throw new NotImplementedException("Unsuported statement type: " + statement.GetType());
			}
		}

		private static void WriteExpressionStatement(ExpressionStatement statement, StreamWriter writer)
		{
			writer.WritePrefix();
			WriteExperesion(statement.expression, writer);
			writer.WriteLine(';');
		}

		private static void WriteReturnStatement(ReturnStatement statement, StreamWriter writer)
		{
			writer.WritePrefix("return ");
			WriteExperesion(statement.expression, writer);
			writer.WriteLine(';');
		}

		private static void WriteLocalDeclarationStatement(LocalDeclarationStatement statement, StreamWriter writer)
		{
			foreach (var variable in statement.variables)
			{
				writer.WritePrefix(string.Format("{0} {1} ", variable.typeFullNameFlat, variable.fullNameFlat));
				if (variable.initializeExpression != null)
				{
					writer.Write("= ");
					WriteExperesion(variable.initializeExpression, writer);
				}
				writer.WriteLine(';');
			}
		}

		private static void WriteExperesion(Expression expression, StreamWriter writer)
		{
			if (expression is AssignmentExpression) WriteAssignmentExpression((AssignmentExpression)expression, writer);
			else if (expression is IdentifierNameExpression) WriteIdentifierNameExperesion((IdentifierNameExpression)expression, writer);
			else if (expression is LiteralExpression) WriteLiteralExpression((LiteralExpression)expression, writer);
			else if (expression is CastExpression) WriteCastExpression((CastExpression)expression, writer);
			else if (expression is AccessExpression) WriteAccessExpression((AccessExpression)expression, writer);
			else throw new NotImplementedException("Unsuported expression type: " + expression.GetType());
		}

		private static void WriteAssignmentExpression(AssignmentExpression expression, StreamWriter writer)
		{
			WriteExperesion(expression.left, writer);
			writer.Write(string.Format(" {0} ", expression.op.ValueText));
			WriteExperesion(expression.right, writer);
		}

		private static void WriteIdentifierNameExperesion(IdentifierNameExpression expression, StreamWriter writer)
		{
			writer.Write(expression.name);
		}

		private static void WriteLiteralExpression(LiteralExpression expression, StreamWriter writer)
		{
			writer.Write(expression.value);
			if (expression.value is float) writer.Write('f');
		}

		private static void WriteCastExpression(CastExpression expression, StreamWriter writer)
		{
			writer.Write(expression.typeFullNameFlat);
		}

		private static void WriteAccessExpression(AccessExpression expression, StreamWriter writer)
		{
			writer.Write(expression.fullNameFlat);
		}
	}
}
