#pragma once
#include <stdio.h>
#include "GC_Boehm.h"
#define null 0
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
typedef wchar_t* System_Char;
typedef double System_Double;
typedef __int16 System_Int16;
typedef __int32 System_Int32;
typedef __int64 System_Int64;
typedef size_t System_IntPtr;
typedef EMPTY_OBJECT System_RuntimeFieldHandle;
typedef EMPTY_OBJECT System_RuntimeTypeHandle;
typedef __int8 System_SByte;
typedef float System_Single;
typedef unsigned __int16 System_UInt16;
typedef unsigned __int32 System_UInt32;
typedef unsigned __int64 System_UInt64;
typedef size_t System_UIntPtr;
typedef void System_Void;
typedef struct System_Collections_DictionaryEntry System_Collections_DictionaryEntry;
typedef EMPTY_OBJECT System_Attribute;
typedef struct System_AttributeUsageAttribute* System_AttributeUsageAttribute;
typedef struct System_CLSCompliantAttribute* System_CLSCompliantAttribute;
typedef EMPTY_OBJECT System_Console;
typedef EMPTY_OBJECT System_FlagsAttribute;
typedef struct System_Array* System_Array;
typedef EMPTY_OBJECT System_CancelEventArgs;
typedef EMPTY_OBJECT System_Delegate;
typedef EMPTY_OBJECT System_Enum;
typedef EMPTY_OBJECT System_EventArgs;
typedef EMPTY_OBJECT System_Exception;
typedef EMPTY_OBJECT System_Math;
typedef EMPTY_OBJECT System_MulticastDelegate;
typedef EMPTY_OBJECT System_Nullable;
typedef EMPTY_OBJECT System_Object;
typedef struct System_ObsoleteAttribute* System_ObsoleteAttribute;
typedef EMPTY_OBJECT System_ParamArrayAttribute;
typedef struct System_String* System_String;
typedef EMPTY_OBJECT System_StringBuilder;
typedef EMPTY_OBJECT System_Type;
typedef EMPTY_OBJECT System_ValueType;
typedef struct System_CodeDom_Compiler_GeneratedCodeAttribute* System_CodeDom_Compiler_GeneratedCodeAttribute;
typedef EMPTY_OBJECT System_Collections_ArrayList;
typedef EMPTY_OBJECT System_Collections_Queue;
typedef EMPTY_OBJECT System_Collections_Stack;
typedef EMPTY_OBJECT System_ComponentModel_BrowsableAttribute;
typedef EMPTY_OBJECT System_ComponentModel_DependencyAttribute;
typedef struct System_ComponentModel_EditorBrowsableAttribute* System_ComponentModel_EditorBrowsableAttribute;
typedef struct System_CS2X_NativeNameAttribute* System_CS2X_NativeNameAttribute;
typedef struct System_Diagnostics_ConditionalAttribute* System_Diagnostics_ConditionalAttribute;
typedef EMPTY_OBJECT System_Diagnostics_Debug;
typedef EMPTY_OBJECT System_Diagnostics_CodeAnalysis_SuppressMessageAttribute;
typedef EMPTY_OBJECT System_Globalization_CultureInfo;
typedef EMPTY_OBJECT System_Globalization_NumberFormatInfo;
typedef struct System_Reflection_AssemblyCompanyAttribute* System_Reflection_AssemblyCompanyAttribute;
typedef struct System_Reflection_AssemblyConfigurationAttribute* System_Reflection_AssemblyConfigurationAttribute;
typedef struct System_Reflection_AssemblyCopyrightAttribute* System_Reflection_AssemblyCopyrightAttribute;
typedef struct System_Reflection_AssemblyCultureAttribute* System_Reflection_AssemblyCultureAttribute;
typedef struct System_Reflection_AssemblyDelaySignAttribute* System_Reflection_AssemblyDelaySignAttribute;
typedef struct System_Reflection_AssemblyDescriptionAttribute* System_Reflection_AssemblyDescriptionAttribute;
typedef struct System_Reflection_AssemblyFileVersionAttribute* System_Reflection_AssemblyFileVersionAttribute;
typedef struct System_Reflection_AssemblyInformationalVersionAttribute* System_Reflection_AssemblyInformationalVersionAttribute;
typedef struct System_Reflection_AssemblyKeyFileAttribute* System_Reflection_AssemblyKeyFileAttribute;
typedef struct System_Reflection_AssemblyProductAttribute* System_Reflection_AssemblyProductAttribute;
typedef struct System_Reflection_AssemblyTitleAttribute* System_Reflection_AssemblyTitleAttribute;
typedef struct System_Reflection_AssemblyTrademarkAttribute* System_Reflection_AssemblyTrademarkAttribute;
typedef struct System_Reflection_AssemblyVersionAttribute* System_Reflection_AssemblyVersionAttribute;
typedef struct System_Reflection_DefaultMemberAttribute* System_Reflection_DefaultMemberAttribute;
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

