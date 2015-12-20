if ($env:appveyor == "true") {
.\packages\coveralls.io.1.3.4\tools\coveralls.net.exe --opencover ".\artifacts\OpenCover.*.xml" --repo-token "3HvPffZf6UKHBmBX3kZG0NSV50g3yyej5"
(New-Object System.Net.WebClient).DownloadFile("https://codecov.io/bash", ".\CodecovUploader.sh")
.\CodecovUploader.sh -X gcov -f ".\artifacts\OpenCover.*.xml"
}

