// =============
// Library References
// =============
#include "CoreLib.h"
#include "TestLib.h"

// =============
// Type forward declares
// =============
enum TestApp_Blaa_MyEnum;
struct TestApp_Blaa_MyInterface;
struct TestApp_Blaa_A2;
struct TestApp_C_B;
struct TestApp_C_B_Program;

// =============
// Types Definitions
// =============
enum TestApp_Blaa_MyEnum
{
};

struct TestApp_Blaa_MyInterface
{
};

struct TestApp_Blaa_A2
{
};

struct TestApp_C_B
{
};

struct TestApp_C_B_Program
{
	TestApp_Blaa_A2* a;
	System_Int32 i;
	System_Int32 _i2;
};

// =============
// Property forward declares
// =============

// =============
// Method forward declares
// =============
System_Void TestApp_C_B_Program_Main(System_Array* args);
TestApp_Blaa_A2* TestApp_C_B_Program_Foo(TestApp_C_B_Program *this, System_Int32 hi, System_String* by);

// =============
// Properties
// =============

// =============
// Methods
// =============
System_Void TestApp_C_B_Program_Main(System_Array* args)
{
}

TestApp_Blaa_A2* TestApp_C_B_Program_Foo(TestApp_C_B_Program *this, System_Int32 hi, System_String* by)
{
}

