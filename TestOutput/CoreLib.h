#pragma once
#include <stdio.h>
#include "GC_Boehm.h"
#define null 0
#define true 1
#define false 0
#define EMPTY_OBJECT void*

// =============
// Library References
// =============

// =============
// Type forward declares
// =============
typedef EMPTY_OBJECT System_IDisposable;
typedef EMPTY_OBJECT System_Collections_ICollection;
typedef EMPTY_OBJECT System_Collections_IEnumerable;
typedef EMPTY_OBJECT System_Collections_IEnumerator;
typedef EMPTY_OBJECT System_ComponentModel_IContainer;
typedef EMPTY_OBJECT System_Boolean;
typedef unsigned __int8 System_Byte;
typedef wchar_t System_Char;
typedef double System_Double;
typedef __int16 System_Int16;
typedef __int32 System_Int32;
typedef __int64 System_Int64;
typedef struct System_IntPtr System_IntPtr;
typedef EMPTY_OBJECT System_RuntimeFieldHandle;
typedef EMPTY_OBJECT System_RuntimeTypeHandle;
typedef __int8 System_SByte;
typedef float System_Single;
typedef unsigned __int16 System_UInt16;
typedef unsigned __int32 System_UInt32;
typedef unsigned __int64 System_UInt64;
typedef struct System_UIntPtr System_UIntPtr;
typedef void System_Void;
typedef struct System_Collections_DictionaryEntry System_Collections_DictionaryEntry;
typedef EMPTY_OBJECT System_Attribute;
typedef struct System_AttributeUsageAttribute System_AttributeUsageAttribute;
typedef EMPTY_OBJECT System_Buffer;
typedef struct System_CLSCompliantAttribute System_CLSCompliantAttribute;
typedef EMPTY_OBJECT System_Console;
typedef EMPTY_OBJECT System_FlagsAttribute;
typedef EMPTY_OBJECT System_GC;
typedef struct System_Array System_Array;
typedef EMPTY_OBJECT System_CancelEventArgs;
typedef EMPTY_OBJECT System_Delegate;
typedef EMPTY_OBJECT System_Enum;
typedef EMPTY_OBJECT System_EventArgs;
typedef EMPTY_OBJECT System_Exception;
typedef EMPTY_OBJECT System_Math;
typedef EMPTY_OBJECT System_MulticastDelegate;
typedef EMPTY_OBJECT System_Nullable;
typedef EMPTY_OBJECT System_Object;
typedef struct System_ObsoleteAttribute System_ObsoleteAttribute;
typedef EMPTY_OBJECT System_ParamArrayAttribute;
typedef struct System_String System_String;
typedef EMPTY_OBJECT System_StringBuilder;
typedef EMPTY_OBJECT System_Type;
typedef EMPTY_OBJECT System_ValueType;
typedef struct System_CodeDom_Compiler_GeneratedCodeAttribute System_CodeDom_Compiler_GeneratedCodeAttribute;
typedef EMPTY_OBJECT System_Collections_ArrayList;
typedef EMPTY_OBJECT System_Collections_Queue;
typedef EMPTY_OBJECT System_Collections_Stack;
typedef EMPTY_OBJECT System_ComponentModel_BrowsableAttribute;
typedef EMPTY_OBJECT System_ComponentModel_DependencyAttribute;
typedef struct System_ComponentModel_EditorBrowsableAttribute System_ComponentModel_EditorBrowsableAttribute;
typedef struct System_CS2X_NativeNameAttribute System_CS2X_NativeNameAttribute;
typedef struct System_Diagnostics_ConditionalAttribute System_Diagnostics_ConditionalAttribute;
typedef EMPTY_OBJECT System_Diagnostics_Debug;
typedef EMPTY_OBJECT System_Diagnostics_CodeAnalysis_SuppressMessageAttribute;
typedef EMPTY_OBJECT System_Globalization_CultureInfo;
typedef EMPTY_OBJECT System_Globalization_NumberFormatInfo;
typedef struct System_Reflection_AssemblyCompanyAttribute System_Reflection_AssemblyCompanyAttribute;
typedef struct System_Reflection_AssemblyConfigurationAttribute System_Reflection_AssemblyConfigurationAttribute;
typedef struct System_Reflection_AssemblyCopyrightAttribute System_Reflection_AssemblyCopyrightAttribute;
typedef struct System_Reflection_AssemblyCultureAttribute System_Reflection_AssemblyCultureAttribute;
typedef struct System_Reflection_AssemblyDelaySignAttribute System_Reflection_AssemblyDelaySignAttribute;
typedef struct System_Reflection_AssemblyDescriptionAttribute System_Reflection_AssemblyDescriptionAttribute;
typedef struct System_Reflection_AssemblyFileVersionAttribute System_Reflection_AssemblyFileVersionAttribute;
typedef struct System_Reflection_AssemblyInformationalVersionAttribute System_Reflection_AssemblyInformationalVersionAttribute;
typedef struct System_Reflection_AssemblyKeyFileAttribute System_Reflection_AssemblyKeyFileAttribute;
typedef struct System_Reflection_AssemblyProductAttribute System_Reflection_AssemblyProductAttribute;
typedef struct System_Reflection_AssemblyTitleAttribute System_Reflection_AssemblyTitleAttribute;
typedef struct System_Reflection_AssemblyTrademarkAttribute System_Reflection_AssemblyTrademarkAttribute;
typedef struct System_Reflection_AssemblyVersionAttribute System_Reflection_AssemblyVersionAttribute;
typedef struct System_Reflection_DefaultMemberAttribute System_Reflection_DefaultMemberAttribute;
typedef EMPTY_OBJECT System_Runtime_CompilerServices_CompilerGeneratedAttribute;
typedef EMPTY_OBJECT System_Runtime_CompilerServices_RuntimeHelpers;
typedef EMPTY_OBJECT System_Runtime_InteropServices_OutAttribute;

