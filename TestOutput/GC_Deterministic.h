#pragma once
#include <stdlib.h>

void CS2X_GC_Init()
{
	// TODO
}

void CS2X_GC_Collect()
{
	// TODO
}

void* CS2X_GC_New(size_t size)
{
	// TODO
	return malloc(size);
}

void* CS2X_GC_NewAtomic(size_t size)
{
	// TODO
	return malloc(size);
}

void* CS2X_GC_Resize(void* object, size_t size)
{
	// TODO
	return realloc(object, size);
}

void CS2X_GC_Delete(void* object)
{
	// TODO
	free(object);
}