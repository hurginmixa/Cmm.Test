using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using CMM.Test.GUI.Models;
using CMM.Test.GUI.Tools;
using CMM.Test.GUI.Wrappers;
using Cmm.API;

namespace CMM.Test.GUI.ViewModels
{
    public class ImportUpdateViewModel : NotifyPropertyHandler
    {
        private readonly ImportUpdateTabModel _importUpdateTabModel;
        private readonly IFileSystemWrapper _fileSystem;
        private readonly ObservableCollection<CmmFormatPropertyViewModel> _converterList;
        private readonly List<CmmFormatPropertyViewModel> _allConverters;
        private CmmFormatPropertyViewModel _selectedConverter;
        private string _converterListFilter;

        private readonly ImportWaferMapKindViewModel[] _importWaferMapKindList;
        private ImportWaferMapKindViewModel _selectedImportKind;

        private eImportUpdateViewModelStatus _status;
        private bool _updateEnabled;

        public ImportUpdateViewModel(ImportUpdateTabModel importUpdateTabModel, IFileSystemWrapper fileSystem)
        {
            _importUpdateTabModel = importUpdateTabModel;
            _fileSystem = fileSystem;

            LoadCommand = new RelayCommand(o => OnLoad(o), o => OnLoadPossible(o));
            UpdateCommand = new RelayCommand(OnUpdateCommand);

            SetResultPathCommand = new RelayCommand(OnCheckResult);
            SelectDataInPathCommand = new RelayCommand(SelectDataInPath);
            SelectDataOutPathCommand = new RelayCommand(SelectDataOutPath);
            
            // Инициализация коллекций конвертеров
            _allConverters =  (new [] { CmmFormatPropertyViewModel.SystemDefaultModel() }).Concat(importUpdateTabModel.CmmWrapper.ImportUpdateConverters.Select(CmmFormatPropertyViewModel.NewModel)).ToList();
            _converterList = new ObservableCollection<CmmFormatPropertyViewModel>(_allConverters);
            _selectedConverter = _allConverters.FirstOrDefault(c => c.Name == importUpdateTabModel.SelectedConverterName.Value);
            _converterListFilter = string.Empty;

            // Создание массива ImportWaferMapViewModel для всех значений enum eImportWaferMapKind
            _importWaferMapKindList = Enum.GetValues(typeof(eImportWaferMapKind)).Cast<eImportWaferMapKind>()
                .Select(kind => new ImportWaferMapKindViewModel(kind, ImportWaferMapKindHelper.GeteImportWaferMapKindDisplayName(kind)))
                .ToArray();
            _selectedImportKind = _importWaferMapKindList.FirstOrDefault(vm => vm.Kind == importUpdateTabModel.ImportKind.Value) ?? _importWaferMapKindList.FirstOrDefault();

            // Добавить обработку свойств
            PropertyChanged += (sender, args) =>
            {
                switch (args.PropertyName)
                {
                    case nameof(SelectedConverter):
                        if (SelectedConverter != null)
                        {
                            _importUpdateTabModel.SelectedConverterName.Value = SelectedConverter.Name;
                        }
                        break;
                        
                    case nameof(ConverterListFilter):
                        FilterConverterList();
                        break;
                        
                    case nameof(SelectedImportKind):
                        if (_importUpdateTabModel.ImportKind != null && SelectedImportKind != null)
                        {
                            _importUpdateTabModel.ImportKind.Value = SelectedImportKind.Kind;
                        }
                        break;
                }
            };

            SetStatus(eImportUpdateViewModelStatus.Start);
        }

        private bool OnLoadPossible(object o)
        {
            string[] values = new[] { WaferId, LotId, DataInPath, WaferMapMask, SubmapId };

            return values.All(v => !string.IsNullOrWhiteSpace(v)) && int.TryParse(SubmapId, out _);
        }

        private void OnLoad(object o)
        {
            eImportUpdateViewModelStatus newStatus = _importUpdateTabModel.DoImport()
                ? eImportUpdateViewModelStatus.Loaded
                : eImportUpdateViewModelStatus.Start;

            SetStatus(newStatus);
        }

        private void OnUpdateCommand(object o)
        {
            SetStatus(eImportUpdateViewModelStatus.Start);
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

            if (!string.IsNullOrWhiteSpace(ResultPath))
            {
                var parts = ResultPath
                    .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                    .Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

                // Требуется минимум 4 подпапки: ...\job\setup\lot\waferId
                if (parts.Length >= 4)
                {
                    selectedFolderModel.WaferId = parts[parts.Length - 1];
                    selectedFolderModel.Lot = parts[parts.Length - 2];
                    selectedFolderModel.Setup = parts[parts.Length - 3];
                    selectedFolderModel.Job = parts[parts.Length - 4];
                }
            }

            if (!ToolsKid.OpenCheckResultDialog((Window) o, selectedFolderModel, _fileSystem))
            {
                return;
            }

            ResultPath = selectedFolderModel.ResultPath;
        }

        public ICommand LoadCommand { get; }
        public ICommand UpdateCommand { get; }

        public ICommand SetResultPathCommand { get; }
        public ICommand SelectDataInPathCommand { get; }
        public ICommand SelectDataOutPathCommand { get; }

        public eImportUpdateViewModelStatus Status
        {
            get => _status;
            set => SetStatus(status: value);
        }

        private void SetStatus(eImportUpdateViewModelStatus status)
        {
            _status = status;

            switch (_status)
            {
                case eImportUpdateViewModelStatus.Start:
                    UpdateEnabled = false;
                    break;
                
                case eImportUpdateViewModelStatus.Loaded:
                    UpdateEnabled = true;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

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

        public bool UpdateEnabled
        {
            get => _updateEnabled;
            set => SetField(ref _updateEnabled, value);
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
        
        public bool InVerification
        {
            get => _importUpdateTabModel.InVerification;
            set => SetField(_importUpdateTabModel.InVerification, value);
        }
        
        public bool NotShowMap
        {
            get => _importUpdateTabModel.NotShowMap;
            set => SetField(_importUpdateTabModel.NotShowMap, value);
        }
        
        public ObservableCollection<CmmFormatPropertyViewModel> ConverterList => _converterList;

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

        // Возвращает массив объектов ImportWaferMapViewModel
        public ImportWaferMapKindViewModel[] ImportKinds => _importWaferMapKindList;
        

        public ImportWaferMapKindViewModel SelectedImportKind
        {
            get => _selectedImportKind;
            set => SetField(ref _selectedImportKind, value);
        }
        
        private void FilterConverterList()
        {
            _converterList.Clear();

            if (string.IsNullOrWhiteSpace(_converterListFilter))
            {
                foreach (var converter in _allConverters)
                {
                    _converterList.Add(converter);
                }

                return;
            }

            var searchText = _converterListFilter.ToLower();

            var filteredList = _allConverters
                .Where(c => string.IsNullOrWhiteSpace(c.Name) || (c.DisplayName != null && c.DisplayName.ToLower().Contains(searchText)))
                .ToList();

            foreach (var converter in filteredList)
            {
                _converterList.Add(converter);
            }

            // Если есть совпадения и нет выбранного элемента, выбираем первый
            if (filteredList.Any() && (_selectedConverter == null || !filteredList.Contains(_selectedConverter)))
            {
                SelectedConverter = filteredList.First();
            }
        }
    }

    public enum eImportUpdateViewModelStatus
    {
        Start,
        Loaded
    }
}
