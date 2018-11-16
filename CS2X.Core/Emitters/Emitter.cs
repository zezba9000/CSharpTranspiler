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
				if (member.Kind != SymbolKind.Method) continue;

				var memberMethod = (IMethodSymbol)member;
				if (IsBackingMethod(memberMethod) || HasNativeName(memberMethod) || memberMethod.Name != method.Name) continue;

				if (memberMethod == method) break;
				++overload;
			}

			return overload;
		}

		/*protected List<ISymbol> GetAllBodyStackAllocations(BlockSyntax body)
		{
			var locals = new List<ISymbol>();
			var walker = new BodySyntaxWalker();
			walker.Visit(body);
			return locals;
		}*/
	}

	/*class BodySyntaxWalker : CSharpSyntaxWalker
	{
		public List<CSharpSyntaxNode> stackAllocations;

		public BodySyntaxWalker()
		{
			stackAllocations = new List<CSharpSyntaxNode>();
		}

		public override void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
		{
			stackAllocations.Add(node);
			base.VisitLocalDeclarationStatement(node);
		}

		public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
		{
			stackAllocations.Add(node);
			base.VisitObjectCreationExpression(node);
		}
	}*/
}
