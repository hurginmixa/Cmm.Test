using System.Windows;

namespace CMM.Test.GUI.Wrappers.DummyImplementations
{
    internal class DummyWrapperImplementations : IWrappers
    {
        public ICmmWrapper CmmWrapper => DummyCmmWrapper.CreateTestCmmWrapper();

        public IFileSystemWrapper FileSystemWrapper => DummyFileSystemWrapper.CreateTestFileSystem();
    }
}