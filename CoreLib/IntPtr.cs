using System.CS2X;

namespace System
{
	public unsafe struct IntPtr
	{
		internal void* ptr;

		public IntPtr(int value)
		{
			ptr = (void*)value;
		}

		public static explicit operator IntPtr(int value)
		{
			return new IntPtr(value);
		}

		public static explicit operator int(IntPtr value)
		{
			return (int)value.ptr;
		}

		public int ToInt32()
		{
			return (int)this;
		}
	}
}
