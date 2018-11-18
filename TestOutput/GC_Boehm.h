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
	void* ptr = GC_malloc(size);
	memset(ptr, 0, size);
	return ptr;
}

void* CS2X_GC_NewAtomic(size_t size)
{
	void* ptr = GC_malloc_atomic(size);
	memset(ptr, 0, size);
	return ptr;
}

void* CS2X_GC_Resize(void* object, size_t oldSize, size_t newSize)
{
	__int8* ptr = GC_realloc(object, newSize);
	size_t sizeDiff = newSize - oldSize;
	if (sizeDiff > 0) memset(ptr + oldSize, 0, sizeDiff);
	return ptr;
}

void CS2X_GC_Delete(void* object)
{
	GC_free(object);
}