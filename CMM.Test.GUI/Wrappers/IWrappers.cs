namespace CMM.Test.GUI.Wrappers
{
    public interface IWrappers
    {
        ICmmWrapper CmmWrapper { get; }

        IFileSystemWrapper FileSystemWrapper { get; }
    }
}