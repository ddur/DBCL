@if not "%appveyor%" == "true" goto end
@set path=C:\\Python34;C:\\Python34\\Scripts;%PATH%
pip install codecov
codecov -f ".\artifacts\OpenCover.*.xml"
:end