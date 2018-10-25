// =============
// Library References
// =============
#include "CoreLib.h"
#include "TestLib.h"

// =============
// Type forward declares
// =============
enum TestApp_Blaa_MyEnum;
enum TestApp_Blaa_MyEnumDefault;
struct TestApp_Blaa_MyInterface;
struct TestApp_Blaa_A2;
struct TestApp_C_MyBase;
struct TestApp_C_B;
struct TestApp_C_B_Program;

// =============
// Types Definitions
// =============
enum TestApp_Blaa_MyEnum : System_Int64
{
	A = 1,
	B,
	C = 4
};

enum TestApp_Blaa_MyEnumDefault
{
	A = 1,
	B,
	C = 4
};

struct TestApp_Blaa_MyInterface
{
};

struct TestApp_Blaa_A2
{
};

struct TestApp_C_MyBase
{
	System_Int32 b;
	System_Int32 baseInt;
};


struct TestApp_C_B
{
};

struct TestApp_C_B_Program
{
	System_Int32 b;
	System_Int32 baseInt;
};

TestApp_Blaa_A2* TestApp_C_B_Program_a;
System_Int32 TestApp_C_B_Program_i;
System_Int32 TestApp_C_B_Program__i2;

// =============
// Property forward declares
// =============
System_Int32 TestApp_C_B_Program_i2_get();
void TestApp_C_B_Program_i2_set(System_Int32 value);
System_Single TestApp_C_B_Program_i6_get(TestApp_C_B_Program* this);
void TestApp_C_B_Program_i7_set(TestApp_C_B_Program* this, System_Single value);

// =============
// Method forward declares
// =============
System_Void TestApp_C_B_Program_Main(System_Array* args);
TestApp_Blaa_A2* TestApp_C_B_Program_Foo(TestApp_C_B_Program* this, System_Int32 hi, System_String* by);

// =============
// Properties
// =============
System_Int32 TestApp_C_B_Program_i2_get()
{
	return TestApp_C_B_Program__i2;
}

void TestApp_C_B_Program_i2_set(System_Int32 value)
{
	TestApp_C_B_Program__i2 = value;
}

System_Single TestApp_C_B_Program_i6_get(TestApp_C_B_Program* this)
{
	return 1.1f;
}

void TestApp_C_B_Program_i7_set(TestApp_C_B_Program* this, System_Single value)
{
	TestApp_C_B_Program_i2_set((System_Int32)value);
}

// =============
// Methods
// =============
System_Void TestApp_C_B_Program_Main(System_Array* args)
{
	TestApp_C_B_Program_i = 888;
	TestApp_C_B_Program_i = 999;
	TestApp_C_B_Program_i = 22;
	System_Int32 abc = 44;
	abc = 33;
}

TestApp_Blaa_A2* TestApp_C_B_Program_Foo(TestApp_C_B_Program* this, System_Int32 hi, System_String* by)
{
	return TestApp_C_B_Program_a;
}

