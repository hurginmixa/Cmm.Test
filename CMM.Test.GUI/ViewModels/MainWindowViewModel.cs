namespace CMM.Test.GUI.ViewModels
{
    internal class MainWindowViewModel
    {
        private readonly CreatingTabViewModel _creatingTabViewModel;
        private readonly ImportUpdateViewModel _importUpdateViewModel;

        public MainWindowViewModel()
        {
            _creatingTabViewModel = new CreatingTabViewModel();

            _importUpdateViewModel = new ImportUpdateViewModel();
        }

        public CreatingTabViewModel CreatingTabViewModel => _creatingTabViewModel;

        public ImportUpdateViewModel ImportUpdateViewModel => _importUpdateViewModel;
    }
}
