# CSharpTranspiler

This project if for doing preliminary experiments to compile a C# subset using Roslyn to portable C. This will come with a micro GC that targets embedded devices with 1kb higher and also a similar GC used on legacy hardware targets such as classic mac for example \[or anything else\] (think OS9 etc). The point is to run C# in an optimized fashion across unsupported .NET targets with minimal effort and testing on old compilers and toolchains will prove that point.<br>

Much of the work done here "at minimum" will be used in the ReignSDK for a C# to Shader-Lang transpiler.<br>

If the C# to C compiler works out well, I may try to transpile C# to Java as well.