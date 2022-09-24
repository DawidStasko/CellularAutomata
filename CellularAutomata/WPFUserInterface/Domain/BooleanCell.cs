using System;
using WPFUserInterface.Common;

namespace WPFUserInterface.Domain;

public class BooleanCell:NotificationBase, ICell
{
    private bool _state;

    public bool State
    {
        get => _state;
        set
        {
            _state = value;
            OnPropertyChanged();
        }
    }

    public Coordinates Coordinates { get; }
    
    public BooleanCell(int x = Int32.MinValue, int y = Int32.MinValue)
    {
        Coordinates = new Coordinates(x, y);
        State = false;
    }

    public virtual void ChangeState()
    {
        State = !State;
    }

}

