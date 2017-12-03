using TestApp.Blaa;
using TestLib;

namespace TestApp.C
{
	class B
	{
		partial class Program : Blaa.MyInterface
		{
			static A2 a = null;

			static void Main(string[] args)
			{
				i = 888;
			}

			static A2 Foo(int hi, string by)
			{
				return a;
			}
		}

		public partial class Program : MyTestLibClass, Blaa.MyInterface
		{
			static int i = 123, i2 = 22;
		}
	}
}
