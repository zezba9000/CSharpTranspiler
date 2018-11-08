using System.Collections;

namespace System
{
	public unsafe abstract class Array : IEnumerable
	{
		internal void* buffer;

		public int Length
		{
			get
			{
				return 0;
			}
		}

		public IEnumerator GetEnumerator()
		{
			return null;
		}
	}
}
