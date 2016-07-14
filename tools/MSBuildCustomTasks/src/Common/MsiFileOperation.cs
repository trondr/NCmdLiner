using System;
using System.IO;
using Microsoft.Deployment.WindowsInstaller;

namespace MSBuildCustomTasks.Common
{
    public class MsiFileOperation : IMsiFileInfo
    {
        public string GetMsiProductCode(string msiFilePath)
        {
            if (string.IsNullOrEmpty(msiFilePath)) throw new ArgumentNullException("msiFilePath");
            if (!File.Exists(msiFilePath)) throw new FileNotFoundException("msi file not found", msiFilePath);
            using (var database = new Database(msiFilePath, DatabaseOpenMode.ReadOnly))
            {
                return GetMsiProperty(database, "ProductCode");
            }
        }

        /// <summary>
        /// Get Msi Property
        /// </summary>
        /// <param name="database"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static string GetMsiProperty(Database database, string propertyName)
        {
            using (View view = database.OpenView("SELECT Value FROM Property WHERE Property.Property='{0}'", propertyName))
            {
                view.Execute();
                using (Record record = view.Fetch())
                {
                    return record[1].ToString();
                }
            }
        }
    }
}
