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
				foreach (var reference in project.references)
				{
					writer.WriteLine(string.Format("#include \"{0}.h\"", reference));
				}

				// write forward declares
				writer.WriteLine();
				foreach (var obj in project.allObjects)
				{
					WriteObjectDeclares(obj, writer);
				}

				// write objects
				writer.WriteLine();
				foreach (var obj in project.enumObjects) WriteObject(obj, writer);
				foreach (var obj in project.interfaceObjects) WriteObject(obj, writer);
				foreach (var obj in project.structObjects) WriteObject(obj, writer);
				foreach (var obj in project.classObjects) WriteObject(obj, writer);

				// write object methods
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
					writer.WriteLine(string.Format("\t{0} {1};", variable.typeFullNameFlat, variable.name));
				}
			}
			else if (type == typeof(EnumType))
			{
				// TODO
			}
		}

		private static void WriteObjectMethods(ObjectType obj, StreamWriter writer)
		{
			var logicalObj = (LogicalType)obj;
			foreach (var method in logicalObj.methods)
			{
				writer.Write(string.Format("{0} {1}(", method.returnType.typeFullNameFlat, method.fullNameFlat));
				if (!obj.isStatic) writer.Write(string.Format("{0} *this, ", obj.fullNameFlat));
				int count = method.parameters.Count;
				for (int i = 0; i != count; ++i)
				{
					var parameter = method.parameters[i];
					if (!parameter.isArray) writer.Write(string.Format("{0} {1}", parameter.typeFullNameFlat, parameter.name));
					else writer.Write(string.Format("{0} *{1}", parameter.typeFullNameFlat, parameter.name));// TODO: this needs to use a System_Array type
					if (i != count-1) writer.Write(", ");
				}
				writer.WriteLine(')');
				writer.WriteLine('{');
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
