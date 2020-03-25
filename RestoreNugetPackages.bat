SET var=%cd%

forfiles /S /M *.sln  /C "cmd /c %var%\Tools\nuget.exe restore @path"
pause