struct System_Collections_DictionaryEntry
{
	System_Object Key;
	System_Object Value;
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
	System_String _message;
};

struct System_String
{
	System_Char* buffer;
};

struct System_CodeDom_Compiler_GeneratedCodeAttribute
{
	System_String _tool;
	System_String _version;
};

struct System_ComponentModel_EditorBrowsableAttribute
{
	System_Int32 _browsableState;
};

struct System_CS2X_NativeNameAttribute
{
	System_Int32 Target;
	System_String Value;
};

struct System_Diagnostics_ConditionalAttribute
{
	System_String _conditionString;
};

struct System_Reflection_AssemblyCompanyAttribute
{
	System_String _company;
};

struct System_Reflection_AssemblyConfigurationAttribute
{
	System_String _configuration;
};

struct System_Reflection_AssemblyCopyrightAttribute
{
	System_String _copyright;
};

struct System_Reflection_AssemblyCultureAttribute
{
	System_String _culture;
};

struct System_Reflection_AssemblyDelaySignAttribute
{
	System_Boolean _delaySign;
};

struct System_Reflection_AssemblyDescriptionAttribute
{
	System_String _description;
};

struct System_Reflection_AssemblyFileVersionAttribute
{
	System_String _version;
};

struct System_Reflection_AssemblyInformationalVersionAttribute
{
	System_String _informationalVersion;
};

struct System_Reflection_AssemblyKeyFileAttribute
{
	System_String _keyFile;
};

struct System_Reflection_AssemblyProductAttribute
{
	System_String _product;
};

struct System_Reflection_AssemblyTitleAttribute
{
	System_String _title;
};

struct System_Reflection_AssemblyTrademarkAttribute
{
	System_String _trademark;
};

struct System_Reflection_AssemblyVersionAttribute
{
	System_String _version;
};

struct System_Reflection_DefaultMemberAttribute
{
	System_String _memberName;
};

