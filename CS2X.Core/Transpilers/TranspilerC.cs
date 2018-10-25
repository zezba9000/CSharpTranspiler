using CS2X.Core.Agnostic;
using CS2X.Core.Agnostic.Syntax;
using CS2X.Core.Agnostic.Types;
using CS2X.Core.Agnostic.Types.MemberDeclarations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2X.Core.Transpilers
{
    public static partial class TranspilerC
	{
		public enum TargetTypes
		{
			VCPP,
			//GCC,
			//Clang
		}

		private delegate void CallbackMethod();

		public static void CompileSolution(Solution solution, TargetTypes type, string outputPath)
		{
			foreach (var project in solution.projects)
			{
				CompileProject(project, outputPath);
			}
		}

		private static void CompileProject(Project project, string outputPath)
		{
			string path = Path.Combine(outputPath, Path.GetFileNameWithoutExtension(project.filename) + (project.type == ProjectTypes.Exe ? ".c" : ".h"));
			using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
			using (var writer = new StreamWriter(stream))
			{
				// make sure libs only get included once
				if (project.type == ProjectTypes.Dll)
				{
					writer.WriteLine("#pragma once" + Environment.NewLine);
				}

				// write reference libs as included headers
				writer.WriteLine(string.Format("// ============={0}// Library References{0}// =============", Environment.NewLine));
				foreach (var reference in project.references)
				{
					writer.WriteLine($"#include \"{reference}.h\"");
				}

				// write object forward declares
				writer.WriteLine(string.Format("{0}// ============={0}// Type forward declares{0}// =============", Environment.NewLine));
				foreach (var obj in project.allObjects)
				{
					WriteObjectDeclares(obj, writer);
				}

				// write objects
				writer.WriteLine(string.Format("{0}// ============={0}// Types Definitions{0}// =============", Environment.NewLine));
				foreach (var obj in project.enumObjects) WriteObject(obj, writer);
				foreach (var obj in project.interfaceObjects) WriteObject(obj, writer);
				foreach (var obj in project.structObjects) WriteObject(obj, writer);
				foreach (var obj in project.classObjects) WriteObject(obj, writer);

				// write object property forward declares
				writer.WriteLine(string.Format("// ============={0}// Property forward declares{0}// =============", Environment.NewLine));
				foreach (var obj in project.structObjects) WriteObjectPropertyDeclares(obj, writer);
				foreach (var obj in project.classObjects) WriteObjectPropertyDeclares(obj, writer);

				// write object method forward declares
				writer.WriteLine(string.Format("{0}// ============={0}// Method forward declares{0}// =============", Environment.NewLine));
				foreach (var obj in project.structObjects) WriteObjectMethodDeclares(obj, writer);
				foreach (var obj in project.classObjects) WriteObjectMethodDeclares(obj, writer);

				// write object properties
				writer.WriteLine(string.Format("{0}// ============={0}// Properties{0}// =============", Environment.NewLine));
				foreach (var obj in project.structObjects) WriteObjectProperties(obj, writer);
				foreach (var obj in project.classObjects) WriteObjectProperties(obj, writer);

				// write object methods
				writer.WriteLine(string.Format("// ============={0}// Methods{0}// =============", Environment.NewLine));
				foreach (var obj in project.structObjects) WriteObjectMethods(obj, writer);
				foreach (var obj in project.classObjects) WriteObjectMethods(obj, writer);
			}
		}
		
		private static void WriteObjectDeclares(ObjectType obj, StreamWriter writer)
		{
			var type = obj.GetType();
			if (type.IsSubclassOf(typeof(LogicalType))) writer.WriteLine($"struct {obj.fullNameFlat};");
			else if (type == typeof(EnumType)) writer.WriteLine($"enum {obj.fullNameFlat};");
		}

		private static void WriteObject(ObjectType obj, StreamWriter writer)
		{
			var type = obj.GetType();

			// get type name
			string typeName = null;
			if (type.IsSubclassOf(typeof(LogicalType))) typeName = "struct";
			else if (type == typeof(EnumType)) typeName = "enum";
			else throw new Exception("CompileObjectDefinition: Unsuported type: " + type);

			// write type
			writer.Write($"{typeName} {obj.fullNameFlat}");
			if (type == typeof(EnumType))
			{
				if (obj.baseObjects.Count != 0) writer.WriteLine(" : " + obj.baseObjects[0].fullNameFlat);
				else writer.WriteLine();
			}
			else
			{
				writer.WriteLine();
			}

			// write body
			writer.WriteLine('{');
			WriteObjectBody(obj, type, writer, out var writeExternalMembers);
			writer.WriteLine("};" + Environment.NewLine);
			writeExternalMembers?.Invoke();
		}

		private static void WriteObjectBodyVariables(LogicalType logicalObj, StreamWriter writer)
		{
			// write base fields first
			foreach (var baseObj in logicalObj.baseObjects)
			{
				WriteObjectBodyVariables((LogicalType)baseObj.objectType, writer);
			}

			// write non-static fields
			foreach (var variable in logicalObj.variables)
			{
				if (variable.isStatic) continue;
				if (variable.isValueType) writer.WriteLine($"\t{variable.typeFullNameFlat} {variable.name};");
				else writer.WriteLine($"\t{variable.typeFullNameFlat}* {variable.name};");
			}
		}

		private static void WriteObjectBody(ObjectType obj, Type type, StreamWriter writer, out CallbackMethod writeExternalMembers)
		{
			if (type.IsSubclassOf(typeof(LogicalType)))
			{
				var logicalObj = (LogicalType)obj;

				// write non-static
				WriteObjectBodyVariables(logicalObj, writer);

				// write static fields
				void writeStaticMembers()
				{
					if (logicalObj.variables.Count != 0)
					{
						foreach (var variable in logicalObj.variables)
						{
							if (!variable.isStatic) continue;
							if (variable.isValueType) writer.WriteLine($"{variable.typeFullNameFlat} {variable.fullNameFlat};");
							else writer.WriteLine($"{variable.typeFullNameFlat}* {variable.fullNameFlat};");
						}

						writer.WriteLine();
					}
				}

				writeExternalMembers = writeStaticMembers;
			}
			else if (type == typeof(EnumType))
			{
				var enumObj = (EnumType)obj;
				foreach (var member in enumObj.members)
				{
					if (!member.valueIsExplicitlySet) writer.Write('\t' + member.name);
					else writer.Write($"\t{member.name} = {member.value}");
					if (member.name != enumObj.members[enumObj.members.Count-1].name) writer.WriteLine(',');
					else writer.WriteLine();
				}
				writeExternalMembers = null;
			}
			else
			{
				writeExternalMembers = null;
			}
		}

		struct Parameter
		{
			public string name, typeFullNameFlat;
			public bool isArray, isValueType;
		}
		private static void WriteParameterBasedDeclare(ObjectType obj, Member member, bool returnIsArray, bool returnIsValueType, string returnFullNameFlat, string fullNameFlat, Parameter[] paramerters, StreamWriter writer)
		{
			// write return type
			if (returnIsArray)
			{
				writer.Write($"System_Array* {fullNameFlat}(");
			}
			else
			{
				if (returnIsValueType) writer.Write($"{returnFullNameFlat} {fullNameFlat}(");
				else writer.Write($"{returnFullNameFlat}* {fullNameFlat}(");
			}

			// if method and object are not static pass "this" ref
			if (!obj.isStatic && !member.isStatic) writer.Write(string.Format("{0}* this{1}", obj.fullNameFlat, (paramerters != null && paramerters.Length != 0) ? ", " : ""));

			// write parameters
			if (paramerters != null)
			{
				int count = paramerters.Length;
				for (int i = 0; i != count; ++i)
				{
					var parameter = paramerters[i];
					if (parameter.isArray)
					{
						writer.Write($"System_Array* {parameter.name}");
					}
					else
					{
						if (parameter.isValueType) writer.Write($"{parameter.typeFullNameFlat} {parameter.name}");
						else writer.Write($"{parameter.typeFullNameFlat}* {parameter.name}");
					}

					if (i != count-1) writer.Write(", ");
				}
			}

			// finish
			writer.Write(')');
		}

		private static void WriteObjectPropertyDeclare(ObjectType obj, PropertyDeclaration property, StreamWriter writer, bool isGet)
		{
			if (isGet)
			{
				WriteParameterBasedDeclare(obj, property, property.isArray, property.isValueType, property.typeFullNameFlat, property.fullNameFlat + "_get", null, writer);
			}
			else
			{
				var parameters = new Parameter[1];
				parameters[0] = new Parameter()
				{
					name = "value",
					typeFullNameFlat = property.typeFullNameFlat,
					isArray = property.isArray,
					isValueType = property.isValueType
				};
				WriteParameterBasedDeclare(obj, property, property.isArray, property.isValueType, "void", property.fullNameFlat + "_set", parameters, writer);
			}
		}

		private static void WriteObjectPropertyDeclares(ObjectType obj, StreamWriter writer)
		{
			var logicalObj = (LogicalType)obj;
			foreach (var property in logicalObj.properties)
			{
				if (property.getBody != null)
				{
					WriteObjectPropertyDeclare(obj, property, writer, true);
					writer.WriteLine(';');
				}

				if (property.setBody != null)
				{
					WriteObjectPropertyDeclare(obj, property, writer, false);
					writer.WriteLine(';');
				}
			}
		}

		private static void WriteObjectProperties(ObjectType obj, StreamWriter writer)
		{
			var logicalObj = (LogicalType)obj;
			foreach (var property in logicalObj.properties)
			{
				if (property.getBody != null)
				{
					WriteObjectPropertyDeclare(obj, property, writer, true);
					writer.WriteLine(Environment.NewLine + '{');
					WriteLogicalBody(property.getBody, writer);
					writer.WriteLine('}' + Environment.NewLine);
				}

				if (property.setBody != null)
				{
					WriteObjectPropertyDeclare(obj, property, writer, false);
					writer.WriteLine(Environment.NewLine + '{');
					WriteLogicalBody(property.setBody, writer);
					writer.WriteLine('}' + Environment.NewLine);
				}
			}
		}

		private static void WriteObjectMethodDeclare(ObjectType obj, MethodDeclaration method, StreamWriter writer)
		{
			var parameters = new Parameter[method.parameters.Count];
			for (int i = 0; i != method.parameters.Count; ++i)
			{
				var parameter = method.parameters[i];
				parameters[i] = new Parameter()
				{
					name = parameter.name,
					typeFullNameFlat = parameter.typeFullNameFlat,
					isArray = parameter.isArray,
					isValueType = parameter.isValueType
				};
			}
			WriteParameterBasedDeclare(obj, method, method.returnType.isArray, method.returnType.isValueType, method.returnType.typeFullNameFlat, method.fullNameFlat, parameters, writer);
		}

		private static void WriteObjectMethodDeclares(ObjectType obj, StreamWriter writer)
		{
			var logicalObj = (LogicalType)obj;
			foreach (var method in logicalObj.methods)
			{
				WriteObjectMethodDeclare(obj, method, writer);
				writer.WriteLine(';');
			}
		}

		private static void WriteObjectMethods(ObjectType obj, StreamWriter writer)
		{
			var logicalObj = (LogicalType)obj;
			foreach (var method in logicalObj.methods)
			{
				WriteObjectMethodDeclare(obj, method, writer);
				writer.WriteLine(Environment.NewLine + '{');
				WriteLogicalBody(method.body, writer);
				writer.WriteLine('}' + Environment.NewLine);
			}
		}
	}
}
