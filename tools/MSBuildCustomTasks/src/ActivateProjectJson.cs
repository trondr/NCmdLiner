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

            var projectJson = Path.Combine(ProjectFolder, "project.json");
            var projectJsonDeactivate = Path.Combine(ProjectFolder, "project.json.deactivate");
            if (File.Exists(projectJsonDeactivate))
            {
                Rename(projectJsonDeactivate, projectJson);
            }
            else if (File.Exists(projectJson))
            {
                Log.LogMessage(MessageImportance.Normal,"project.json has allready been activated.");
            }
            else
            {
                Log.LogError("Unable to activate project.json. There is no project.json to be found in project folder '{0}'.", ProjectFolder);
                return false;
            }
            return base.Execute();
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