// =============
// Property forward declares
// =============
System_Int32 System_AttributeUsageAttribute_get_ValidOn(System_AttributeUsageAttribute this);
System_Boolean System_CLSCompliantAttribute_get_IsCompliant(System_CLSCompliantAttribute this);
System_Int32 System_Array_get_Length(System_Array this);
System_Exception System_Exception_get_InnerException(System_Exception this);
System_String System_Exception_get_Message(System_Exception this);
System_String System_Exception_get_StackTrace(System_Exception this);
System_Boolean System_ObsoleteAttribute_get_IsError(System_ObsoleteAttribute this);
System_String System_ObsoleteAttribute_get_Message(System_ObsoleteAttribute this);
System_Type System_Type_get_BaseType(System_Type this);
System_String System_Type_get_Name(System_Type this);
System_String System_CodeDom_Compiler_GeneratedCodeAttribute_get_Tool(System_CodeDom_Compiler_GeneratedCodeAttribute this);
System_String System_CodeDom_Compiler_GeneratedCodeAttribute_get_Version(System_CodeDom_Compiler_GeneratedCodeAttribute this);
System_Int32 System_Collections_ArrayList_get_Count(System_Collections_ArrayList this);
System_Int32 System_Collections_Queue_get_Count(System_Collections_Queue this);
System_Int32 System_Collections_Stack_get_Count(System_Collections_Stack this);
System_Int32 System_ComponentModel_EditorBrowsableAttribute_get_State(System_ComponentModel_EditorBrowsableAttribute this);
System_String System_Diagnostics_ConditionalAttribute_get_ConditionString(System_Diagnostics_ConditionalAttribute this);
System_String System_Reflection_AssemblyCompanyAttribute_get_Company(System_Reflection_AssemblyCompanyAttribute this);
System_String System_Reflection_AssemblyConfigurationAttribute_get_Configuration(System_Reflection_AssemblyConfigurationAttribute this);
System_String System_Reflection_AssemblyCopyrightAttribute_get_Copyright(System_Reflection_AssemblyCopyrightAttribute this);
System_String System_Reflection_AssemblyCultureAttribute_get_Culture(System_Reflection_AssemblyCultureAttribute this);
System_Boolean System_Reflection_AssemblyDelaySignAttribute_get_DelaySign(System_Reflection_AssemblyDelaySignAttribute this);
System_String System_Reflection_AssemblyDescriptionAttribute_get_Description(System_Reflection_AssemblyDescriptionAttribute this);
System_String System_Reflection_AssemblyFileVersionAttribute_get_Version(System_Reflection_AssemblyFileVersionAttribute this);
System_String System_Reflection_AssemblyInformationalVersionAttribute_get_InformationalVersion(System_Reflection_AssemblyInformationalVersionAttribute this);
System_String System_Reflection_AssemblyKeyFileAttribute_get_KeyFile(System_Reflection_AssemblyKeyFileAttribute this);
System_String System_Reflection_AssemblyProductAttribute_get_Product(System_Reflection_AssemblyProductAttribute this);
System_String System_Reflection_AssemblyTitleAttribute_get_Title(System_Reflection_AssemblyTitleAttribute this);
System_String System_Reflection_AssemblyTrademarkAttribute_get_Trademark(System_Reflection_AssemblyTrademarkAttribute this);
System_String System_Reflection_AssemblyVersionAttribute_get_Version(System_Reflection_AssemblyVersionAttribute this);
System_String System_Reflection_DefaultMemberAttribute_get_MemberName(System_Reflection_DefaultMemberAttribute this);

