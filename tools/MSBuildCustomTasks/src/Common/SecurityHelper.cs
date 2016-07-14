using System.Security.Principal;

namespace MSBuildCustomTasks.Common
{
    public class SecurityHelper
    {
        public static string GetCurrentWindowsIdentityName()
        {
            var windowsIdentity = WindowsIdentity.GetCurrent();
            if (windowsIdentity != null)
                return windowsIdentity.Name;
            return "<unknown>";
        }
    }
}
