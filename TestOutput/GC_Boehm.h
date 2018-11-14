#pragma once
#include "boehm\include\gc.h"

void* GC_New(size_t size)
{
	return GC_malloc(size);
}

void* GC_NewAtomic(size_t size)
{
	return GC_malloc_atomic(size);
}

void* GC_Resize(void* object, size_t size)
{
	return GC_realloc(object, size);
}

void GC_Delete(void* object)
{
	GC_free(object);
}