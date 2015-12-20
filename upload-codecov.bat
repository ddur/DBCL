@if not "%appveyor%" == "true" goto end
@set path=C:\\Python34;C:\\Python34\\Scripts;%PATH%
pip install codecov
codecov -X gcov -t "164a4e94-5b54-4162-b500-b290a69c8307" -f ".\artifacts\OpenCover.BitSetArray.xml"
:end