// =============
// Method forward declares
// =============
System_Void System_Collections_DictionaryEntry_CONSTRUCTOR__0(System_Collections_DictionaryEntry* this, System_Object key, System_Object value);
System_Void System_AttributeUsageAttribute_CONSTRUCTOR__0(System_AttributeUsageAttribute this, System_Int32 validOn);
System_Void System_CLSCompliantAttribute_CONSTRUCTOR__0(System_CLSCompliantAttribute this, System_Boolean isCompliant);
System_Void System_Console_WriteLine__0(System_String value);
System_Collections_IEnumerator System_Array_GetEnumerator__0(System_Array this);
System_Void System_Exception_CONSTRUCTOR__0(System_Exception this);
System_Void System_Exception_CONSTRUCTOR__1(System_Exception this, System_String message);
System_Type System_Object_GetType__0(System_Object this);
System_String System_Object_ToString__0(System_Object this);
System_Void System_ObsoleteAttribute_CONSTRUCTOR__0(System_ObsoleteAttribute this);
System_Void System_ObsoleteAttribute_CONSTRUCTOR__1(System_ObsoleteAttribute this, System_String message);
System_Void System_ObsoleteAttribute_CONSTRUCTOR__2(System_ObsoleteAttribute this, System_String message, System_Boolean error);
System_Void System_String_CONSTRUCTOR__0(System_String this, System_Char* value);
System_Void System_CodeDom_Compiler_GeneratedCodeAttribute_CONSTRUCTOR__0(System_CodeDom_Compiler_GeneratedCodeAttribute this, System_String tool, System_String version);
System_Collections_IEnumerator System_Collections_ArrayList_GetEnumerator__0(System_Collections_ArrayList this);
System_Collections_IEnumerator System_Collections_Queue_GetEnumerator__0(System_Collections_Queue this);
System_Collections_IEnumerator System_Collections_Stack_GetEnumerator__0(System_Collections_Stack this);
System_Void System_ComponentModel_EditorBrowsableAttribute_CONSTRUCTOR__0(System_ComponentModel_EditorBrowsableAttribute this, System_Int32 state);
System_Void System_CS2X_NativeNameAttribute_CONSTRUCTOR__0(System_CS2X_NativeNameAttribute this, System_Int32 target, System_String value);
System_Void System_Diagnostics_ConditionalAttribute_CONSTRUCTOR__0(System_Diagnostics_ConditionalAttribute this, System_String conditionString);
System_Void System_Reflection_AssemblyCompanyAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyCompanyAttribute this, System_String company);
System_Void System_Reflection_AssemblyConfigurationAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyConfigurationAttribute this, System_String configuration);
System_Void System_Reflection_AssemblyCopyrightAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyCopyrightAttribute this, System_String copyright);
System_Void System_Reflection_AssemblyCultureAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyCultureAttribute this, System_String culture);
System_Void System_Reflection_AssemblyDelaySignAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyDelaySignAttribute this, System_Boolean delaySign);
System_Void System_Reflection_AssemblyDescriptionAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyDescriptionAttribute this, System_String description);
System_Void System_Reflection_AssemblyFileVersionAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyFileVersionAttribute this, System_String version);
System_Void System_Reflection_AssemblyInformationalVersionAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyInformationalVersionAttribute this, System_String informationalVersion);
System_Void System_Reflection_AssemblyKeyFileAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyKeyFileAttribute this, System_String keyFile);
System_Void System_Reflection_AssemblyProductAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyProductAttribute this, System_String product);
System_Void System_Reflection_AssemblyTitleAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyTitleAttribute this, System_String title);
System_Void System_Reflection_AssemblyTrademarkAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyTrademarkAttribute this, System_String trademark);
System_Void System_Reflection_AssemblyVersionAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyVersionAttribute this, System_String version);
System_Void System_Reflection_DefaultMemberAttribute_CONSTRUCTOR__0(System_Reflection_DefaultMemberAttribute this, System_String memberName);

// =============
// Properties
// =============
System_Int32 System_AttributeUsageAttribute_get_ValidOn(System_AttributeUsageAttribute this)
{
	return this->_attributeTarget;
}

System_Boolean System_CLSCompliantAttribute_get_IsCompliant(System_CLSCompliantAttribute this)
{
	return this->_isCompliant;
}

System_Int32 System_Array_get_Length(System_Array this)
{
	return 0;
}

System_Exception System_Exception_get_InnerException(System_Exception this)
{
	return null;
}

System_String System_Exception_get_Message(System_Exception this)
{
	return null;
}

System_String System_Exception_get_StackTrace(System_Exception this)
{
	return null;
}

System_Boolean System_ObsoleteAttribute_get_IsError(System_ObsoleteAttribute this)
{
	return this->_error;
}

System_String System_ObsoleteAttribute_get_Message(System_ObsoleteAttribute this)
{
	return this->_message;
}

System_Type System_Type_get_BaseType(System_Type this)
{
	return null;
}

System_String System_Type_get_Name(System_Type this)
{
	return null;
}

System_String System_CodeDom_Compiler_GeneratedCodeAttribute_get_Tool(System_CodeDom_Compiler_GeneratedCodeAttribute this)
{
	return this->_tool;
}

System_String System_CodeDom_Compiler_GeneratedCodeAttribute_get_Version(System_CodeDom_Compiler_GeneratedCodeAttribute this)
{
	return this->_version;
}

System_Int32 System_Collections_ArrayList_get_Count(System_Collections_ArrayList this)
{
	return 0;
}

System_Int32 System_Collections_Queue_get_Count(System_Collections_Queue this)
{
	return 0;
}

System_Int32 System_Collections_Stack_get_Count(System_Collections_Stack this)
{
	return 0;
}

