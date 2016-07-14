namespace MSBuildCustomTasks.Common
{
    public interface IMsiFileInfo
    {
        string GetMsiProductCode(string msiFilePath);
    }
}