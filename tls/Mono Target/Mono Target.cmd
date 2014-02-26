@Echo Off
Echo Preparing build environment for building mono.net assemblies
If EXIST "%ProgramFiles(x86)%" goto Is64BitSystem
If NOT EXIST "%ProgramFiles(x86)%" goto Is32BitSystem

:Is64BitSystem
Set PFiles=%ProgramFiles(x86)%
Set RegFile32=mono.registration.reg
Set RegFile64=mono.registration.x64.reg
goto Start

:Is32BitSystem
Set PFiles=%ProgramFiles%
Set RegFile32=mono.registration.reg
goto Start

:Start
Echo 1. Copy the contents of %PFiles%\Mono-2.10.8\lib\mono\4.0 to %PFiles%\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\Profile\Mono.
If NOT EXIST "%PFiles%\Mono-2.10.9\lib\mono\4.0" goto MonoNotInstalled
Robocopy.exe "%PFiles%\Mono-2.10.9\lib\mono\4.0" "%PFiles%\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\Profile\Mono" *.* /S /E /V

Echo 2. Create the folder RedistList and add to the new folder a file called FrameworkList.xml with the following content
IF NOT EXIST "%PFiles%\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\Profile\Mono\RedistList" mkdir "%PFiles%\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\Profile\Mono\RedistList"
copy ".\RedistList\FrameworkList.xml" "%PFiles%\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\Profile\Mono\RedistList"

Echo 3. Update reqistry
IF EXIST ".\%RegFile32%"  regedit.exe /s ".\%RegFile32%"
IF EXIST ".\%RegFile64%" regedit.exe /s ".\%RegFile64%"

goto End

:MonoNotInstalled
Echo ERROR: Mono-2.10.9 is not installed. Unable to prepare MSBuild environment for Mono
pause
goto End

:End
