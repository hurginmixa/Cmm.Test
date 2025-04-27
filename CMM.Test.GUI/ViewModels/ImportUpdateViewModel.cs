using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
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
            SelectDataInPathCommand = new RelayCommand(SelectDataInPath);
            SelectDataOutPathCommand = new RelayCommand(SelectDataOutPath);
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
        public ICommand SelectDataInPathCommand { get; }
        public ICommand SelectDataOutPathCommand { get; }
        
        private void SelectDataInPath(object obj)
        {
            var dialog = new FolderBrowserDialog();
            if (!string.IsNullOrEmpty(DataInPath))
            {
                dialog.SelectedPath = DataInPath;
            }
            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                DataInPath = dialog.SelectedPath;
            }
        }

        private void SelectDataOutPath(object obj)
        {
            var dialog = new FolderBrowserDialog();
            if (!string.IsNullOrEmpty(DataOutPath))
            {
                dialog.SelectedPath = DataOutPath;
            }
            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                DataOutPath = dialog.SelectedPath;
            }
        }

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

        public string DataInPath
        {
            get => _importUpdateTabModel.DataInPath;
            set => SetField(_importUpdateTabModel.DataInPath, value);
        }

        public string DataOutPath
        {
            get => _importUpdateTabModel.DataOutPath;
            set => SetField(_importUpdateTabModel.DataOutPath, value);
        }
    }
}