// =============
// Types Definitions
// =============
#define System_AttributeTargets_Assembly 1
#define System_AttributeTargets_Module 2
#define System_AttributeTargets_Class 4
#define System_AttributeTargets_Struct 8
#define System_AttributeTargets_Enum 16
#define System_AttributeTargets_Constructor 32
#define System_AttributeTargets_Method 64
#define System_AttributeTargets_Property 128
#define System_AttributeTargets_Field 256
#define System_AttributeTargets_Event 512
#define System_AttributeTargets_Interface 1024
#define System_AttributeTargets_Parameter 2048
#define System_AttributeTargets_Delegate 4096
#define System_AttributeTargets_ReturnValue 8192
#define System_AttributeTargets_GenericParameter 16384
#define System_AttributeTargets_All 32767

#define System_ComponentModel_EditorBrowsableState_Always 0
#define System_ComponentModel_EditorBrowsableState_Never 1
#define System_ComponentModel_EditorBrowsableState_Advanced 2

#define System_CS2X_NativeTargets_C 0

struct System_IntPtr
{
	System_Void* ptr;
};

struct System_UIntPtr
{
	System_Void* ptr;
};

struct System_Collections_DictionaryEntry
{
	System_Object* Key;
	System_Object* Value;
};

struct System_AttributeUsageAttribute
{
	System_Int32 _attributeTarget;
	System_Boolean AllowMultiple;
	System_Boolean Inherited;
};

struct System_CLSCompliantAttribute
{
	System_Boolean _isCompliant;
};

struct System_Array
{
	System_Void* buffer;
};

struct System_ObsoleteAttribute
{
	System_Boolean _error;
	System_String* _message;
};

struct System_String
{
	System_Int32 Length;
	System_Char* buffer;
};

struct System_CodeDom_Compiler_GeneratedCodeAttribute
{
	System_String* _tool;
	System_String* _version;
};

struct System_ComponentModel_EditorBrowsableAttribute
{
	System_Int32 _browsableState;
};

struct System_CS2X_NativeNameAttribute
{
	System_Int32 Target;
	System_String* Value;
};

struct System_Diagnostics_ConditionalAttribute
{
	System_String* _conditionString;
};

struct System_Reflection_AssemblyCompanyAttribute
{
	System_String* _company;
};

struct System_Reflection_AssemblyConfigurationAttribute
{
	System_String* _configuration;
};

struct System_Reflection_AssemblyCopyrightAttribute
{
	System_String* _copyright;
};

struct System_Reflection_AssemblyCultureAttribute
{
	System_String* _culture;
};

struct System_Reflection_AssemblyDelaySignAttribute
{
	System_Boolean _delaySign;
};

struct System_Reflection_AssemblyDescriptionAttribute
{
	System_String* _description;
};

struct System_Reflection_AssemblyFileVersionAttribute
{
	System_String* _version;
};

struct System_Reflection_AssemblyInformationalVersionAttribute
{
	System_String* _informationalVersion;
};

struct System_Reflection_AssemblyKeyFileAttribute
{
	System_String* _keyFile;
};

struct System_Reflection_AssemblyProductAttribute
{
	System_String* _product;
};

struct System_Reflection_AssemblyTitleAttribute
{
	System_String* _title;
};

struct System_Reflection_AssemblyTrademarkAttribute
{
	System_String* _trademark;
};

struct System_Reflection_AssemblyVersionAttribute
{
	System_String* _version;
};

struct System_Reflection_DefaultMemberAttribute
{
	System_String* _memberName;
};

// =============
// Property forward declares
// =============
System_Int32 System_AttributeUsageAttribute_get_ValidOn(System_AttributeUsageAttribute* this);
System_Boolean System_CLSCompliantAttribute_get_IsCompliant(System_CLSCompliantAttribute* this);
System_Int32 System_Array_get_Length(System_Array* this);
System_Exception* System_Exception_get_InnerException(System_Exception* this);
System_String* System_Exception_get_Message(System_Exception* this);
System_String* System_Exception_get_StackTrace(System_Exception* this);
System_Boolean System_ObsoleteAttribute_get_IsError(System_ObsoleteAttribute* this);
System_String* System_ObsoleteAttribute_get_Message(System_ObsoleteAttribute* this);
System_Type* System_Type_get_BaseType(System_Type* this);
System_String* System_Type_get_Name(System_Type* this);
System_String* System_CodeDom_Compiler_GeneratedCodeAttribute_get_Tool(System_CodeDom_Compiler_GeneratedCodeAttribute* this);
System_String* System_CodeDom_Compiler_GeneratedCodeAttribute_get_Version(System_CodeDom_Compiler_GeneratedCodeAttribute* this);
System_Int32 System_Collections_ArrayList_get_Count(System_Collections_ArrayList* this);
System_Int32 System_Collections_Queue_get_Count(System_Collections_Queue* this);
System_Int32 System_Collections_Stack_get_Count(System_Collections_Stack* this);
System_Int32 System_ComponentModel_EditorBrowsableAttribute_get_State(System_ComponentModel_EditorBrowsableAttribute* this);
System_String* System_Diagnostics_ConditionalAttribute_get_ConditionString(System_Diagnostics_ConditionalAttribute* this);
System_String* System_Reflection_AssemblyCompanyAttribute_get_Company(System_Reflection_AssemblyCompanyAttribute* this);
System_String* System_Reflection_AssemblyConfigurationAttribute_get_Configuration(System_Reflection_AssemblyConfigurationAttribute* this);
System_String* System_Reflection_AssemblyCopyrightAttribute_get_Copyright(System_Reflection_AssemblyCopyrightAttribute* this);
System_String* System_Reflection_AssemblyCultureAttribute_get_Culture(System_Reflection_AssemblyCultureAttribute* this);
System_Boolean System_Reflection_AssemblyDelaySignAttribute_get_DelaySign(System_Reflection_AssemblyDelaySignAttribute* this);
System_String* System_Reflection_AssemblyDescriptionAttribute_get_Description(System_Reflection_AssemblyDescriptionAttribute* this);
System_String* System_Reflection_AssemblyFileVersionAttribute_get_Version(System_Reflection_AssemblyFileVersionAttribute* this);
System_String* System_Reflection_AssemblyInformationalVersionAttribute_get_InformationalVersion(System_Reflection_AssemblyInformationalVersionAttribute* this);
System_String* System_Reflection_AssemblyKeyFileAttribute_get_KeyFile(System_Reflection_AssemblyKeyFileAttribute* this);
System_String* System_Reflection_AssemblyProductAttribute_get_Product(System_Reflection_AssemblyProductAttribute* this);
System_String* System_Reflection_AssemblyTitleAttribute_get_Title(System_Reflection_AssemblyTitleAttribute* this);
System_String* System_Reflection_AssemblyTrademarkAttribute_get_Trademark(System_Reflection_AssemblyTrademarkAttribute* this);
System_String* System_Reflection_AssemblyVersionAttribute_get_Version(System_Reflection_AssemblyVersionAttribute* this);
System_String* System_Reflection_DefaultMemberAttribute_get_MemberName(System_Reflection_DefaultMemberAttribute* this);

