using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AlexandreHtrb.AvaloniaUITest;

public abstract class UITestBaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void ChangeProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        where T : class
    {
        if (field != value)
        {
            field = value;
            OnPropertyChanged(propertyName);
        }
    }

    protected void ChangeProperty(ref bool field, bool value, [CallerMemberName] string? propertyName = null)
    {
        if (field != value)
        {
            field = value;
            OnPropertyChanged(propertyName);
        }
    }

    protected void ChangeProperty(ref int field, int value, [CallerMemberName] string? propertyName = null)
    {
        if (field != value)
        {
            field = value;
            OnPropertyChanged(propertyName);
        }
    }

    protected void ChangeProperty(ref string? field, string? value, [CallerMemberName] string? propertyName = null)
    {
        if (field != value)
        {
            field = value;
            OnPropertyChanged(propertyName);
        }
    }
}