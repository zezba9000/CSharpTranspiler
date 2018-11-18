using System.CS2X;

namespace System
{
	public static class GC
	{
		[NativeName(NativeTargets.C, "CS2X_GC_New")]
		public unsafe static void* CS2X_GC_New(void* size)
		{
			return null;
		}

		[NativeName(NativeTargets.C, "CS2X_GC_NewAtomic")]
		public unsafe static void* CS2X_GC_NewAtomic(void* size)
		{
			return null;
		}
	}
}
