#r "paket:
nuget Fake.IO.FileSystem
nuget Fake.DotNet.AssemblyInfoFile
nuget Fake.DotNet.MSBuild
nuget Fake.DotNet.Testing.Nunit
nuget Fake.Testing.Common
nuget Fake.DotNet.NuGet
nuget Fake.IO.Zip
nuget Fake.Runtime
nuget NUnit.Console
nuget Fake.Core.Environment
nuget Fake.Core.Target //"
#load "./.fake/build.fsx/intellisense.fsx"

open Fake.IO
open Fake.IO.Globbing.Operators //enables !! and globbing
open Fake.DotNet
open Fake.Core
open Fake.Testing
open Fake.DotNet.Testing


//Properties
let libName = "NCmdLiner"
let buildFolder = System.IO.Path.GetFullPath("./build/")
let toolsFolder = System.IO.Path.GetFullPath("./tools/")
let nugetExeFilePath = toolsFolder + "NuGet/NuGet.exe"
let buildLibFolder = buildFolder + "lib"
let buildTestFolder = buildFolder + "test"
let artifactFolder = System.IO.Path.GetFullPath("./artifact/")
let artifactLibFolder = artifactFolder + "lib"
let NugetApiKey = Fake.Core.Environment.environVarOrNone "NuGet.ApiKey"
let NugetLocalRepository = Fake.Core.Environment.environVarOrNone "NuGet.LocalRepository"
#nowarn "52"
let year = (System.DateTime.Now.Year - 2000)
let dayOfYear = (System.DateTime.Now.DayOfYear)
let buildVersion = (sprintf "%02d%03d" year dayOfYear) //Example: 19063

let assemblyVersion =
    let majorVersion = "3"
    let minorVersion = "0"    
    let revisionVersion = "344"
    sprintf "%s.%s.%s.%s" majorVersion minorVersion buildVersion revisionVersion //Example: 1.0.19063.1

let getVersion file = 
    System.Reflection.AssemblyName.GetAssemblyName(file).Version.ToString()

let getVersion' file =
    match file with
    |None
        -> ""
    |Some f -> (getVersion f)

//Targets
Target.create "Clean" (fun _ ->
    Trace.trace "Clean build folder..."
    Shell.cleanDirs [ buildFolder; artifactFolder ]
)

Target.create "RestorePackages" (fun _ ->
     ("./" + libName + ".sln")
     |> Fake.DotNet.NuGet.Restore.RestoreMSSolutionPackages (fun p ->
         { p with             
             Retries = 4 })
   )

Target.create "BuildLib" (fun _ -> 
    Trace.trace "Building lib..."    

    !! "src/lib/**/*.csproj"
        |> MSBuild.runRelease id buildLibFolder "Build"
        |> Trace.logItems "BuildLib-Output: "
)

Target.create "BuildTest" (fun _ -> 
    Trace.trace "Building test..."

    !! "src/test/**/*.csproj"
        |> MSBuild.runRelease id buildTestFolder "Build"
        |> Trace.logItems "BuildTest-Output: "
)

let nugetGlobalPackagesFolder =
    System.Environment.ExpandEnvironmentVariables("%userprofile%\.nuget\packages")

let nunitConsoleRunner =
    Trace.trace "Locating nunit-console.exe..."
    let consoleRunner = 
        !! (nugetGlobalPackagesFolder + "/**/nunit3-console.exe")
        |> Seq.head
    printfn "Console runner:  %s" consoleRunner
    consoleRunner

Target.create "Test" (fun _ -> 
    Trace.trace "Testing app..."    
    !! ("build/test/**/*.Tests.dll")    
    |> NUnit3.run (fun p ->
        {p with ToolPath = nunitConsoleRunner;Where = "cat==UnitTests";TraceLevel=NUnit3.NUnit3TraceLevel.Verbose})
)


let toProjectAndVersion appName nupkgFileName =
    let fileNameWithOutExtension = System.IO.Path.GetFileNameWithoutExtension(nupkgFileName)
    let version = fileNameWithOutExtension.Replace(appName,"").Trim([|'.'|])
    (appName, version)

let getNugetPackageFile() =
    Trace.trace "Getting Nuget package..." 
    let nugetPackageFile =
        !! (sprintf "%s\*.nupkg" artifactFolder)
        |> Seq.toArray
        |> Array.head
    Trace.trace (sprintf "Nuget package: %s" nugetPackageFile)
    new System.IO.FileInfo(nugetPackageFile)

Target.create "LocalPublish" (fun _ ->    
    match NugetLocalRepository with
    |None ->
        Fake.Runtime.Trace.traceError ("Nuget local repository folder path not set in environment variable 'NuGet.LocalRepository'.")
        Fake.Runtime.Trace.traceError ("Publish of nuget package to Nuget local respository folder will be skipped.")
    |Some localRepositoryFolder->
        Fake.IO.Shell.mkdir localRepositoryFolder
        let nugetFiles =
            !! "build\lib\*.nupkg"
            ++ "build\lib\*.snupkg"
            |>Seq.toArray
        Fake.Runtime.Trace.trace (sprintf "Publishing nuget package and nuget symbol package to folder %s: %A" localRepositoryFolder nugetFiles)
        Fake.IO.Shell.copyFiles localRepositoryFolder nugetFiles
        Fake.Runtime.Trace.trace (sprintf "Publishing nuget package and nuget symbol package to folder %s: %A" artifactFolder nugetFiles)
        Fake.IO.Shell.copyFiles artifactFolder nugetFiles
    Trace.trace( sprintf "Build version: %s" buildVersion)
)

Target.create "Publish" (fun _ ->
    Trace.trace "Publishing library to Nuget repository..."    
    let nugetPackageFile = getNugetPackageFile()    
    let (project,version) = toProjectAndVersion libName nugetPackageFile.Name    
    match NugetApiKey with
    |None ->
        Fake.Runtime.Trace.traceError ("Nuget ApiKey not set in environment variable 'NuGet.ApiKey'.")        
    |Some apikey ->
        Trace.trace("Calling NuGetPublish package...")
        Fake.DotNet.NuGet.NuGet.NuGetPublish (fun o -> 
            {o with 
                AccessKey = apikey
                ToolPath = nugetExeFilePath                    
                Project = project
                Version = version          
                WorkingDir = artifactFolder
                OutputPath = artifactFolder
                Publish = false
                PublishUrl="https://www.nuget.org"                
                PublishTrials = 1                
            }
        )        
    Trace.trace( sprintf "Build version: %s" buildVersion)
    ()
)

Target.create "Default" (fun _ ->
    Trace.trace (libName + "." + assemblyVersion)    
    Trace.trace( sprintf "Build version: %s" buildVersion)
)

//Dependencies
open Fake.Core.TargetOperators

"Clean" 
    ==> "RestorePackages"
    ==> "BuildLib"
    ==> "BuildTest"    
    ==> "Test"    
    ==> "LocalPublish"
    ==> "Publish"
    ==> "Default"

//Start build
Target.runOrDefault "Default"