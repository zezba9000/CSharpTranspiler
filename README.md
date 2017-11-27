# C# / CSharp Transpiler<br>

This project is for doing preliminary experiments to compile a C# 2.0 subset using Roslyn to portable C. This will come with a micro GC that targets embedded devices with 1kb or higher and also a similar GC used on legacy hardware targets such as classic mac for example \[or anything else\] (think macOS9, SNES, DOS, RISC-V, MIPS, SPARK, PPC, Homebrew etc). The point is to run C# in an <u>optimized fashion</u> across unsupported .NET targets with <u>minimal effort</u> and testing on old compilers and toolchains will prove that point.<br>

Much of the work done here "at minimum" will be used in the ReignSDK for a C# to Shader-Lang transpiler.<br>

#### If the C# to C compiler works out well, I may try to transpile C# to all langs listed below.
- CS2SL [C# to ShaderLang]
- CS2C [C# to C]
- CS2JV [C# to Java]
- CS2JS [C# to javaScript]
- CS2PY [C# to Python]