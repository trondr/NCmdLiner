@Echo Off
Set ProductName=NCmdLiner
dotnet build "%~dp0src\%ProductName%"
dotnet build "%~dp0test\%ProductName%.Tests"
dotnet test "%~dp0test\%ProductName%.Tests"