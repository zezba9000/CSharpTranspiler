using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.IO;
using System.Linq;
using System.Threading;

using CoreSolution = CS2X.Core.Solution;
using CoreProject = CS2X.Core.Project;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.CS2X;
using System.Text;

namespace CS2X.Core.Emitters
{
	/// <summary>
	/// C language emitter
	/// </summary>
	public class EmitterC : Emitter
	{
		#region Enums
		public enum CompilerTargets
		{
			/// <summary>
			/// Visual C++
			/// </summary>
			VCPP,

			/// <summary>
			/// GNU Compiler Collection
			/// </summary>
			GCC,

			/// <summary>
			/// C language family frontend for LLVM
			/// </summary>
			Clang
		}

		public enum PlatformTypes
		{
			/// <summary>
			/// Exe projects are output as .c files while dll projects as .h headers
			/// </summary>
			Desktop,

			/// <summary>
			/// Exe projects are output as .ino files in name-matched folders containing all dependancy headers
			/// void Main' is output as 'void setup'
			/// </summary>
			Arduino,

			/// <summary>
			/// Exe projects are output as headers
			/// 'void Main' is output as 'void ExeMain'
			/// </summary>
			Embedded
		}

		public enum GCTypes
		{
			/// <summary>
			/// Deterministic Sweep,Compact,Repeat GC
			/// Use on low memory embedded devices with no OS
			/// Heap objects are NOT thread safe!
			/// Uses brk and sbrk
			/// </summary>
			Micro,

			/// <summary>
			/// Deterministic Mark,Sweep GC
			/// Use on modern desktops, consoles, classic systems, homebrew or high memory embedded devices
			/// Heap objects are NOT thread safe!
			/// Uses malloc
			/// </summary>
			Deterministic,

			/// <summary>
			/// Stop-the-world GC
			/// Use on desktop and high memory devices
			/// Heap objects are thread safe
			/// </summary>
			Boehm
		}
		#endregion

		public readonly CompilerTargets target;
		public readonly PlatformTypes platform;
		public readonly GCTypes gc;
		private readonly string thisKeyword = "this";

		/// <summary>
		/// Active project
		/// </summary>
		private CoreProject project;

		/// <summary>
		/// Active project stream writer
		/// </summary>
		private StreamWriter writer;

		/// <summary>
		/// Active method
		/// </summary>
		private IMethodSymbol activeMethod;

		/// <summary>
		/// Active method syntax semantic model
		/// </summary>
		private SemanticModel semanticModel;

		public EmitterC(CoreSolution solution, string outputPath, CompilerTargets target, PlatformTypes platform, GCTypes gc, string thisKeyword = "this")
		: base(solution, outputPath, NativeTargets.C)
		{
			this.target = target;
			this.thisKeyword = thisKeyword;
		}

		public override void Emit(bool clean)
		{
			lock (this)
			{
				foreach (var project in solution.projects)
				{
					EmitProject(project);
				}
			}
		}

