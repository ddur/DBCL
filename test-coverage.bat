@if "%appveyor%" == "True" set dbcl_build_folder=%appveyor_build_folder% else set dbcl_build_folder=%cd%

@set dbcl_artifacts_folder=%dbcl_build_folder%\reports
@if exist %dbcl_artifacts_folder%\. (del /Q %dbcl_artifacts_folder%\*) else (md %dbcl_artifacts_folder%)
@set dbcl_packages_folder=%dbcl_build_folder%\packages

@set dbcl_nunit_runner_console=%dbcl_packages_folder%\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe
@set dbcl_nunit_runner_options=/nologo /noshadow /work:%dbcl_artifacts_folder%

@set dbcl_zip_console=%dbcl_packages_folder%\7-Zip.CommandLine.9.20.0\tools\7za.exe

@set dbcl_coveralls_console=%dbcl_packages_folder%\coveralls.io.1.3.4\tools\coveralls.net.exe
@set dbcl_coveralls_options=--opencover %dbcl_artifacts_folder%\OpenCover.*.xml --repo-token "3HvPffZf6UKHBmBX3kZG0NSV50g3yyej5"

@set dbcl_report_generator_console=%dbcl_packages_folder%\ReportGenerator.2.3.5.0\tools\ReportGenerator.exe
@set dbcl_report_generator_options=-targetdir:%dbcl_artifacts_folder% -reports:%dbcl_artifacts_folder%\OpenCover.*.xml

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

@echo AppVeyor env.variable: %appveyor%
@echo AppVeyor Build Folder: %appveyor_build_folder%
@echo Current        Folder: %cd%
@echo dbcl Artifacts Folder: %dbcl_artifacts_folder%
@echo dbcl     Build Folder: %dbcl_build_folder%
@echo NUnit         Options: %dbcl_nunit_runner_options%
@echo.

@if "%appveyor%" == "True" goto after-build
@Rem Local Build
@if exist "C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe" ("C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe" DBCL.sln)

:after-build
@echo Runining test with -filter:"+[DBCL]DD.Extends*"
@echo.
@%OpenCoverCommand% ^
-output:"%dbcl_artifacts_folder%\OpenCover.Extensions.xml" ^
-filter:"+[DBCL]DD.Extends*" ^
-target:"%dbcl_nunit_runner_console%" ^
-targetdir:".\Source\Test\NUnit.Extensions\bin\Debug" ^
-targetargs:"NUnit.Extensions.dll /result=\"TestResult.xml\" %dbcl_nunit_runner_options%"
@echo -------------------------------------
@echo.
@echo.
@rem if not "%appveyor%" == "True" pause

@echo Runining again with -filter:"+[*]DD.Extends*"
@echo.
@%OpenCoverCommand% ^
-output:"%dbcl_artifacts_folder%\OpenCover.Extensions.xml" ^
-filter:"+[*]DD.Extends*" ^
-target:"%dbcl_nunit_runner_console%" ^
-targetdir:".\Source\Test\NUnit.Extensions\bin\Debug" ^
-targetargs:"NUnit.Extensions.dll /result=\"TestResult.xml\" %dbcl_nunit_runner_options%"
@echo -------------------------------------
@echo.
@echo.
@rem if not "%appveyor%" == "True" pause
@goto end

@%OpenCoverCommand% ^
-output:"%dbcl_artifacts_folder%\OpenCover.ICodeSet.xml" ^
-filter:"-[*]DD.Collections.ICodeSet.*Test* +[*]DD.Collections.ICodeSet* +[*]DD.Text*" ^
-target:"%dbcl_nunit_runner_console%" ^
-targetdir:".\Source\Test\NUnit.ICodeSet\bin\Debug" ^
-targetargs:"NUnit.ICodeSet.dll /result=\"ICodeSet.TestResult.xml\" %dbcl_nunit_runner_options%"
@echo -------------------------------------
@echo.
@echo.
@rem if not "%appveyor%" == "True" pause

@%OpenCoverCommand% ^
-output:"%dbcl_artifacts_folder%\OpenCover.BitSetArray.xml" ^
-filter:"-[*]DD.Collections.BitSetArrayTest* +[*]DD.Collections.BitSetArray*" ^
-target:"%dbcl_nunit_runner_console%" ^
-targetdir:".\Source\Test\NUnit.BitSetArray\bin\Debug" ^
-targetargs:"NUnit.BitSetArray.dll /result=\"BitSetArray.TestResult.xml\" %dbcl_nunit_runner_options%"
@echo -------------------------------------
@echo.
@echo.
@rem if not "%appveyor%" == "True" pause

@%OpenCoverCommand% ^
-output:"%dbcl_artifacts_folder%\OpenCover.Diagnostics.xml" ^
-filter:"-[*]DD.Diagnostics.SuccessTest* +[*]DD.Diagnostics*" ^
-target:"%dbcl_nunit_runner_console%" ^
-targetdir:".\Source\Test\NUnit.Diagnostics\bin\Debug" ^
-targetargs:"NUnit.Diagnostics.dll /result=\"Diagnostics.TestResult.xml\" %dbcl_nunit_runner_options%"
@echo -------------------------------------
@echo.
@echo.
@rem if not "%appveyor%" == "True" pause

:end