// =============
// Method forward declares
// =============
System_Boolean System_Boolean_CONSTRUCTOR__0();
System_Byte System_Byte_CONSTRUCTOR__0();
System_Char System_Char_CONSTRUCTOR__0();
System_Double System_Double_CONSTRUCTOR__0();
System_Int16 System_Int16_CONSTRUCTOR__0();
System_Int32 System_Int32_CONSTRUCTOR__0();
System_Int64 System_Int64_CONSTRUCTOR__0();
System_IntPtr System_IntPtr_CONSTRUCTOR__0(System_Int32 value);
System_IntPtr System_IntPtr_op_Explicit__0(System_Int32 value);
System_Int32 System_IntPtr_op_Explicit__1(System_IntPtr value);
System_Int32 System_IntPtr_ToInt32__0(System_IntPtr* this);
System_IntPtr System_IntPtr_CONSTRUCTOR__1();
System_RuntimeFieldHandle System_RuntimeFieldHandle_CONSTRUCTOR__0();
System_RuntimeTypeHandle System_RuntimeTypeHandle_CONSTRUCTOR__0();
System_SByte System_SByte_CONSTRUCTOR__0();
System_Single System_Single_CONSTRUCTOR__0();
System_UInt16 System_UInt16_CONSTRUCTOR__0();
System_UInt32 System_UInt32_CONSTRUCTOR__0();
System_UInt64 System_UInt64_CONSTRUCTOR__0();
System_UIntPtr System_UIntPtr_CONSTRUCTOR__0(System_UInt32 value);
System_UIntPtr System_UIntPtr_op_Explicit__0(System_UInt32 value);
System_UInt32 System_UIntPtr_op_Explicit__1(System_UIntPtr value);
System_UInt32 System_UIntPtr_ToUInt32__0(System_UIntPtr* this);
System_UIntPtr System_UIntPtr_CONSTRUCTOR__1();
System_Collections_DictionaryEntry System_Collections_DictionaryEntry_CONSTRUCTOR__0(System_Object* key, System_Object* value);
System_Collections_DictionaryEntry System_Collections_DictionaryEntry_CONSTRUCTOR__1();
System_Attribute* System_Attribute_CONSTRUCTOR__0();
System_AttributeUsageAttribute* System_AttributeUsageAttribute_CONSTRUCTOR__0(System_Int32 validOn);
System_Void System_Buffer_BlockCopy__0(System_Array* src, System_Int32 srcOffset, System_Array* dst, System_Int32 dstOffset, System_Int32 count);
System_CLSCompliantAttribute* System_CLSCompliantAttribute_CONSTRUCTOR__0(System_Boolean isCompliant);
System_Void System_Console_WriteLine__0(System_String* value);
System_FlagsAttribute* System_FlagsAttribute_CONSTRUCTOR__0();
System_Collections_IEnumerator* System_Array_GetEnumerator__0(System_Array* this);
System_Array* System_Array_CONSTRUCTOR__0();
System_CancelEventArgs* System_CancelEventArgs_CONSTRUCTOR__0();
System_Delegate* System_Delegate_CONSTRUCTOR__0();
System_Enum* System_Enum_CONSTRUCTOR__0();
System_EventArgs* System_EventArgs_CONSTRUCTOR__0();
System_Exception* System_Exception_CONSTRUCTOR__0();
System_Exception* System_Exception_CONSTRUCTOR__1(System_String* message);
System_MulticastDelegate* System_MulticastDelegate_CONSTRUCTOR__0();
System_Type* System_Object_GetType__0(System_Object* this);
System_String* System_Object_ToString__0(System_Object* this);
System_Object* System_Object_CONSTRUCTOR__0();
System_ObsoleteAttribute* System_ObsoleteAttribute_CONSTRUCTOR__0();
System_ObsoleteAttribute* System_ObsoleteAttribute_CONSTRUCTOR__1(System_String* message);
System_ObsoleteAttribute* System_ObsoleteAttribute_CONSTRUCTOR__2(System_String* message, System_Boolean error);
System_ParamArrayAttribute* System_ParamArrayAttribute_CONSTRUCTOR__0();
System_String* System_String_CONSTRUCTOR__0(System_Char* value);
System_Void System_String_Finalize(System_String* this, void* data);
System_StringBuilder* System_StringBuilder_CONSTRUCTOR__0();
System_Type* System_Type_CONSTRUCTOR__0();
System_ValueType* System_ValueType_CONSTRUCTOR__0();
System_CodeDom_Compiler_GeneratedCodeAttribute* System_CodeDom_Compiler_GeneratedCodeAttribute_CONSTRUCTOR__0(System_String* tool, System_String* version);
System_Collections_IEnumerator* System_Collections_ArrayList_GetEnumerator__0(System_Collections_ArrayList* this);
System_Collections_ArrayList* System_Collections_ArrayList_CONSTRUCTOR__0();
System_Collections_IEnumerator* System_Collections_Queue_GetEnumerator__0(System_Collections_Queue* this);
System_Collections_Queue* System_Collections_Queue_CONSTRUCTOR__0();
System_Collections_IEnumerator* System_Collections_Stack_GetEnumerator__0(System_Collections_Stack* this);
System_Collections_Stack* System_Collections_Stack_CONSTRUCTOR__0();
System_ComponentModel_BrowsableAttribute* System_ComponentModel_BrowsableAttribute_CONSTRUCTOR__0();
System_ComponentModel_DependencyAttribute* System_ComponentModel_DependencyAttribute_CONSTRUCTOR__0();
System_ComponentModel_EditorBrowsableAttribute* System_ComponentModel_EditorBrowsableAttribute_CONSTRUCTOR__0(System_Int32 state);
System_CS2X_NativeNameAttribute* System_CS2X_NativeNameAttribute_CONSTRUCTOR__0(System_Int32 target, System_String* value);
System_Diagnostics_ConditionalAttribute* System_Diagnostics_ConditionalAttribute_CONSTRUCTOR__0(System_String* conditionString);
System_Diagnostics_Debug* System_Diagnostics_Debug_CONSTRUCTOR__0();
System_Diagnostics_CodeAnalysis_SuppressMessageAttribute* System_Diagnostics_CodeAnalysis_SuppressMessageAttribute_CONSTRUCTOR__0();
System_Globalization_CultureInfo* System_Globalization_CultureInfo_CONSTRUCTOR__0();
System_Globalization_NumberFormatInfo* System_Globalization_NumberFormatInfo_CONSTRUCTOR__0();
System_Reflection_AssemblyCompanyAttribute* System_Reflection_AssemblyCompanyAttribute_CONSTRUCTOR__0(System_String* company);
System_Reflection_AssemblyConfigurationAttribute* System_Reflection_AssemblyConfigurationAttribute_CONSTRUCTOR__0(System_String* configuration);
System_Reflection_AssemblyCopyrightAttribute* System_Reflection_AssemblyCopyrightAttribute_CONSTRUCTOR__0(System_String* copyright);
System_Reflection_AssemblyCultureAttribute* System_Reflection_AssemblyCultureAttribute_CONSTRUCTOR__0(System_String* culture);
System_Reflection_AssemblyDelaySignAttribute* System_Reflection_AssemblyDelaySignAttribute_CONSTRUCTOR__0(System_Boolean delaySign);
System_Reflection_AssemblyDescriptionAttribute* System_Reflection_AssemblyDescriptionAttribute_CONSTRUCTOR__0(System_String* description);
System_Reflection_AssemblyFileVersionAttribute* System_Reflection_AssemblyFileVersionAttribute_CONSTRUCTOR__0(System_String* version);
System_Reflection_AssemblyInformationalVersionAttribute* System_Reflection_AssemblyInformationalVersionAttribute_CONSTRUCTOR__0(System_String* informationalVersion);
System_Reflection_AssemblyKeyFileAttribute* System_Reflection_AssemblyKeyFileAttribute_CONSTRUCTOR__0(System_String* keyFile);
System_Reflection_AssemblyProductAttribute* System_Reflection_AssemblyProductAttribute_CONSTRUCTOR__0(System_String* product);
System_Reflection_AssemblyTitleAttribute* System_Reflection_AssemblyTitleAttribute_CONSTRUCTOR__0(System_String* title);
System_Reflection_AssemblyTrademarkAttribute* System_Reflection_AssemblyTrademarkAttribute_CONSTRUCTOR__0(System_String* trademark);
System_Reflection_AssemblyVersionAttribute* System_Reflection_AssemblyVersionAttribute_CONSTRUCTOR__0(System_String* version);
System_Reflection_DefaultMemberAttribute* System_Reflection_DefaultMemberAttribute_CONSTRUCTOR__0(System_String* memberName);
System_Runtime_CompilerServices_CompilerGeneratedAttribute* System_Runtime_CompilerServices_CompilerGeneratedAttribute_CONSTRUCTOR__0();
System_Runtime_InteropServices_OutAttribute* System_Runtime_InteropServices_OutAttribute_CONSTRUCTOR__0();

