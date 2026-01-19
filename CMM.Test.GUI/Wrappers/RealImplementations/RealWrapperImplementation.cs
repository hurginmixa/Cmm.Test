namespace CMM.Test.GUI.Wrappers.RealImplementations
{
    internal class RealWrapperImplementation: IWrappers
    {
        public ICmmWrapper GetCmmWrapper() => new RealCmmWrapper();

        public IFileSystemWrapper GetFileSystemWrapper() => new RealFileSystemWrapper();
    }
}