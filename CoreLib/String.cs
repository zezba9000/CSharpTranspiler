using System.CS2X;

namespace System
{
	public unsafe sealed class String
	{
		public int Length { get; private set; }
		internal char* buffer;

		[NativeName(NativeTargets.C, "wcslen")]
		private unsafe static int wcslen(char* str)
		{
			return 0;
		}

		public String(char* value)
		{
			Length = wcslen(value);
			void* size = (void*)(Length * sizeof(char));
			buffer = (char*)GC.CS2X_Malloc(size);
			Buffer.memcpy(buffer, value, size);
		}

		~String()
		{
			GC.CS2X_Delete(buffer);
			buffer = null;
		}
	}
}