		private void EmitProject(CoreProject project)
		{
			this.project = project;
			string path = Path.Combine(outputPath, Path.GetFileNameWithoutExtension(project.roslynProject.FilePath) + (project.type == ProjectTypes.Exe ? ".c" : ".h"));
			using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
			using (writer = new StreamWriter(stream))
			{
				// make sure libs only get included once
				if (project.type == ProjectTypes.Dll)
				{
					writer.WriteLine("#pragma once");
					if (platform != PlatformTypes.Arduino) writer.WriteLine("#include <stdio.h>");
					if (project.roslynProject.Name == "CoreLib")
					{
						writer.WriteLine("#include \"GC_Boehm.h\"");
						writer.WriteLine("#define null 0");
					}
					writer.WriteLine("#define EMPTY_OBJECT void*");
					writer.WriteLine();
				}

				// write reference libs as included headers
				writer.WriteLine(string.Format("// ============={0}// Library References{0}// =============", Environment.NewLine));
				foreach (var reference in project.references)
				{
					writer.WriteLine($"#include \"{reference.AssemblyName}.h\"");
				}

				// write object forward declares
				writer.WriteLine(string.Format("{0}// ============={0}// Type forward declares{0}// =============", Environment.NewLine));
				foreach (var obj in project.enumObjects) WriteObject(obj, false);
				foreach (var obj in project.interfaceObjects) WriteObject(obj, false);
				foreach (var obj in project.structObjects) WriteObject(obj, false);
				foreach (var obj in project.classObjects) WriteObject(obj, false);

				// write objects
				writer.WriteLine(string.Format("{0}// ============={0}// Types Definitions{0}// =============", Environment.NewLine));
				foreach (var obj in project.enumObjects) WriteObject(obj, true);
				foreach (var obj in project.interfaceObjects) WriteObject(obj, true);
				foreach (var obj in project.structObjects) WriteObject(obj, true);
				foreach (var obj in project.classObjects) WriteObject(obj, true);

				// write object property forward declares
				writer.WriteLine(string.Format("// ============={0}// Property forward declares{0}// =============", Environment.NewLine));
				foreach (var obj in project.structObjects) WriteObjectProperties(obj, false);
				foreach (var obj in project.classObjects) WriteObjectProperties(obj, false);

				// write object method forward declares
				writer.WriteLine(string.Format("{0}// ============={0}// Method forward declares{0}// =============", Environment.NewLine));
				foreach (var obj in project.structObjects) WriteObjectMethods(obj, false);
				foreach (var obj in project.classObjects) WriteObjectMethods(obj, false);

				// write object properties
				writer.WriteLine(string.Format("{0}// ============={0}// Properties{0}// =============", Environment.NewLine));
				foreach (var obj in project.structObjects) WriteObjectProperties(obj, true);
				foreach (var obj in project.classObjects) WriteObjectProperties(obj, true);

				// write object methods
				writer.WriteLine(string.Format("// ============={0}// Methods{0}// =============", Environment.NewLine));
				foreach (var obj in project.structObjects) WriteObjectMethods(obj, true);
				foreach (var obj in project.classObjects) WriteObjectMethods(obj, true);

				// write entry point
				if (project.type == ProjectTypes.Exe && platform == PlatformTypes.Desktop)
				{
					writer.WriteLine(string.Format("// ============={0}// Entry Point{0}// =============", Environment.NewLine));
					var entryPointMethod = project.compilation.GetEntryPoint(CancellationToken.None);
					if (entryPointMethod != null)
					{
						writer.WriteLine("void main()");
						writer.WriteLine('{');
						writer.WriteLine($"\t{GetFullNameFlat(entryPointMethod)}__0();");
						writer.WriteLine('}');
					}
					else
					{
						writer.WriteLine("// <<< NO ENTRY POINT FOUND >>>");
					}
				}
			}
		}

		private string GetNativeCTypeName(ITypeSymbol type)
		{
			if (type is INamedTypeSymbol)
			{
				if (type.TypeKind == TypeKind.Enum)
				{
					var namedType = (INamedTypeSymbol)type;
					return GetFullNameFlat(namedType.EnumUnderlyingType);
				}
			}
			if (type is IPointerTypeSymbol)
			{
				var ptrType = (IPointerTypeSymbol)type;
				return GetFullNameFlat(ptrType.PointedAtType) + '*';
			}
			else if (type is IArrayTypeSymbol)
			{
				return GetFullNameFlat(project.compilation.GetSpecialType(SpecialType.System_Array));
			}

			return GetFullNameFlat(type);
		}

		#region Object Layout
		private void WriteObject(INamedTypeSymbol obj, bool writeBody)
		{
			var type = obj.TypeKind;

			// get type name
			if (IsLogicalType(type))
			{
				if (ObjectHasNonStaticFields(obj))
				{
					string formatString;
					if (writeBody)
					{
						formatString = "struct {0}";
					}
					else
					{
						if (obj.IsValueType) formatString = "typedef struct {0} {0}";
						else formatString = "typedef struct {0}* {0}";
					}
					writer.Write(string.Format(formatString, GetFullNameFlat(obj)));
				}
				else
				{
					if (writeBody)
					{
						WriteObjectBodyFields(obj, out var writeExternalMembers);
						writeExternalMembers?.Invoke();
						return;
					}
					else
					{
						string nativeName = GetNativeName(obj, "EMPTY_OBJECT");
						writer.Write($"typedef {nativeName} {GetFullNameFlat(obj)}");
					}
				}
			}
			else if (type == TypeKind.Enum)
			{
				if (writeBody)
				{
					string name = GetFullNameFlat(obj);
					foreach (var member in obj.GetMembers())
					{
						if (member.Kind != SymbolKind.Field || member.IsImplicitlyDeclared) continue;

						var field = (IFieldSymbol)member;
						writer.WriteLine($"#define {name}_{field.Name} {field.ConstantValue}");
					}

					writer.WriteLine();
				}
				
				return;// enum fields only need defines so return here
			}
			else
			{
				throw new Exception("Unsuported TypeKind: " + type);
			}

			// finish
			if (writeBody)
			{
				writer.WriteLine();
				writer.WriteLine('{');
				WriteObjectBodyFields(obj, out var writeExternalMembers);
				writer.WriteLine("};" + Environment.NewLine);
				writeExternalMembers?.Invoke();
			}
			else
			{
				writer.WriteLine(';');
			}
		}

