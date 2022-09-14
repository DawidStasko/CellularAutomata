using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.TextFormatting;
using Microsoft.Xaml.Behaviors.Core;
using WPFUserInterface.Common;
using WPFUserInterface.Domain.Neighborhoods;

namespace WPFUserInterface.Domain;

public class RectangleBoard:NotificationBase
{
    private INeighborhood _neighborhood;
    private ObservableCollection<SimpleCell> _cells;
    private Dictionary<SimpleCell, bool> _nextGeneration;
    
    public ObservableCollection<SimpleCell> Cells
    {
        get => _cells;
        set
        {
            _cells = value;
            OnPropertyChanged();
        }
    }

    public RectangleBoard(int width, int height)
    {
        Cells = new ObservableCollection<SimpleCell>();

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var position = new Point(j * 15 + 1.0, i * 15 + 1.0);
                Cells.Add(new SimpleCell( position, j, i));
            }
        }

        _neighborhood = new MooreNeighborhood(Cells, BoundaryConditions.BoundaryConditions.Constant);
    }

    public void CalculateNextGeneration()
    {
        _nextGeneration = new Dictionary<SimpleCell, bool>(); 
        foreach (var processedCell in Cells)
        {
            var neighbors = _neighborhood.GetNeighbors(processedCell);
            int aliveCells = neighbors.Count(neighbor => neighbor.State);

            bool newState = false;

            switch (aliveCells)
            {
                case 3:
                case 2 when processedCell.State:
                    newState = true;
                    break;
            }

            _nextGeneration.Add(processedCell, newState);
        }

        foreach (var kvp in _nextGeneration)
        {
            kvp.Key.State = kvp.Value;
        }
    }
}