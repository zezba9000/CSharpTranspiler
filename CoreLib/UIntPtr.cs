using System.CS2X;

namespace System
{
	public unsafe struct UIntPtr
	{
		internal void* ptr;

		public UIntPtr(uint value)
		{
			ptr = (void*)value;
		}

		public static explicit operator UIntPtr(uint value)
		{
			return new UIntPtr(value);
		}

		public static explicit operator uint(UIntPtr value)
		{
			return (uint)value.ptr;
		}

		public uint ToUInt32()
		{
			return (uint)this;
		}
	}
}
