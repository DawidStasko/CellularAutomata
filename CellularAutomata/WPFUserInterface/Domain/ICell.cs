using System.ComponentModel;

namespace WPFUserInterface.Domain;

public interface ICell:INotifyPropertyChanged
{
    bool State { get; set;  }
    Coordinates Coordinates { get; }
    void ChangeState();
}