// =============
// Properties
// =============
System_Int32 System_AttributeUsageAttribute_get_ValidOn(System_AttributeUsageAttribute* this)
{
	return this->_attributeTarget;
}

System_Boolean System_CLSCompliantAttribute_get_IsCompliant(System_CLSCompliantAttribute* this)
{
	return this->_isCompliant;
}

System_Int32 System_Array_get_Length(System_Array* this)
{
	return 0;
}

System_Exception* System_Exception_get_InnerException(System_Exception* this)
{
	return null;
}

System_String* System_Exception_get_Message(System_Exception* this)
{
	return null;
}

System_String* System_Exception_get_StackTrace(System_Exception* this)
{
	return null;
}

System_Boolean System_ObsoleteAttribute_get_IsError(System_ObsoleteAttribute* this)
{
	return this->_error;
}

System_String* System_ObsoleteAttribute_get_Message(System_ObsoleteAttribute* this)
{
	return this->_message;
}

System_Type* System_Type_get_BaseType(System_Type* this)
{
	return null;
}

System_String* System_Type_get_Name(System_Type* this)
{
	return null;
}

System_String* System_CodeDom_Compiler_GeneratedCodeAttribute_get_Tool(System_CodeDom_Compiler_GeneratedCodeAttribute* this)
{
	return this->_tool;
}

