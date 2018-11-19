using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;

using CoreSolution = CS2X.Core.Solution;
using System.CS2X;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;

namespace CS2X.Core.Emitters
{
	class MethodOverload
	{
		public string name;
		public int count;
	}

	public abstract class Emitter
	{
		protected delegate void CallbackMethod();

		public readonly CoreSolution solution;
		public readonly string outputPath;
		public readonly NativeTargets target;

		public Emitter(CoreSolution solution, string outputPath, NativeTargets target)
		{
			this.solution = solution;
			this.outputPath = outputPath;
			this.target = target;
		}

		public abstract void Emit(bool clean);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected string GetFullNameFlat<T>(T member) where T : ISymbol
		{
			return GetFullName(member, '_');
		}

		protected string GetFullName<T>(T member, char delimiter = '.') where T : ISymbol
		{
			var value = new StringBuilder();
			var namedSymbol = member as INamespaceOrTypeSymbol;
			if (namedSymbol != null)
			{
				BuildFullName(namedSymbol, ref value, delimiter);
			}
			else
			{
				BuildFullName(member.ContainingType, ref value, delimiter);
				value.Append(delimiter);
				value.Append(member.Name);
			}
			return value.ToString();
		}

		private void BuildFullName(INamespaceOrTypeSymbol member, ref StringBuilder value, char delimiter)
		{
			if (member.ContainingNamespace != null && !member.ContainingNamespace.IsGlobalNamespace) BuildFullName(member.ContainingNamespace, ref value, delimiter);
			else if (member.ContainingType != null) BuildFullName(member.ContainingType, ref value, delimiter);

			if (value.Length != 0) value.Append(delimiter);
			value.Append(member.Name);
		}

		protected bool IsLogicalType(TypeKind typeKind)
		{
			return typeKind == TypeKind.Struct || typeKind == TypeKind.Class || typeKind == TypeKind.Interface;
		}

		protected SeparatedSyntaxList<VariableDeclaratorSyntax> GetVariables(LocalDeclarationStatementSyntax statement, SemanticModel semanticModel)
		{
			var typeInfo = semanticModel.GetTypeInfo(statement.Declaration.Type);
			return statement.Declaration.Variables;
		}

		protected bool ObjectHasNonStaticFields(ITypeSymbol obj)
		{
			foreach (var member in obj.GetMembers())
			{
				if (member.Kind != SymbolKind.Field || member.IsStatic) continue;
				return true;
			}

			return false;
		}

		protected bool IsBackingField(IFieldSymbol field)
		{
			return field.AssociatedSymbol != null && field.AssociatedSymbol.Kind == SymbolKind.Property;
		}

		protected bool IsBackingMethod(IMethodSymbol method)
		{
			return method.AssociatedSymbol != null && method.AssociatedSymbol.Kind == SymbolKind.Property;
		}

		protected bool IsAutoPropery(IPropertySymbol property)
		{
			var members = property.ContainingType.GetMembers();
			return members.Any(x => x.Kind == SymbolKind.Field && ((IFieldSymbol)x).AssociatedSymbol == property);
		}

		protected bool HasNativeName(ISymbol obj)
		{
			foreach (var attribute in obj.GetAttributes())
			{
				if (attribute.AttributeClass.Name != "NativeNameAttribute") continue;
				var args = attribute.ConstructorArguments;
				if (args != null && args.Length == 2 && (NativeTargets)args[0].Value == target)
				{
					return true;
				}
			}

			return false;
		}

		protected bool IsAtomicObject(INamedTypeSymbol obj)
		{
			foreach (var member in obj.GetMembers())
			{
				if (member.Kind != SymbolKind.Field) continue;
				var field = (IFieldSymbol)member;
				if (!field.Type.IsValueType) return false;
			}

			return true;
		}

		protected IMethodSymbol GetDeconstructorMethod(INamedTypeSymbol obj)
		{
			foreach (var member in obj.GetMembers())
			{
				if (member.Kind != SymbolKind.Method) continue;
				var method = (IMethodSymbol)member;
				if (!method.IsImplicitlyDeclared && method.MethodKind == MethodKind.Destructor) return method;
			}

			return null;
		}