		private void GetFieldInfo(IFieldSymbol field, out ITypeSymbol fieldType, out string fieldName)
		{
			if (IsBackingField(field))
			{
				var property = (IPropertySymbol)field.AssociatedSymbol;
				fieldType = property.Type;
				fieldName = property.Name;
			}
			else
			{
				fieldType = field.Type;
				fieldName = field.Name;
			}
		}

		private void WriteObjectBodyNonStaticFields(INamedTypeSymbol obj)
		{
			// write base fields first
			if (obj.BaseType != null) WriteObjectBodyNonStaticFields(obj.BaseType);

			// write non-static fields
			foreach (var member in obj.GetMembers())
			{
				if (member.Kind != SymbolKind.Field || member.IsStatic) continue;

				var field = (IFieldSymbol)member;
				GetFieldInfo(field, out var fieldType, out string fieldName);
				writer.WriteLine(string.Format("\t{0} {1};", GetNativeCTypeName(fieldType), fieldName));
			}
		}

		private void WriteObjectBodyFields(INamedTypeSymbol obj, out CallbackMethod writeExternalMembers)
		{
			var type = obj.TypeKind;
			if (IsLogicalType(type))
			{
				// write non-static
				WriteObjectBodyNonStaticFields(obj);

				// write static fields
				void writeStaticFields()
				{
					bool fieldWrote = false;
					foreach (var member in obj.GetMembers())
					{
						if (member.Kind != SymbolKind.Field || !member.IsStatic) continue;

						var field = (IFieldSymbol)member;
						GetFieldInfo(field, out var fieldType, out string fieldName);
						writer.WriteLine(string.Format("{0} {1}_{2};", GetNativeCTypeName(fieldType), GetFullNameFlat(field.ContainingType), fieldName));
						fieldWrote = true;
					}

					if (fieldWrote) writer.WriteLine();
				}

				writeExternalMembers = writeStaticFields;
			}
			else
			{
				throw new Exception("Unsupported object type: " + type);
			}
		}
		
		private void WriteObjectMethodDeclare(IMethodSymbol method, int? methodOverload)
		{
			string methodOverloadValue = (methodOverload != null) ? ("__" + methodOverload.Value) : "";
			if (method.MethodKind == MethodKind.Constructor) writer.Write($"System_Void {GetFullNameFlat(method).Replace(".ctor", "CONSTRUCTOR")}{methodOverloadValue}(");
			else writer.Write($"{GetNativeCTypeName(method.ReturnType)} {GetFullNameFlat(method)}{methodOverloadValue}(");

			// if method and object are not static pass 'this' ref
			if (!method.IsStatic && !method.ContainingType.IsStatic)
			{
				string ptr = method.ContainingType.IsValueType ? "*" : "";// 'this' should allows pass by ptr ref
				writer.Write(string.Format("{0}{1} {2}{3}", GetFullNameFlat(method.ContainingType), ptr, thisKeyword, (method.Parameters != null && method.Parameters.Length != 0) ? ", " : ""));
			}

			// write parameters
			if (method.Parameters != null)
			{
				int count = method.Parameters.Length;
				for (int i = 0; i != count; ++i)
				{
					var parameter = method.Parameters[i];
					writer.Write($"{GetNativeCTypeName(parameter.Type)} {parameter.Name}");
					if (i != count - 1) writer.Write(", ");
				}
			}

			// finish
			writer.Write(')');
		}

