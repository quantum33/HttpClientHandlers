For .NET Framework projects, you can encoutner a compatibility trouble between v4.0.0.0 and v4.2.0.0 of System.Net.Http.dll.

Error message can be:
InvalidCastException: [A]System.Net.Http.Headers.MediaTypeHeaderValue cannot be cast to [B]System.Net.Http.Headers.MediaTypeHeaderValue. 
Type A originates from 'System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a' in the context 'Default' at location 'C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System.Net.Http\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Net.Http.dll'. 
Type B originates from 'System.Net.Http, Version=4.2.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a' in the context 'Default' at location '[your csharp project]\bin\Debug\System.Net.Http.dll'.

=> You can update Visual Studio in order to solve your problem.
It will update the System.Net.Http.dll into the GAC (not the best solution)

For more informations (or any other solution), follow this issue (still open this day):
https://github.com/dotnet/corefx/issues/25773