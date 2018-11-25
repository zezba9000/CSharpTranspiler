using CoreSolution = CS2X.Core.Solution;
using CoreProject = CS2X.Core.Project;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.CS2X;

namespace CS2X.Core.Emitters
{
	/// <summary>
	/// C language emitter
	/// </summary>
	public class EmitterC : Emitter
	{
		#region Enums
		public enum CVersions
		{
			/// <summary>
			/// Should compile on almost any C compiler
			/// </summary>
			c89,

			/// <summary>
			/// Helps keeps objects more 1 to 1 with C# but doesn't compile on more obscure compilers
			/// </summary>
			c99
		}

		public enum CompilerTargets
		{
			/// <summary>
			/// Don't try handle any C compiler edge case (just output C)
			/// </summary>
			Unspecified,

			/// <summary>
			/// Visual C (https://visualstudio.microsoft.com/)
			/// </summary>
			VC,

			/// <summary>
			/// GNU Compiler Collection (https://gcc.gnu.org/)
			/// </summary>
			GCC,

			/// <summary>
			/// C language family frontend for LLVM (https://clang.llvm.org/)
			/// </summary>
			Clang,

			/// <summary>
			/// A freeware C compiler for 6502 based systems (https://www.cc65.org/)
			/// </summary>
			cc65
		}

		public enum PlatformTypes
		{
			/// <summary>
			/// Exe projects are output as .c files while dll projects as .h headers
			/// </summary>
			Standalone,

			/// <summary>
			/// Exe projects are output as headers to be compiled in a C compiler
			/// 'void Main' is output as 'void ExeMain'
			/// </summary>
			EmbeddedC,

			/// <summary>
			/// Exe projects are output as headers to be compiled in a C++ compiler
			/// 'void Main' is output as 'void ExeMain'
			/// </summary>
			EmbeddedCpp,

			/// <summary>
			/// Exe projects are output as .ino files in name-matched folders while dll projects as .h headers
			/// void Main' is output as 'void setup'
			/// </summary>
			Arduino
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

		public readonly CVersions cVersion;
		public readonly CompilerTargets target;
		public readonly PlatformTypes platform;
		public readonly GCTypes gc;
		private readonly string thisKeyword, nullKeyword, trueKeyword, falseKeyword;
		private readonly bool isCppEnviroment;

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

		public EmitterC(CoreSolution solution, string outputPath, CVersions cVersion, CompilerTargets target, PlatformTypes platform, GCTypes gc)
		: base(solution, outputPath, NativeTargets.C)
		{
			this.cVersion = cVersion;
			this.target = target;
			this.platform = platform;
			this.gc = gc;

			// validate compiler option compatibility
			if (target == CompilerTargets.cc65) throw new NotImplementedException("cc65 compiler requires float to FixedPoint convertion. TODO");

			if (cVersion == CVersions.c99)
			{
				if (target == CompilerTargets.cc65) throw new Exception("cc65 isn't fully c99 compliant and thus must be used with c89");
			}

			if (platform == PlatformTypes.EmbeddedCpp)
			{
				isCppEnviroment = true;
			}
			else if (platform == PlatformTypes.Arduino)
			{
				isCppEnviroment = true;
				if (gc != GCTypes.Micro) throw new Exception("Only 'micro' gc supported on Arduino platform");
			}

			thisKeyword = "this";
			nullKeyword = "null";
			trueKeyword = "true";
			falseKeyword = "false";
			if (isCppEnviroment)
			{
				PrefixCppKeyword(ref thisKeyword);
				PrefixCppKeyword(ref nullKeyword);
			}
		}

