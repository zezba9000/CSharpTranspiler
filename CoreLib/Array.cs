using System.Collections;

namespace System
{
	public abstract class Array : IEnumerable
	{
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
