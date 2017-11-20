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
			// TODO: check if dll or exe. (dll = .h : exe = .cpp)
			string path = outputPath + Path.GetFileNameWithoutExtension(project.filename) + ".c";
			using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
			using (var writer = new StreamWriter(stream))
			{
				// TODO: write dependancy libs as included headers

				// write forward declares
				foreach (var obj in project.allObjects)
				{
					var type = obj.GetType();
					if (type == typeof(ClassObject) || type == typeof(StructObject) || type == typeof(InterfaceObject)) writer.WriteLine(string.Format("struct {0};", obj.fullNameFlat));
					else if (type == typeof(EnumObject)) writer.WriteLine(string.Format("enum {0};", obj.fullNameFlat));
				}

				// write user body definitions
				foreach (var obj in project.classObjects)
				{
					CompileObjectDefinition(obj, writer);
				}
			}
		}

		private static void CompileObjectDefinition(ObjectBase obj, StreamWriter writer)
		{
			var type = obj.GetType();
			if (type == typeof(ClassObject) || type == typeof(StructObject)) CompileObjectDefinition_Class_Struct(obj, writer);
		}

		private static void CompileObjectDefinition_Class_Struct(ObjectBase obj, StreamWriter writer)
		{
			var type = obj.GetType();
			writer.WriteLine(string.Format("{0} {1}", type == typeof(ClassObject) ? "class":"struct", obj.fullNameFlat));
		}

		private static void CompileObject(ObjectBase obj, StreamWriter writer)
		{
			var type = obj.GetType();
			if (type == typeof(ClassObject) || type == typeof(StructObject)) CompileObject_Class_Struct(obj, writer);
		}

		private static void CompileObject_Class_Struct(ObjectBase obj, StreamWriter writer)
		{
			
		}
	}
}
