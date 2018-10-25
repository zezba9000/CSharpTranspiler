using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
	static class StreamWriterEx
	{
		public static string prefix = string.Empty;

		public static void WritePrefix(this StreamWriter self)
		{
			WritePrefix(self, string.Empty);
		}

		public static void WritePrefix(this StreamWriter self, string value)
		{
			self.Write(prefix + value);
		}

		public static void WriteLinePrefix(this StreamWriter self)
		{
			WriteLinePrefix(self, string.Empty);
		}

		public static void WriteLinePrefix(this StreamWriter self, string value)
		{
			self.WriteLine(prefix + value);
		}
	}
}
