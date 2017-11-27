using CSharpTranspiler.Types;
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

		private static void WriteObjectDeclares(ObjectBase obj, StreamWriter writer)
		{
			var type = obj.GetType();
			if (type.IsSubclassOf(typeof(LogicalObject))) writer.WriteLine(string.Format("struct {0};", obj.fullNameFlat));
			else if (type == typeof(EnumObject)) writer.WriteLine(string.Format("enum {0};", obj.fullNameFlat));
		}

		private static void WriteObject(ObjectBase obj, StreamWriter writer)
		{
			var type = obj.GetType();

			// get type name
			string typeName = null;
			if (type.IsSubclassOf(typeof(LogicalObject))) typeName = "struct";
			else if (type == typeof(EnumObject)) typeName = "enum";
			else throw new Exception("CompileObjectDefinition: Unsuported type: " + type);

			// write type
			writer.WriteLine(string.Format("{0} {1}", typeName, obj.fullNameFlat));

			// write body
			writer.WriteLine('{');
			WriteObjectBody(obj, type, writer);
			writer.WriteLine("};" + Environment.NewLine);
		}

		private static void WriteObjectBody(ObjectBase obj, Type type, StreamWriter writer)
		{
			if (type.IsSubclassOf(typeof(LogicalObject)))
			{
				var logicalObj = (LogicalObject)obj;
				foreach (var field in logicalObj.fields)
				{
					// TODO
				}
			}
			else if (type == typeof(EnumObject))
			{
				// TODO
			}
		}

		private static void WriteObjectMethods(ObjectBase obj, StreamWriter writer)
		{
			// TODO
		}
	}
}
