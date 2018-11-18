using System.CS2X;

namespace System
{
	public static class Console
	{
		[NativeName(NativeTargets.C, "wprintf")]
		private unsafe static void wprintf(char* value) { }

		public unsafe static void WriteLine(string value)
		{
			wprintf(value.buffer);
		}
	}
}
