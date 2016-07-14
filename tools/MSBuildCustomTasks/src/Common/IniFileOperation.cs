using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace MSBuildCustomTasks.Common
{
    
    /// <summary>
    /// Ini file operation
    /// </summary>
    public class IniFileOperation : IIniFileOperation
    {        
        [DllImport("kernel32", SetLastError = true)]
        private static extern int WritePrivateProfileString(string section, string key,string val,string filePath);
        [DllImport("kernel32", SetLastError = true)]
        private static extern int GetPrivateProfileString(string section, string key,string def, StringBuilder retVal, int size,string filePath);

        public string Read(string path, string section, string key)
        {
            var value = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", value, 255, path);
            var errorCode = Marshal.GetLastWin32Error();
            if(errorCode != 0)
            {
                throw new Win32Exception(errorCode, string.Format("Failed to read '\"{0}\"[{1}]{2}'", path, section, key));
            }
            return value.ToString();
        }

        public void Write(string path, string section, string key, string value)
        {
            WritePrivateProfileString(section,key,value,path);
            var errorCode = Marshal.GetLastWin32Error();
            if(errorCode != 0)
            {
                throw new Win32Exception(errorCode, string.Format("Failed to write '\"{0}\"[{1}]{2}={3}'", path, section, key, value));
            }
        }
    }
}
