using CMM.Test.GUI.Models;
using CMM.Test.GUI.Wrappers;

namespace CMM.Test.GUI.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly CreatingTabViewModel _creatingTabViewModel;
        private readonly ImportUpdateViewModel _importUpdateViewModel;

        public MainWindowViewModel(CmmTestModel model, IFileSystemWrapper fileSystem)
        {
            _creatingTabViewModel = new CreatingTabViewModel(model.CreatingTabModel, fileSystem);

            _importUpdateViewModel = new ImportUpdateViewModel(model.ImportUpdateTabModel, fileSystem);
        }

        public CreatingTabViewModel CreatingTabViewModel => _creatingTabViewModel;

        public ImportUpdateViewModel ImportUpdateViewModel => _importUpdateViewModel;
    }
}
