@if not "%appveyor%" == "true" goto end
@rem set path=C:\\Python34;C:\\Python34\\Scripts;%PATH%
@rem pip install codecov
@rem codecov -X gcov -t "164a4e94-5b54-4162-b500-b290a69c8307" -f ".\artifacts\OpenCover.BitSetArray.xml"
:end