		private void WriteObjectProperties(INamedTypeSymbol obj, bool writeBody)
		{
			foreach (var member in obj.GetMembers())
			{
				if (member.Kind != SymbolKind.Property || member.IsImplicitlyDeclared) continue;

				var property = (IPropertySymbol)member;
				if (IsAutoPropery(property)) continue;

				if (property.GetMethod != null)
				{
					WriteObjectMethodDeclare(property.GetMethod, null);
					if (writeBody)
					{
						writer.WriteLine(Environment.NewLine + '{');
						WriteMethodBody(property.GetMethod);
						writer.WriteLine('}' + Environment.NewLine);
					}
					else
					{
						writer.WriteLine(';');
					}
				}

				if (property.SetMethod != null)
				{
					WriteObjectMethodDeclare(property.SetMethod, null);
					if (writeBody)
					{
						writer.WriteLine(Environment.NewLine + '{');
						WriteMethodBody(property.SetMethod);
						writer.WriteLine('}' + Environment.NewLine);
					}
					else
					{
						writer.WriteLine(';');
					}
				}
			}
		}

		private void WriteObjectMethods(INamedTypeSymbol obj, bool writeBody)
		{
			var members = obj.GetMembers();
			var overloads = new List<MethodOverload>();
			foreach (var member in members)
			{
				if (member.Kind != SymbolKind.Method || member.IsImplicitlyDeclared) continue;

				var method = (IMethodSymbol)member;
				if (IsBackingMethod(method) || HasNativeName(method)) continue;

				MethodOverload overload;
				overload = overloads.FirstOrDefault(x => x.name == method.Name);
				if (overload == null)
				{
					overload = new MethodOverload() { name = method.Name };
					overloads.Add(overload);
				}

				if (method.AssociatedSymbol != null && method.AssociatedSymbol is IPropertySymbol) WriteObjectMethodDeclare(method, null);
				else WriteObjectMethodDeclare(method, overload.count);
				if (writeBody)
				{
					writer.WriteLine(Environment.NewLine + '{');
					WriteMethodBody(method);
					writer.WriteLine('}' + Environment.NewLine);
				}
				else
				{
					writer.WriteLine(';');
				}

				++overload.count;
			}
		}
		#endregion

		#region Logical Bodies
		private void WriteMethodBody(IMethodSymbol method)
		{
			foreach (var syntaxRef in method.DeclaringSyntaxReferences)
			{
				var syntaxDeclaration = syntaxRef.GetSyntax();
				BlockSyntax body = null;
				if (syntaxDeclaration is AccessorDeclarationSyntax)
				{
					var syntax = (AccessorDeclarationSyntax)syntaxDeclaration;
					body = syntax.Body;
				}
				else if (syntaxDeclaration is MethodDeclarationSyntax)
				{
					var syntax = (MethodDeclarationSyntax)syntaxDeclaration;
					body = syntax.Body;
				}
				else if (syntaxDeclaration is ConstructorDeclarationSyntax)
				{
					var syntax = (ConstructorDeclarationSyntax)syntaxDeclaration;
					body = syntax.Body;
				}
				else
				{
					throw new Exception("Unsupported method syntax type: " + syntaxDeclaration.GetType());
				}

				if (body != null)
				{
					activeMethod = method;
					semanticModel = project.compilation.GetSemanticModel(syntaxDeclaration.SyntaxTree);
					WriteBlockSyntax(body);
				}
			}
		}

		private void WriteBlockSyntax(BlockSyntax body)
		{
			StreamWriterEx.prefix = "\t";
			foreach (var statement in body.Statements)
			{
				if (statement is ExpressionStatementSyntax) WriteExpressionStatement((ExpressionStatementSyntax)statement);
				else if (statement is ReturnStatementSyntax) WriteReturnStatement((ReturnStatementSyntax)statement);
				else if (statement is LocalDeclarationStatementSyntax) WriteLocalDeclarationStatement((LocalDeclarationStatementSyntax)statement);
				else throw new NotImplementedException("Unsuported statement type: " + statement.GetType());
			}
			StreamWriterEx.prefix = "";
		}

		private void WriteExpressionStatement(ExpressionStatementSyntax statement)
		{
			writer.WritePrefix();
			WriteExperesion(statement.Expression);
			writer.WriteLine(';');
		}

		private void WriteReturnStatement(ReturnStatementSyntax statement)
		{
			writer.WritePrefix("return ");
			WriteExperesion(statement.Expression);
			writer.WriteLine(';');
		}

