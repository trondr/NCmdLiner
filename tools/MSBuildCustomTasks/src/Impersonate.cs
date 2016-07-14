using System.Security.Principal;
using Microsoft.Build.Framework;
using Microsoft.Build.Tasks;
using MSBuildCustomTasks.Common;

namespace MSBuildCustomTasks
{
    /// <summary>
    /// Source: http://stackoverflow.com/questions/1160311/how-can-i-copy-files-as-a-different-user-from-msbuild
    /// </summary>
    public class Impersonate : CallTarget
    {
        public string Domain { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public override bool Execute()
        {
            using (new Impersonator(UserName, Domain, Password))
            {
                Log.LogMessage(MessageImportance.Normal, "Windows identity before execution of task: " + GetCurrentWindowsIdentity());                                
                Log.LogMessage(MessageImportance.Normal, "Impersonate tasks='{0}'. UserName={1}; Domain={2}", string.Join(";", Targets), UserName, Domain);
                try
                {
                    return base.Execute();
                }
                finally
                {
                    Log.LogMessage(MessageImportance.Normal, "Impersonated windows identity after execution of task: " + GetCurrentWindowsIdentity());
                }
            }
        }

        private string GetCurrentWindowsIdentity()
        {
            var windowsIdentity = WindowsIdentity.GetCurrent();
            if (windowsIdentity != null)
                return windowsIdentity.Name;
            return "<unknown>";
        }
    }
}
