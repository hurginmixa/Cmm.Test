namespace CMM.Test.GUI.ViewModels
{
    internal class MainWindowViewModel
    {
        private readonly CreatingTabViewModel _creatingTabViewModel;

        public MainWindowViewModel()
        {
            _creatingTabViewModel = new CreatingTabViewModel();
        }

        public CreatingTabViewModel CreatingTabViewModel => _creatingTabViewModel;
    }
}