		private void WriteLocalDeclarationStatement(LocalDeclarationStatementSyntax statement)
		{
			var typeInfo = semanticModel.GetTypeInfo(statement.Declaration.Type);
			string typeName = GetNativeCTypeName(typeInfo.Type);
			var variables = GetVariables(statement, semanticModel);
			foreach (var variable in variables)
			{
				writer.WritePrefix($"{typeName} {variable.Identifier.ValueText}");
				if (variable.Initializer != null && variable.Initializer.Value != null)
				{
					writer.Write(" = ");
					WriteExperesion(variable.Initializer.Value);
				}
				writer.WriteLine(';');
			}
		}

		private void WriteExperesion(ExpressionSyntax expression)
		{
			if (expression is AssignmentExpressionSyntax) WriteAssignmentExpression((AssignmentExpressionSyntax)expression);
			else if (expression is BinaryExpressionSyntax) WriteBinaryExpression((BinaryExpressionSyntax)expression);
			else if (expression is ThisExpressionSyntax) WriteThisExpression((ThisExpressionSyntax)expression);
			else if (expression is IdentifierNameSyntax) WriteIdentifierName((IdentifierNameSyntax)expression);
			else if (expression is MemberAccessExpressionSyntax) WriteMemberAccessExpression((MemberAccessExpressionSyntax)expression);
			else if (expression is LiteralExpressionSyntax) WriteLiteralExpression((LiteralExpressionSyntax)expression);
			else if (expression is CastExpressionSyntax) WriteCastExpression((CastExpressionSyntax)expression);
			else if (expression is ParenthesizedExpressionSyntax) WriteParenthesizedExpression((ParenthesizedExpressionSyntax)expression);
			else if (expression is InvocationExpressionSyntax) WriteInvocationExpression((InvocationExpressionSyntax)expression);
			else throw new NotImplementedException("Unsuported expression type: " + expression.GetType());
		}

		private void WriteCaller(ExpressionSyntax expression)
		{
			if (expression is MemberAccessExpressionSyntax)
			{
				var accessExpression = (MemberAccessExpressionSyntax)expression;
				expression = accessExpression.Expression;
			}
			else
			{
				var symbolInfo = semanticModel.GetSymbolInfo(expression);
				if (symbolInfo.Symbol.ContainingType == activeMethod.ContainingType) expression = SyntaxFactory.ThisExpression();
			}

			WriteExperesion(expression);
		}

		private void WriteAssignmentExpression(AssignmentExpressionSyntax expression)
		{
			// check if we need to convert property operator to method call
			var symbolInfo = semanticModel.GetSymbolInfo(expression.Left);
			if (symbolInfo.Symbol.Kind == SymbolKind.Property && !IsAutoPropery((IPropertySymbol)symbolInfo.Symbol))
			{
				var propertySymbol = symbolInfo.Symbol as IPropertySymbol;
				writer.Write($"{GetFullNameFlat(propertySymbol.SetMethod)}(");
				if (!propertySymbol.IsStatic)
				{
					WriteCaller(expression.Left);
					writer.Write(", ");
				}
				WriteExperesion(expression.Right);
				writer.Write(')');

				return;
			}

			// write normal operator
			WriteOperatorExpression(expression.Left, expression.Right, expression.OperatorToken);
		}

		private void WriteBinaryExpression(BinaryExpressionSyntax expression)
		{
			WriteOperatorExpression(expression.Left, expression.Right, expression.OperatorToken);
		}

		private void WriteOperatorExpression(ExpressionSyntax expressionLeft, ExpressionSyntax expressionRight, SyntaxToken operatorToken)
		{
			WriteExperesion(expressionLeft);
			writer.Write($" {operatorToken.ValueText} ");
			WriteExperesion(expressionRight);
		}

		private void WriteThisExpression(ThisExpressionSyntax expression)
		{
			writer.Write(thisKeyword);
		}

		private void WriteSymbolAccess(ISymbol symbol)
		{
			if (symbol.Kind == SymbolKind.Property && !IsAutoPropery((IPropertySymbol)symbol))
			{
				var propertySymbol = (IPropertySymbol)symbol;
				writer.Write(GetFullNameFlat(propertySymbol.GetMethod));
			}
			else if (symbol.Kind == SymbolKind.Method)
			{
				var methodSymbol = (IMethodSymbol)symbol;
				string name = $"{GetFullNameFlat(symbol)}__{GetMethodOverloadIndex(methodSymbol)}";
				writer.Write(GetNativeName(symbol, name));

			}
			else if (symbol.IsStatic)
			{
				writer.Write(GetNativeName(symbol, GetFullNameFlat(symbol)));
			}
			else
			{
				writer.Write(symbol.Name);
			}
		}

