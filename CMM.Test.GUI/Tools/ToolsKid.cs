using CMM.Test.GUI.CmmWrappers;
using CMM.Test.GUI.Models;
using CMM.Test.GUI.Views;
using System.Windows;

namespace CMM.Test.GUI.Tools
{
    internal static class ToolsKid
    {
        public static bool OpenCheckResultDialog(Window owner, string baseResultPath, SelectedFolderModel selectedFolderModel, IFileSystemWrapper fileSystem)
        {
            var dialog = new SelectFolderView(baseResultPath, selectedFolderModel, fileSystem)
            {
                Owner = owner
            };

            return dialog.ShowDialog() ?? false;
        }
    }
}
