D:
cd "D:\Work2\IITS\Project\BRC\Source Codes\git"
ng build
pause
C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe ICMS\ICMS.csproj /p:DeployOnBuild=true
pause
rem xcopy "D:\Work2\IITS\Project\BRC\Source Codes\git\ICMS\obj\Debug\Package\PackageTmp" "D:\Work2\IITS\Project\BRC\Source Codes\git\Published\ICMSCommSvc\" /S /Y
rem pause
rem xcopy "D:\Work2\IITS\Project\BRC\Source Codes\git\ICMS\obj\Debug\Package\PackageTmp" "D:\Work2\IITS\Project\BRC\Source Codes\git\Published\Internship\" /S /Y
rem pause
REM copy "D:\Work2\IITS\Project\BRC\Source Codes\git\Published\main.js.commsvcv1.txt" "D:\Work2\IITS\Project\BRC\Source Codes\git\Published\ICMSCommSvc\Scripts\libs\main.js" /Y
REM copy "D:\Work2\IITS\Project\BRC\Source Codes\git\Published\main.js.internshipv1.txt" "D:\Work2\IITS\Project\BRC\Source Codes\git\Published\ICMSInternship\Scripts\libs\main.js" /Y
pause
