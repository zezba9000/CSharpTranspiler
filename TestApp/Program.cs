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

			A2 Foo(int hi, string by)
			{
				return a;
			}
		}

		public partial class Program : MyTestLibClass, Blaa.MyInterface
		{
			static int i = 123;

			static int _i2 = 22;
			static int i2
			{
				get {return _i2;}
				set {_i2 = value;}
			}

			static int i3 {get; set;}
		}
	}
}
