using System.IO;

namespace CMM.Test.GUI.CmmWrappers
{
    public class FileSystemWrapper : IFileSystemWrapper
    {
        public bool DirectoryExists(string basePath) => Directory.Exists(basePath);

        public string[] GetDirectories(string basePath) => Directory.GetDirectories(basePath);

        public bool FileExists(string fileName) => File.Exists(fileName);
    }
}