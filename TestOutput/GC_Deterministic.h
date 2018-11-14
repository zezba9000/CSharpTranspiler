#pragma once
#include <stdlib.h>

void* GC_New(size_t size)
{
	return malloc(size);
}

void* GC_NewAtomic(size_t size)
{
	return malloc(size);
}

void* GC_Resize(void* object, size_t size)
{
	return realloc(object, size);
}

void GC_Delete(void* object)
{
	free(object);
}