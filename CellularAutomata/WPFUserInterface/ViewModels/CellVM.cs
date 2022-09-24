using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors.Core;
using WPFUserInterface.Common;
using WPFUserInterface.Domain;

namespace WPFUserInterface.ViewModels;

public class CellVM:NotificationBase
{
    private readonly ICell _cell;
    private Point? _point;
    public bool State => _cell.State;

    public Point Position =>
        _point ??= new Point(_cell.Coordinates.X * 15 + 1.0,
            _cell.Coordinates.Y * 15 + 1.0);

    public ICommand ChangeStateCommand { get; }

    public CellVM(ICell cell)
    {
        _cell = cell;
        ChangeStateCommand = new ActionCommand(ChangeState);
        _cell.PropertyChanged += OnCellChanged;
    }

    private void OnCellChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ICell.State))
        {
            OnPropertyChanged(nameof(State));
        }
    }


    private void ChangeState()
    {
        _cell.ChangeState();
    }

}