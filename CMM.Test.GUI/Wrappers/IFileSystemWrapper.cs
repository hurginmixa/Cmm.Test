namespace CMM.Test.GUI.Wrappers
{
    public interface IFileSystemWrapper
    {
        bool DirectoryExists(string basePath);
        
        string[] GetDirectories(string basePath);
        
        bool FileExists(string fileName);
    }
}