		private void PrefixCppKeyword(ref string keyword)
		{
			keyword = "__" + keyword;
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
						switch (gc)
						{
							case GCTypes.Micro: writer.WriteLine("#include \"GC_Micro.h\""); break;
							case GCTypes.Deterministic: writer.WriteLine("#include \"GC_Deterministic.h\""); break;
							case GCTypes.Boehm: writer.WriteLine("#include \"GC_Boehm.h\""); break;

							default: throw new Exception("Unsuported GC: " + gc);
						}

						writer.WriteLine("#define EMPTY_OBJECT void*");
						writer.WriteLine($"#define {nullKeyword} 0");
						if (!isCppEnviroment)
						{
							writer.WriteLine($"#define {trueKeyword} 1");
							writer.WriteLine($"#define {falseKeyword} 0");
						}
					}
					
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
				writer.WriteLine(string.Format("{0}// ============={0}// Type Definitions{0}// =============", Environment.NewLine));
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
				if (project.type == ProjectTypes.Exe && platform == PlatformTypes.Standalone)
				{
					writer.WriteLine(string.Format("// ============={0}// Entry Point{0}// =============", Environment.NewLine));
					var entryPointMethod = project.compilation.GetEntryPoint(CancellationToken.None);
					if (entryPointMethod != null)
					{
						writer.WriteLine("void main()");
						writer.WriteLine('{');
						writer.WriteLine("\tCS2X_GC_Init();");
						writer.WriteLine($"\t{GetFullNameFlat(entryPointMethod)}__0();");
						writer.WriteLine("\tCS2X_GC_Collect();");// must force collection so deconstructors fire
						writer.WriteLine('}');
					}
					else
					{
						writer.WriteLine("// <<< NO ENTRY POINT FOUND >>>");
					}
				}
			}
		}

