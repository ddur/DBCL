(New-Object System.Net.WebClient).DownloadFile("https://codecov.io/bash", ".\CodecovUploader.sh")
.\CodecovUploader.sh -X gcov -f "c:\projects\dbcl\reports\OpenCover.BitSetArray.xml" -f "c:\projects\dbcl\reports\OpenCover.ICodeSet.xml" -f "c:\projects\dbcl\reports\OpenCover.Extensions.xml" -f "c:\projects\dbcl\reports\OpenCover.Text.xml"
