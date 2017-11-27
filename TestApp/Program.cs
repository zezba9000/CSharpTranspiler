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

		partial class Program : Blaa.A2, Blaa.MyInterface
		{
			static int i = 123;
		}
	}
}
