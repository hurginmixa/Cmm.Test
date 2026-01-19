namespace CMM.Test.GUI.Wrappers.RealImplementations
{
    internal class RealWrapperImplementation: IWrappers
    {
        public ICmmWrapper CmmWrapper => new RealCmmWrapper();

        public IFileSystemWrapper FileSystemWrapper => new RealFileSystemWrapper();
    }
}