using System.CS2X;

namespace System
{
	public static class Buffer
	{
		[NativeName(NativeTargets.C, "memcpy")]
		public unsafe static void memcpy(void* dst, void* src, void* size) { }

		public static void BlockCopy(Array src, int srcOffset, Array dst, int dstOffset, int count)
		{
			// TODO
		}
	}
}
