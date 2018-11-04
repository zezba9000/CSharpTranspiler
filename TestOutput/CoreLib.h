#pragma once
#define null 0

// =============
// Library References
// =============

// =============
// Type forward declares
// =============
typedef enum System_AttributeTargets System_AttributeTargets;
typedef enum System_ComponentModel_EditorBrowsableState System_ComponentModel_EditorBrowsableState;
typedef struct System_IDisposable System_IDisposable;
typedef struct System_Collections_ICollection System_Collections_ICollection;
typedef struct System_Collections_IEnumerable System_Collections_IEnumerable;
typedef struct System_Collections_IEnumerator System_Collections_IEnumerator;
typedef struct System_ComponentModel_IContainer System_ComponentModel_IContainer;
typedef struct System_Boolean System_Boolean;
typedef struct System_Byte System_Byte;
typedef struct System_Char System_Char;
typedef struct System_Double System_Double;
typedef struct System_Int16 System_Int16;
typedef struct System_Int32 System_Int32;
typedef struct System_Int64 System_Int64;
typedef struct System_IntPtr System_IntPtr;
typedef struct System_RuntimeFieldHandle System_RuntimeFieldHandle;
typedef struct System_RuntimeTypeHandle System_RuntimeTypeHandle;
typedef struct System_SByte System_SByte;
typedef struct System_Single System_Single;
typedef struct System_UInt16 System_UInt16;
typedef struct System_UInt32 System_UInt32;
typedef struct System_UInt64 System_UInt64;
typedef struct System_UIntPtr System_UIntPtr;
typedef struct System_Void System_Void;
typedef struct System_Collections_DictionaryEntry System_Collections_DictionaryEntry;
typedef struct System_Attribute System_Attribute;
typedef struct System_AttributeUsageAttribute System_AttributeUsageAttribute;
typedef struct System_CLSCompliantAttribute System_CLSCompliantAttribute;
typedef struct System_FlagsAttribute System_FlagsAttribute;
typedef struct System_Array System_Array;
typedef struct System_CancelEventArgs System_CancelEventArgs;
typedef struct System_Delegate System_Delegate;
typedef struct System_Enum System_Enum;
typedef struct System_EventArgs System_EventArgs;
typedef struct System_Exception System_Exception;
typedef struct System_Math System_Math;
typedef struct System_MulticastDelegate System_MulticastDelegate;
typedef struct System_Nullable System_Nullable;
typedef struct System_Object System_Object;
typedef struct System_ObsoleteAttribute System_ObsoleteAttribute;
typedef struct System_ParamArrayAttribute System_ParamArrayAttribute;
typedef struct System_String System_String;
typedef struct System_StringBuilder System_StringBuilder;
typedef struct System_Type System_Type;
typedef struct System_ValueType System_ValueType;
typedef struct System_CodeDom_Compiler_GeneratedCodeAttribute System_CodeDom_Compiler_GeneratedCodeAttribute;
typedef struct System_Collections_ArrayList System_Collections_ArrayList;
typedef struct System_Collections_Queue System_Collections_Queue;
typedef struct System_Collections_Stack System_Collections_Stack;
typedef struct System_ComponentModel_BrowsableAttribute System_ComponentModel_BrowsableAttribute;
typedef struct System_ComponentModel_DependencyAttribute System_ComponentModel_DependencyAttribute;
typedef struct System_ComponentModel_EditorBrowsableAttribute System_ComponentModel_EditorBrowsableAttribute;
typedef struct System_Diagnostics_ConditionalAttribute System_Diagnostics_ConditionalAttribute;
typedef struct System_Diagnostics_Debug System_Diagnostics_Debug;
typedef struct System_Diagnostics_CodeAnalysis_SuppressMessageAttribute System_Diagnostics_CodeAnalysis_SuppressMessageAttribute;
typedef struct System_Globalization_CultureInfo System_Globalization_CultureInfo;
typedef struct System_Globalization_NumberFormatInfo System_Globalization_NumberFormatInfo;
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
typedef struct System_Runtime_CompilerServices_CompilerGeneratedAttribute System_Runtime_CompilerServices_CompilerGeneratedAttribute;
typedef struct System_Runtime_CompilerServices_RuntimeHelpers System_Runtime_CompilerServices_RuntimeHelpers;
typedef struct System_Runtime_InteropServices_OutAttribute System_Runtime_InteropServices_OutAttribute;