System_String* System_CodeDom_Compiler_GeneratedCodeAttribute_get_Version(System_CodeDom_Compiler_GeneratedCodeAttribute* this)
{
	return this->_version;
}

System_Int32 System_Collections_ArrayList_get_Count(System_Collections_ArrayList* this)
{
	return 0;
}

System_Int32 System_Collections_Queue_get_Count(System_Collections_Queue* this)
{
	return 0;
}

System_Int32 System_Collections_Stack_get_Count(System_Collections_Stack* this)
{
	return 0;
}

System_Int32 System_ComponentModel_EditorBrowsableAttribute_get_State(System_ComponentModel_EditorBrowsableAttribute* this)
{
	return this->_browsableState;
}

System_String* System_Diagnostics_ConditionalAttribute_get_ConditionString(System_Diagnostics_ConditionalAttribute* this)
{
	return this->_conditionString;
}

System_String* System_Reflection_AssemblyCompanyAttribute_get_Company(System_Reflection_AssemblyCompanyAttribute* this)
{
	return this->_company;
}

System_String* System_Reflection_AssemblyConfigurationAttribute_get_Configuration(System_Reflection_AssemblyConfigurationAttribute* this)
{
	return this->_configuration;
}

System_String* System_Reflection_AssemblyCopyrightAttribute_get_Copyright(System_Reflection_AssemblyCopyrightAttribute* this)
{
	return this->_copyright;
}

System_String* System_Reflection_AssemblyCultureAttribute_get_Culture(System_Reflection_AssemblyCultureAttribute* this)
{
	return this->_culture;
}

System_Boolean System_Reflection_AssemblyDelaySignAttribute_get_DelaySign(System_Reflection_AssemblyDelaySignAttribute* this)
{
	return this->_delaySign;
}

System_String* System_Reflection_AssemblyDescriptionAttribute_get_Description(System_Reflection_AssemblyDescriptionAttribute* this)
{
	return this->_description;
}

System_String* System_Reflection_AssemblyFileVersionAttribute_get_Version(System_Reflection_AssemblyFileVersionAttribute* this)
{
	return this->_version;
}

System_String* System_Reflection_AssemblyInformationalVersionAttribute_get_InformationalVersion(System_Reflection_AssemblyInformationalVersionAttribute* this)
{
	return this->_informationalVersion;
}

System_String* System_Reflection_AssemblyKeyFileAttribute_get_KeyFile(System_Reflection_AssemblyKeyFileAttribute* this)
{
	return this->_keyFile;
}

System_String* System_Reflection_AssemblyProductAttribute_get_Product(System_Reflection_AssemblyProductAttribute* this)
{
	return this->_product;
}

System_String* System_Reflection_AssemblyTitleAttribute_get_Title(System_Reflection_AssemblyTitleAttribute* this)
{
	return this->_title;
}

System_String* System_Reflection_AssemblyTrademarkAttribute_get_Trademark(System_Reflection_AssemblyTrademarkAttribute* this)
{
	return this->_trademark;
}

System_String* System_Reflection_AssemblyVersionAttribute_get_Version(System_Reflection_AssemblyVersionAttribute* this)
{
	return this->_version;
}

System_String* System_Reflection_DefaultMemberAttribute_get_MemberName(System_Reflection_DefaultMemberAttribute* this)
{
	return this->_memberName;
}

// =============
// Methods
// =============
System_Boolean System_Boolean_CONSTRUCTOR__0()
{
	System_Boolean this = {0};
	return this;
}

System_Byte System_Byte_CONSTRUCTOR__0()
{
	System_Byte this = {0};
	return this;
}

System_Char System_Char_CONSTRUCTOR__0()
{
	System_Char this = {0};
	return this;
}

System_Double System_Double_CONSTRUCTOR__0()
{
	System_Double this = {0};
	return this;
}

System_Int16 System_Int16_CONSTRUCTOR__0()
{
	System_Int16 this = {0};
	return this;
}

System_Int32 System_Int32_CONSTRUCTOR__0()
{
	System_Int32 this = {0};
	return this;
}

System_Int64 System_Int64_CONSTRUCTOR__0()
{
	System_Int64 this = {0};
	return this;
}

System_IntPtr System_IntPtr_CONSTRUCTOR__0(System_Int32 value)
{
	System_IntPtr this = {0};
	this.ptr = (System_Void*)value;
	return this;
}

System_IntPtr System_IntPtr_op_Explicit__0(System_Int32 value)
{
}

System_Int32 System_IntPtr_op_Explicit__1(System_IntPtr value)
{
}

System_Int32 System_IntPtr_ToInt32__0(System_IntPtr* this)
{
	return (System_Int32)this;
}

System_IntPtr System_IntPtr_CONSTRUCTOR__1()
{
	System_IntPtr this = {0};
	return this;
}

System_RuntimeFieldHandle System_RuntimeFieldHandle_CONSTRUCTOR__0()
{
	System_RuntimeFieldHandle this = {0};
	return this;
}

System_RuntimeTypeHandle System_RuntimeTypeHandle_CONSTRUCTOR__0()
{
	System_RuntimeTypeHandle this = {0};
	return this;
}

System_SByte System_SByte_CONSTRUCTOR__0()
{
	System_SByte this = {0};
	return this;
}

