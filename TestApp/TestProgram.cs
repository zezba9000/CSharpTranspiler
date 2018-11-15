using System;
using TestApp.Blaa;
using TestLib;

struct TestStruct
{
	public float x;

	public TestStruct(float x)
	{
		this.x = x;
	}

	public TestStruct NewMe()
	{
		return new TestStruct();
	}
}

class TestClassE
{
	public int key { get; set; }
	public TestClassE(int key, TestIn testIn)
	{
		TestIn.singleton.GetObj().Get().key = 123;
		TestIn.singleton.GetObjProp = null;
		this.key = Add(key);
		this.key = this.Add(this.key);
		this.key = AddStatic(Add(key));
		this.key = testIn.Add(key);
		testIn.SetMe(this);
		TestIn.singleton.SetMe(this);
	}

	private int Add(int key)
	{
		return key + 1;
	}

	private static int AddStatic(int key)
	{
		return key + 1;
	}

	private TestClassE Get()
	{
		return this;
		return new TestClassE(0, null);
	}
}

class TestIn
{
	public static TestIn singleton;
	public TestClassE obj;

	public int Add(int key)
	{
		return key + 2;
	}

	public void SetMe(TestClassE s)
	{
		
	}

	public TestClassE GetObj()
	{
		return obj;
	}

	public TestClassE GetObjProp
	{
		get
		{
			return obj;
		}
		set
		{
			obj = value;
		}
	}
}

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
			int[] boo2;

			public Program()
			{
				this.b += 1;
			}

			static A2 Foo()
			{
				int[] boo;
				string myString = "Hello World!";
				Console.WriteLine(myString);
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

				//Foo();
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

			void Yahoo(Program p)
			{
				var val = p.i6;
			}

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
					i4 = i6 + i3;
					i4 = i3 + i6;
					i2 = (int)(i6 + i3);
					i2 = (int)(i3 + i6);
				}
			}
		}
	}
}
