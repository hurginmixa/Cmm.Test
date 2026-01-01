using System.Windows;

namespace CMM.Test.GUI.Wrappers.DummyImplementations
{
    internal class DummyWrapperImplementations : IWrappers
    {
        public DummyWrapperImplementations()
        {
        }

        public ICmmWrapper GetCmmWrapper() => DummyCmmWrapper.CreateTestCmmWrapper();

        public IFileSystemWrapper FileSystemWrapper() => DummyFileSystemWrapper.CreateTestFileSystem();
    }
}