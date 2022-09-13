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

    public int HorizontalNo { get; }

    public int VerticalNo { get; }

    #endregion

    public SimpleCell(Point position, int size, int horizontalNo = Int32.MinValue, int verticalNo = Int32.MinValue)
    {
        HorizontalNo = horizontalNo; 
        VerticalNo = verticalNo;
        Position = position;
        Size = size;
        State = (new Random().Next() % 2) == 0;
        ChangeStateCommand = new ActionCommand(ChangeState);
    }

    protected virtual void ChangeState()
    {
        State = !State;
    }

}

