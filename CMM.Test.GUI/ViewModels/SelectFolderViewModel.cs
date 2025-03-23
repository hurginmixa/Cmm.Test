using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using CMM.Test.GUI.Models;
using CMM.Test.GUI.Tools;
using System.Windows.Input;

public class SelectFolderViewModel : INotifyPropertyChanged
{
    private string _selectedJob = string.Empty;
    private string _selectedSetup = string.Empty;
    private string _selectedLot = string.Empty;
    private string _selectedWafer = string.Empty;

    private readonly string _basePath;
    private readonly SelectedFolderModel _model;

    public ICommand OkCommand { get; }
    public ICommand CancelCommand { get; }

    public SelectFolderViewModel(string basePath, SelectedFolderModel model)
    {
        _basePath = basePath;
        _model = model;

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

        OkCommand = new RelayCommand(o =>
        {
            if (o is Window window)
            {
                _model.Job = SelectedJob;
                _model.Setup = SelectedSetup;
                _model.Lot = SelectedLot;
                _model.WaferId = SelectedWafer;

                window.DialogResult = true;
                window.Close();
            }
        });
        
        CancelCommand = new RelayCommand(o =>
        {
            if (o is Window window)
            {
                window.DialogResult = false;
                window.Close();
            }
        });
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

            if (_selectedWafer != value)
            {
                _selectedWafer = value;
                OnPropertyChanged();
            }
        }
    }

    public void LoadJobs()
    {
        Jobs.Clear();

        Jobs.AddRange(GetJobList(_basePath));

        OnPropertyChanged();

        SelectedJob = Jobs.Count > 0 ? Jobs[0] : string.Empty;
    }

    public static IEnumerable<string> GetJobList(string basePath)
    {
        if (!Directory.Exists(basePath))
        {
            yield break;
        }

        foreach (string jobFolder in Directory.GetDirectories(basePath).Where(path => HasRequiredDepth(path, 3)))
        {
            yield return Path.GetFileName(jobFolder);
        }
    }

    private static bool HasRequiredDepth(string path, int depthLimit)
    {
        int depth = 0;

        while (depth < depthLimit && Directory.Exists(path) && Directory.GetDirectories(path).Any())
        {
            path = Directory.GetDirectories(path).First();

            depth++;
        }

        return depth >= depthLimit;
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

            if (Directory.Exists(setupsPath))
            {
                Setups.AddRange(Directory.GetDirectories(setupsPath).Where(path => HasRequiredDepth(path, 2)).Select(Path.GetFileName));
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
            if (Directory.Exists(lotsPath))
            {
                Lots.AddRange(Directory.GetDirectories(lotsPath).Where(path => HasRequiredDepth(path, 1)).Select(Path.GetFileName));
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
            if (Directory.Exists(wafersPath))
            {
                Wafers.AddRange(Directory.GetDirectories(wafersPath).Select(Path.GetFileName));
            }
        }

        OnPropertyChanged();

        SelectedWafer = Wafers.Count > 0 ? Wafers[0] : string.Empty;
    }

    public string GetFullPath()
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