// =============
// Types Definitions
// =============
enum System_AttributeTargets
{
	Assembly = 1,
	Module = 2,
	Class = 4,
	Struct = 8,
	Enum = 16,
	Constructor = 32,
	Method = 64,
	Property = 128,
	Field = 256,
	Event = 512,
	Interface = 1024,
	Parameter = 2048,
	Delegate = 4096,
	ReturnValue = 8192,
	GenericParameter = 16384,
	All = 32767
};

enum System_ComponentModel_EditorBrowsableState
{
	Always = 0,
	Never = 1,
	Advanced = 2
};

struct System_IDisposable
{
	char : 0;
};

struct System_Collections_ICollection
{
	char : 0;
};

struct System_Collections_IEnumerable
{
	char : 0;
};

struct System_Collections_IEnumerator
{
	char : 0;
};

struct System_ComponentModel_IContainer
{
	char : 0;
};

struct System_Boolean
{
	char : 0;
};

struct System_Byte
{
	char : 0;
};

struct System_Char
{
	char : 0;
};

struct System_Double
{
	char : 0;
};

struct System_Int16
{
	char : 0;
};

struct System_Int32
{
	char : 0;
};

struct System_Int64
{
	char : 0;
};

struct System_IntPtr
{
	char : 0;
};

struct System_RuntimeFieldHandle
{
	char : 0;
};

struct System_RuntimeTypeHandle
{
	char : 0;
};

struct System_SByte
{
	char : 0;
};

struct System_Single
{
	char : 0;
};

struct System_UInt16
{
	char : 0;
};

struct System_UInt32
{
	char : 0;
};

struct System_UInt64
{
	char : 0;
};

struct System_UIntPtr
{
	char : 0;
};

struct System_Void
{
	char : 0;
};

struct System_Collections_DictionaryEntry
{
	System_Object Key;
	System_Object Value;
	char : 0;
};

struct System_Attribute
{
	char : 0;
};

struct System_AttributeUsageAttribute
{
	System_AttributeTargets _attributeTarget;
	System_Boolean AllowMultiple;
	System_Boolean Inherited;
};

struct System_CLSCompliantAttribute
{
	System_Boolean _isCompliant;
};

struct System_FlagsAttribute
{
	char : 0;
};

struct System_Array
{
	char : 0;
};

struct System_CancelEventArgs
{
	char : 0;
};

struct System_Delegate
{
	char : 0;
};

struct System_Enum
{
	char : 0;
};

struct System_EventArgs
{
	char : 0;
};

struct System_Exception
{
	char : 0;
};

struct System_Math
{
	char : 0;
};

struct System_MulticastDelegate
{
	char : 0;
};

struct System_Nullable
{
	char : 0;
};

struct System_Object
{
	char : 0;
};

struct System_ObsoleteAttribute
{
	System_Boolean _error;
	System_String* _message;
};

struct System_ParamArrayAttribute
{
	char : 0;
};

struct System_String
{
	char : 0;
};

struct System_StringBuilder
{
	char : 0;
};

struct System_Type
{
	char : 0;
};

struct System_ValueType
{
	char : 0;
};

struct System_CodeDom_Compiler_GeneratedCodeAttribute
{
	System_String* _tool;
	System_String* _version;
};

struct System_Collections_ArrayList
{
	char : 0;
};

struct System_Collections_Queue
{
	char : 0;
};

struct System_Collections_Stack
{
	char : 0;
};

