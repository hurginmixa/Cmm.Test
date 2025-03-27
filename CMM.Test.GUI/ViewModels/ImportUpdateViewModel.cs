using System.ComponentModel;
using System.Runtime.CompilerServices;
using CMM.Test.GUI.Models;

namespace CMM.Test.GUI.ViewModels;

internal class ImportUpdateViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly ImportUpdateTabModel _importUpdateTabModel;

    public ImportUpdateViewModel(ImportUpdateTabModel importUpdateTabModel)
    {
        _importUpdateTabModel = importUpdateTabModel;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
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