System_Single System_Single_CONSTRUCTOR__0()
{
	System_Single this = {0};
	return this;
}

System_UInt16 System_UInt16_CONSTRUCTOR__0()
{
	System_UInt16 this = {0};
	return this;
}

System_UInt32 System_UInt32_CONSTRUCTOR__0()
{
	System_UInt32 this = {0};
	return this;
}

System_UInt64 System_UInt64_CONSTRUCTOR__0()
{
	System_UInt64 this = {0};
	return this;
}

System_UIntPtr System_UIntPtr_CONSTRUCTOR__0(System_UInt32 value)
{
	System_UIntPtr this = {0};
	this.ptr = (System_Void*)value;
	return this;
}

System_UIntPtr System_UIntPtr_op_Explicit__0(System_UInt32 value)
{
}

System_UInt32 System_UIntPtr_op_Explicit__1(System_UIntPtr value)
{
}

System_UInt32 System_UIntPtr_ToUInt32__0(System_UIntPtr* this)
{
	return (System_UInt32)this;
}

System_UIntPtr System_UIntPtr_CONSTRUCTOR__1()
{
	System_UIntPtr this = {0};
	return this;
}

System_Collections_DictionaryEntry System_Collections_DictionaryEntry_CONSTRUCTOR__0(System_Object* key, System_Object* value)
{
	System_Collections_DictionaryEntry this = {0};
	this.Key = key;
	this.Value = value;
	return this;
}

System_Collections_DictionaryEntry System_Collections_DictionaryEntry_CONSTRUCTOR__1()
{
	System_Collections_DictionaryEntry this = {0};
	return this;
}

System_Attribute* System_Attribute_CONSTRUCTOR__0()
{
	System_Attribute* this = CS2X_GC_New(sizeof(System_Attribute));
	return this;
}

System_AttributeUsageAttribute* System_AttributeUsageAttribute_CONSTRUCTOR__0(System_Int32 validOn)
{
	System_AttributeUsageAttribute* this = CS2X_GC_New(sizeof(System_AttributeUsageAttribute));
	this->_attributeTarget = validOn;
	return this;
}

System_Void System_Buffer_BlockCopy__0(System_Array* src, System_Int32 srcOffset, System_Array* dst, System_Int32 dstOffset, System_Int32 count)
{
}

System_CLSCompliantAttribute* System_CLSCompliantAttribute_CONSTRUCTOR__0(System_Boolean isCompliant)
{
	System_CLSCompliantAttribute* this = CS2X_GC_New(sizeof(System_CLSCompliantAttribute));
	this->_isCompliant = isCompliant;
	return this;
}

System_Void System_Console_WriteLine__0(System_String* value)
{
	wprintf(value->buffer);
}

System_FlagsAttribute* System_FlagsAttribute_CONSTRUCTOR__0()
{
	System_FlagsAttribute* this = CS2X_GC_New(sizeof(System_FlagsAttribute));
	return this;
}

System_Collections_IEnumerator* System_Array_GetEnumerator__0(System_Array* this)
{
	return null;
}

System_Array* System_Array_CONSTRUCTOR__0()
{
	System_Array* this = CS2X_GC_New(sizeof(System_Array));
	return this;
}

System_CancelEventArgs* System_CancelEventArgs_CONSTRUCTOR__0()
{
	System_CancelEventArgs* this = CS2X_GC_New(sizeof(System_CancelEventArgs));
	return this;
}

System_Delegate* System_Delegate_CONSTRUCTOR__0()
{
	System_Delegate* this = CS2X_GC_New(sizeof(System_Delegate));
	return this;
}

System_Enum* System_Enum_CONSTRUCTOR__0()
{
	System_Enum* this = CS2X_GC_New(sizeof(System_Enum));
	return this;
}

System_EventArgs* System_EventArgs_CONSTRUCTOR__0()
{
	System_EventArgs* this = CS2X_GC_New(sizeof(System_EventArgs));
	return this;
}

System_Exception* System_Exception_CONSTRUCTOR__0()
{
	System_Exception* this = CS2X_GC_New(sizeof(System_Exception));
	return this;
}

System_Exception* System_Exception_CONSTRUCTOR__1(System_String* message)
{
	System_Exception* this = CS2X_GC_New(sizeof(System_Exception));
	return this;
}

System_MulticastDelegate* System_MulticastDelegate_CONSTRUCTOR__0()
{
	System_MulticastDelegate* this = CS2X_GC_New(sizeof(System_MulticastDelegate));
	return this;
}

System_Type* System_Object_GetType__0(System_Object* this)
{
	return null;
}

System_String* System_Object_ToString__0(System_Object* this)
{
	return null;
}

System_Object* System_Object_CONSTRUCTOR__0()
{
	System_Object* this = CS2X_GC_New(sizeof(System_Object));
	return this;
}

System_ObsoleteAttribute* System_ObsoleteAttribute_CONSTRUCTOR__0()
{
	System_ObsoleteAttribute* this = CS2X_GC_New(sizeof(System_ObsoleteAttribute));
	return this;
}

System_ObsoleteAttribute* System_ObsoleteAttribute_CONSTRUCTOR__1(System_String* message)
{
	System_ObsoleteAttribute* this = CS2X_GC_New(sizeof(System_ObsoleteAttribute));
	this->_message = message;
	return this;
}

System_ObsoleteAttribute* System_ObsoleteAttribute_CONSTRUCTOR__2(System_String* message, System_Boolean error)
{
	System_ObsoleteAttribute* this = CS2X_GC_New(sizeof(System_ObsoleteAttribute));
	this->_message = message;
	this->_error = error;
	return this;
}

