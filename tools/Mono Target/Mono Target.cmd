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
Set MonoLibFolder=%PFiles%\Mono\lib\mono\4.5
Echo MonoLibFolder=%MonoLibFolder%
If NOT EXIST "%MonoLibFolder%" goto MonoNotInstalled

Set DotNetFrameworkFolder=C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5
Echo DotNetFrameworkFolder=%DotNetFrameworkFolder%
If NOT EXIST "%DotNetFrameworkFolder%" goto DotNetFrameworkNotInstalled

Set DotNetFrameworkProfileFolder=%DotNetFrameworkFolder%\Profile
Echo DotNetFrameworkProfileFolder=%DotNetFrameworkProfileFolder%
If NOT EXIST "%DotNetFrameworkProfileFolder%" mkdir "%DotNetFrameworkProfileFolder%"

pushd %~dp0
cd "%DotNetFrameworkProfileFolder%"
mklink /d Mono "%MonoLibFolder%"
popd
Set DotNetFrameworkProfileMonoRedistListFolder=%DotNetFrameworkFolder%\Profile\Mono\RedistList
Echo DotNetFrameworkProfileMonoRedistListFolder=%DotNetFrameworkProfileMonoRedistListFolder%
If NOT EXIST "%DotNetFrameworkProfileMonoRedistListFolder%" mkdir "%DotNetFrameworkProfileMonoRedistListFolder%"
Echo copy "%~dp0RedistList\FrameworkList.xml" "%DotNetFrameworkProfileMonoRedistListFolder%\FrameworkList.xml"
copy "%~dp0RedistList\FrameworkList.xml" "%DotNetFrameworkProfileMonoRedistListFolder%\FrameworkList.xml"

Echo 3. Update reqistry
IF EXIST ".\%RegFile32%"  regedit.exe /s ".\%RegFile32%"
IF EXIST ".\%RegFile64%" regedit.exe /s ".\%RegFile64%"
goto End

:DotNetFrameworkNotInstalled
Echo ERROR: .NET Framework 4.5 is not installed. Unable to prepare MSBuild environment for Mono

:MonoNotInstalled
Echo ERROR: Mono-2.10.9 is not installed. Unable to prepare MSBuild environment for Mono
pause
goto End

:End