		protected AttributeData GetAttribute(ISymbol obj, string attributeName)
		{
			foreach (var attribute in obj.GetAttributes())
			{
				if (attribute.AttributeClass.Name != attributeName) continue;
				return attribute;
			}

			return null;
		}

		protected string GetNativeName(ISymbol obj, string defaultValue)
		{
			var attribute = GetAttribute(obj, "NativeNameAttribute");
			if (attribute == null) return defaultValue;
			var args = attribute.ConstructorArguments;
			if (args != null && args.Length == 2 && (NativeTargets)args[0].Value == target)
			{
				return (string)args[1].Value;
			}

			return defaultValue;
		}

		protected bool IsResultValueType(ISymbol obj)
		{
			if (obj is IParameterSymbol)
			{
				var type = (IParameterSymbol)obj;
				return type.Type.IsValueType;
			}
			else if (obj is IPropertySymbol)
			{
				var type = (IPropertySymbol)obj;
				return type.Type.IsValueType;
			}
			else if (obj is IFieldSymbol)
			{
				var type = (IFieldSymbol)obj;
				return type.Type.IsValueType;
			}
			else if (obj is IMethodSymbol)
			{
				var type = (IMethodSymbol)obj;
				return type.ReturnType.IsValueType;
			}
			else if (obj is ITypeSymbol)
			{
				var type = (ITypeSymbol)obj;
				return type.IsValueType;
			}
			
			return true;
		}

		protected int GetMethodOverloadIndex(IMethodSymbol method)
		{
			int overload = 0;
			foreach (var member in method.ContainingType.GetMembers())
			{
				if (member.Kind != SymbolKind.Method) continue;

				var memberMethod = (IMethodSymbol)member;
				if (IsBackingMethod(memberMethod) || HasNativeName(memberMethod) || memberMethod.Name != method.Name) continue;

				if (memberMethod == method) break;
				++overload;
			}

			return overload;
		}

		protected int GetMethodOverloadIndex(INamedTypeSymbol obj, params string[] methodParameterTypes)
		{
			var method = FindMethodWithParameters(obj, methodParameterTypes);
			if (method == null) return 0;
			return GetMethodOverloadIndex(method);
		}

		protected IMethodSymbol FindMethodWithParameters(INamedTypeSymbol obj, params string[] methodParameterTypes)
		{
			foreach (var member in obj.GetMembers())
			{
				if (member.Kind != SymbolKind.Method) continue;
				var method = (IMethodSymbol)member;
				var parms = method.Parameters;
				if (parms.Length == methodParameterTypes.Length)
				{
					bool found = true;
					for (int i = 0; i != methodParameterTypes.Length; ++i)
					{
						string pType = methodParameterTypes[i];
						var parm = parms[i].Type.ToString();
						if (parm != pType)
						{
							found = false;
							break;
						}
					}

					if (found) return method;
				}
			}

			return null;
		}

		protected List<LocalDeclarationStatementSyntax> GetStackVariables(BlockSyntax body)
		{
			var nodes = new List<LocalDeclarationStatementSyntax>();
			foreach (var member in body.Statements)
			{
				if (member is LocalDeclarationStatementSyntax) nodes.Add((LocalDeclarationStatementSyntax)member);
			}
			return nodes;
		}

		protected List<LocalDeclarationStatementSyntax> GetAllStackVariables(BlockSyntax body)
		{
			var bodyLocalDeclarationsSyntaxWalker = new BodyLocalDeclarationsSyntaxWalker();
			bodyLocalDeclarationsSyntaxWalker.Visit(body);
			return bodyLocalDeclarationsSyntaxWalker.nodes;
		}
	}

	class BodyLocalDeclarationsSyntaxWalker : CSharpSyntaxWalker
	{
		public List<LocalDeclarationStatementSyntax> nodes;

		public BodyLocalDeclarationsSyntaxWalker()
		{
			nodes = new List<LocalDeclarationStatementSyntax>();
		}

		public override void Visit(SyntaxNode node)
		{
			nodes.Clear();
			base.Visit(node);
		}

		public override void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
		{
			nodes.Add(node);
			base.VisitLocalDeclarationStatement(node);
		}
	}
}
