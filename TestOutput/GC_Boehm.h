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

void CS2X_GC_DisableAutoCollections()
{
	/* boehm doesn't support this (do nothing...) */
}

void CS2X_GC_EnableAutoCollections()
{
	/* boehm doesn't support this (do nothing...) */
}

/* ====================================== */
/* manual allocation methods(non-GC heap) */
/* ====================================== */
void* CS2X_Malloc(size_t size)
{
	return malloc(size);
}

void CS2X_Delete(void* ptr)
{
	return free(ptr);
}