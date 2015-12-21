@Rem if not "%appveyor%" == "true" goto end
.\packages\coveralls.io.1.3.4\tools\coveralls.net.exe --opencover ".\artifacts\OpenCover.*.xml" --repo-token "3HvPffZf6UKHBmBX3kZG0NSV50g3yyej5"
:end