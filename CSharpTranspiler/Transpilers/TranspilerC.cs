using CSharpTranspiler.Agnostic;
using CSharpTranspiler.Agnostic.Types;
using CSharpTranspiler.Agnostic.Types.MemberDeclarations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTranspiler.Transpilers
{
    public static class TranspilerC
	{
		public enum TargetTypes
		{
			VCPP,
			//GCC,
			//Clang
		}

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
					writer.WriteLine(string.Format("#include \"{0}.h\"", reference));
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
				writer.WriteLine(string.Format("{0}// ============={0}// Methods{0}// =============", Environment.NewLine));
				foreach (var obj in project.structObjects) WriteObjectMethods(obj, writer);
				foreach (var obj in project.classObjects) WriteObjectMethods(obj, writer);
			}
		}

		private static void WriteObjectDeclares(ObjectType obj, StreamWriter writer)
		{
			var type = obj.GetType();
			if (type.IsSubclassOf(typeof(LogicalType))) writer.WriteLine(string.Format("struct {0};", obj.fullNameFlat));
			else if (type == typeof(EnumType)) writer.WriteLine(string.Format("enum {0};", obj.fullNameFlat));
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
			writer.WriteLine(string.Format("{0} {1}", typeName, obj.fullNameFlat));

			// write body
			writer.WriteLine('{');
			WriteObjectBody(obj, type, writer);
			writer.WriteLine("};" + Environment.NewLine);
		}

		private static void WriteObjectBody(ObjectType obj, Type type, StreamWriter writer)
		{
			if (type.IsSubclassOf(typeof(LogicalType)))
			{
				var logicalObj = (LogicalType)obj;
				foreach (var variable in logicalObj.variables)
				{
					if (variable.isValueType) writer.WriteLine(string.Format("\t{0} {1};", variable.typeFullNameFlat, variable.name));
					else writer.WriteLine(string.Format("\t{0}* {1};", variable.typeFullNameFlat, variable.name));
				}
			}
			else if (type == typeof(EnumType))
			{
				// TODO
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
				writer.Write(string.Format("System_Array* {0}(", fullNameFlat));
			}
			else
			{
				if (returnIsValueType) writer.Write(string.Format("{0} {1}(", returnFullNameFlat, fullNameFlat));
				else writer.Write(string.Format("{0}* {1}(", returnFullNameFlat, fullNameFlat));
			}

			// if method and object are not static pass "this" ref
			if (!obj.isStatic && !member.isStatic) writer.Write(string.Format("{0} *this, ", obj.fullNameFlat));

			// write parameters
			int count = paramerters.Length;
			for (int i = 0; i != count; ++i)
			{
				var parameter = paramerters[i];
				if (parameter.isArray)
				{
					writer.Write(string.Format("System_Array* {0}", parameter.name));
				}
				else
				{
					if (parameter.isValueType) writer.Write(string.Format("{0} {1}", parameter.typeFullNameFlat, parameter.name));
					else writer.Write(string.Format("{0}* {1}", parameter.typeFullNameFlat, parameter.name));
				}

				if (i != count-1) writer.Write(", ");
			}

			// finish
			writer.Write(')');
		}

		private static void WriteObjectPropertyDeclare(ObjectType obj, PropertyDeclaration property, StreamWriter writer)
		{
			var parameters = new Parameter[1];
			parameters[0] = new Parameter()
			{
				name = property.name,
				typeFullNameFlat = property.typeFullNameFlat,
				isArray = property.isArray,
				isValueType = property.isValueType
			};
			WriteParameterBasedDeclare(obj, property, property.isArray, property.isValueType, property.typeFullNameFlat, property.fullNameFlat, parameters, writer);
		}

		private static void WriteObjectPropertyDeclares(ObjectType obj, StreamWriter writer)
		{
			var logicalObj = (LogicalType)obj;
			foreach (var property in logicalObj.properties)
			{
				WriteObjectPropertyDeclare(obj, property, writer);
				writer.WriteLine(';');
			}
		}

		private static void WriteObjectProperties(ObjectType obj, StreamWriter writer)
		{
			var logicalObj = (LogicalType)obj;
			foreach (var property in logicalObj.properties)
			{
				// TODO
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
				WriteObjectMethodBody(method, writer);
				writer.WriteLine('}' + Environment.NewLine);
			}
		}

		private static void WriteObjectMethodBody(MethodDeclaration method, StreamWriter writer)
		{
			// TODO
		}
	}
}
