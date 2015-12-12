@echo off
".\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe" -register:user -mergebyhash ^
-output:"coverage.BitSetArray.xml" ^
-filter:"+[DBCL]DD.Collections.BitSetArray*" ^
-target:".\packages\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe" ^
-targetdir:".\Source\Test\NUnit.BitSetArray\bin\Debug" ^
-targetargs:"\"NUnit.BitSetArray.dll\" /labels /xml=\"TestResult.xml\" "
@echo -------------------------------------
@echo.
@echo.
".\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe" -register:user -mergebyhash ^
-output:"coverage.ICodeSet.xml" ^
-filter:"+[DBCL]DD.Collections.ICodeSet* +[DBCL]DD.Text*" ^
-target:".\packages\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe" ^
-targetdir:".\Source\Test\NUnit.ICodeSet\bin\Debug" ^
-targetargs:"\"NUnit.ICodeSet.dll\" /labels /xml=\"TestResult.xml\" "
@echo -------------------------------------
@echo.
@echo.
".\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe" -register:user -mergebyhash ^
-output:"coverage.Extensions.xml" ^
-filter:"+[DBCL]DD.Extends*" ^
-target:".\packages\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe" ^
-targetdir:".\Source\Test\NUnit.Extensions\bin\Debug" ^
-targetargs:"\"NUnit.Extensions.dll\" /labels /xml=\"TestResult.xml\" "
@echo -------------------------------------
@echo.
@echo.
".\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe" -register:user -mergebyhash ^
-output:"coverage.Diagnostics.xml" ^
-filter:"+[DBCL]DD.Diagnostics*" ^
-target:".\packages\NUnit.Runners.Net4.2.6.4\tools\nunit-console-x86.exe" ^
-targetdir:".\Source\Test\NUnit.Diagnostics\bin\Debug" ^
-targetargs:"\"NUnit.Diagnostics.dll\" /labels /xml=\"TestResult.xml\" "
@echo -------------------------------------
@echo.   
@echo.  
