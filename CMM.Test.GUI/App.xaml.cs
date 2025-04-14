using System.Configuration;
using System.Data;
using System.Windows;
using CMM.Test.GUI.CmmWrappers.DummyImplementations;
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

            DummyCmmWrapper cmmWrapper = DummyCmmWrapper.CreateTestCmmWrapper();

            cmmWrapper.DoCreateEvent += s =>
            {
                return true;
            };

            CmmTestModel cmmTestModel = new CmmTestModel(cmmWrapper);

            DummyFileSystemWrapper fileSystem = DummyFileSystemWrapper.CreateTestFileSystem(cmmTestModel.BaseResultsPath);

            MainWindowViewModel viewModel = new MainWindowViewModel(cmmTestModel, fileSystem);
            
            MainWindow window = new MainWindow(viewModel);
            
            window.Show();

            this.MainWindow = window;
        }
    }

}
