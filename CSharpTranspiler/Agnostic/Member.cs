using CSharpTranspiler.Agnostic.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTranspiler.Agnostic
{
	public abstract class Member
	{
		public bool isStatic;
		public List<Modifiers> modifiers;
		public List<AttributeSyntax> attributes;

		public Member(SyntaxTokenList? modifiers, SyntaxList<AttributeListSyntax>? attributeList)
		{
			// parse modifiers
			if (modifiers != null)
			{
				this.modifiers = new List<Modifiers>();
				AddModifiers(modifiers.Value);
			}

			// parse attributes
			attributes = new List<AttributeSyntax>();
			if (attributeList != null)
			{
				foreach (var list in attributeList)
				{
					attributes.AddRange(list.Attributes);
				}
			}
		}

		protected void AddModifiers(SyntaxTokenList modifiers)
		{
			foreach (var modifier in modifiers)
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
					case SyntaxKind.StaticKeyword: newModifier = Modifiers.Static; isStatic = true; break;
					case SyntaxKind.UnsafeKeyword: newModifier = Modifiers.Unsafe; break;
					case SyntaxKind.VirtualKeyword: newModifier = Modifiers.Virtual; break;
					case SyntaxKind.VolatileKeyword: newModifier = Modifiers.Volatile; break;

					default: throw new Exception("Unsuported modifier: " + kind);
				}

				if (!this.modifiers.Contains(newModifier)) this.modifiers.Add(newModifier);
			}
		}
	}
}
