using System;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Tasks;
using MSBuildCustomTasks.Common;

namespace MSBuildCustomTasks
{
    public class ResolveScriptInstallPackage: CallTarget
    {
        public override bool Execute()
        {
            Log.LogMessage(MessageImportance.Normal, "Copying script install package template '{0}' to script install package target '{1}'...", ScriptInstallPackageSourcePath, ScriptInstallPackageTargetPath);
            DirectoryOperation.CopyDirectory(ScriptInstallPackageSourcePath, ScriptInstallPackageTargetPath);
            
            Log.LogMessage(MessageImportance.Normal, "Copying Windows Installer file '{0}' to script install package target '{1}'...", SourceMsiFile, TargetMsiFile);
            File.Copy(SourceMsiFile, TargetMsiFile, true);

            Log.LogMessage(MessageImportance.Normal, "Updating vendor install ini '{0}'...", VendorInstallIni);
            if (!File.Exists(VendorInstallIni))
                throw new FileNotFoundException("Vendor install ini file not found: " + VendorInstallIni);
            var iniFileOperation = new IniFileOperation2();
            iniFileOperation.Write(VendorInstallIni, "VendorInstall", "MsiFile", Path.GetFileName(TargetMsiFile));

            Log.LogMessage(MessageImportance.Normal, "Updating package definition file '{0}'...", PackageDefinitionSms);

            iniFileOperation.Write(PackageDefinitionSms, "Package Definition", "Name", PackageDefinitionName);
            iniFileOperation.Write(PackageDefinitionSms, "Package Definition", "Version", PackageDefinitionVersion);
            iniFileOperation.Write(PackageDefinitionSms, "Package Definition", "Publisher", PackageDefinitionPublisher);
            iniFileOperation.Write(PackageDefinitionSms, "INSTALL", "CommandLine", PackageDefinitionInstallCommandLine);            
            iniFileOperation.Write(PackageDefinitionSms, "UNINSTALL", "CommandLine", PackageDefinitionUnInstallCommandLine);
            var msiFileOperation = new MsiFileOperation();
            iniFileOperation.Write(PackageDefinitionSms, "DetectionMethod", "MsiProductCode", msiFileOperation.GetMsiProductCode(TargetMsiFile));
            return base.Execute();
        }

        [Required]
        public string ScriptInstallPackageSourcePath { get; set; }

        [Required]
        public string ScriptInstallPackageTargetPath { get; set; }

        [Required]
        public string SourceMsiFile { get; set; }
        
        [Required]
        public string TargetMsiFile { get; set; }
       

        [Required]
        public string VendorInstallIni { get; set; }

        [Required]
        public string PackageDefinitionSms { get; set; }

        [Required]
        public string PackageDefinitionName { get; set; }

        [Required]
        public string PackageDefinitionVersion { get; set; }

        [Required]
        public string PackageDefinitionPublisher { get; set; }

        [Required]
        public string PackageDefinitionInstallCommandLine { get; set; }

        [Required]
        public string PackageDefinitionUnInstallCommandLine { get; set; }

    }
}