		private void WriteIdentifierName(IdentifierNameSyntax expression)
		{
			// get symbol info
			var symbolInfo = semanticModel.GetSymbolInfo(expression);
			bool isProperty = symbolInfo.Symbol.Kind == SymbolKind.Property && !IsAutoPropery((IPropertySymbol)symbolInfo.Symbol);
			
			// write 'this' access if non static field
			if (!isProperty && !symbolInfo.Symbol.IsStatic && !(symbolInfo.Symbol is ILocalSymbol || symbolInfo.Symbol is IParameterSymbol || symbolInfo.Symbol is IMethodSymbol))
			{
				writer.Write($"{thisKeyword}->");
			}

			// write symbol
			WriteSymbolAccess(symbolInfo.Symbol);

			// write property closer
			if (isProperty)
			{
				if (symbolInfo.Symbol.IsStatic) writer.Write("()");
				else writer.Write($"({thisKeyword})");
			}
		}

		private void WriteMemberAccessExpression(MemberAccessExpressionSyntax expression)
		{
			// if member is globally defined just directly reference
			var symbolInfo = semanticModel.GetSymbolInfo(expression);
			if (symbolInfo.Symbol is IMethodSymbol)
			{
				WriteSymbolAccess(symbolInfo.Symbol);
				return;
			}
			if (symbolInfo.Symbol is IPropertySymbol && !IsAutoPropery((IPropertySymbol)symbolInfo.Symbol))
			{
				WriteSymbolAccess(symbolInfo.Symbol);
				writer.Write('(');
				if (!symbolInfo.Symbol.IsStatic) WriteCaller(expression.Expression);
				writer.Write(')');
				return;
			}
			else if (symbolInfo.Symbol.IsStatic)
			{
				WriteSymbolAccess(symbolInfo.Symbol);
				return;
			}
			

			// write caller expression
			WriteExperesion(expression.Expression);

			// check caller access type
			var symbolInfoAccessor = semanticModel.GetSymbolInfo(expression.Expression);
			if (expression.Expression.GetType() != typeof(ThisExpressionSyntax) && IsResultValueType(symbolInfoAccessor.Symbol)) writer.Write('.');
			else writer.Write("->");

			// write final symbol
			WriteSymbolAccess(symbolInfo.Symbol);
		}

		private void WriteLiteralExpression(LiteralExpressionSyntax expression)
		{
			var value = semanticModel.GetConstantValue(expression);
			if (value.HasValue)
			{
				if (value.Value == null)
				{
					writer.Write("null");
				}
				else
				{
					if (value.Value is string) writer.Write($"L\"{value.Value}\"");
					else writer.Write(value.Value);
					if (value.Value is float) writer.Write('f');
				}
			}
			else
			{
				throw new Exception("Unsupported literal token: " + expression.Token.ValueText);
			}
		}

		private void WriteCastExpression(CastExpressionSyntax expression)
		{
			var symbolInfo = semanticModel.GetSymbolInfo(expression.Type);
			writer.Write($"({GetFullNameFlat(symbolInfo.Symbol)})");
			WriteExperesion(expression.Expression);
		}

		private void WriteParenthesizedExpression(ParenthesizedExpressionSyntax expression)
		{
			writer.Write('(');
			WriteExperesion(expression.Expression);
			writer.Write(')');
		}

		private void WriteInvocationExpression(InvocationExpressionSyntax expression)
		{
			// write method name
			WriteExperesion(expression.Expression);
			writer.Write('(');

			// write caller
			var symbolInfo = semanticModel.GetSymbolInfo(expression.Expression);
			if (!symbolInfo.Symbol.IsStatic)
			{
				WriteCaller(expression.Expression);
				if (expression.ArgumentList.Arguments.Count != 0) writer.Write(", ");
			}

			// write arguments
			if (expression.ArgumentList != null)
			{
				var lastArg = expression.ArgumentList.Arguments.LastOrDefault();
				foreach (var arg in expression.ArgumentList.Arguments)
				{
					WriteExperesion(arg.Expression);
					if (arg != lastArg) writer.Write(", ");
				}
			}

			writer.Write(')');
		}
		#endregion
	}
}
