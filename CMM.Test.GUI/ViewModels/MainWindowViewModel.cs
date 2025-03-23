using CMM.Test.GUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CMM.Test.GUI.Models;

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

            var dialog = new SelectFolderView(basePath, selectedFolderModel);
            dialog.Owner = (Window) o;
            dialog.ShowDialog();
        }
    }
}
