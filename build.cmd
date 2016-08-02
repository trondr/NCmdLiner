@Echo Off
Set ProductName=NCmdLiner
dotnet restore "%~dp0src\%ProductName%"

Set Configuration=Release

Set DotNetFrameWork=net35
Set OutputDir=%DotNetFrameWork%
dotnet build "%~dp0src\%ProductName%" --output "%~dp0bin\%Configuration%\%OutputDir%" --build-base-path "%~dp0bin\obj\%Configuration%\%OutputDir%" --configuration Release --framework %DotNetFrameWork%

Set DotNetFrameWork=net452
Set OutputDir=%DotNetFrameWork%
dotnet build "%~dp0src\%ProductName%" --output "%~dp0bin\%Configuration%\%OutputDir%" --build-base-path "%~dp0bin\obj\%Configuration%\%OutputDir%" --configuration Release --framework %DotNetFrameWork%

Set DotNetFrameWork=net461
Set OutputDir=%DotNetFrameWork%
dotnet build "%~dp0src\%ProductName%" --output "%~dp0bin\%Configuration%\%OutputDir%" --build-base-path "%~dp0bin\obj\%Configuration%\%OutputDir%" --configuration Release --framework %DotNetFrameWork%

Set DotNetFrameWork=.NETFramework,Version=v4.5,Profile=Mono
Set OutputDir=net45-mono
dotnet build "%~dp0src\%ProductName%" --output "%~dp0bin\%Configuration%\%OutputDir%" --build-base-path "%~dp0bin\obj\%Configuration%\%OutputDir%" --configuration Release --framework %DotNetFrameWork%

Set DotNetFrameWork=netstandard1.6
Set OutputDir=%DotNetFrameWork%
dotnet build "%~dp0src\%ProductName%" --output "%~dp0bin\%Configuration%\%OutputDir%" --build-base-path "%~dp0bin\obj\%Configuration%\%OutputDir%" --configuration Release --framework %DotNetFrameWork%

dotnet restore "%~dp0test\%ProductName%.Tests"
Set DotNetFrameWork=netcoreapp1.0
Set OutputDir=%DotNetFrameWork%
dotnet build "%~dp0test\%ProductName%.Tests" --output "%~dp0bin\%Configuration%\Tests\%OutputDir%" --build-base-path "%~dp0bin\obj\%Configuration%\Tests\%OutputDir%" --configuration Release --framework %DotNetFrameWork%
dotnet test "%~dp0test\%ProductName%.Tests" --output "%~dp0bin\%Configuration%\Tests\%OutputDir%" --build-base-path "%~dp0bin\obj\%Configuration%\Tests\%OutputDir%" --configuration Release --framework %DotNetFrameWork%

Set DotNetFrameWork=net461
Set OutputDir=%DotNetFrameWork%
dotnet build "%~dp0test\%ProductName%.Tests" --output "%~dp0bin\%Configuration%\Tests\%OutputDir%" --build-base-path "%~dp0bin\obj\%Configuration%\Tests\%OutputDir%" --configuration Release --framework %DotNetFrameWork%
dotnet test "%~dp0test\%ProductName%.Tests" --output "%~dp0bin\%Configuration%\Tests\%OutputDir%" --build-base-path "%~dp0bin\obj\%Configuration%\Tests\%OutputDir%" --configuration Release --framework %DotNetFrameWork%

dotnet pack "%~dp0src\%ProductName%" --output "%~dp0bin\NugetPackages" --build-base-path "%~dp0bin\obj\%Configuration% --configuration Release
