using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using WPFUserInterface.Common;
using WPFUserInterface.Domain.Boundaries;
using WPFUserInterface.Domain.BoundaryConditions;

namespace WPFUserInterface.Domain.Neighborhoods;

public class MooreNeighborhood:NeighborhoodBase
{
    public MooreNeighborhood(IEnumerable<ICell> cells, BoundaryConditions.BoundaryConditionsTypes conditionsType)
    {
        Guard.Against.Null(cells);
        Guard.Against.EmptyCollection(cells);

        var maxWidth = cells.Select(c => c.Coordinates.X).Max();
        var maxHeight = cells.Select(c => c.Coordinates.Y).Max();

        IBoundary _boundaryCells = BoundaryCellsFactory.Create(conditionsType, cells, maxWidth, maxHeight);

        foreach (var processedCell in cells)
        {
            double neighborhoodDistance = (new Coordinates(0, 0)).Distance(new Coordinates(1, 1));
            var neighbors = cells.Where(c => c.Coordinates.Distance(processedCell.Coordinates) <= neighborhoodDistance && c != processedCell).ToList();
            var neighborsFromBoundary = (_boundaryCells.BoundaryCells.Where(c => c.Coordinates.Distance(processedCell.Coordinates) <= neighborhoodDistance).ToList());
            neighbors.AddRange(neighborsFromBoundary);
            _neighborhoods.Add(processedCell, neighbors);
        }
    }
}