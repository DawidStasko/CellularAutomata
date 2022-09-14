using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors.Core;
using WPFUserInterface.Common;

namespace WPFUserInterface.Domain;

public class SimpleCell:NotificationBase 
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
    
    public SimpleCell(int x = Int32.MinValue, int y = Int32.MinValue)
    {
        Coordinates = new Coordinates(x, y);
        State = false;//(new Random().Next() % 2) == 0;
    }

    public virtual void ChangeState()
    {
        State = !State;
    }

}

