@echo off
@if "%appveyor%" == "true" goto test-coverage

@Rem Local Build
@if exist "C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe" ("C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe" DBCL.sln)

@:test-coverage
@if exist .\artifacts\. (del /Q .\artifacts\*) else (md .\artifacts)

@Rem OpenCover release build @"E:\GitHub\opencover\main\bin\Debug\OpenCover.Console.exe" -register:user -mergebyhash ^
@Rem OpenCover MSI installed @"C:\Program Files (x86)\OpenCover\OpenCover.Console.exe" -register:user -mergebyhash ^
@Rem OpenCover nuget package @".\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe" -register:user -mergebyhash ^

@".\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe" -register:user -mergebyhash ^
-output:".\artifacts\OpenCover.BitSetArray.xml" ^
-filter:"-[*]DD.Collections.BitSetArray.*Test* +[*]DD.Collections.BitSetArray*" ^
-target:".\packages\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe" ^
-targetdir:".\Source\Test\NUnit.BitSetArray\bin\Debug" ^
-targetargs:"\"NUnit.BitSetArray.dll\" /labels /xml=\"BitSetArray.TestResult.xml\" "
@echo -------------------------------------
@echo.
@echo.
@".\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe" -register:user -mergebyhash ^
-output:".\artifacts\OpenCover.ICodeSet.xml" ^
-filter:"-[*]DD.Collections.ICodeSet.*Test* +[*]DD.Collections.ICodeSet* +[*]DD.Text*" ^
-target:".\packages\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe" ^
-targetdir:".\Source\Test\NUnit.ICodeSet\bin\Debug" ^
-targetargs:"\"NUnit.ICodeSet.dll\" /labels /xml=\"ICodeSet.TestResult.xml\" "
@echo -------------------------------------
@echo.
@echo.
@".\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe" -register:user -mergebyhash ^
-output:".\artifacts\OpenCover.Extensions.xml" ^
-filter:"+[*]DD.Extends*" ^
-target:".\packages\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe" ^
-targetdir:".\Source\Test\NUnit.Extensions\bin\Debug" ^
-targetargs:"\"NUnit.Extensions.dll\" /labels /xml=\"Extensions.TestResult.xml\" "
@echo -------------------------------------
@echo.
@echo.
@".\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe" -register:user -mergebyhash ^
-output:".\artifacts\OpenCover.Diagnostics.xml" ^
-filter:"+[*]DD.Diagnostics*" ^
-target:".\packages\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe" ^
-targetdir:".\Source\Test\NUnit.Diagnostics\bin\Debug" ^
-targetargs:"\"NUnit.Diagnostics.dll\" /labels /xml=\"Diagnostics.TestResult.xml\" "
@echo -------------------------------------
@echo.
@echo.
