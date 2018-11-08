namespace System.CS2X
{
	[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	public class NativeNameAttribute : Attribute
	{
		public readonly NativeTargets Target;
		public readonly string Value;

		public NativeNameAttribute(NativeTargets target, string value)
		{
			this.Target = target;
			this.Value = value;
		}
	}
}
