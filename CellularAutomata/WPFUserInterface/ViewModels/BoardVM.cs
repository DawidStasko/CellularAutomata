using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors.Core;
using WPFUserInterface.Common;
using WPFUserInterface.Domain;
using WPFUserInterface.Domain.BoundaryConditions;
using WPFUserInterface.Domain.Neighborhoods;

namespace WPFUserInterface.ViewModels;

public class BoardVM:NotificationBase
{
    private RectangleBoard _board;
    private int _boardHeight;
    private int _boardWidth;
    private ICommand _calculateNextGenerationCommand;
    private ObservableCollection<CellVM> _cells = new ObservableCollection<CellVM>();

    #region Properties

    public ObservableCollection<CellVM> Cells
    {
        get => _cells;
        set
        {
            _cells = value; 
            OnPropertyChanged();
        }
    }

    public ICommand CalculateNextGenerationCommand
    {
        get => _calculateNextGenerationCommand;
        private set
        {
            _calculateNextGenerationCommand = value; 
            OnPropertyChanged();
        }
    }

    public ICommand DrawBoardCommand { get; }

    public int BoardHeight
    {
        get => _boardHeight;
        set
        {
            _boardHeight = value;
            OnPropertyChanged();
        }

    }

    public int BoardWidth
    {
        get => _boardWidth;
        set
        {
            _boardWidth = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public BoardVM()
    {
        BoardHeight = 3; 
        BoardWidth = 3;
        DrawBoardCommand = new ActionCommand(DrawBoard);
    }

    private void DrawBoard()
    {
        var boardData = new BoardData() {BoundaryConditionType = BoundaryConditionsTypes.Constant, Height = BoardHeight, Width = BoardWidth, NeighborhoodType = NeighborhoodType.Moore};
        _board = new RectangleBoard(boardData);
        foreach (var cell in _board.Cells)
        {
            Cells.Add(new CellVM(cell));
        }
        CalculateNextGenerationCommand = new ActionCommand(CalculateNextGeneration);
    }

    private void CalculateNextGeneration()
    {
        _board.CalculateNextGeneration();
    }
}