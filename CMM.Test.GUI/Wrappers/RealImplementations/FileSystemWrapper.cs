using System.IO;

namespace CMM.Test.GUI.Wrappers.RealImplementations
{
    public class FileSystemWrapper : IFileSystemWrapper
    {
        public string BaseResultsPath => throw new System.NotImplementedException();

        public bool DirectoryExists(string basePath) => Directory.Exists(basePath);

        public string[] GetDirectories(string basePath) => Directory.GetDirectories(basePath);

        public bool FileExists(string fileName) => File.Exists(fileName);
    }
}