using System.CS2X;

namespace System
{
	public static class GC
	{
		[NativeName(NativeTargets.C, "CS2X_GC_New")]
		internal unsafe static void* CS2X_GC_New(void* size)
		{
			return null;
		}

		[NativeName(NativeTargets.C, "CS2X_GC_NewAtomic")]
		internal unsafe static void* CS2X_GC_NewAtomic(void* size)
		{
			return null;
		}

		[NativeName(NativeTargets.C, "CS2X_GC_DisableAutoCollections")]
		internal static void DisableAutoCollections()
		{
			
		}

		[NativeName(NativeTargets.C, "CS2X_GC_EnableAutoCollections")]
		internal static void EnableAutoCollections()
		{

		}

		// ======================================
		// manual allocation methods(non-GC heap)
		// ======================================
		[NativeName(NativeTargets.C, "CS2X_Malloc")]
		internal unsafe static void* CS2X_Malloc(void* size)
		{
			return null;
		}

		[NativeName(NativeTargets.C, "CS2X_Delete")]
		internal unsafe static void CS2X_Delete(void* ptr)
		{

		}
	}
}
