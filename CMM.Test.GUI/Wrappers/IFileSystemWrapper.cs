namespace CMM.Test.GUI.Wrappers
{
    public interface IFileSystemWrapper
    {
        string BaseResultsPath { get; }

        bool DirectoryExists(string basePath);
        
        string[] GetDirectories(string basePath);
        
        bool FileExists(string fileName);
    }
}
