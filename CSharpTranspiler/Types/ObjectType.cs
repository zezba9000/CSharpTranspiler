using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.CodeAnalysis.CSharp.Extensions;
using CSharpTranspiler.Syntax;

namespace CSharpTranspiler.Types
{
	public class BaseObject
	{
		public BaseTypeSyntax baseType;
		public TypeInfo typeInfo;
		public string fullName;
		public bool isInterface;
	}

	public abstract class ObjectType
	{
		public List<BaseTypeDeclarationSyntax> declarationSyntaxes;

		public string name, fullName, fullNameFlat;
		public List<Modifiers> modifiers;
		public List<BaseObject> baseObjects;
		
		public ObjectType(string name, string fullName, BaseTypeDeclarationSyntax declarationSyntax, SemanticModel semanticModel)
		{
			this.name = name;
			this.fullName = fullName;
			fullNameFlat = fullName.Replace('.', '_');
			declarationSyntaxes = new List<BaseTypeDeclarationSyntax>();
			declarationSyntaxes.Add(declarationSyntax);

			// parse modifiers
			modifiers = new List<Modifiers>();
			AddModifiers(declarationSyntax);

			// parse base types
			baseObjects = new List<BaseObject>();
			AddBaseObjects(declarationSyntax, semanticModel);
		}

		private void AddModifiers(BaseTypeDeclarationSyntax declarationSyntax)
		{
			foreach (var modifier in declarationSyntax.Modifiers)
			{
				var kind = modifier.Kind();
				Modifiers newModifier;
				switch (kind)
				{
					// access
					case SyntaxKind.PublicKeyword: newModifier = Modifiers.Public; break;
					case SyntaxKind.PrivateKeyword: newModifier = Modifiers.Private; break;
					case SyntaxKind.InternalKeyword: newModifier = Modifiers.Internal; break;
					case SyntaxKind.ProtectedKeyword: newModifier = Modifiers.Protected; break;

					// other
					case SyntaxKind.AbstractKeyword: newModifier = Modifiers.Abstract; break;
					case SyntaxKind.ConstKeyword: newModifier = Modifiers.Const; break;
					case SyntaxKind.EventKeyword: newModifier = Modifiers.Event; break;
					case SyntaxKind.ExternKeyword: newModifier = Modifiers.Extern; break;
					case SyntaxKind.NewKeyword: newModifier = Modifiers.New; break;
					case SyntaxKind.OverrideKeyword: newModifier = Modifiers.Override; break;
					case SyntaxKind.PartialKeyword: newModifier = Modifiers.Partial; break;
					case SyntaxKind.ReadOnlyKeyword: newModifier = Modifiers.ReadOnly; break;
					case SyntaxKind.SealedKeyword: newModifier = Modifiers.Sealed; break;
					case SyntaxKind.StaticKeyword: newModifier = Modifiers.Static; break;
					case SyntaxKind.UnsafeKeyword: newModifier = Modifiers.Unsafe; break;
					case SyntaxKind.VirtualKeyword: newModifier = Modifiers.Virtual; break;
					case SyntaxKind.VolatileKeyword: newModifier = Modifiers.Volatile; break;

					default: throw new Exception("Unsuported modifier: " + kind);
				}

				if (!modifiers.Contains(newModifier)) modifiers.Add(newModifier);
			}
		}

		private void AddBaseObjects(BaseTypeDeclarationSyntax declarationSyntax, SemanticModel semanticModel)
		{
			if (declarationSyntax.BaseList == null) return;
			foreach (var baseType in declarationSyntax.BaseList.Types)
			{
				var typeInfo = semanticModel.GetTypeInfo(baseType.Type);
				var typeSymbol = typeInfo.Type;
				if (!baseObjects.Exists(x => x.typeInfo.Type == typeSymbol))
				{
					var obj = new BaseObject()
					{
						baseType = baseType,
						typeInfo = typeInfo,
						fullName = typeSymbol.ToDisplayString(),
						isInterface = typeSymbol.TypeKind == TypeKind.Interface
					};
					
					baseObjects.Add(obj);
				}
			}
		}

		public virtual void MergePartial(BaseTypeDeclarationSyntax declarationSyntax, SemanticModel semanticModel)
		{
			if (declarationSyntaxes.Contains(declarationSyntax)) throw new Exception("Error: Partial already merged!");
			declarationSyntaxes.Add(declarationSyntax);
			
			AddModifiers(declarationSyntax);
			AddBaseObjects(declarationSyntax, semanticModel);
		}
	}
}
