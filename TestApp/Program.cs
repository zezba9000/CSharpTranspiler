using TestApp.Blaa;
using TestLib;

namespace TestApp.C
{
	class MyBase// : MyTestLibClass
	{
		int baseInt;
	}

	class B
	{
		partial class Program : MyInterface
		{
			static A2 a = null;

			static void Main(string[] args)
			{
				C.B.Program.i = 888;
				Program.i = 999;
				i = 22;

				int abc = 44;
				abc = 33;
			}

			A2 Foo(int hi, string by)
			{
				return a;
			}
		}

		public partial class Program : MyBase, Blaa.MyInterface
		{
			static int i = 123;

			static int _i2 = 22;
			static int i2
			{
				get {return _i2;}
				set {_i2 = value;}
			}

			float i3 {get; set;}
			public float i4 {private get; set;}
			public float i5 {get; private set;}
			
			public float i6
			{
				get
				{
					return 1.1f;
				}
			}

			public float i7
			{
				set
				{
					i2 = (int)value;
				}
			}
		}
	}
}