System_ParamArrayAttribute* System_ParamArrayAttribute_CONSTRUCTOR__0()
{
	System_ParamArrayAttribute* this = CS2X_GC_New(sizeof(System_ParamArrayAttribute));
	return this;
}

System_String* System_String_CONSTRUCTOR__0(System_Char* value)
{
	System_String* this = CS2X_GC_New(sizeof(System_String));
	GC_register_finalizer(this, System_String_Finalize, 0, 0, 0);
	this->Length = wcslen(value);
	System_Void* size = (System_Void*)(this->Length * sizeof(System_Char));
	this->buffer = (System_Char*)CS2X_Malloc(size);
	memcpy(this->buffer, value, size);
	return this;
}

System_Void System_String_Finalize(System_String* this, void* data)
{
	CS2X_Delete(this->buffer);
	this->buffer = null;
}

System_StringBuilder* System_StringBuilder_CONSTRUCTOR__0()
{
	System_StringBuilder* this = CS2X_GC_New(sizeof(System_StringBuilder));
	return this;
}

System_Type* System_Type_CONSTRUCTOR__0()
{
	System_Type* this = CS2X_GC_New(sizeof(System_Type));
	return this;
}

System_ValueType* System_ValueType_CONSTRUCTOR__0()
{
	System_ValueType* this = CS2X_GC_New(sizeof(System_ValueType));
	return this;
}

System_CodeDom_Compiler_GeneratedCodeAttribute* System_CodeDom_Compiler_GeneratedCodeAttribute_CONSTRUCTOR__0(System_String* tool, System_String* version)
{
	System_CodeDom_Compiler_GeneratedCodeAttribute* this = CS2X_GC_New(sizeof(System_CodeDom_Compiler_GeneratedCodeAttribute));
	this->_tool = tool;
	this->_version = version;
	return this;
}

System_Collections_IEnumerator* System_Collections_ArrayList_GetEnumerator__0(System_Collections_ArrayList* this)
{
	return null;
}

System_Collections_ArrayList* System_Collections_ArrayList_CONSTRUCTOR__0()
{
	System_Collections_ArrayList* this = CS2X_GC_New(sizeof(System_Collections_ArrayList));
	return this;
}

System_Collections_IEnumerator* System_Collections_Queue_GetEnumerator__0(System_Collections_Queue* this)
{
	return null;
}

System_Collections_Queue* System_Collections_Queue_CONSTRUCTOR__0()
{
	System_Collections_Queue* this = CS2X_GC_New(sizeof(System_Collections_Queue));
	return this;
}

System_Collections_IEnumerator* System_Collections_Stack_GetEnumerator__0(System_Collections_Stack* this)
{
	return null;
}

System_Collections_Stack* System_Collections_Stack_CONSTRUCTOR__0()
{
	System_Collections_Stack* this = CS2X_GC_New(sizeof(System_Collections_Stack));
	return this;
}

System_ComponentModel_BrowsableAttribute* System_ComponentModel_BrowsableAttribute_CONSTRUCTOR__0()
{
	System_ComponentModel_BrowsableAttribute* this = CS2X_GC_New(sizeof(System_ComponentModel_BrowsableAttribute));
	return this;
}

System_ComponentModel_DependencyAttribute* System_ComponentModel_DependencyAttribute_CONSTRUCTOR__0()
{
	System_ComponentModel_DependencyAttribute* this = CS2X_GC_New(sizeof(System_ComponentModel_DependencyAttribute));
	return this;
}

System_ComponentModel_EditorBrowsableAttribute* System_ComponentModel_EditorBrowsableAttribute_CONSTRUCTOR__0(System_Int32 state)
{
	System_ComponentModel_EditorBrowsableAttribute* this = CS2X_GC_New(sizeof(System_ComponentModel_EditorBrowsableAttribute));
	this->_browsableState = state;
	return this;
}

System_CS2X_NativeNameAttribute* System_CS2X_NativeNameAttribute_CONSTRUCTOR__0(System_Int32 target, System_String* value)
{
	System_CS2X_NativeNameAttribute* this = CS2X_GC_New(sizeof(System_CS2X_NativeNameAttribute));
	this->Target = target;
	this->Value = value;
	return this;
}

System_Diagnostics_ConditionalAttribute* System_Diagnostics_ConditionalAttribute_CONSTRUCTOR__0(System_String* conditionString)
{
	System_Diagnostics_ConditionalAttribute* this = CS2X_GC_New(sizeof(System_Diagnostics_ConditionalAttribute));
	this->_conditionString = conditionString;
	return this;
}

System_Diagnostics_Debug* System_Diagnostics_Debug_CONSTRUCTOR__0()
{
	System_Diagnostics_Debug* this = CS2X_GC_New(sizeof(System_Diagnostics_Debug));
	return this;
}

System_Diagnostics_CodeAnalysis_SuppressMessageAttribute* System_Diagnostics_CodeAnalysis_SuppressMessageAttribute_CONSTRUCTOR__0()
{
	System_Diagnostics_CodeAnalysis_SuppressMessageAttribute* this = CS2X_GC_New(sizeof(System_Diagnostics_CodeAnalysis_SuppressMessageAttribute));
	return this;
}

System_Globalization_CultureInfo* System_Globalization_CultureInfo_CONSTRUCTOR__0()
{
	System_Globalization_CultureInfo* this = CS2X_GC_New(sizeof(System_Globalization_CultureInfo));
	return this;
}

System_Globalization_NumberFormatInfo* System_Globalization_NumberFormatInfo_CONSTRUCTOR__0()
{
	System_Globalization_NumberFormatInfo* this = CS2X_GC_New(sizeof(System_Globalization_NumberFormatInfo));
	return this;
}

