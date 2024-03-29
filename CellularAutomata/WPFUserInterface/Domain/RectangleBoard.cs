﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
    private Dictionary<ICell, bool> _nextGeneration;
    
    public IEnumerable<ICell> Cells { get; set; }

    public RectangleBoard(BoardData data)
    {
        var cells = new List<ICell>();

        for (int i = 0; i < data.Height; i++)
        {
            for (int j = 0; j < data.Width; j++)
            {
                cells.Add(new BooleanCell( j, i));
            }
        }

        Cells = cells;

        _neighborhood = NeighborhoodsFactory.Create(Cells, data.BoundaryConditionType, data.NeighborhoodType);
    }

    public Task CalculateNextGenerationAsync()
    {
        _nextGeneration = new Dictionary<ICell, bool>(); 
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
        return Task.CompletedTask;
    }

    public static Task<RectangleBoard> CreateAsync(BoardData data)
    {
        var newItem = new RectangleBoard(data);
        return Task.FromResult(newItem);
    }
}

