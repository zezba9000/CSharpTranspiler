#pragma once
#include <stdio.h>
#define EMPTY_OBJECT void*

// =============
// Library References
// =============
#include "CoreLib.h"

// =============
// Type forward declares
// =============
typedef struct TestLib_MyTestLibClass TestLib_MyTestLibClass;

// =============
// Types Definitions
// =============
struct TestLib_MyTestLibClass
{
	System_Int32 b;
};

// =============
// Property forward declares
// =============

// =============
// Method forward declares
// =============
TestLib_MyTestLibClass* TestLib_MyTestLibClass_CONSTRUCTOR__0();

// =============
// Properties
// =============
// =============
// Methods
// =============
TestLib_MyTestLibClass* TestLib_MyTestLibClass_CONSTRUCTOR__0()
{
	TestLib_MyTestLibClass* this = CS2X_GC_NewAtomic(sizeof(TestLib_MyTestLibClass));
	return this;
}

