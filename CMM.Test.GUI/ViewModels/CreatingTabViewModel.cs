using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Cmm.API;
using CMM.Test.GUI.Models;
using CMM.Test.GUI.Tools;
using CMM.Test.GUI.Views;
using CMM.Test.GUI.Wrappers;

namespace CMM.Test.GUI.ViewModels
{
    public class CreatingTabViewModel : NotifyPropertyHandler
    {
        private readonly CreatingTabModel _model;
        private readonly IFileSystemWrapper _fileSystem;
        private readonly ObservableCollection<CmmFormatPropertyViewModel> _converterNameList;
        private readonly List<CmmFormatPropertyViewModel> _allConverters;       

        private CmmFormatPropertyViewModel _selectedConverter;
        private bool _isResultLoaded;
        private bool _isReadyToCreate;
        private string _converterListFilter;

        public CreatingTabViewModel(CreatingTabModel model, IFileSystemWrapper fileSystem)
        {
            _model = model;
            _fileSystem = fileSystem;

            _allConverters = model.CmmWrapper.CreatingConverters.Select(CmmFormatPropertyViewModel.NewModel).ToList();
            _converterNameList = new ObservableCollection<CmmFormatPropertyViewModel>(_allConverters);

            _selectedConverter = _allConverters.FirstOrDefault(c => c.Name == model.ConverterName);

            _isResultLoaded = false;
            _isReadyToCreate = false;

            LoadResMapCommand = new RelayCommand(o => OnLoadResult(o), _ => IsDataCorrect && fileSystem.FileExists(GetScanLogIniPath()));

            CheckResultCommand = new RelayCommand(o => OnCheckResult(o));       

            CreateCommand = new RelayCommand(o => _model.DoCreate(), _ => IsReadyToCreate);

            RTPCommand = new RelayCommand(o => _model.OpenRtp(), _ => IsReadyToCreate);

            // Здесь собраны все реакции на изменения свойств
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
                        
                    case nameof(SelectedExportFlatPosition):
                        _model.ExportFlatPosition = SelectedExportFlatPosition;
                        break;

                    case nameof(ConverterListFilter):
                        FilterConverterList();
                        break;
                }
            };
        }

        public ICommand LoadResMapCommand { get; }

        public ICommand CheckResultCommand { get; }

        public ICommand CreateCommand { get; }

        public ICommand RTPCommand { get; }

        public IEnumerable<eExportFlatPosition> ExportFlatPositions => Enum.GetValues(typeof(eExportFlatPosition)).Cast<eExportFlatPosition>();

        public eExportFlatPosition SelectedExportFlatPosition
        {
            get => _model.ExportFlatPosition;
            set => SetField(_model.ExportFlatPosition, value);
        }

        public bool IsResultLoaded
        {
            get => _isResultLoaded;
            private set => SetField(ref _isResultLoaded, value);
        }

        public bool IsReadyToCreate
        {
            get => _isReadyToCreate;
            private set => SetField(ref _isReadyToCreate, value);
        }

        public string JobName
        {
            get=> _model.JobName.Value;
            set => SetField(_model.JobName, value);
        }

        public string SetupName
        {
            get=> _model.SetupName.Value;
            set => SetField(_model.SetupName, value);
        }

        public string LotName
        {
            get=> _model.Lot.Value;
            set => SetField(_model.Lot, value);
        }

        public string WaferId
        {
            get=> _model.WaferId.Value;
            set => SetField(_model.WaferId, value);
        }

        public ObservableCollection<CmmFormatPropertyViewModel> ConverterNameList => _converterNameList;

        public CmmFormatPropertyViewModel SelectedConverter
        {
            get => _selectedConverter;
            set => SetField(ref _selectedConverter, value);
        }

        public string ConverterListFilter
        {
            get => _converterListFilter;
            set => SetField(ref _converterListFilter, value);
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

            if (!ToolsKid.OpenCheckResultDialog((Window) o, _model.CmmTestModel.BaseResultsPath, selectedFolderModel, _fileSystem))
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
                string scanLogIniPath = GetScanLogIniPath();
                if (_fileSystem.FileExists(scanLogIniPath))
                {
                    IsResultLoaded = true;
                    return;
                }
            }

            IsResultLoaded = false;
        }

        private string GetScanLogIniPath()
        {
            return Path.Combine(_model.CmmTestModel.BaseResultsPath, JobName, SetupName, LotName, WaferId, "ScanLog.ini");
        }

        private void FilterConverterList()
        {
            _converterNameList.Clear();

            if (string.IsNullOrWhiteSpace(_converterListFilter))
            {
                foreach (var converter in _allConverters)
                {
                    _converterNameList.Add(converter);
                }

                return;
            }

            var searchText = _converterListFilter.ToLower();

            var filteredList = _allConverters
                .Where(c => c.Name.ToLower().Contains(searchText) || (c.DisplayName != null && c.DisplayName.ToLower().Contains(searchText)))
                .ToList();

            foreach (var converter in filteredList)
            {
                _converterNameList.Add(converter);
            }

            // Если есть совпадения и нет выбранного элемента, выбираем первый подходящий
            if (filteredList.Any() && (_selectedConverter == null || !filteredList.Contains(_selectedConverter)))
            {
                SelectedConverter = filteredList.First();
            }
        }
    }
}
