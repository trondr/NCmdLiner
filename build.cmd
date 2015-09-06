@ECHO OFF
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
msbuild.exe %ProductName%.build %1 %2 %3 %4 %5 %6 %7 %8 %9
Set BuildErrorLevel=%ERRORLEVEL%
IF %BuildErrorLevel%==0 Set BuildMessage=Sucessfully build %ProductName%
IF NOT %BuildErrorLevel% == 0 Set BuildMessage=Failed to build %ProductName%

:End
Echo %BuildMessage%
