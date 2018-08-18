@echo off

%SystemRoot%\Microsoft.NET\Framework64\v4.0.30319\regasm.exe /nologo /unregister "%~dp0..\src\Sample.Win\bin\Release\Sample.Win.dll"
%SystemRoot%\Microsoft.NET\Framework64\v4.0.30319\regasm.exe /nologo /unregister "%~dp0..\src\Sample.Wpf\bin\Release\Sample.Wpf.dll"

pause
