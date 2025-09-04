using System.Windows;

namespace CMM.Test.GUI.Wrappers.DummyImplementations
{
    internal class DummyWrapperImplementations : IWrappers
    {
        private readonly Window _mainWindow;

        public DummyWrapperImplementations(Window mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public ICmmWrapper GetCmmWrapper() => DummyCmmWrapper.CreateTestCmmWrapper(_mainWindow);

        public IFileSystemWrapper FileSystemWrapper() => DummyFileSystemWrapper.CreateTestFileSystem();
    }
}