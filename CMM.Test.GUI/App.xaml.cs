using System.Configuration;
using System.Data;
using System.Windows;
using CMM.Test.GUI.Models;
using CMM.Test.GUI.ViewModels;
using CMM.Test.GUI.Views;

namespace CMM.Test.GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //
            string basePath = @"\\mixa7th\c$\Falcon\ScanResults";
            SelectedFolderModel selectedFolderModel = new SelectedFolderModel();

            var window = new SelectFolderView(basePath, selectedFolderModel);

            //
            //var window = new MainWindow();
            
            window.Show();

            this.MainWindow = window;
        }
    }

}
