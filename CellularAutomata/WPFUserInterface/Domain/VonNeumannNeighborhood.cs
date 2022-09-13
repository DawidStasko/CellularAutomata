﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WPFUserInterface.Common;

namespace WPFUserInterface.Domain;

public class VonNeumannNeighborhood:INeighborhood
{
    private Dictionary<SimpleCell, IEnumerable<SimpleCell>?> _neighborhoods = new Dictionary<SimpleCell, IEnumerable<SimpleCell>?>();

    public VonNeumannNeighborhood(IEnumerable<SimpleCell> cells, BoundaryBehaviour behaviour)
    {
        var maxWidth = cells.Select(c => c.Coordinates.X).Max();
        var maxHeight = cells.Select(c => c.Coordinates.Y).Max();

        foreach (var processedCell in cells)
        {
            IList<SimpleCell> neighbors = new List<SimpleCell>();

            TwoDimensionBoundaries boundaryCell =
                BoundaryChecker.CheckBoundary(processedCell.Coordinates, maxWidth, maxHeight); 


            if (behaviour == BoundaryBehaviour.Zero)
            {
                double neighborhoodDistance = (new Coordinates(0,0)).Distance(new Coordinates(1,0));
                neighbors = cells.Where(c=>c.Coordinates.Distance(processedCell.Coordinates)<=neighborhoodDistance && c != processedCell).ToList();
            }

            _neighborhoods.Add(processedCell, neighbors);

        }
    }

    public IEnumerable<SimpleCell> GetNeighbors(SimpleCell cell)
    {
        _neighborhoods.TryGetValue(cell, out IEnumerable<SimpleCell>? neighbors);
        return neighbors ?? Enumerable.Empty<SimpleCell>();

    }
}