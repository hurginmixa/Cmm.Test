using System;
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

            // Глобальная обработка необработанных исключений
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            IWrappers wrappers = WrappersFabric.MakeWrappers();

            IFileSystemWrapper fileSystem = wrappers.FileSystemWrapper;

            _cmmTestModel = CmmTestModelHelper.CreateFromIni(wrappers);

            MainWindow window = new MainWindow
            {
                ViewModel = new MainWindowViewModel(_cmmTestModel, fileSystem)
            };

            window.Show();

            MainWindow = window;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(
                $"An unhandled UI exception occurred:\n{e.Exception.Message}",
                "Unhandled UI Exception",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            e.Handled = true;
            Shutdown();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            string message = ex != null ? ex.Message : "Unknown error";
            MessageBox.Show(
                $"An unhandled non-UI exception occurred:\n{message}",
                "Unhandled Exception",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            Shutdown();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            CmmTestModelHelper.SaveToIni(_cmmTestModel);
        }
    }

}
