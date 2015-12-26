@if not "%appveyor%" == "True" goto end
"SET PATH=C:\\Python34;C:\\Python34\\Scripts;%PATH%"
pip install codecov > nul
codecov -f "c:\projects\dbcl\reports\OpenCover.BitSetArray.xml" > nul
codecov -f "c:\projects\dbcl\reports\OpenCover.ICodeSet.xml" > nul
codecov -f "c:\projects\dbcl\reports\OpenCover.Extensions.xml" > nul
codecov -f "c:\projects\dbcl\reports\OpenCover.Diagnostics.xml" > nul
:end
