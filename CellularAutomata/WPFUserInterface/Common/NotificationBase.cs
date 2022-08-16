using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPFUserInterface.Annotations;

namespace WPFUserInterface.Common;

public class NotificationBase:INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}