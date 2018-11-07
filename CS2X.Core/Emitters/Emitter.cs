using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;

using CoreSolution = CS2X.Core.Solution;

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

		public Emitter(CoreSolution solution, string outputPath)
		{
			this.solution = solution;
			this.outputPath = outputPath;
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
	}
}
