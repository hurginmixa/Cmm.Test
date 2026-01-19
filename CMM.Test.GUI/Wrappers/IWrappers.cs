namespace CMM.Test.GUI.Wrappers
{
    public interface IWrappers
    {
        ICmmWrapper GetCmmWrapper();
        
        IFileSystemWrapper GetFileSystemWrapper();
    }
}