using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;

using CoreSolution = CS2X.Core.Solution;
using System.CS2X;

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
		protected delegate bool AppendMemberNameCallback(INamespaceOrTypeSymbol member, ref StringBuilder value);

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
		protected string GetFullNameFlat<T>(T member, AppendMemberNameCallback writeMemberNameCallback = null) where T : ISymbol
		{
			return GetFullName(member, '_', writeMemberNameCallback);
		}

		protected string GetFullName<T>(T member, char delimiter = '.', AppendMemberNameCallback writeMemberNameCallback = null) where T : ISymbol
		{
			var value = new StringBuilder();
			var namedSymbol = member as INamespaceOrTypeSymbol;
			if (namedSymbol != null)
			{
				BuildFullName(namedSymbol, ref value, delimiter, writeMemberNameCallback);
			}
			else
			{
				BuildFullName(member.ContainingType, ref value, delimiter, writeMemberNameCallback);
				value.Append(delimiter);
				value.Append(member.Name);
			}
			return value.ToString();
		}

		private void BuildFullName(INamespaceOrTypeSymbol member, ref StringBuilder value, char delimiter, AppendMemberNameCallback appendMemberName)
		{
			if (member.ContainingNamespace != null && !member.ContainingNamespace.IsGlobalNamespace) BuildFullName(member.ContainingNamespace, ref value, delimiter, appendMemberName);
			else if (member.ContainingType != null) BuildFullName(member.ContainingType, ref value, delimiter, appendMemberName);

			if (value.Length != 0) value.Append(delimiter);
			if (appendMemberName == null || !appendMemberName(member, ref value)) value.Append(member.Name);
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

		protected bool ObjectHasNotStaticFields(ITypeSymbol obj)
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

		protected string GetNativeName(ISymbol obj, string defaultValue)
		{
			foreach (var attribute in obj.GetAttributes())
			{
				if (attribute.AttributeClass.Name != "NativeNameAttribute") continue;
				var args = attribute.ConstructorArguments;
				if (args != null && args.Length == 2 && (NativeTargets)args[0].Value == target)
				{
					defaultValue = (string)args[1].Value;
					break;
				}
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
				if (member.Kind != SymbolKind.Method || member.IsImplicitlyDeclared) continue;

				var memberMethod = (IMethodSymbol)member;
				if (IsBackingMethod(memberMethod) || HasNativeName(memberMethod) || memberMethod.Name != method.Name) continue;

				if (memberMethod == method) break;
				++overload;
			}

			return overload;
		}
	}
}
