namespace CMM.Test.GUI.Wrappers
{
    internal interface IWrappers
    {
        ICmmWrapper GetCmmWrapper();
        
        IFileSystemWrapper FileSystemWrapper();
    }
}