		private string GetNativeCTypeInstance(ITypeSymbol type)
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
				return GetFullNameFlat(project.compilation.GetSpecialType(SpecialType.System_Array)) + '*';
			}

			string result = GetFullNameFlat(type);
			if (!type.IsValueType) result += '*';
			return result;
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
					if (writeBody) formatString = "struct {0}";
					else formatString = "typedef struct {0} {0}";
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
				writer.WriteLine(string.Format("\t{0} {1};", GetNativeCTypeInstance(fieldType), fieldName));
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
						writer.WriteLine(string.Format("{0} {1}_{2};", GetNativeCTypeInstance(fieldType), GetFullNameFlat(field.ContainingType), fieldName));
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
			if (method.MethodKind == MethodKind.Constructor)
			{
				string typeName = GetFullNameFlat(method.ContainingType);
				string ptr = method.ContainingType.IsValueType ? "" : "*";
				writer.Write($"{typeName}{ptr} {typeName}_CONSTRUCTOR{methodOverloadValue}(");
			}
			else
			{
				writer.Write($"{GetNativeCTypeInstance(method.ReturnType)} {GetFullNameFlat(method)}{methodOverloadValue}(");

				// if method and object are not static pass 'this' ref
				if (!method.IsStatic && !method.ContainingType.IsStatic)
				{
					string paramEnd = (method.Parameters != null && method.Parameters.Length != 0) ? ", " : "";
					writer.Write($"{GetFullNameFlat(method.ContainingType)}* {thisKeyword}");
					if (method.MethodKind == MethodKind.Destructor && gc == GCTypes.Boehm) writer.Write(", void* data");
					writer.Write(paramEnd);
				}
			}

			// write parameters
			if (method.Parameters != null)
			{
				int count = method.Parameters.Length;
				for (int i = 0; i != count; ++i)
				{
					var parameter = method.Parameters[i];
					writer.Write($"{GetNativeCTypeInstance(parameter.Type)} {parameter.Name}");
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
			// get deconstructor method
			IMethodSymbol deconstructorMethod = null;
			if (!obj.IsValueType) deconstructorMethod = GetDeconstructorMethod(obj);

			// check if method is atomic
			bool isAtomic = IsAtomicObject(obj);

			// generate normal methods
			var members = obj.GetMembers();
			var overloads = new List<MethodOverload>();
			foreach (var member in members)
			{
				if (member.Kind != SymbolKind.Method) continue;

				var method = (IMethodSymbol)member;
				if (IsBackingMethod(method) || HasNativeName(method)) continue;

				// check for struct default constructor
				bool isDefaultConstructor = false;
				if (method.IsImplicitlyDeclared)
				{
					if (method.MethodKind == MethodKind.Constructor) isDefaultConstructor = true;
					else continue;
					if (isDefaultConstructor && obj.SpecialType == SpecialType.System_Void) continue;// don't generate void constructor
				}

				// get overload
				MethodOverload overload;
				overload = overloads.FirstOrDefault(x => x.name == method.Name);
				if (overload == null)
				{
					overload = new MethodOverload() { name = method.Name };
					overloads.Add(overload);
				}

				// write declaration
				if (method.AssociatedSymbol != null && method.AssociatedSymbol is IPropertySymbol) WriteObjectMethodDeclare(method, null);
				else WriteObjectMethodDeclare(method, (method.MethodKind != MethodKind.Destructor) ? overload.count : (int?)null);
				if (writeBody)
				{
					writer.WriteLine(Environment.NewLine + '{');
					if (method.MethodKind == MethodKind.Constructor)
					{
						string typeName = GetFullNameFlat(obj);
						if (obj.TypeKind == TypeKind.Struct)
						{
							writer.WriteLine($"\t{typeName} {thisKeyword} = {{0}};");
						}
						else
						{
							if (isAtomic) writer.WriteLine($"\t{typeName}* {thisKeyword} = CS2X_GC_NewAtomic(sizeof({typeName}));");
							else writer.WriteLine($"\t{typeName}* {thisKeyword} = CS2X_GC_New(sizeof({typeName}));");
							if (deconstructorMethod != null)
							{
								switch (gc)
								{
									case GCTypes.Boehm: writer.WriteLine($"\tGC_register_finalizer({thisKeyword}, {GetFullNameFlat(deconstructorMethod)}, 0, 0, 0);"); break;
									default: throw new Exception("TODO: Missing GC deconstructor method");
								}
							}
						}
					}
					if (!isDefaultConstructor) WriteMethodBody(method);
					if (method.MethodKind == MethodKind.Constructor) writer.WriteLine($"\treturn {thisKeyword};");// if constructor return allocated this ref
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
			if (method.DeclaringSyntaxReferences.Count() != 1) throw new Exception("Multiple method syntax refs not supported");
			var syntaxRef = method.DeclaringSyntaxReferences.First();
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
			else if (syntaxDeclaration is DestructorDeclarationSyntax)
			{
				var syntax = (DestructorDeclarationSyntax)syntaxDeclaration;
				body = syntax.Body;
			}
			else if (syntaxDeclaration is ConversionOperatorDeclarationSyntax)
			{
				// TODO
			}
			else
			{
				throw new Exception("Unsupported method syntax type: " + syntaxDeclaration.GetType());
			}

			if (body != null)
			{
				activeMethod = method;
				semanticModel = project.compilation.GetSemanticModel(syntaxDeclaration.SyntaxTree);
				WriteBlockSyntax(body, false);
			}
		}

		private void WriteBlockStartStackVariables(BlockSyntax block)
		{
			List<string> c89TypeBuff = null;
			foreach (var variable in GetStackVariables(block))
			{
				if (variable is LocalDeclarationStatementSyntax)
				{
					WriteLocalDeclarationStatement((LocalDeclarationStatementSyntax)variable, false);
				}
				else if (variable is VariableDeclarationSyntax && cVersion == CVersions.c89)
				{
					if (c89TypeBuff == null) c89TypeBuff = new List<string>();
					WriteVariableDeclaration((VariableDeclarationSyntax)variable, c89TypeBuff);
				}
			}
		}

		private string GetCompactLocalTypeIdentifier(string identifier, ITypeSymbol symbol)
		{
			if (cVersion == CVersions.c89) return $"{identifier}__{symbol.Name}";
			else return identifier;
		}

		private void WriteVariableDeclaration(VariableDeclarationSyntax declaration, List<string> c89TypeBuff)
		{
			var typeSymbol = semanticModel.GetTypeInfo(declaration.Type);
			string typeName = GetNativeCTypeInstance(typeSymbol.Type);

			foreach (var variable in declaration.Variables)
			{
				string compactTypeName = GetCompactLocalTypeIdentifier(variable.Identifier.ValueText, typeSymbol.Type);
				if (c89TypeBuff != null)
				{
					if (c89TypeBuff.Contains(compactTypeName)) continue;
					c89TypeBuff.Add(compactTypeName);
				}

				writer.WriteLinePrefix($"{typeName} {compactTypeName};");
			}
		}

		private void WriteBlockSyntax(BlockSyntax block, bool writeBrackets)
		{
			if (writeBrackets) writer.WriteLinePrefix('{');
			StreamWriterEx.Tab();
			if (cVersion == CVersions.c89) WriteBlockStartStackVariables(block);
			foreach (var statement in block.Statements) WriteStatementSyntax(statement);
			StreamWriterEx.RemoveTab();
			if (writeBrackets) writer.WriteLinePrefix('}');
		}

		private void WriteStatementSyntax(StatementSyntax statement)
		{
			if (statement is BlockSyntax) WriteBlockSyntax((BlockSyntax)statement, true);
			else if (statement is ExpressionStatementSyntax) WriteExpressionStatement((ExpressionStatementSyntax)statement);
			else if (statement is ReturnStatementSyntax) WriteReturnStatement((ReturnStatementSyntax)statement);
			else if (statement is LocalDeclarationStatementSyntax) WriteLocalDeclarationStatement((LocalDeclarationStatementSyntax)statement, true);
			else if (statement is IfStatementSyntax) WriteIfStatement((IfStatementSyntax)statement);
			else if (statement is WhileStatementSyntax) WriteWhileStatement((WhileStatementSyntax)statement);
			else if (statement is DoStatementSyntax) WriteDoStatement((DoStatementSyntax)statement);
			else if (statement is ForStatementSyntax) WriteForStatement((ForStatementSyntax)statement);
			else if (statement is EmptyStatementSyntax) WriteEmptyStatement((EmptyStatementSyntax)statement);
			else if (statement is BreakStatementSyntax) WriteBreakStatement((BreakStatementSyntax)statement);
			else if (statement is ContinueStatementSyntax) WriteContinueStatement((ContinueStatementSyntax)statement);
			else throw new NotImplementedException("Unsuported statement type: " + statement.GetType());
		}

		private void WriteExpressionStatement(ExpressionStatementSyntax statement)
		{
			writer.WritePrefix();
			WriteExperesion(statement.Expression);
			writer.WriteLine(';');
		}

		private void WriteReturnStatement(ReturnStatementSyntax statement)
		{
			writer.WritePrefix("return");
			if (statement.Expression != null)
			{
				writer.Write(' ');
				WriteExperesion(statement.Expression);
			}
			writer.WriteLine(';');
		}

		private void WriteLocalDeclarationStatement(LocalDeclarationStatementSyntax statement, bool writeInitializer)
		{
			var typeInfo = semanticModel.GetTypeInfo(statement.Declaration.Type);
			string typeName = GetNativeCTypeInstance(typeInfo.Type);
			foreach (var variable in statement.Declaration.Variables)
			{
				// write identifier
				if (cVersion == CVersions.c89 && writeInitializer)
				{
					if (variable.Initializer != null) writer.WritePrefix(variable.Identifier.ValueText);
				}
				else
				{
					writer.WritePrefix($"{typeName} {variable.Identifier.ValueText}");
				}

				// write initializer
				if (writeInitializer && variable.Initializer != null && variable.Initializer.Value != null)
				{
					writer.Write(" = ");
					WriteExperesion(variable.Initializer.Value);
				}

				// close statement
				if (cVersion == CVersions.c89 && writeInitializer)
				{
					if (variable.Initializer != null) writer.WriteLine(';');
				}
				else
				{
					writer.WriteLine(';');
				}
			}
		}

		private void WriteFlowStatement(StatementSyntax statement)
		{
			if (statement is BlockSyntax)
			{
				writer.WriteLine(')');
				WriteStatementSyntax(statement);
			}
			else
			{
				StreamWriterEx.RemoveTab();
				writer.Write(") ");
				WriteStatementSyntax(statement);
				StreamWriterEx.Tab();
			}
		}

		private void WriteIfStatement(IfStatementSyntax statement)
		{
			if (statement.Parent is ElseClauseSyntax) writer.Write("if (");
			else writer.WritePrefix("if (");
			WriteExperesion(statement.Condition);
			WriteFlowStatement(statement.Statement);

			if (statement.Else != null)
			{
				if (statement.Else.Statement is BlockSyntax) writer.WriteLinePrefix("else");
				else writer.WritePrefix("else ");
				WriteStatementSyntax(statement.Else.Statement);
			}
		}

		private void WriteWhileStatement(WhileStatementSyntax statement)
		{
			writer.WritePrefix("while (");
			WriteExperesion(statement.Condition);
			WriteFlowStatement(statement.Statement);
		}

		private void WriteDoStatement(DoStatementSyntax statement)
		{
			writer.WriteLinePrefix("do");
			writer.WriteLinePrefix('{');
			WriteBlockSyntax((BlockSyntax)statement.Statement, false);
			writer.WritePrefix("} while (");
			WriteExperesion(statement.Condition);
			writer.WriteLine(");");
		}
		
		private void WriteForStatement(ForStatementSyntax statement)
		{
			writer.WritePrefix("for (");

			// write declarations
			if (statement.Declaration != null)
			{
				var typeSymbol = semanticModel.GetTypeInfo(statement.Declaration.Type);
				if (cVersion != CVersions.c89) writer.Write($"{GetNativeCTypeInstance(typeSymbol.Type)} ");
				var variables = statement.Declaration.Variables;
				var lastVariable = variables.LastOrDefault();
				foreach (var variable in variables)
				{
					writer.Write(GetCompactLocalTypeIdentifier(variable.Identifier.ValueText, typeSymbol.Type));
					if (variable.Initializer != null)
					{
						writer.Write(" = ");
						WriteExperesion(variable.Initializer.Value);
					}
					if (variable != lastVariable) writer.Write(", ");
				}
				writer.Write("; ");
			}

			// write initializers
			var lastInitializer = statement.Initializers.LastOrDefault();
			foreach (var initializer in statement.Initializers)
			{
				WriteExperesion(initializer);
				if (initializer != lastInitializer) writer.Write(", ");
			}
			if (lastInitializer != null) writer.Write("; ");

			// write condition
			if (statement.Condition != null) WriteExperesion(statement.Condition);
			writer.Write("; ");

			// write incrementors
			var lastIncrementor = statement.Incrementors.LastOrDefault();
			foreach (var incrementor in statement.Incrementors)
			{
				WriteExperesion(incrementor);
				if (incrementor != lastIncrementor) writer.Write(", ");
			}

			// write statement
			WriteFlowStatement(statement.Statement);
		}

		private void WriteEmptyStatement(EmptyStatementSyntax statement)
		{
			writer.WriteLine(';');
		}

		private void WriteBreakStatement(BreakStatementSyntax statement)
		{
			writer.WriteLinePrefix("break;");
		}

		private void WriteContinueStatement(ContinueStatementSyntax statement)
		{
			writer.WriteLinePrefix("continue;");
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
			else if (expression is ObjectCreationExpressionSyntax) WriteObjectCreationExpression((ObjectCreationExpressionSyntax)expression);
			else if (expression is SizeOfExpressionSyntax) WriteSizeOfExpression((SizeOfExpressionSyntax)expression);
			else if (expression is PostfixUnaryExpressionSyntax) WritePostfixUnaryExpression((PostfixUnaryExpressionSyntax)expression);
			else if (expression is PrefixUnaryExpressionSyntax) WritePrefixUnaryExpression((PrefixUnaryExpressionSyntax)expression);
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

			if (expression.Kind() == SyntaxKind.ThisExpression && activeMethod.ContainingType.IsValueType && activeMethod.MethodKind == MethodKind.Constructor) writer.Write('&');
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
			else if (symbol.Kind == SymbolKind.Local)
			{
				if (cVersion == CVersions.c89)
				{
					var decSyntaxRef = symbol.DeclaringSyntaxReferences.First();
					var decSyntax = decSyntaxRef.GetSyntax();
					if (decSyntax.Parent is VariableDeclarationSyntax && decSyntax.Parent.Parent is ForStatementSyntax)
					{
						var typeSymbol = (ILocalSymbol)symbol;
						writer.Write(GetCompactLocalTypeIdentifier(symbol.Name, typeSymbol.Type));
					}
					else
					{
						writer.Write(symbol.Name);
					}
				}
				else
				{
					writer.Write(symbol.Name);
				}
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
				if (activeMethod.MethodKind == MethodKind.Constructor && activeMethod.ContainingType.IsValueType) writer.Write($"{thisKeyword}.");
				else writer.Write($"{thisKeyword}->");
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
			if (expression.Expression.GetType() == typeof(ThisExpressionSyntax))
			{
				if (activeMethod.MethodKind == MethodKind.Constructor && activeMethod.ContainingType.IsValueType) writer.Write('.');
				else writer.Write("->");
			}
			else if (IsResultValueType(symbolInfoAccessor.Symbol))
			{
				writer.Write('.');
			}
			else
			{
				writer.Write("->");
			}

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
					writer.Write(nullKeyword);
				}
				else if (value.Value is bool)
				{
					bool boolValue = (bool)value.Value;
					writer.Write(boolValue ? trueKeyword : falseKeyword);
				}
				else
				{
					if (value.Value is string)
					{
						int index = GetMethodOverloadIndex(project.compilation.GetSpecialType(SpecialType.System_String), "char*");
						writer.Write($"System_String_CONSTRUCTOR__{index}(L\"{value.Value}\")");
					}
					else
					{
						writer.Write(value.Value);
					}

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
			writer.Write($"({GetNativeCTypeInstance((ITypeSymbol)symbolInfo.Symbol)})");
			WriteExperesion(expression.Expression);
		}

		private void WriteParenthesizedExpression(ParenthesizedExpressionSyntax expression)
		{
			writer.Write('(');
			WriteExperesion(expression.Expression);
			writer.Write(')');
		}

		private void WriteArguments(ArgumentListSyntax argumentList)
		{
			if (argumentList != null)
			{
				var lastArg = argumentList.Arguments.LastOrDefault();
				foreach (var arg in argumentList.Arguments)
				{
					WriteExperesion(arg.Expression);
					if (arg != lastArg) writer.Write(", ");
				}
			}
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
			if (expression.ArgumentList != null) WriteArguments(expression.ArgumentList);
			writer.Write(')');
		}

		private void WriteObjectCreationExpression(ObjectCreationExpressionSyntax expression)
		{
			var symbolInfo = semanticModel.GetSymbolInfo(expression);
			int overloadIndex = GetMethodOverloadIndex((IMethodSymbol)symbolInfo.Symbol);
			var typeSymbolInfo = semanticModel.GetSymbolInfo(expression.Type);
			var typeSymbol = (ITypeSymbol)typeSymbolInfo.Symbol;
			writer.Write($"{GetFullNameFlat(typeSymbol)}_CONSTRUCTOR__{overloadIndex}");

			// write arguments
			writer.Write('(');
			if (expression.ArgumentList != null) WriteArguments(expression.ArgumentList);
			writer.Write(')');
		}

		private void WriteSizeOfExpression(SizeOfExpressionSyntax expression)
		{
			var symbolInfo = semanticModel.GetSymbolInfo(expression.Type);
			writer.Write($"sizeof({GetFullNameFlat(symbolInfo.Symbol)})");
		}

		private void WritePostfixUnaryExpression(PostfixUnaryExpressionSyntax expression)
		{
			WriteExperesion(expression.Operand);
			writer.Write(expression.OperatorToken);
		}

		private void WritePrefixUnaryExpression(PrefixUnaryExpressionSyntax expression)
		{
			writer.Write(expression.OperatorToken);
			WriteExperesion(expression.Operand);
		}
		#endregion
	}
}
