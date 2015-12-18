@echo off
@if exist .\artifacts\. (del /Q .\artifacts\*) else (md .\artifacts)

@Rem if exist "C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe" ("C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe" DBCL.sln)

@rem test release build @"E:\GitHub\opencover\main\bin\Debug\OpenCover.Console.exe" -register:user -mergebyhash ^
@rem test MSI installed @"C:\Program Files (x86)\OpenCover\OpenCover.Console.exe" -register:user -mergebyhash ^
@rem test Specs tested  @"E:\GitHub\opencover\main\OpenCover.Specs\bin\Release\zipFolder\OpenCover.Console.exe" -register:user -mergebyhash ^
@rem ORIGINAL @".\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe" -register:user -mergebyhash ^

@Rem "E:\GitHub\opencover\main\bin\Debug\OpenCover.Console.exe" -register:user -mergebyhash ^
@".\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe" -register:user -mergebyhash ^
-output:".\artifacts\OpenCover.BitSetArray.xml" ^
-filter:"-[*]DD.Collections.BitSetArray.*Test* +[*]DD.Collections.BitSetArray*" ^
-target:".\packages\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe" ^
-targetdir:".\Source\Test\NUnit.BitSetArray\bin\Debug" ^
-targetargs:"\"NUnit.BitSetArray.dll\" /labels /xml=\"BitSetArray.TestResult.xml\" "
@echo -------------------------------------
@echo.
@echo.
@Rem "E:\GitHub\opencover\main\bin\Debug\OpenCover.Console.exe" -register:user -mergebyhash ^
@".\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe" -register:user -mergebyhash ^
-output:".\artifacts\OpenCover.ICodeSet.xml" ^
-filter:"-[*]DD.Collections.ICodeSet.*Test* +[*]DD.Collections.ICodeSet* +[*]DD.Text*" ^
-target:".\packages\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe" ^
-targetdir:".\Source\Test\NUnit.ICodeSet\bin\Debug" ^
-targetargs:"\"NUnit.ICodeSet.dll\" /labels /xml=\"ICodeSet.TestResult.xml\" "
@echo -------------------------------------
@echo.
@echo.
@Rem "E:\GitHub\opencover\main\bin\Debug\OpenCover.Console.exe" -register:user -mergebyhash ^
@".\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe" -register:user -mergebyhash ^
-output:".\artifacts\OpenCover.Extensions.xml" ^
-filter:"+[*]DD.Extends*" ^
-target:".\packages\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe" ^
-targetdir:".\Source\Test\NUnit.Extensions\bin\Debug" ^
-targetargs:"\"NUnit.Extensions.dll\" /labels /xml=\"Extensions.TestResult.xml\" "
@echo -------------------------------------
@echo.
@echo.
@Rem "E:\GitHub\opencover\main\bin\Debug\OpenCover.Console.exe" -register:user -mergebyhash ^
@".\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe" -register:user -mergebyhash ^
-output:".\artifacts\OpenCover.Diagnostics.xml" ^
-filter:"+[*]DD.Diagnostics*" ^
-target:".\packages\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe" ^
-targetdir:".\Source\Test\NUnit.Diagnostics\bin\Debug" ^
-targetargs:"\"NUnit.Diagnostics.dll\" /labels /xml=\"Diagnostics.TestResult.xml\" "
@echo -------------------------------------
@echo.
@echo.

@Rem change directory to artifacts
@cd artifacts

@:Codecov
@Rem Run codecov uploader
@if not "%appveyor%"=="true" goto RunReportGenerator
@"SET PATH=C:\\Python34;C:\\Python34\\Scripts;%PATH%"
@pip install codecov
@codecov -f "OpenCover.BitSetArray.xml"

@:RunReportGenerator
@Rem Run ReportGenerator and create reports
@"..\packages\ReportGenerator.2.3.5.0\tools\ReportGenerator" -targetdir:.\ ^
-reports:"OpenCover.BitSetArray.xml;OpenCover.ICodeSet.xml;OpenCover.Extensions.xml;OpenCover.Diagnostics.xml"

@:ZipReportsToArtifactFile
@"..\packages\7-Zip.CommandLine.9.20.0\tools\7za" a "CodeCoverage.7z" "*"
@Rem change directory o artifacts
@cd ..
@echo -------------------------------------
@echo %date%
@echo %time%
@echo.
