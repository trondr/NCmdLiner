@ECHO OFF
Set ProductName=NCmdLiner

IF EXIST "%VSDEVCMD%" goto Build

:Env
Set VSDEVCMD=%VS120COMNTOOLS%VsDevCmd.bat
IF NOT EXIST "%VSDEVCMD%" set BuildMessage="Visual Studio 2013 do not seem to be installed. Terminating." & goto End
Echo Preparing build environment...
call "%VSDEVCMD%"

:Build
Echo Building %ProductName%...
msbuild.exe %ProductName%.build %1 %2 %3 %4 %5 %6 %7 %8 %9
Set BuildErrorLevel=%ERRORLEVEL%
IF %BuildErrorLevel%==0 Set BuildMessage=Sucessfully build %ProductName%
IF NOT %BuildErrorLevel% == 0 Set BuildMessage=Failed to build %ProductName%

:End
Echo %BuildMessage%
