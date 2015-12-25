@if "%appveyor%" == "True" goto test-coverage

@Rem Local Build
@if exist "C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe" ("C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe" DBCL.sln)

:test-coverage
@set reports_dir=%cd%\reports
@if exist %reports_dir%\. (del /Q %reports_dir%\*) else (md %reports_dir%)

@Rem OpenCover Debug Build
@set OpenCoverDebugBuild="E:\GitHub\opencover\main\bin\Debug\OpenCover.Console.exe"

@Rem OpenCover Release Build
@set OpenCoverReleaseBuild="E:\GitHub\opencover\main\bin\Release\OpenCover.Console.exe"

@Rem OpenCover MSI Installed
@set OpenCoverMsiInstalled="C:\Program Files (x86)\OpenCover\OpenCover.Console.exe"

@Rem OpenCover Nuget Package
@set OpenCoverNugetPackage=".\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe"

@Rem OpenCover Options
@set OpenCoverOptions=-register:user -threshold:1 -mergebyhash -hideskipped:All

@Rem OpenCover command
@set OpenCoverCommand=%OpenCoverMsiInstalled% %OpenCoverOptions%
@if "%appveyor%" == "True" set OpenCoverCommand=%OpenCoverNugetPackage% %OpenCoverOptions%

@Rem NUnit output folder
@set nunit_work_option_folder=/work:%reports_dir%

@echo My Reports Directory : %reports_dir%
@echo AppVeyor env.variable: %appveyor%
@echo AppVeyor Build Folder: %appveyor_build_folder%
@echo Current Directory    : %cd%
@echo NUnit /work:.. Option: %nunit_work_option_folder%
@echo.

@echo Runining test with -filter:"+[DBCL]DD.Extends*"
@echo.
@%OpenCoverCommand% ^
-output:".\artifacts\OpenCover.Extensions.xml" ^
-filter:"+[DBCL]DD.Extends*" ^
-target:".\packages\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe" ^
-targetdir:".\Source\Test\NUnit.Extensions\bin\Debug" ^
-targetargs:"NUnit.Extensions.dll /nologo /noshadow /result=\"TestResult.xml\" "
@echo -------------------------------------
@echo.
@echo.
@rem if not "%appveyor%" == "True" pause

@echo Runining again with -filter:"+[*]DD.Extends*"
@echo.
@%OpenCoverCommand% ^
-output:".\artifacts\OpenCover.Extensions.xml" ^
-filter:"+[*]DD.Extends*" ^
-target:".\packages\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe" ^
-targetdir:".\Source\Test\NUnit.Extensions\bin\Debug" ^
-targetargs:"NUnit.Extensions.dll /nologo /noshadow /result=\"TestResult.xml\" "
@echo -------------------------------------
@echo.
@echo.
@rem if not "%appveyor%" == "True" pause

@%OpenCoverCommand% ^
-output:".\artifacts\OpenCover.ICodeSet.xml" ^
-filter:"-[*]DD.Collections.ICodeSet.*Test* +[*]DD.Collections.ICodeSet* +[*]DD.Text*" ^
-target:".\packages\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe" ^
-targetdir:".\Source\Test\NUnit.ICodeSet\bin\Debug" ^
-targetargs:"NUnit.ICodeSet.dll /nologo /noshadow /result=\"ICodeSet.TestResult.xml\" "
@echo -------------------------------------
@echo.
@echo.
@rem if not "%appveyor%" == "True" pause

@%OpenCoverCommand% ^
-output:".\artifacts\OpenCover.BitSetArray.xml" ^
-filter:"-[*]DD.Collections.BitSetArrayTest* +[*]DD.Collections.BitSetArray*" ^
-target:".\packages\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe" ^
-targetdir:".\Source\Test\NUnit.BitSetArray\bin\Debug" ^
-targetargs:"NUnit.BitSetArray.dll /nologo /noshadow /result=\"BitSetArray.TestResult.xml\" "
@echo -------------------------------------
@echo.
@echo.
@rem if not "%appveyor%" == "True" pause

@%OpenCoverCommand% ^
-output:".\artifacts\OpenCover.Diagnostics.xml" ^
-filter:"-[*]DD.Diagnostics.SuccessTest* +[*]DD.Diagnostics*" ^
-target:".\packages\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe" ^
-targetdir:".\Source\Test\NUnit.Diagnostics\bin\Debug" ^
-targetargs:"NUnit.Diagnostics.dll /nologo /noshadow /result=\"Diagnostics.TestResult.xml\" "
@echo -------------------------------------
@echo.
@echo.
@rem if not "%appveyor%" == "True" pause