struct System_ComponentModel_BrowsableAttribute
{
	char : 0;
};

struct System_ComponentModel_DependencyAttribute
{
	char : 0;
};

struct System_ComponentModel_EditorBrowsableAttribute
{
	System_ComponentModel_EditorBrowsableState _browsableState;
};

struct System_Diagnostics_ConditionalAttribute
{
	System_String* _conditionString;
};

struct System_Diagnostics_Debug
{
	char : 0;
};

struct System_Diagnostics_CodeAnalysis_SuppressMessageAttribute
{
	char : 0;
};

struct System_Globalization_CultureInfo
{
	char : 0;
};

struct System_Globalization_NumberFormatInfo
{
	char : 0;
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

struct System_Runtime_CompilerServices_CompilerGeneratedAttribute
{
	char : 0;
};

struct System_Runtime_CompilerServices_RuntimeHelpers
{
	char : 0;
};

struct System_Runtime_InteropServices_OutAttribute
{
	char : 0;
};

// =============
// Property forward declares
// =============
System_AttributeTargets System_AttributeUsageAttribute_get_ValidOn(System_AttributeUsageAttribute* this);
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
System_ComponentModel_EditorBrowsableState System_ComponentModel_EditorBrowsableAttribute_get_State(System_ComponentModel_EditorBrowsableAttribute* this);
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
System_Void System_Collections_DictionaryEntry_CONSTRUCTOR(System_Collections_DictionaryEntry* this, System_Object* key, System_Object* value);
System_Void System_AttributeUsageAttribute_CONSTRUCTOR(System_AttributeUsageAttribute* this, System_AttributeTargets validOn);
System_Void System_CLSCompliantAttribute_CONSTRUCTOR(System_CLSCompliantAttribute* this, System_Boolean isCompliant);
System_Collections_IEnumerator* System_Array_GetEnumerator(System_Array* this);
System_Void System_Exception_CONSTRUCTOR(System_Exception* this);
System_Void System_Exception_CONSTRUCTOR(System_Exception* this, System_String* message);
System_Type* System_Object_GetType(System_Object* this);
System_String* System_Object_ToString(System_Object* this);
System_Void System_ObsoleteAttribute_CONSTRUCTOR(System_ObsoleteAttribute* this);
System_Void System_ObsoleteAttribute_CONSTRUCTOR(System_ObsoleteAttribute* this, System_String* message);
System_Void System_ObsoleteAttribute_CONSTRUCTOR(System_ObsoleteAttribute* this, System_String* message, System_Boolean error);
System_Void System_CodeDom_Compiler_GeneratedCodeAttribute_CONSTRUCTOR(System_CodeDom_Compiler_GeneratedCodeAttribute* this, System_String* tool, System_String* version);
System_Collections_IEnumerator* System_Collections_ArrayList_GetEnumerator(System_Collections_ArrayList* this);
System_Collections_IEnumerator* System_Collections_Queue_GetEnumerator(System_Collections_Queue* this);
System_Collections_IEnumerator* System_Collections_Stack_GetEnumerator(System_Collections_Stack* this);
System_Void System_ComponentModel_EditorBrowsableAttribute_CONSTRUCTOR(System_ComponentModel_EditorBrowsableAttribute* this, System_ComponentModel_EditorBrowsableState state);
System_Void System_Diagnostics_ConditionalAttribute_CONSTRUCTOR(System_Diagnostics_ConditionalAttribute* this, System_String* conditionString);
System_Void System_Reflection_AssemblyCompanyAttribute_CONSTRUCTOR(System_Reflection_AssemblyCompanyAttribute* this, System_String* company);
System_Void System_Reflection_AssemblyConfigurationAttribute_CONSTRUCTOR(System_Reflection_AssemblyConfigurationAttribute* this, System_String* configuration);
System_Void System_Reflection_AssemblyCopyrightAttribute_CONSTRUCTOR(System_Reflection_AssemblyCopyrightAttribute* this, System_String* copyright);
System_Void System_Reflection_AssemblyCultureAttribute_CONSTRUCTOR(System_Reflection_AssemblyCultureAttribute* this, System_String* culture);
System_Void System_Reflection_AssemblyDelaySignAttribute_CONSTRUCTOR(System_Reflection_AssemblyDelaySignAttribute* this, System_Boolean delaySign);
System_Void System_Reflection_AssemblyDescriptionAttribute_CONSTRUCTOR(System_Reflection_AssemblyDescriptionAttribute* this, System_String* description);
System_Void System_Reflection_AssemblyFileVersionAttribute_CONSTRUCTOR(System_Reflection_AssemblyFileVersionAttribute* this, System_String* version);
System_Void System_Reflection_AssemblyInformationalVersionAttribute_CONSTRUCTOR(System_Reflection_AssemblyInformationalVersionAttribute* this, System_String* informationalVersion);
System_Void System_Reflection_AssemblyKeyFileAttribute_CONSTRUCTOR(System_Reflection_AssemblyKeyFileAttribute* this, System_String* keyFile);
System_Void System_Reflection_AssemblyProductAttribute_CONSTRUCTOR(System_Reflection_AssemblyProductAttribute* this, System_String* product);
System_Void System_Reflection_AssemblyTitleAttribute_CONSTRUCTOR(System_Reflection_AssemblyTitleAttribute* this, System_String* title);
System_Void System_Reflection_AssemblyTrademarkAttribute_CONSTRUCTOR(System_Reflection_AssemblyTrademarkAttribute* this, System_String* trademark);
System_Void System_Reflection_AssemblyVersionAttribute_CONSTRUCTOR(System_Reflection_AssemblyVersionAttribute* this, System_String* version);
System_Void System_Reflection_DefaultMemberAttribute_CONSTRUCTOR(System_Reflection_DefaultMemberAttribute* this, System_String* memberName);

// =============
// Properties
// =============
System_AttributeTargets System_AttributeUsageAttribute_get_ValidOn(System_AttributeUsageAttribute* this)
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

System_ComponentModel_EditorBrowsableState System_ComponentModel_EditorBrowsableAttribute_get_State(System_ComponentModel_EditorBrowsableAttribute* this)
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
System_Void System_Collections_DictionaryEntry_CONSTRUCTOR(System_Collections_DictionaryEntry* this, System_Object* key, System_Object* value)
{
	this->Key = key;
	this->Value = value;
}

System_Void System_AttributeUsageAttribute_CONSTRUCTOR(System_AttributeUsageAttribute* this, System_AttributeTargets validOn)
{
	this->_attributeTarget = validOn;
}

System_Void System_CLSCompliantAttribute_CONSTRUCTOR(System_CLSCompliantAttribute* this, System_Boolean isCompliant)
{
	this->_isCompliant = isCompliant;
}

System_Collections_IEnumerator* System_Array_GetEnumerator(System_Array* this)
{
	return null;
}

System_Void System_Exception_CONSTRUCTOR(System_Exception* this)
{
}

System_Void System_Exception_CONSTRUCTOR(System_Exception* this, System_String* message)
{
}

System_Type* System_Object_GetType(System_Object* this)
{
	return null;
}

System_String* System_Object_ToString(System_Object* this)
{
	return null;
}

System_Void System_ObsoleteAttribute_CONSTRUCTOR(System_ObsoleteAttribute* this)
{
}

System_Void System_ObsoleteAttribute_CONSTRUCTOR(System_ObsoleteAttribute* this, System_String* message)
{
	this->_message = message;
}

System_Void System_ObsoleteAttribute_CONSTRUCTOR(System_ObsoleteAttribute* this, System_String* message, System_Boolean error)
{
	this->_message = message;
	this->_error = error;
}

System_Void System_CodeDom_Compiler_GeneratedCodeAttribute_CONSTRUCTOR(System_CodeDom_Compiler_GeneratedCodeAttribute* this, System_String* tool, System_String* version)
{
	this->_tool = tool;
	this->_version = version;
}

System_Collections_IEnumerator* System_Collections_ArrayList_GetEnumerator(System_Collections_ArrayList* this)
{
	return null;
}

System_Collections_IEnumerator* System_Collections_Queue_GetEnumerator(System_Collections_Queue* this)
{
	return null;
}

System_Collections_IEnumerator* System_Collections_Stack_GetEnumerator(System_Collections_Stack* this)
{
	return null;
}

System_Void System_ComponentModel_EditorBrowsableAttribute_CONSTRUCTOR(System_ComponentModel_EditorBrowsableAttribute* this, System_ComponentModel_EditorBrowsableState state)
{
	this->_browsableState = state;
}

System_Void System_Diagnostics_ConditionalAttribute_CONSTRUCTOR(System_Diagnostics_ConditionalAttribute* this, System_String* conditionString)
{
	this->_conditionString = conditionString;
}

System_Void System_Reflection_AssemblyCompanyAttribute_CONSTRUCTOR(System_Reflection_AssemblyCompanyAttribute* this, System_String* company)
{
	this->_company = company;
}

System_Void System_Reflection_AssemblyConfigurationAttribute_CONSTRUCTOR(System_Reflection_AssemblyConfigurationAttribute* this, System_String* configuration)
{
	this->_configuration = configuration;
}

System_Void System_Reflection_AssemblyCopyrightAttribute_CONSTRUCTOR(System_Reflection_AssemblyCopyrightAttribute* this, System_String* copyright)
{
	this->_copyright = copyright;
}

System_Void System_Reflection_AssemblyCultureAttribute_CONSTRUCTOR(System_Reflection_AssemblyCultureAttribute* this, System_String* culture)
{
	this->_culture = culture;
}

System_Void System_Reflection_AssemblyDelaySignAttribute_CONSTRUCTOR(System_Reflection_AssemblyDelaySignAttribute* this, System_Boolean delaySign)
{
	this->_delaySign = delaySign;
}

System_Void System_Reflection_AssemblyDescriptionAttribute_CONSTRUCTOR(System_Reflection_AssemblyDescriptionAttribute* this, System_String* description)
{
	this->_description = description;
}

System_Void System_Reflection_AssemblyFileVersionAttribute_CONSTRUCTOR(System_Reflection_AssemblyFileVersionAttribute* this, System_String* version)
{
	this->_version = version;
}

System_Void System_Reflection_AssemblyInformationalVersionAttribute_CONSTRUCTOR(System_Reflection_AssemblyInformationalVersionAttribute* this, System_String* informationalVersion)
{
	this->_informationalVersion = informationalVersion;
}

System_Void System_Reflection_AssemblyKeyFileAttribute_CONSTRUCTOR(System_Reflection_AssemblyKeyFileAttribute* this, System_String* keyFile)
{
	this->_keyFile = keyFile;
}

System_Void System_Reflection_AssemblyProductAttribute_CONSTRUCTOR(System_Reflection_AssemblyProductAttribute* this, System_String* product)
{
	this->_product = product;
}

System_Void System_Reflection_AssemblyTitleAttribute_CONSTRUCTOR(System_Reflection_AssemblyTitleAttribute* this, System_String* title)
{
	this->_title = title;
}

System_Void System_Reflection_AssemblyTrademarkAttribute_CONSTRUCTOR(System_Reflection_AssemblyTrademarkAttribute* this, System_String* trademark)
{
	this->_trademark = trademark;
}

System_Void System_Reflection_AssemblyVersionAttribute_CONSTRUCTOR(System_Reflection_AssemblyVersionAttribute* this, System_String* version)
{
	this->_version = version;
}

System_Void System_Reflection_DefaultMemberAttribute_CONSTRUCTOR(System_Reflection_DefaultMemberAttribute* this, System_String* memberName)
{
	this->_memberName = memberName;
}

