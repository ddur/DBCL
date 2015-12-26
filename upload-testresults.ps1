$wc = New-Object 'System.Net.WebClient'
$wc.UploadFile("https://ci.appveyor.com/api/testresults/nunit/($env:APPVEYOR_JOB_ID)", "($env:APPVEYOR_BUILD_FOLDER)\reports\NUnit*.xml")
