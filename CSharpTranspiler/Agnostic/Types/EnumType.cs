using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace CSharpTranspiler.Agnostic.Types
{
	public class EnumMember
	{
		public string name;
		public object value;
		public bool valueIsExplicitlySet;

		public EnumMember(string name, object value, bool valueIsExplicitlySet)
		{
			this.name = name;
			this.value = value;
			this.valueIsExplicitlySet = valueIsExplicitlySet;
		}
	}

	public class EnumType : ObjectType
	{
		public List<EnumMember> members;

		public EnumType(EnumDeclarationSyntax declaration, SemanticModel semanticModel)
		: base(declaration, semanticModel)
		{
			members = new List<EnumMember>();
			object currentValue = 0;
			foreach (var member in declaration.Members)
			{
				bool valueExplicitlySet = false;
				if (member.EqualsValue != null && member.EqualsValue.Value != null)
				{
					var value = semanticModel.GetConstantValue(member.EqualsValue.Value);
					currentValue = value.HasValue ? value.Value : null;
					valueExplicitlySet = true;
				}
				
				members.Add(new EnumMember(member.Identifier.Text, currentValue, valueExplicitlySet));
				currentValue = Iterate(currentValue);
			}
		}

		private object Iterate(dynamic value)
		{
			return Convert.ChangeType(value + 1, value.GetType());
		}
	}
}
