using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CMM.Test.GUI.Models;
using CMM.Test.GUI.Tools;
using CMM.Test.GUI.Views;

namespace CMM.Test.GUI.ViewModels
{
    internal class CreatingTabViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly CreatingTabModel _modelCreatingTabModel;

        private string _jobName;
        private string _setupName;
        private string _lotName;
        private string _waferId;
        private string _resultPath;

        public CreatingTabViewModel(CreatingTabModel modelCreatingTabModel)
        {
            _modelCreatingTabModel = modelCreatingTabModel;
    
            _jobName = "Job Name";
            _setupName = "Setup Name";
            _lotName = "Lot Name";
            _waferId = "Wafer Id";
            _resultPath = "Result Path";

            LoadResMapCommand = new RelayCommand(o => LoadResult(o));

            CheckResultCommand = new RelayCommand(o => OpenCheckResultDialog(o));
        
            CreateCommand = new RelayCommand(o => Create(o), () => CanCreate);
        }

        public ICommand LoadResMapCommand { get; }

        public ICommand CheckResultCommand { get; }
    
        public ICommand CreateCommand { get; }

        public string JobName
        {
            get => _jobName;
            set => SetField(ref _jobName, value);
        }

        public string SetupName
        {
            get => _setupName;
            set => SetField(ref _setupName, value);
        }

        public string LotName
        {
            get => _lotName;
            set => SetField(ref _lotName, value);
        }

        public string WaferId
        {
            get => _waferId;
            set => SetField(ref _waferId, value);
        }

        public string ResultPath
        {
            get => _resultPath;
            set => SetField(ref _resultPath, value);
        }

        public bool CanCreate => true;

        private void LoadResult(object o)
        {
        }

        private void OpenCheckResultDialog(object o)
        {
            string basePath = @"\\mixa7th\c$\Falcon\ScanResults";
            SelectedFolderModel selectedFolderModel = new SelectedFolderModel();

            bool result = true;

            var dialog = new SelectFolderView(basePath, selectedFolderModel);
            dialog.Owner = (Window) o;

            result = dialog.ShowDialog() ?? false;

            if (result)
            {
                JobName = selectedFolderModel.Job;
                SetupName = selectedFolderModel.Setup;
                LotName = selectedFolderModel.Lot;
                WaferId = selectedFolderModel.WaferId;
                ResultPath = selectedFolderModel.ResultPath;
            }
        }

        private void Create(object o)
        {
            // TODO: Implement creation logic
            MessageBox.Show("Create command executed!");
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
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