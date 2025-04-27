using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using CMM.Test.GUI.CmmWrappers;
using CMM.Test.GUI.Models;
using CMM.Test.GUI.Tools;

namespace CMM.Test.GUI.ViewModels
{
    public class ImportUpdateViewModel : NotifyPropertyHandler
    {
        private readonly ImportUpdateTabModel _importUpdateTabModel;
        private readonly IFileSystemWrapper _fileSystem;

        public ImportUpdateViewModel(ImportUpdateTabModel importUpdateTabModel, IFileSystemWrapper fileSystem)
        {
            _importUpdateTabModel = importUpdateTabModel;
            _fileSystem = fileSystem;

            SetResultPathCommand = new RelayCommand(OnCheckResult);
        }

        private void OnCheckResult(object o)
        {
            SelectedFolderModel selectedFolderModel = new SelectedFolderModel()
            {
                Job = "",
                Setup = "",
                Lot = "",
                WaferId = ""
            };

            if (!ToolsKid.OpenCheckResultDialog((Window) o, _importUpdateTabModel.CmmTestModel.BaseResultsPath, selectedFolderModel, _fileSystem))
            {
                return;
            }

            ResultPath = selectedFolderModel.ResultPath;
        }

        public ICommand SetResultPathCommand { get; }

        public string ResultPath
        {
            get => _importUpdateTabModel.ResultPath;
            set => SetField(_importUpdateTabModel.ResultPath, value);
        }

        public bool UsingResultPath
        {
            get => _importUpdateTabModel.UsingResultPath;
            set => SetField(_importUpdateTabModel.UsingResultPath, value);
        }

        public string LotId
        {
            get => _importUpdateTabModel.LotId;
            set => SetField(_importUpdateTabModel.LotId, value);
        }

        public string WaferId
        {
            get => _importUpdateTabModel.WaferId;
            set => SetField(_importUpdateTabModel.WaferId, value);
        }

        public string WaferMapMask
        {
            get => _importUpdateTabModel.WaferMapMask;
            set => SetField(_importUpdateTabModel.WaferMapMask, value);
        }

        public string SubmapId
        {
            get => _importUpdateTabModel.SubmapId;
            set => SetField(_importUpdateTabModel.SubmapId, value);
        }
    }
}
