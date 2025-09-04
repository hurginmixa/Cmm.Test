using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using CMM.Test.GUI.Models;
using CMM.Test.GUI.Tools;
using CMM.Test.GUI.Wrappers;

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

        public ImportUpdateViewModel(ImportUpdateTabModel importUpdateTabModel, IFileSystemWrapper fileSystem)
        {
            _importUpdateTabModel = importUpdateTabModel;
            _fileSystem = fileSystem;

            SetResultPathCommand = new RelayCommand(OnCheckResult);
            SelectDataInPathCommand = new RelayCommand(SelectDataInPath);
            SelectDataOutPathCommand = new RelayCommand(SelectDataOutPath);
            
            // Инициализация коллекций конвертеров
            _allConverters =  (new [] { CmmFormatPropertyViewModel.SystemDefaultModel() }).Concat(importUpdateTabModel.CmmWrapper.ImportUpdateConverters.Select(CmmFormatPropertyViewModel.NewModel)).ToList();
            _converterList = new ObservableCollection<CmmFormatPropertyViewModel>(_allConverters);
            _selectedConverter = _allConverters.FirstOrDefault(c => c.Name == importUpdateTabModel.SelectedConverterName.Value);
            _converterListFilter = string.Empty;
            
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
                }
            };
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

            if (!ToolsKid.OpenCheckResultDialog((Window) o, _importUpdateTabModel.FileSystemWrapper.BaseResultsPath, selectedFolderModel, _fileSystem))
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
}
