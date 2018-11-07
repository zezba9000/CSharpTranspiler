namespace System
{
	public enum NativeTypeNameTargets
	{
		C
	}

	[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple = true)]
	public class NativeTypeNameAttribute : Attribute
	{
		public readonly NativeTypeNameTargets Target;
		public readonly string Value;

		public NativeTypeNameAttribute(NativeTypeNameTargets target, string value)
		{
			this.Target = target;
			this.Value = value;
		}
	}
}