System_Int32 System_ComponentModel_EditorBrowsableAttribute_get_State(System_ComponentModel_EditorBrowsableAttribute this)
{
	return this->_browsableState;
}

System_String System_Diagnostics_ConditionalAttribute_get_ConditionString(System_Diagnostics_ConditionalAttribute this)
{
	return this->_conditionString;
}

System_String System_Reflection_AssemblyCompanyAttribute_get_Company(System_Reflection_AssemblyCompanyAttribute this)
{
	return this->_company;
}

System_String System_Reflection_AssemblyConfigurationAttribute_get_Configuration(System_Reflection_AssemblyConfigurationAttribute this)
{
	return this->_configuration;
}

System_String System_Reflection_AssemblyCopyrightAttribute_get_Copyright(System_Reflection_AssemblyCopyrightAttribute this)
{
	return this->_copyright;
}

System_String System_Reflection_AssemblyCultureAttribute_get_Culture(System_Reflection_AssemblyCultureAttribute this)
{
	return this->_culture;
}

System_Boolean System_Reflection_AssemblyDelaySignAttribute_get_DelaySign(System_Reflection_AssemblyDelaySignAttribute this)
{
	return this->_delaySign;
}

System_String System_Reflection_AssemblyDescriptionAttribute_get_Description(System_Reflection_AssemblyDescriptionAttribute this)
{
	return this->_description;
}

System_String System_Reflection_AssemblyFileVersionAttribute_get_Version(System_Reflection_AssemblyFileVersionAttribute this)
{
	return this->_version;
}

System_String System_Reflection_AssemblyInformationalVersionAttribute_get_InformationalVersion(System_Reflection_AssemblyInformationalVersionAttribute this)
{
	return this->_informationalVersion;
}

System_String System_Reflection_AssemblyKeyFileAttribute_get_KeyFile(System_Reflection_AssemblyKeyFileAttribute this)
{
	return this->_keyFile;
}

System_String System_Reflection_AssemblyProductAttribute_get_Product(System_Reflection_AssemblyProductAttribute this)
{
	return this->_product;
}

System_String System_Reflection_AssemblyTitleAttribute_get_Title(System_Reflection_AssemblyTitleAttribute this)
{
	return this->_title;
}

System_String System_Reflection_AssemblyTrademarkAttribute_get_Trademark(System_Reflection_AssemblyTrademarkAttribute this)
{
	return this->_trademark;
}

System_String System_Reflection_AssemblyVersionAttribute_get_Version(System_Reflection_AssemblyVersionAttribute this)
{
	return this->_version;
}

System_String System_Reflection_DefaultMemberAttribute_get_MemberName(System_Reflection_DefaultMemberAttribute this)
{
	return this->_memberName;
}

// =============
// Methods
// =============
System_Void System_Collections_DictionaryEntry_CONSTRUCTOR__0(System_Collections_DictionaryEntry* this, System_Object key, System_Object value)
{
	this->Key = key;
	this->Value = value;
}

System_Void System_AttributeUsageAttribute_CONSTRUCTOR__0(System_AttributeUsageAttribute this, System_Int32 validOn)
{
	this->_attributeTarget = validOn;
}

System_Void System_CLSCompliantAttribute_CONSTRUCTOR__0(System_CLSCompliantAttribute this, System_Boolean isCompliant)
{
	this->_isCompliant = isCompliant;
}

System_Void System_Console_WriteLine__0(System_String value)
{
	wprintf(value->buffer);
}

System_Collections_IEnumerator System_Array_GetEnumerator__0(System_Array this)
{
	return null;
}

System_Void System_Exception_CONSTRUCTOR__0(System_Exception this)
{
}

System_Void System_Exception_CONSTRUCTOR__1(System_Exception this, System_String message)
{
}

System_Type System_Object_GetType__0(System_Object this)
{
	return null;
}

System_String System_Object_ToString__0(System_Object this)
{
	return null;
}

