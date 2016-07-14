using System.IO;

namespace MSBuildCustomTasks
{
    public class DirectoryOperation
    {
        /// <summary>
        /// http://stackoverflow.com/questions/58744/best-way-to-copy-the-entire-contents-of-a-directory-in-c-sharp
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        public static void CopyDirectory(string sourcePath, string destinationPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(sourcePath, destinationPath), true);
        }
    }
}