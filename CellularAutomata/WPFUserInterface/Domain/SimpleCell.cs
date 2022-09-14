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

    #region Properties

    public Point Position { get; }

    public bool State
    {
        get => _state;
        set
        {
            _state = value;
            OnPropertyChanged();
        }
    }
    
    public ICommand ChangeStateCommand { get; }

    public Coordinates Coordinates { get; }

    #endregion

    public SimpleCell(Point position, int x = Int32.MinValue, int y = Int32.MinValue)
    {
        Coordinates = new Coordinates(x, y);
        Position = position;
        State = false;//(new Random().Next() % 2) == 0;
        ChangeStateCommand = new ActionCommand(ChangeState);
    }

    protected virtual void ChangeState()
    {
        State = !State;
    }

}

