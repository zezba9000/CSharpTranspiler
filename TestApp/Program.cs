using TestLib;

namespace TestApp.C
{
	class B
	{
		partial class Program : Blaa.MyInterface
		{
			static void Main(string[] args)
			{
				i = 888;
			}
		}

		public partial class Program : MyTestLibClass, Blaa.MyInterface
		{
			static int i = 123;
		}
	}
}
