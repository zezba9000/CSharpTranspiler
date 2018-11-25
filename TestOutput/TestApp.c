// =============
// Library References
// =============
#include "CoreLib.h"
#include "TestLib.h"

// =============
// Type forward declares
// =============
typedef EMPTY_OBJECT TestApp_Blaa_MyInterface;
typedef struct TestStruct TestStruct;
typedef struct TestClassE TestClassE;
typedef struct TestIn TestIn;
typedef struct MyPartial MyPartial;
typedef EMPTY_OBJECT TestApp_Blaa_A2;
typedef struct TestApp_C_MyBase TestApp_C_MyBase;
typedef EMPTY_OBJECT TestApp_C_B;
typedef struct TestApp_C_Program TestApp_C_Program;

// =============
// Type Definitions
// =============
#define TestApp_Blaa_MyEnum_A 1
#define TestApp_Blaa_MyEnum_B 2
#define TestApp_Blaa_MyEnum_C 4

#define TestApp_Blaa_MyEnumDefault_A 1
#define TestApp_Blaa_MyEnumDefault_B 2
#define TestApp_Blaa_MyEnumDefault_C 4

struct TestStruct
{
	System_Double x;
};

struct TestClassE
{
	System_Int32 key;
};

struct TestIn
{
	TestClassE* obj;
};

TestIn* TestIn_singleton;

struct MyPartial
{
	System_Int32 iabc;
	System_Int32 i234;
};

struct TestApp_C_MyBase
{
	System_Int32 b;
	System_Int32 baseInt;
};

struct TestApp_C_Program
{
	System_Int32 b;
	System_Int32 baseInt;
	System_Array* boo2;
	System_Int32 iBlaa;
	System_Single i3;
	System_Single i4;
	System_Single i5;
};

TestApp_Blaa_A2* TestApp_C_Program_a;
System_Int32 TestApp_C_Program_i;
System_Int32 TestApp_C_Program__i2;
System_Single TestApp_C_Program_i8;

// =============
// Property forward declares
// =============
TestClassE* TestIn_get_GetObjProp(TestIn* this);
System_Void TestIn_set_GetObjProp(TestIn* this, TestClassE* value);
System_Int32 TestApp_C_Program_get_i2();
System_Void TestApp_C_Program_set_i2(System_Int32 value);
System_Single TestApp_C_Program_get_i6(TestApp_C_Program* this);
System_Void TestApp_C_Program_set_i7(TestApp_C_Program* this, System_Single value);

// =============
// Method forward declares
// =============
TestStruct TestStruct_CONSTRUCTOR__0(System_Double y);
System_Int32 TestStruct_Foo__0(TestStruct* this);
TestStruct TestStruct_NewMe__0(TestStruct* this);
TestStruct TestStruct_CONSTRUCTOR__1();
TestClassE* TestClassE_CONSTRUCTOR__0(System_Int32 key, TestIn* testIn);
System_Int32 TestClassE_Add__0(TestClassE* this, System_Int32 key);
System_Int32 TestClassE_AddStatic__0(System_Int32 key);
TestClassE* TestClassE_Get__0(TestClassE* this);
System_Int32 TestIn_Add__0(TestIn* this, System_Int32 key);
System_Void TestIn_SetMe__0(TestIn* this, TestClassE* s);
TestClassE* TestIn_GetObj__0(TestIn* this);
TestIn* TestIn_CONSTRUCTOR__0();
System_Void MyPartial_Foo__0(MyPartial* this);
MyPartial* MyPartial_CONSTRUCTOR__0();
TestApp_Blaa_A2* TestApp_Blaa_A2_CONSTRUCTOR__0();
TestApp_C_MyBase* TestApp_C_MyBase_CONSTRUCTOR__0();
TestApp_C_B* TestApp_C_B_CONSTRUCTOR__0();
TestApp_C_Program* TestApp_C_Program_CONSTRUCTOR__0();
TestApp_Blaa_A2* TestApp_C_Program_Foo__0();
System_Void TestApp_C_Program_Main__0();
TestApp_Blaa_A2* TestApp_C_Program_Foo__1(TestApp_C_Program* this, System_Int32 hi, System_String* by, System_String* by2);
System_Void TestApp_C_Program_Yahoo__0(TestApp_C_Program* this, TestApp_C_Program* p);

// =============
// Properties
// =============
TestClassE* TestIn_get_GetObjProp(TestIn* this)
{
	return this->obj;
}

System_Void TestIn_set_GetObjProp(TestIn* this, TestClassE* value)
{
	this->obj = value;
}

System_Int32 TestApp_C_Program_get_i2()
{
	return TestApp_C_Program__i2;
}

System_Void TestApp_C_Program_set_i2(System_Int32 value)
{
	TestApp_C_Program__i2 = value;
}

System_Single TestApp_C_Program_get_i6(TestApp_C_Program* this)
{
	return 1.1f;
}

System_Void TestApp_C_Program_set_i7(TestApp_C_Program* this, System_Single value)
{
	TestApp_C_Program_set_i2((System_Int32)value);
	this->i4 += this->i3;
	this->i4 += TestApp_C_Program_get_i6(this);
	this->i4 = TestApp_C_Program_get_i6(this) + this->i3;
	this->i4 = this->i3 + TestApp_C_Program_get_i6(this);
	TestApp_C_Program_set_i2((System_Int32)(TestApp_C_Program_get_i6(this) + this->i3));
	TestApp_C_Program_set_i2((System_Int32)(this->i3 + TestApp_C_Program_get_i6(this)));
}

