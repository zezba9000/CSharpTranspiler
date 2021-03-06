﻿using System;
using TestApp.Blaa;
using TestLib;

struct TestStruct
{
	public double x;
	public static int hi = 99;

	public TestStruct(double y)
	{
		this.x = y;
		int i = 1;
		if (y == 0);
		else if (y == 1) x = y;
		else if (y == 2)
		{
			if (true)
			{
				int i3 = Foo();
				int i2 = 1;
				x = y + i + i2;
			}
		}

		while (true)
		{
			break;
		}

		while (true) continue;

		do
		{
			return;
		} while (false);

		// block
		{
			for (i = 0; i != 1; i++)
			{
				continue;
			}

			for (int x2 = 0; x2 != 1; ++x2)
			{
				continue;
			}
		}

		for (int x2 = 0; ; ++x2)// test duplicate c89 stack vars with the same name
		{
			continue;
		}

		for (float x2 = 0, y2 = 1; x2 != 1 && y2 != 0; ++x2, --y2)
		{
			continue;
		}

		int x3, y3;
		for (x3 = 0, y3 = 1, y = 0; x3 != 1 && y3 != 0 && y != 0; ++x3, --y3, y = y + 1)
		{
			continue;
		}

		switch (i)
		{
			case 66 + 1:
			case 55:
				i = 5;
				break;

			case 33: i = 6; break;
			case 22: break;

			default:
				i = 9;
				break;
		}
	}

	private int Foo()
	{
		return 4;
	}

	public TestStruct NewMe()
	{
		return new TestStruct();
	}
}

class TestClassE
{
	int blaa777 = 777;
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
		return new TestClassE(0, new TestIn());
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
				string myString = "Blaa!";
				Console.WriteLine(myString);
				return a;
			}

			static void Main()
			{
				Console.WriteLine("Hello World!");
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
			static Program p = new Program();

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
