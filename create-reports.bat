@echo off
@".\packages\ReportGenerator.2.3.5.0\tools\ReportGenerator" -targetdir:.\artifacts ^
-reports:".\artifacts\OpenCover.*.xml"
@echo -------------------------------------
@echo.
@echo.