System_Void System_ObsoleteAttribute_CONSTRUCTOR__0(System_ObsoleteAttribute this)
{
}

System_Void System_ObsoleteAttribute_CONSTRUCTOR__1(System_ObsoleteAttribute this, System_String message)
{
	this->_message = message;
}

System_Void System_ObsoleteAttribute_CONSTRUCTOR__2(System_ObsoleteAttribute this, System_String message, System_Boolean error)
{
	this->_message = message;
	this->_error = error;
}

System_Void System_String_CONSTRUCTOR__0(System_String this, System_Char* value)
{
	this->buffer = value;
}

System_Void System_CodeDom_Compiler_GeneratedCodeAttribute_CONSTRUCTOR__0(System_CodeDom_Compiler_GeneratedCodeAttribute this, System_String tool, System_String version)
{
	this->_tool = tool;
	this->_version = version;
}

System_Collections_IEnumerator System_Collections_ArrayList_GetEnumerator__0(System_Collections_ArrayList this)
{
	return null;
}

System_Collections_IEnumerator System_Collections_Queue_GetEnumerator__0(System_Collections_Queue this)
{
	return null;
}

System_Collections_IEnumerator System_Collections_Stack_GetEnumerator__0(System_Collections_Stack this)
{
	return null;
}

System_Void System_ComponentModel_EditorBrowsableAttribute_CONSTRUCTOR__0(System_ComponentModel_EditorBrowsableAttribute this, System_Int32 state)
{
	this->_browsableState = state;
}

System_Void System_CS2X_NativeNameAttribute_CONSTRUCTOR__0(System_CS2X_NativeNameAttribute this, System_Int32 target, System_String value)
{
	this->Target = target;
	this->Value = value;
}

System_Void System_Diagnostics_ConditionalAttribute_CONSTRUCTOR__0(System_Diagnostics_ConditionalAttribute this, System_String conditionString)
{
	this->_conditionString = conditionString;
}

System_Void System_Reflection_AssemblyCompanyAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyCompanyAttribute this, System_String company)
{
	this->_company = company;
}

System_Void System_Reflection_AssemblyConfigurationAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyConfigurationAttribute this, System_String configuration)
{
	this->_configuration = configuration;
}

System_Void System_Reflection_AssemblyCopyrightAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyCopyrightAttribute this, System_String copyright)
{
	this->_copyright = copyright;
}

System_Void System_Reflection_AssemblyCultureAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyCultureAttribute this, System_String culture)
{
	this->_culture = culture;
}

System_Void System_Reflection_AssemblyDelaySignAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyDelaySignAttribute this, System_Boolean delaySign)
{
	this->_delaySign = delaySign;
}

System_Void System_Reflection_AssemblyDescriptionAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyDescriptionAttribute this, System_String description)
{
	this->_description = description;
}

System_Void System_Reflection_AssemblyFileVersionAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyFileVersionAttribute this, System_String version)
{
	this->_version = version;
}

System_Void System_Reflection_AssemblyInformationalVersionAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyInformationalVersionAttribute this, System_String informationalVersion)
{
	this->_informationalVersion = informationalVersion;
}

System_Void System_Reflection_AssemblyKeyFileAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyKeyFileAttribute this, System_String keyFile)
{
	this->_keyFile = keyFile;
}

System_Void System_Reflection_AssemblyProductAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyProductAttribute this, System_String product)
{
	this->_product = product;
}

System_Void System_Reflection_AssemblyTitleAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyTitleAttribute this, System_String title)
{
	this->_title = title;
}

System_Void System_Reflection_AssemblyTrademarkAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyTrademarkAttribute this, System_String trademark)
{
	this->_trademark = trademark;
}

System_Void System_Reflection_AssemblyVersionAttribute_CONSTRUCTOR__0(System_Reflection_AssemblyVersionAttribute this, System_String version)
{
	this->_version = version;
}

System_Void System_Reflection_DefaultMemberAttribute_CONSTRUCTOR__0(System_Reflection_DefaultMemberAttribute this, System_String memberName)
{
	this->_memberName = memberName;
}

