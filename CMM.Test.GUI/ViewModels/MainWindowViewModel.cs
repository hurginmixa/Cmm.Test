using CMM.Test.GUI.CmmWrappers;
using CMM.Test.GUI.Models;

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
