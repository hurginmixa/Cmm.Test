using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CMM.Test.GUI.CmmWrappers;
using CMM.Test.GUI.CmmWrappers.DummyImplementations;
using CMM.Test.GUI.Models;
using CMM.Test.GUI.Tools;
using CMM.Test.GUI.Views;

namespace CMM.Test.GUI.ViewModels
{
    public class CreatingTabViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly CreatingTabModel _model;
        private readonly IFileSystemWrapper _fileSystem;
        private readonly ObservableCollection<CmmFormatPropertyViewModel> _converterNameList;

        private CmmFormatPropertyViewModel _selectedConverter;
        private bool _isResultLoaded;
        private bool _isReadyToCreate;

        public CreatingTabViewModel(CreatingTabModel model, IFileSystemWrapper fileSystem)
        {
            _model = model;
            _fileSystem = fileSystem;

            _converterNameList = new ObservableCollection<CmmFormatPropertyViewModel>(model.CmmWrapper.CreatingConverters.Select(r => new CmmFormatPropertyViewModel(r)));

            _selectedConverter = null;
            _isResultLoaded = false;
            _isReadyToCreate = false;

            LoadResMapCommand = new RelayCommand(o => OnLoadResult(o), _ => IsDataCorrect);

            CheckResultCommand = new RelayCommand(o => OnCheckResult(o));

            CreateCommand = new RelayCommand(o => _model.DoCreate(), _ => IsReadyToCreate);

            PropertyChanged += (sender, args) =>
            {
                switch (args.PropertyName)
                {
                    case nameof(IsResultLoaded):
                        CheckReadyToCreate();
                        break;

                    case nameof(SelectedConverter):
                        _model.ConverterName.Value = SelectedConverter?.Name ?? "";
                        CheckReadyToCreate();
                        break;
                }
            };
        }

        public ICommand LoadResMapCommand { get; }

        public ICommand CheckResultCommand { get; }
    
        public ICommand CreateCommand { get; }

        public string JobName
        {
            get => _model.JobName.Value;
            set => SetField(_model.JobName, value);
        }

        public string SetupName
        {
            get => _model.SetupName.Value;
            set => SetField(_model.SetupName, value);
        }

        public string LotName
        {
            get => _model.Lot.Value;
            set => SetField(_model.Lot, value);
        }

        public string WaferId
        {
            get => _model.WaferId.Value;
            set => SetField(_model.WaferId, value);
        }

        public bool IsResultLoaded
        {
            get => _isResultLoaded;
            set => SetField(ref _isResultLoaded, value);
        }

        public bool IsReadyToCreate
        {
            get => _isReadyToCreate;
            set => SetField(ref _isReadyToCreate, value);
        }

        public ObservableCollection<CmmFormatPropertyViewModel> ConverterNameList
        {
            get => _converterNameList;
            set => SetField(_converterNameList, value);
        }

        public CmmFormatPropertyViewModel SelectedConverter
        {
            get => _selectedConverter;
            set => SetField(ref _selectedConverter, value);
        }

        private void CheckReadyToCreate()
        {
            IsReadyToCreate = IsResultLoaded && SelectedConverter != null;
        }

        public bool CreateOnInternalBins 
        { 
            get=> _model.CreateOnInternalBins.Value;
            set => SetField(_model.CreateOnInternalBins, value);
        }

        public bool AssumeAutoCycle 
        { 
            get=> _model.AssumeAutoCycle.Value;
            set => SetField(_model.AssumeAutoCycle, value);
        }
        
        public bool AssumeVerification 
        { 
            get=> _model.AssumeVerification.Value;
            set => SetField(_model.AssumeVerification, value);
        }

        public bool NotShowMap 
        { 
            get=> _model.NotShowMap.Value;
            set => SetField(_model.NotShowMap, value);
        }

        public bool ImportAfterCreate 
        { 
            get=> _model.ImportAfterCreate.Value;
            set => SetField(_model.ImportAfterCreate, value);
        }

        private void OnLoadResult(object o)
        {
            LoadResult();
        }

        private void OnCheckResult(object o)
        {
            SelectedFolderModel selectedFolderModel = new SelectedFolderModel()
            {
                Job = JobName,
                Setup = SetupName,
                Lot = LotName,
                WaferId = WaferId
            };

            if (!OpenCheckResultDialog((Window) o, _model.CmmTestModel.BaseResultsPath, selectedFolderModel, _fileSystem))
            {
                return;
            }

            JobName = selectedFolderModel.Job;
            SetupName = selectedFolderModel.Setup;
            LotName = selectedFolderModel.Lot;
            WaferId = selectedFolderModel.WaferId;

            LoadResult();
        }

        private bool IsDataCorrect => !string.IsNullOrWhiteSpace(JobName) && !string.IsNullOrWhiteSpace(SetupName) && !string.IsNullOrWhiteSpace(LotName) && !string.IsNullOrWhiteSpace(WaferId);

        private void LoadResult()
        {
            if (IsDataCorrect)
            {
                string resultPath = Path.Combine(_model.CmmTestModel.BaseResultsPath, JobName, SetupName, LotName, WaferId, "ScanLog.ini");
                if (_fileSystem.FileExists(resultPath))
                {
                    IsResultLoaded = true;
                    return;
                }
            }

            IsResultLoaded = false;
        }

        private static bool OpenCheckResultDialog(Window owner, string baseResultPath, SelectedFolderModel selectedFolderModel, IFileSystemWrapper fileSystem)
        {
            var dialog = new SelectFolderView(baseResultPath, selectedFolderModel, fileSystem)
            {
                Owner = owner
            };

            return dialog.ShowDialog() ?? false;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool SetField<T>(RefProperty<T> field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field.Value, value))
            {
                return false;
            }

            field.Value = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}