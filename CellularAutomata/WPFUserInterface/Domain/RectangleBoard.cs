using System;
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

public class RectangleBoard
{
    private INeighborhood _neighborhood;
    private Dictionary<SimpleCell, bool> _nextGeneration;
    
    public IEnumerable<SimpleCell> Cells { get; set; }

    public RectangleBoard(BoardData data)
    {
        var cells = new List<SimpleCell>();

        for (int i = 0; i < data.Height; i++)
        {
            for (int j = 0; j < data.Width; j++)
            {
                cells.Add(new SimpleCell( j, i));
            }
        }

        Cells = cells;

        switch (data.NeighborhoodType)
        {
            case NeighborhoodType.VonNeumann:
                _neighborhood = new VonNeumannNeighborhood(Cells, data.BoundaryConditionType);
                break;
            case NeighborhoodType.Moore:
                _neighborhood = new MooreNeighborhood(Cells, data.BoundaryConditionType);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
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