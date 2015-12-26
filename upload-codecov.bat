"SET PATH=C:\\Python34;C:\\Python34\\Scripts;%PATH%"
pip install codecov
codecov -f "c:\projects\dbcl\reports\OpenCover.BitSetArray.xml"
codecov -f "c:\projects\dbcl\reports\OpenCover.ICodeSet.xml"
codecov -f "c:\projects\dbcl\reports\OpenCover.Extensions.xml"
codecov -f "c:\projects\dbcl\reports\OpenCover.Diagnostics.xml"
