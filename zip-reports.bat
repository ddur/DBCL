@echo off
@if "%appveyor%"=="True" appveyor PushArtifact "%dbcl_artifacts_folder%\OpenCover.Extensions_b.xml"
@%dbcl_zip_console% a "%dbcl_artifacts_folder%\CodeCoverage.7z" "%dbcl_artifacts_folder%\*"
@echo -------------------------------------
@echo.
@echo.
