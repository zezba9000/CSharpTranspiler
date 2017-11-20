

namespace TestApp.C
{
	class B
	{
		partial class Program : Blaa.A
		{
			static void Main(string[] args)
			{
				i = 888;
			}
		}

		partial class Program : Blaa.A2, Blaa.A
		{
			static int i = 123;
		}
	}
}
