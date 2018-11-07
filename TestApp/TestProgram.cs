using TestApp.Blaa;
using TestLib;

partial class MyPartial
{
	public int iabc;
}

partial class MyPartial
{
	public int i234;

	public void Foo()
	{
		int i = 0;
	}
}

namespace TestApp.C
{
	class MyBase : MyTestLibClass
	{
		protected int baseInt;
	}

	class B
	{
		partial class Program : MyInterface
		{
			static A2 a = null;

			public Program()
			{
				this.b += 1;
			}

			A2 Foo()
			{
				return a;
			}

			static void Main()
			{
				C.B.Program.i = 888;
				Program.i = 999;
				i = 22;

				i2 = 0;
				C.B.Program.i2 = 1;

				int abc = 44;
				abc = 33;
			}

			A2 Foo(int hi, string by, string by2)
			{
				int foo2 = hi + baseInt;
				foo2 = hi;
				foo2 = baseInt;
				return a;
			}
		}

		public partial class Program : MyBase, Blaa.MyInterface
		{
			static int i = 123;
			int iBlaa;

			static int _i2 = 22;
			static int i2
			{
				get {return _i2;}
				set {_i2 = value;}
			}

			float i3 {get; set;}
			public float i4 {private get; set;}
			public float i5 {get; private set;}
			public static float i8 { get; private set; }

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
					i4 += i3;
					i4 += i6;
				}
			}
		}
	}
}
