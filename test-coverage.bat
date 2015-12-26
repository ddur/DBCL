@set dbcl_build_folder=%appveyor_build_folder%
@if not "%appveyor%" == "True" set dbcl_build_folder=%cd%

@set dbcl_artifacts_folder=%dbcl_build_folder%\reports
@if exist %dbcl_artifacts_folder%\. (del /Q %dbcl_artifacts_folder%\*) else (md %dbcl_artifacts_folder%)
@set dbcl_packages_folder=%dbcl_build_folder%\packages

@set dbcl_nunit_runner_console=%dbcl_packages_folder%\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe
@set dbcl_nunit_runner_options=/nologo /noshadow /work:%dbcl_artifacts_folder%

@set dbcl_zip_console=%dbcl_packages_folder%\7-Zip.CommandLine.9.20.0\tools\7za.exe

@set dbcl_coveralls_console=%dbcl_packages_folder%\coveralls.io.1.3.4\tools\coveralls.net.exe
@set dbcl_coveralls_options=--opencover %dbcl_artifacts_folder%\OpenCover.*.xml --repo-token "%coveralls_repo_token%"

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

@Rem OpenCover Command
@set OpenCoverCommand=%OpenCoverNugetPackage%

@Rem Local OpenCover Override
@if not "%appveyor%" == "True" set OpenCoverCommand=%OpenCoverReleaseBuild%

@Rem OpenCover Options
@set OpenCoverOptions=-register:user -threshold:1 -mergebyhash -hideskipped:All

@Rem OpenCover command
@set OpenCoverCommandAndOptions=%OpenCoverCommand% %OpenCoverOptions%

@echo AppVeyor env.variable: %appveyor%
@echo AppVeyor Build Folder: %appveyor_build_folder%
@echo Current        Folder: %cd%
@echo dbcl Artifacts Folder: %dbcl_artifacts_folder%
@echo dbcl     Build Folder: %dbcl_build_folder%
@echo NUnit         Options: %dbcl_nunit_runner_options%
@echo OpenCover     Command: %OpenCoverCommand%
@echo OpenCover     Options: %OpenCoverOptions%
@%OpenCoverCommand% -version 
@rem if not "%appveyor%" == "True" pause
@echo.

@Rem Local Build?
@if not "%appveyor%" == "True" if exist "C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe" ("C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe" DBCL.sln)

@call :run-single-test "Extensions"  "+[DBCL]DD.Extends*"
@rem call :run-single-test "ICodeSet"    "+[DBCL]DD.Collections.ICodeSet* +[DBCL]DD.Text*"
@rem call :run-single-test "BitSetArray" "+[DBCL]DD.Collections.BitSetArray*"
@rem call :run-single-test "Diagnostics" "+[DBCL]DD.Diagnostics*"
@exit /b

:run-single-test
@echo assembly  name: %1
@echo filter applied: %2 
@%OpenCoverCommandAndOptions% ^
-output:"%dbcl_artifacts_folder%\OpenCover.%1.xml" ^
-filter:%2 ^
-target:"%dbcl_nunit_runner_console%" ^
-targetdir:".\Source\Test\NUnit.%1\bin\Debug" ^
-targetargs:"NUnit.%1.dll /result:\"NUnit.%1.xml\" %dbcl_nunit_runner_options%"
@echo -------------------------------------
@echo.
@rem if not "%appveyor%" == "True" pause
@exit /b
