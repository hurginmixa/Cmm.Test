using System.Configuration;
using System.Data;
using System.Windows;
using CMM.Test.GUI.Models;
using CMM.Test.GUI.ViewModels;
using CMM.Test.GUI.Views;
using CMM.Test.GUI.Wrappers;

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

            IWrappers wrappers = WrappersFabric.MakeWrappers(MainWindow);

            ICmmWrapper cmmWrapper = wrappers.GetCmmWrapper();

            IFileSystemWrapper fileSystem = wrappers.FileSystemWrapper();

            _cmmTestModel = CmmTestModelHelper.CreateFromIni(cmmWrapper);

            MainWindow window = new MainWindow
            {
                ViewModel = new MainWindowViewModel(_cmmTestModel, fileSystem)
            };

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
