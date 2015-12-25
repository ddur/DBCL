@echo off
@echo if "%appveyor%"=="True" appveyor PushArtifact "%dbcl_artifacts_folder%\*.xml"
@echo %dbcl_zip_console% a "%dbcl_artifacts_folder%\CodeCoverage.7z" "%dbcl_artifacts_folder%\*"
@echo -------------------------------------
@echo.
@echo.
