namespace CMM.Test.GUI.UnitTests;

public class TemporaryFolder : IDisposable
{
    private readonly string _folderPath;

    public TemporaryFolder()
    {
        _folderPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

        Directory.CreateDirectory(_folderPath);
    }

    public string FolderPath => _folderPath;

    public void Dispose()
    {
        Directory.Delete(_folderPath, recursive: true);
    }
}