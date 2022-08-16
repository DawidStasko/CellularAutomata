using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors.Core;
using WPFUserInterface.Common;

namespace WPFUserInterface.Domain;

public class SimpleCell:NotificationBase
{
    private bool _state;

    #region Properties

    public int Size { get; }

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

    #endregion

    public SimpleCell(Point position, int size)
    {
        Position = position;
        Size = size;
        State = (new Random().Next() % 2) == 0;
        ChangeStateCommand = new ActionCommand(() => { State = !State; });
    }
}