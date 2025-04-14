using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CMM.Test.GUI.CmmWrappers;
using CMM.Test.GUI.Models;
using CMM.Test.GUI.Tools;

namespace CMM.Test.GUI.ViewModels
{
    public class SelectFolderViewModel : INotifyPropertyChanged
    {
        private string _selectedJob = string.Empty;
        private string _selectedSetup = string.Empty;
        private string _selectedLot = string.Empty;
        private string _selectedWafer = string.Empty;

        private readonly string _basePath;
        private readonly IFileSystemWrapper _fileSystem;
        private readonly SelectedFolderModel _model;

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }

        public SelectFolderViewModel(string basePath, SelectedFolderModel model, IFileSystemWrapper fileSystem)
        {
            _basePath = basePath;
            _model = model;
            _fileSystem = fileSystem;

            OkCommand = new RelayCommand(
                o =>
                {
                    if (!(o is Window window))
                    {
                        return;
                    }

                    _model.Job = SelectedJob;
                    _model.Setup = SelectedSetup;
                    _model.Lot = SelectedLot;
                    _model.WaferId = SelectedWafer;
                    _model.ResultPath = GetFullPath();

                    window.DialogResult = true;
                    window.Close();
                },
                o => !string.IsNullOrWhiteSpace(GetFullPath()));
        
            CancelCommand = new RelayCommand(o =>
            {
                if (o is Window window)
                {
                    window.DialogResult = false;
                    window.Close();
                }
            });

            LoadJobs();

            if (!Jobs.Any())
            {
                _selectedJob = string.Empty;
                _selectedSetup = string.Empty;
                _selectedLot = string.Empty;
                _selectedWafer = string.Empty;

                return;
            }

            SelectedJob = !string.IsNullOrEmpty(_model.Job) && Jobs.Contains(_model.Job) ? _model.Job : Jobs.First();

            SelectedSetup = !string.IsNullOrEmpty(_model.Setup) && Setups.Contains(_model.Setup) ? _model.Setup : Setups.First();

            SelectedLot = !string.IsNullOrEmpty(_model.Lot) && Lots.Contains(_model.Lot) ? _model.Lot : Lots.First();

            SelectedWafer = !string.IsNullOrEmpty(_model.WaferId) && Wafers.Contains(_model.WaferId) ? _model.WaferId : Wafers.First();
        }

        public RangeObservableCollection<string> Jobs { get; private set; } = new RangeObservableCollection<string>();

        public RangeObservableCollection<string> Setups { get; private set; } = new RangeObservableCollection<string>();

        public RangeObservableCollection<string> Lots { get; private set; } = new RangeObservableCollection<string>();

        public RangeObservableCollection<string> Wafers { get; private set; } = new RangeObservableCollection<string>();

        public string SelectedJob
        {
            get => _selectedJob;
            set
            {
                if (!Jobs.Contains(value))
                {
                    return;
                }

                _selectedJob = value;
                OnPropertyChanged();
                LoadSetups();
            }
        }

        public string SelectedSetup
        {
            get => _selectedSetup;
            set
            {
                if (!Setups.Contains(value))
                {
                    return;
                }

                _selectedSetup = value;
                OnPropertyChanged();
                LoadLots();
            }
        }

        public string SelectedLot
        {
            get => _selectedLot;
            set
            {
                if (!Lots.Contains(value))
                {
                    return;
                }

                _selectedLot = value;
                OnPropertyChanged();
                LoadWafers();
            }
        }

        public string SelectedWafer
        {
            get => _selectedWafer;
            set
            {
                if (!Wafers.Contains(value))
                {
                    return;
                }

                _selectedWafer = value;
                OnPropertyChanged();
            }
        }

        public void LoadJobs()
        {
            Jobs.Clear();

            Jobs.AddRange(GetJobList(_basePath, _fileSystem));

            OnPropertyChanged();

            SelectedJob = Jobs.Count > 0 ? Jobs[0] : string.Empty;
        }

        public static IEnumerable<string> GetJobList(string basePath, IFileSystemWrapper fileSystem)
        {
            if (!fileSystem.DirectoryExists(basePath))
            {
                yield break;
            }

            foreach (string jobFolder in fileSystem.GetDirectories(basePath).Where(path => HasRequiredDepth(fileSystem, path, 3)))
            {
                yield return Path.GetFileName(jobFolder);
            }
        }

        private static bool HasRequiredDepth(IFileSystemWrapper fileSystem, string path, int depthLimit)
        {
            int depth = 0;

            string[] directories;

            while (depth < depthLimit && fileSystem.DirectoryExists(path) && (directories = fileSystem.GetDirectories(path)).Any())
            {
                path = directories.First();

                depth++;
            }

            return depth >= depthLimit && fileSystem.FileExists(Path.Combine(path, "ScanLog.ini"));
        }

        private void LoadSetups()
        {
            Setups.Clear();

            if (string.IsNullOrWhiteSpace(SelectedJob))
            {
                SelectedSetup = string.Empty;
            }
            else
            {
                string setupsPath = Path.Combine(_basePath, SelectedJob);

                if (_fileSystem.DirectoryExists(setupsPath))
                {
                    Setups.AddRange(_fileSystem.GetDirectories(setupsPath).Where(path => HasRequiredDepth(_fileSystem, path, 2)).Select(Path.GetFileName));
                }
            }

            OnPropertyChanged();

            SelectedSetup = Setups.Count > 0 ? Setups[0] : string.Empty;
        }

        private void LoadLots()
        {
            Lots.Clear();

            if (string.IsNullOrWhiteSpace(SelectedJob) || string.IsNullOrWhiteSpace(SelectedSetup))
            {
                SelectedLot = string.Empty;
            }
            else
            {
                string lotsPath = Path.Combine(_basePath, SelectedJob, SelectedSetup);
                if (_fileSystem.DirectoryExists(lotsPath))
                {
                    Lots.AddRange(_fileSystem.GetDirectories(lotsPath).Where(path => HasRequiredDepth(_fileSystem, path, 1)).Select(Path.GetFileName));
                }
            }

            OnPropertyChanged();

            SelectedLot = Lots.Count > 0 ? Lots[0] : string.Empty;
        }

        private void LoadWafers()
        {
            Wafers.Clear();

            if (string.IsNullOrWhiteSpace(SelectedJob) || string.IsNullOrWhiteSpace(SelectedSetup) || string.IsNullOrWhiteSpace(SelectedLot))
            {
                SelectedWafer = string.Empty;
            }
            else
            {
                string wafersPath = Path.Combine(_basePath, SelectedJob, SelectedSetup, SelectedLot);
                if (_fileSystem.DirectoryExists(wafersPath))
                {
                    Wafers.AddRange(_fileSystem.GetDirectories(wafersPath).Select(Path.GetFileName));
                }
            }

            OnPropertyChanged();

            SelectedWafer = Wafers.Count > 0 ? Wafers[0] : string.Empty;
        }

        private string GetFullPath()
        {
            if (string.IsNullOrEmpty(SelectedJob) || string.IsNullOrEmpty(SelectedSetup) ||
                string.IsNullOrEmpty(SelectedLot) || string.IsNullOrEmpty(SelectedWafer))
                return string.Empty;

            return Path.Combine(_basePath, SelectedJob, SelectedSetup, SelectedLot, SelectedWafer);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}