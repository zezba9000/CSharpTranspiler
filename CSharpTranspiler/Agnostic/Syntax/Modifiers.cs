using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTranspiler.Agnostic.Syntax
{
	public enum Modifiers
	{
		// access
		Public,
		Private,
		Internal,
		Protected,

		// other
		Abstract,
		Const,
		Event,
		Extern,
		New,
		Override,
		Partial,
		ReadOnly,
		Sealed,
		Static,
		Unsafe,
		Virtual,
		Volatile

		// Not supported
		//Async,
	}
}
