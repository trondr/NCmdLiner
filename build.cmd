@Echo Off
Set ProductName=NCmdLiner

IF EXIST "%VSDEVCMD%" goto Build
IF EXIST "%MSBUILDPATH%" goto Build

:VSEnv
Set VSDEVCMD=%VS140COMNTOOLS%VsDevCmd.bat
Echo Checking to see if Visual Studio 2015 is installed ("%VS140COMNTOOLS%")
IF NOT EXIST "%VSDEVCMD%" set BuildMessage="Visual Studio 2015 do not seem to be installed, trying MSBuild instead..." & goto MSBuildEnv
Echo Preparing build environment...
call "%VSDEVCMD%"
goto Build

:MSBuildEnv
Set MSBUILDPATH=%ProgramFiles(x86)%\MSBuild\14.0\Bin
Echo Checking to see if MSBuild is installed ("%MSBUILDPATH%")
IF NOT EXIST "%MSBUILDPATH%" set BuildMessage="Neither Visual Studio 2015 or MSBuild  seem to be installed. Terminating." & goto end
Set Path=%Path%;%MSBUILDPATH%

:Build
Echo Building %ProductName%...
Echo msbuild.exe %ProductName%.build %1 %2 %3 %4 %5 %6 %7 %8 %9
msbuild.exe %ProductName%.build %1 %2 %3 %4 %5 %6 %7 %8 %9
Set BuildErrorLevel=%ERRORLEVEL%
IF %BuildErrorLevel%==0 Set BuildMessage=Sucessfully build %ProductName%
IF NOT %BuildErrorLevel% == 0 Set BuildMessage=Failed to build %ProductName%

REM dotnet restore "%~dp0src\%ProductName%"
REM Set Configuration=Release
REM 
REM Set DotNetFrameWork=net35
REM Set OutputDir=%DotNetFrameWork%
REM dotnet build "%~dp0src\%ProductName%" --output "%~dp0bin\%Configuration%\%OutputDir%" --build-base-path "%~dp0bin\obj\%Configuration%\%OutputDir%" --configuration Release --framework %DotNetFrameWork%
REM 
REM Set DotNetFrameWork=net451
REM Set OutputDir=%DotNetFrameWork%
REM dotnet build "%~dp0src\%ProductName%" --output "%~dp0bin\%Configuration%\%OutputDir%" --build-base-path "%~dp0bin\obj\%Configuration%\%OutputDir%" --configuration Release --framework %DotNetFrameWork%
REM 
REM 
REM Set DotNetFrameWork=net452
REM Set OutputDir=%DotNetFrameWork%
REM dotnet build "%~dp0src\%ProductName%" --output "%~dp0bin\%Configuration%\%OutputDir%" --build-base-path "%~dp0bin\obj\%Configuration%\%OutputDir%" --configuration Release --framework %DotNetFrameWork%
REM 
REM Set DotNetFrameWork=net461
REM Set OutputDir=%DotNetFrameWork%
REM dotnet build "%~dp0src\%ProductName%" --output "%~dp0bin\%Configuration%\%OutputDir%" --build-base-path "%~dp0bin\obj\%Configuration%\%OutputDir%" --configuration Release --framework %DotNetFrameWork%
REM 
REM Set DotNetFrameWork=.NETFramework,Version=v4.5,Profile=Mono
REM Set OutputDir=net45-mono
REM dotnet build "%~dp0src\%ProductName%" --output "%~dp0bin\%Configuration%\%OutputDir%" --build-base-path "%~dp0bin\obj\%Configuration%\%OutputDir%" --configuration Release --framework %DotNetFrameWork%
REM 
REM Set DotNetFrameWork=netstandard1.6
REM Set OutputDir=%DotNetFrameWork%
REM dotnet build "%~dp0src\%ProductName%" --output "%~dp0bin\%Configuration%\%OutputDir%" --build-base-path "%~dp0bin\obj\%Configuration%\%OutputDir%" --configuration Release --framework %DotNetFrameWork%
REM 
REM dotnet restore "%~dp0test\%ProductName%.Tests"
REM Set DotNetFrameWork=netcoreapp1.0
REM Set OutputDir=%DotNetFrameWork%
REM dotnet build "%~dp0test\%ProductName%.Tests" --output "%~dp0bin\%Configuration%\Tests\%OutputDir%" --build-base-path "%~dp0bin\obj\%Configuration%\Tests\%OutputDir%" --configuration Release --framework %DotNetFrameWork%
REM dotnet test "%~dp0test\%ProductName%.Tests" --output "%~dp0bin\%Configuration%\Tests\%OutputDir%" --build-base-path "%~dp0bin\obj\%Configuration%\Tests\%OutputDir%" --configuration Release --framework %DotNetFrameWork%
REM 
REM Set DotNetFrameWork=net461
REM Set OutputDir=%DotNetFrameWork%
REM dotnet build "%~dp0test\%ProductName%.Tests" --output "%~dp0bin\%Configuration%\Tests\%OutputDir%" --build-base-path "%~dp0bin\obj\%Configuration%\Tests\%OutputDir%" --configuration Release --framework %DotNetFrameWork%
REM dotnet test "%~dp0test\%ProductName%.Tests" --output "%~dp0bin\%Configuration%\Tests\%OutputDir%" --build-base-path "%~dp0bin\obj\%Configuration%\Tests\%OutputDir%" --configuration Release --framework %DotNetFrameWork%
REM 
REM dotnet pack "%~dp0src\%ProductName%" --output "%~dp0bin\NugetPackages" --build-base-path "%~dp0bin\obj\%Configuration% --configuration Release

:End
Echo %BuildMessage%
