#pragma once
#include "boehm\include\gc.h"

void CS2X_GC_Init()
{
	GC_INIT();
}

void CS2X_GC_Collect()
{
	GC_gcollect();
}

void* CS2X_GC_New(size_t size)
{
	return GC_malloc(size);
}

void* CS2X_GC_NewAtomic(size_t size)
{
	return GC_malloc_atomic(size);
}

void* CS2X_GC_Resize(void* object, size_t size)
{
	return GC_realloc(object, size);
}

void CS2X_GC_Delete(void* object)
{
	GC_free(object);
}