// =============
// Methods
// =============
TestStruct TestStruct_CONSTRUCTOR__0(System_Double y)
{
	TestStruct this = {0};
	this.x = y;
	System_Int32 i = 1;
	if (y == 0) ;
	else if (y == 1) this.x = y;
	else if (y == 2)
	{
		if (true)
		{
			System_Int32 i3 = TestStruct_Foo__0(&this);
			System_Int32 i2 = 1;
			this.x = y + i + i2;
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
	{
		for (i = 0; i != 1; i++)
		{
			continue;
		}
		for (System_Int32 x2 = 0; x2 != 1; ++x2)
		{
			continue;
		}
	}
	for (System_Int32 x2 = 0; ; ++x2)
	{
		continue;
	}
	for (System_Single x2 = 0, y2 = 1; x2 != 1 && y2 != 0; ++x2, --y2)
	{
		continue;
	}
	System_Int32 x3;
	System_Int32 y3;
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
		case 33:
			i = 6;
			break;
		case 22:
			break;
		default:
			i = 9;
			break;
	}
	return this;
}

System_Int32 TestStruct_Foo__0(TestStruct* this)
{
	return 4;
}

TestStruct TestStruct_NewMe__0(TestStruct* this)
{
	return TestStruct_CONSTRUCTOR__1();
}

TestStruct TestStruct_CONSTRUCTOR__1()
{
	TestStruct this = {0};
	return this;
}

TestClassE* TestClassE_CONSTRUCTOR__0(System_Int32 key, TestIn* testIn)
{
	TestClassE* this = CS2X_GC_NewAtomic(sizeof(TestClassE));
	TestClassE_Get__0(TestIn_GetObj__0(TestIn_singleton))->key = 123;
	TestIn_set_GetObjProp(TestIn_singleton, null);
	this->key = TestClassE_Add__0(this, key);
	this->key = TestClassE_Add__0(this, this->key);
	this->key = TestClassE_AddStatic__0(TestClassE_Add__0(this, key));
	this->key = TestIn_Add__0(testIn, key);
	TestIn_SetMe__0(testIn, this);
	TestIn_SetMe__0(TestIn_singleton, this);
	return this;
}

System_Int32 TestClassE_Add__0(TestClassE* this, System_Int32 key)
{
	return key + 1;
}

System_Int32 TestClassE_AddStatic__0(System_Int32 key)
{
	return key + 1;
}

TestClassE* TestClassE_Get__0(TestClassE* this)
{
	return this;
	return TestClassE_CONSTRUCTOR__0(0, TestIn_CONSTRUCTOR__0());
}

System_Int32 TestIn_Add__0(TestIn* this, System_Int32 key)
{
	return key + 2;
}

System_Void TestIn_SetMe__0(TestIn* this, TestClassE* s)
{
}

TestClassE* TestIn_GetObj__0(TestIn* this)
{
	return this->obj;
}

TestIn* TestIn_CONSTRUCTOR__0()
{
	TestIn* this = CS2X_GC_New(sizeof(TestIn));
	return this;
}

System_Void MyPartial_Foo__0(MyPartial* this)
{
	System_Int32 i = 0;
}

MyPartial* MyPartial_CONSTRUCTOR__0()
{
	MyPartial* this = CS2X_GC_NewAtomic(sizeof(MyPartial));
	return this;
}

TestApp_Blaa_A2* TestApp_Blaa_A2_CONSTRUCTOR__0()
{
	TestApp_Blaa_A2* this = CS2X_GC_NewAtomic(sizeof(TestApp_Blaa_A2));
	return this;
}

TestApp_C_MyBase* TestApp_C_MyBase_CONSTRUCTOR__0()
{
	TestApp_C_MyBase* this = CS2X_GC_NewAtomic(sizeof(TestApp_C_MyBase));
	return this;
}

TestApp_C_B* TestApp_C_B_CONSTRUCTOR__0()
{
	TestApp_C_B* this = CS2X_GC_NewAtomic(sizeof(TestApp_C_B));
	return this;
}

TestApp_C_Program* TestApp_C_Program_CONSTRUCTOR__0()
{
	TestApp_C_Program* this = CS2X_GC_New(sizeof(TestApp_C_Program));
	this->b += 1;
	return this;
}

TestApp_Blaa_A2* TestApp_C_Program_Foo__0()
{
	System_Array* boo;
	System_String* myString = System_String_CONSTRUCTOR__0(L"Blaa!");
	System_Console_WriteLine__0(myString);
	return TestApp_C_Program_a;
}

System_Void TestApp_C_Program_Main__0()
{
	System_Console_WriteLine__0(System_String_CONSTRUCTOR__0(L"Hello World!"));
	TestApp_C_Program_i = 888;
	TestApp_C_Program_i = 999;
	TestApp_C_Program_i = 22;
	TestApp_C_Program_set_i2(0);
	TestApp_C_Program_set_i2(1);
	System_Int32 abc = 44;
	abc = 33;
}

TestApp_Blaa_A2* TestApp_C_Program_Foo__1(TestApp_C_Program* this, System_Int32 hi, System_String* by, System_String* by2)
{
	System_Int32 foo2 = hi + this->baseInt;
	foo2 = hi;
	foo2 = this->baseInt;
	return TestApp_C_Program_a;
}

System_Void TestApp_C_Program_Yahoo__0(TestApp_C_Program* this, TestApp_C_Program* p)
{
	System_Single val = TestApp_C_Program_get_i6(this);
}

// =============
// Entry Point
// =============
void main()
{
	CS2X_GC_Init();
	TestApp_C_Program_Main__0();
	CS2X_GC_Collect();
}
