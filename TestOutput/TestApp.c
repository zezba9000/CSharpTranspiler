// =============
// Library References
// =============
#include "CoreLib.h"
#include "TestLib.h"

// =============
// Type forward declares
// =============
typedef EMPTY_OBJECT TestApp_Blaa_MyInterface;
typedef struct MyPartial MyPartial;
typedef EMPTY_OBJECT TestApp_Blaa_A2;
typedef struct TestApp_C_MyBase TestApp_C_MyBase;
typedef EMPTY_OBJECT TestApp_C_B;
typedef struct TestApp_C_Program TestApp_C_Program;

// =============
// Types Definitions
// =============
#define TestApp_Blaa_MyEnum_A 1
#define TestApp_Blaa_MyEnum_B 2
#define TestApp_Blaa_MyEnum_C 4

#define TestApp_Blaa_MyEnumDefault_A 1
#define TestApp_Blaa_MyEnumDefault_B 2
#define TestApp_Blaa_MyEnumDefault_C 4

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
System_Int32 TestApp_C_Program_get_i2();
System_Void TestApp_C_Program_set_i2(System_Int32 value);
System_Single TestApp_C_Program_get_i6(TestApp_C_Program* this);
System_Void TestApp_C_Program_set_i7(TestApp_C_Program* this, System_Single value);

// =============
// Method forward declares
// =============
System_Void MyPartial_Foo__0(MyPartial* this);
System_Void TestApp_C_Program_CONSTRUCTOR__0(TestApp_C_Program* this);
TestApp_Blaa_A2* TestApp_C_Program_Foo__0(TestApp_C_Program* this);
System_Void TestApp_C_Program_Main__0();
TestApp_Blaa_A2* TestApp_C_Program_Foo__1(TestApp_C_Program* this, System_Int32 hi, System_String* by, System_String* by2);
System_Void TestApp_C_Program_Yahoo__0(TestApp_C_Program* this, TestApp_C_Program* p);

// =============
// Properties
// =============
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
System_Void MyPartial_Foo__0(MyPartial* this)
{
	System_Int32 i = 0;
}

System_Void TestApp_C_Program_CONSTRUCTOR__0(TestApp_C_Program* this)
{
	this->b += 1;
}

TestApp_Blaa_A2* TestApp_C_Program_Foo__0(TestApp_C_Program* this)
{
	System_Array* boo;
	System_String* myString = L"Hello World!";
	System_Console_WriteLine(myString);
	return TestApp_C_Program_a;
}

System_Void TestApp_C_Program_Main__0()
{
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
	System_Single val = TestApp_C_Program_get_i6(p);
}

// =============
// Entry Point
// =============
void main()
{
	TestApp_C_Program_Main__0();
}
