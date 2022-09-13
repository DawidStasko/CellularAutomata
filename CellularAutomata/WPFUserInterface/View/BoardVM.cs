using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors.Core;
using WPFUserInterface.Common;
using WPFUserInterface.Domain;

namespace WPFUserInterface.View;

public class BoardVM:NotificationBase
{
    private RectangleBoard _board;
    private int _boardHeight;
    private int _boardWidth;
    private int _cellSize;
    private ICommand _calculateNextGenerationCommand;

    #region Properties

    public ObservableCollection<SimpleCell> Cells  => _board?.Cells ?? new ObservableCollection<SimpleCell>();

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

    public int CellSize
    {
        get => _cellSize;
        set
        {
            _cellSize = value; 
            OnPropertyChanged();
        }
    }

    #endregion

    public BoardVM()
    {
        BoardHeight = 3; 
        BoardWidth = 3;
        CellSize = 15;
        DrawBoardCommand = new ActionCommand(DrawBoard);
    }

    private void DrawBoard()
    {
        _board = new RectangleBoard(BoardWidth, BoardHeight, CellSize);
        CalculateNextGenerationCommand = new ActionCommand(()=> _board.CalculateNextGeneration());
        OnPropertyChanged(nameof(Cells));
    }


}