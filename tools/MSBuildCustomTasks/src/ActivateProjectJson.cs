using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Tasks;

namespace MSBuildCustomTasks
{
    public class ActivateProjectJson : CallTarget
    {
        public override bool Execute()
        {
            if (!Directory.Exists(ProjectFolder))
            {
                Log.LogError("Unable to deactivate project.json Project folder not found: " + ProjectFolder);
                return false;
            }

            var projectJsonDeactivate = Path.Combine(ProjectFolder, "project.json.deactivate");
            var projectLockJsonDeactivate = Path.Combine(ProjectFolder, "project.lock.json.deactivate");
            var activated = ActivateFile(projectJsonDeactivate);
            if(File.Exists(projectLockJsonDeactivate))
                activated &= ActivateFile(projectLockJsonDeactivate);
            if (!activated)
                return false;
            return base.Execute();
        }

        private bool ActivateFile(string file)
        {
            var activeFile = file.Replace(".deactivate", "");
            if (File.Exists(file))
            {
                Rename(file, activeFile);
                return true;
            }
            if (File.Exists(activeFile))
            {
                Log.LogMessage(MessageImportance.Normal,"File '{0}' has allready been activated.", file);
                return true;
            }
            else
            {
                Log.LogError("Unable to activate file '{0}' as the file cannot be found in the project folder '{1}'.", file, ProjectFolder);
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