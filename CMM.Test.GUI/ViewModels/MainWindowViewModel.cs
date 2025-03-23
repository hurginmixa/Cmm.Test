using CMM.Test.GUI.Views;
using System.Windows;
using System.Windows.Input;
using CMM.Test.GUI.Models;
using CMM.Test.GUI.Tools;

namespace CMM.Test.GUI.ViewModels
{
    internal class MainWindowViewModel
    {
        public ICommand OpenDialogCommand { get; }

        public MainWindowViewModel()
        {
            OpenDialogCommand = new RelayCommand(o => OpenDialog(o));
        }

        private void OpenDialog(object o)
        {
            string basePath = @"\\mixa7th\c$\Falcon\ScanResults";
            SelectedFolderModel selectedFolderModel = new SelectedFolderModel();

            bool result = true;

            while (result)
            {
                var dialog = new SelectFolderView(basePath, selectedFolderModel);
                dialog.Owner = (Window) o;

                result = dialog.ShowDialog() ?? false;
            }
        }
    }
}
