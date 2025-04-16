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
        private CmmTestModel _cmmTestModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DummyCmmWrapper cmmWrapper = DummyCmmWrapper.CreateTestCmmWrapper();

            cmmWrapper.DoCreateEvent += s =>
            {
                if (MainWindow != null)
                {
                    MessageBox.Show(MainWindow, $" Creating {s}");
                }

                return true;
            };

            cmmWrapper.OpenCreatingRtpEvent += s =>
            {
                if (MainWindow != null)
                {
                    MessageBox.Show(MainWindow, $" Creating Rrp {s}");
                }
            };

            _cmmTestModel = CmmTestModelHelper.CreateFromIni(cmmWrapper);

            DummyFileSystemWrapper fileSystem = DummyFileSystemWrapper.CreateTestFileSystem(_cmmTestModel.BaseResultsPath);

            MainWindowViewModel viewModel = new MainWindowViewModel(_cmmTestModel, fileSystem);
            
            MainWindow window = new MainWindow(viewModel);
            
            window.Show();

            MainWindow = window;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            CmmTestModelHelper.SaveToIni(_cmmTestModel);
        }
    }

}
