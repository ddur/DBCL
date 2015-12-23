@echo off
@if "%appveyor%" == "true" goto test-coverage

@Rem Local Build
@if exist "C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe" ("C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe" DBCL.sln)

:test-coverage
@if exist .\artifacts\. (del /Q .\artifacts\*) else (md .\artifacts)

@Rem OpenCover Debug Build
@set OpenCoverDebugBuild=E:\GitHub\opencover\main\bin\Debug\OpenCover.Console.exe

@Rem OpenCover Release Build
@set OpenCoverReleaseBuild=E:\GitHub\opencover\main\bin\Release\OpenCover.Console.exe

@Rem OpenCover MSI Installed
@set OpenCoverMsiInstalled=C:\Program Files (x86)\OpenCover\OpenCover.Console.exe

@Rem OpenCover Nuget Package
@set OpenCoverNugetPackage=.\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe

@Rem OpenCover Options
@set OpenCoverOptions=-register:user -mergebyhash -hideskipped:All

@Rem OpenCover command
@set OpenCoverCommand=%OpenCoverReleaseBuild% %OpenCoverOptions%

@if "%appveyor%" == "true" set OpenCoverCommand=%OpenCoverNugetPackage% %OpenCoverOptions%
@if not "%appveyor_build_folder%" == "" set nunit_work_option_folder="/work:\"%appveyor_build_folder%\""


@%OpenCoverCommand% ^
-output:".\artifacts\OpenCover.ICodeSet.xml" ^
-filter:"-[*]DD.Collections.ICodeSet.*Test* +[*]DD.Collections.ICodeSet* +[*]DD.Text*" ^
-target:".\packages\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe" ^
-targetdir:".\Source\Test\NUnit.ICodeSet\bin\Debug" ^
-targetargs:"NUnit.ICodeSet.dll %nunit_work_option_folder% /out:\"ICodeSet.TestResult.xml\""
@echo -------------------------------------
@echo.
@echo.
@if not "%appveyor%" == "true" pause

@%OpenCoverCommand% ^
-output:".\artifacts\OpenCover.BitSetArray.xml" ^
-filter:"-[*]DD.Collections.BitSetArrayTest* +[*]DD.Collections.BitSetArray*" ^
-target:".\packages\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe" ^
-targetdir:".\Source\Test\NUnit.BitSetArray\bin\Debug" ^
-targetargs:"NUnit.BitSetArray.dll %nunit_work_option_folder% /out:\"BitSetArray.TestResult.xml\""
@echo -------------------------------------
@echo.
@echo.
@if not "%appveyor%" == "true" pause

@%OpenCoverCommand% ^
-output:".\artifacts\OpenCover.Diagnostics.xml" ^
-filter:"-[*]DD.Diagnostics.SuccessTest* +[*]DD.Diagnostics*" ^
-target:".\packages\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe" ^
-targetdir:".\Source\Test\NUnit.Diagnostics\bin\Debug" ^
-targetargs:"NUnit.Diagnostics.dll %nunit_work_option_folder% /out:\"Diagnostics.TestResult.xml\""
@echo -------------------------------------
@echo.
@echo.
@if not "%appveyor%" == "true" pause

@%OpenCoverCommand% ^
-output:".\artifacts\OpenCover.Extensions.xml" ^
-filter:"+[*]DD.Extends*" ^
-target:".\packages\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe" ^
-targetdir:".\Source\Test\NUnit.Extensions\bin\Debug" ^
-targetargs:"NUnit.Extensions.dll %nunit_work_option_folder% /out:\"Extensions.TestResult.xml\""
@echo -------------------------------------
@echo.
@echo.
@if not "%appveyor%" == "true" pause