System_Reflection_AssemblyCompanyAttribute* System_Reflection_AssemblyCompanyAttribute_CONSTRUCTOR__0(System_String* company)
{
	System_Reflection_AssemblyCompanyAttribute* this = CS2X_GC_New(sizeof(System_Reflection_AssemblyCompanyAttribute));
	this->_company = company;
	return this;
}

System_Reflection_AssemblyConfigurationAttribute* System_Reflection_AssemblyConfigurationAttribute_CONSTRUCTOR__0(System_String* configuration)
{
	System_Reflection_AssemblyConfigurationAttribute* this = CS2X_GC_New(sizeof(System_Reflection_AssemblyConfigurationAttribute));
	this->_configuration = configuration;
	return this;
}

System_Reflection_AssemblyCopyrightAttribute* System_Reflection_AssemblyCopyrightAttribute_CONSTRUCTOR__0(System_String* copyright)
{
	System_Reflection_AssemblyCopyrightAttribute* this = CS2X_GC_New(sizeof(System_Reflection_AssemblyCopyrightAttribute));
	this->_copyright = copyright;
	return this;
}

System_Reflection_AssemblyCultureAttribute* System_Reflection_AssemblyCultureAttribute_CONSTRUCTOR__0(System_String* culture)
{
	System_Reflection_AssemblyCultureAttribute* this = CS2X_GC_New(sizeof(System_Reflection_AssemblyCultureAttribute));
	this->_culture = culture;
	return this;
}

System_Reflection_AssemblyDelaySignAttribute* System_Reflection_AssemblyDelaySignAttribute_CONSTRUCTOR__0(System_Boolean delaySign)
{
	System_Reflection_AssemblyDelaySignAttribute* this = CS2X_GC_New(sizeof(System_Reflection_AssemblyDelaySignAttribute));
	this->_delaySign = delaySign;
	return this;
}

System_Reflection_AssemblyDescriptionAttribute* System_Reflection_AssemblyDescriptionAttribute_CONSTRUCTOR__0(System_String* description)
{
	System_Reflection_AssemblyDescriptionAttribute* this = CS2X_GC_New(sizeof(System_Reflection_AssemblyDescriptionAttribute));
	this->_description = description;
	return this;
}

System_Reflection_AssemblyFileVersionAttribute* System_Reflection_AssemblyFileVersionAttribute_CONSTRUCTOR__0(System_String* version)
{
	System_Reflection_AssemblyFileVersionAttribute* this = CS2X_GC_New(sizeof(System_Reflection_AssemblyFileVersionAttribute));
	this->_version = version;
	return this;
}

System_Reflection_AssemblyInformationalVersionAttribute* System_Reflection_AssemblyInformationalVersionAttribute_CONSTRUCTOR__0(System_String* informationalVersion)
{
	System_Reflection_AssemblyInformationalVersionAttribute* this = CS2X_GC_New(sizeof(System_Reflection_AssemblyInformationalVersionAttribute));
	this->_informationalVersion = informationalVersion;
	return this;
}

System_Reflection_AssemblyKeyFileAttribute* System_Reflection_AssemblyKeyFileAttribute_CONSTRUCTOR__0(System_String* keyFile)
{
	System_Reflection_AssemblyKeyFileAttribute* this = CS2X_GC_New(sizeof(System_Reflection_AssemblyKeyFileAttribute));
	this->_keyFile = keyFile;
	return this;
}

System_Reflection_AssemblyProductAttribute* System_Reflection_AssemblyProductAttribute_CONSTRUCTOR__0(System_String* product)
{
	System_Reflection_AssemblyProductAttribute* this = CS2X_GC_New(sizeof(System_Reflection_AssemblyProductAttribute));
	this->_product = product;
	return this;
}

System_Reflection_AssemblyTitleAttribute* System_Reflection_AssemblyTitleAttribute_CONSTRUCTOR__0(System_String* title)
{
	System_Reflection_AssemblyTitleAttribute* this = CS2X_GC_New(sizeof(System_Reflection_AssemblyTitleAttribute));
	this->_title = title;
	return this;
}

System_Reflection_AssemblyTrademarkAttribute* System_Reflection_AssemblyTrademarkAttribute_CONSTRUCTOR__0(System_String* trademark)
{
	System_Reflection_AssemblyTrademarkAttribute* this = CS2X_GC_New(sizeof(System_Reflection_AssemblyTrademarkAttribute));
	this->_trademark = trademark;
	return this;
}

System_Reflection_AssemblyVersionAttribute* System_Reflection_AssemblyVersionAttribute_CONSTRUCTOR__0(System_String* version)
{
	System_Reflection_AssemblyVersionAttribute* this = CS2X_GC_New(sizeof(System_Reflection_AssemblyVersionAttribute));
	this->_version = version;
	return this;
}

System_Reflection_DefaultMemberAttribute* System_Reflection_DefaultMemberAttribute_CONSTRUCTOR__0(System_String* memberName)
{
	System_Reflection_DefaultMemberAttribute* this = CS2X_GC_New(sizeof(System_Reflection_DefaultMemberAttribute));
	this->_memberName = memberName;
	return this;
}

System_Runtime_CompilerServices_CompilerGeneratedAttribute* System_Runtime_CompilerServices_CompilerGeneratedAttribute_CONSTRUCTOR__0()
{
	System_Runtime_CompilerServices_CompilerGeneratedAttribute* this = CS2X_GC_New(sizeof(System_Runtime_CompilerServices_CompilerGeneratedAttribute));
	return this;
}

System_Runtime_InteropServices_OutAttribute* System_Runtime_InteropServices_OutAttribute_CONSTRUCTOR__0()
{
	System_Runtime_InteropServices_OutAttribute* this = CS2X_GC_New(sizeof(System_Runtime_InteropServices_OutAttribute));
	return this;
}

