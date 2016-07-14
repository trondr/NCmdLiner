using System.Text;
using IniParser;

namespace MSBuildCustomTasks.Common
{
    public interface IIniFileOperation
    {
        string Read(string path, string section, string key);

        void Write(string path, string section, string key, string value);
    }

    public class IniFileOperation2 : IIniFileOperation
    {
        public string Read(string path, string section, string key)
        {
            var ini = new FileIniDataParser();
            var iniData = ini.ReadFile(path, Encoding.ASCII);
            var value = iniData[section][key];
            return value;
        }

        public void Write(string path, string section, string key, string value)
        {
            var ini = new FileIniDataParser();
            var iniData = ini.ReadFile(path, Encoding.ASCII);
            iniData[section][key] = value;
            ini.WriteFile(path,iniData,Encoding.ASCII);
        }
    }
}