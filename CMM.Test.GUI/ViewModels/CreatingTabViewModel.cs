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

        public CreatingTabViewModel(CreatingTabModel modelCreatingTabModel)
        {
            _modelCreatingTabModel = modelCreatingTabModel;
    
            LoadResMapCommand = new RelayCommand(o => LoadResult(o));

            CheckResultCommand = new RelayCommand(o => OpenCheckResultDialog(o));
        
            CreateCommand = new RelayCommand(o => Create(o), () => CanCreate);
        }

        public ICommand LoadResMapCommand { get; }

        public ICommand CheckResultCommand { get; }
    
        public ICommand CreateCommand { get; }

        public string JobName
        {
            get => _modelCreatingTabModel.JobName.Value;
            set => SetField(_modelCreatingTabModel.JobName, value);
        }

        public string SetupName
        {
            get => _modelCreatingTabModel.SetupName.Value;
            set => SetField(_modelCreatingTabModel.SetupName, value);
        }

        public string LotName
        {
            get => _modelCreatingTabModel.Lot.Value;
            set => SetField(_modelCreatingTabModel.Lot, value);
        }

        public string WaferId
        {
            get => _modelCreatingTabModel.WaferId.Value;
            set => SetField(_modelCreatingTabModel.WaferId, value);
        }

        public bool CreateOnInternalBins 
        { 
            get=> _modelCreatingTabModel.CreateOnInternalBins.Value;
            set => SetField(_modelCreatingTabModel.CreateOnInternalBins, value);
        }

        public bool AssumeAutoCycle 
        { 
            get=> _modelCreatingTabModel.AssumeAutoCycle.Value;
            set => SetField(_modelCreatingTabModel.AssumeAutoCycle, value);
        }
        
        public bool AssumeVerification 
        { 
            get=> _modelCreatingTabModel.AssumeVerification.Value;
            set => SetField(_modelCreatingTabModel.AssumeVerification, value);
        }

        public bool NotShowMap 
        { 
            get=> _modelCreatingTabModel.NotShowMap.Value;
            set => SetField(_modelCreatingTabModel.NotShowMap, value);
        }

        public bool NotShowMap1 
        { 
            get=> _modelCreatingTabModel.ImportAfterCreate.Value;
            set => SetField(_modelCreatingTabModel.ImportAfterCreate, value);
        }

        public bool CanCreate => true;

        private void LoadResult(object o)
        {
        }

        private void OpenCheckResultDialog(object o)
        {
            string basePath = @"\\mixa7th\c$\Falcon\ScanResults";
            SelectedFolderModel selectedFolderModel = new SelectedFolderModel()
            {
                Job = JobName,
                Setup = SetupName,
                Lot = LotName,
                WaferId = WaferId
            };

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

        protected bool SetField<T>(RefProperty<T> field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field.Value, value))
            {
                return false;
            }

            field.Value = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}