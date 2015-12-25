@echo off
@".\packages\7-Zip.CommandLine.9.20.0\tools\7za" a ".\reports\CodeCoverage.7z" ".\reports\*"
@if "%appveyor%"=="True" appveyor PushArtifact ".\reports\CodeCoverage.7z"
@echo -------------------------------------
@echo.
@echo.
