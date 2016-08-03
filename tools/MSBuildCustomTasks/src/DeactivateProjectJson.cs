using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Tasks;

namespace MSBuildCustomTasks
{
    public class DeActivateProjectJson : CallTarget
    {
        public override bool Execute()
        {
            if (!Directory.Exists(ProjectFolder))
            {
                Log.LogError("Unable to deactivate project.json Project folder not found: " + ProjectFolder);
                return false;
            }

            var projectJson = Path.Combine(ProjectFolder, "project.json");
            var projectLockJson = Path.Combine(ProjectFolder, "project.lock.json");
            var deactivated = DeActivateFile(projectJson);
            if(File.Exists(projectLockJson))
                deactivated &= DeActivateFile(projectLockJson);
            if (!deactivated)
                return false;
            return base.Execute();
        }

        private bool DeActivateFile(string file)
        {
            var deActiveFile = file + ".deactivate";
            if (File.Exists(file))
            {
                Rename(file, deActiveFile);
                return true;
            }
            if (File.Exists(deActiveFile))
            {
                Log.LogMessage(MessageImportance.Normal,"File '{0}' has allready been deactivated.", file);
                return true;
            }
            else
            {
                Log.LogError("Unable to deactivate file '{0}' as the file cannot be found in the project folder '{1}'.", file, ProjectFolder);
                return false;
            }
        }

        private void Rename(string file1, string file2)
        {
            if (File.Exists(file1))
            {
                Delete(file2);
                Log.LogMessage(MessageImportance.Normal, "Renaming '{0}'->'{1}'", file1, file2);
                File.Move(file1, file2);
            }
        }

        private void Delete(string file)
        {
            if (File.Exists(file))
            {
                Log.LogMessage(MessageImportance.Normal, "Deleting '{0}'...", file);
                File.Delete(file);
            }
        }

        [Required]
        public string ProjectFolder { get; set; }
    }
}