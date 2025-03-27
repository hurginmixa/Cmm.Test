using CMM.Test.GUI.Models;

namespace CMM.Test.GUI.ViewModels
{
    internal class MainWindowViewModel
    {
        private readonly CreatingTabViewModel _creatingTabViewModel;
        private readonly ImportUpdateViewModel _importUpdateViewModel;

        public MainWindowViewModel(CmmTestModel model)
        {
            _creatingTabViewModel = new CreatingTabViewModel(model.CreatingTabModel);

            _importUpdateViewModel = new ImportUpdateViewModel(model.ImportUpdateTabModel);
        }

        public CreatingTabViewModel CreatingTabViewModel => _creatingTabViewModel;

        public ImportUpdateViewModel ImportUpdateViewModel => _importUpdateViewModel;
    }
}
