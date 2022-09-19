using System.Collections.Generic;
using System.Linq;
using WPFUserInterface.Common;
using WPFUserInterface.Domain.Boundaries;
using WPFUserInterface.Domain.BoundaryConditions;

namespace WPFUserInterface.Domain.Neighborhoods;

public class VonNeumannNeighborhood:NeighborhoodBase
{
    public VonNeumannNeighborhood(IEnumerable<SimpleCell> cells, BoundaryConditions.BoundaryConditionsTypes conditionsType)
    {
        var maxWidth = cells.Select(c => c.Coordinates.X).Max();
        var maxHeight = cells.Select(c => c.Coordinates.Y).Max();

        IBoundary? _boundaryCells = BoundaryCellsFactory.Create(conditionsType, cells, maxWidth, maxHeight);

        foreach (var processedCell in cells)
        {
            double neighborhoodDistance = (new Coordinates(0,0)).Distance(new Coordinates(1,0));
            var neighbors = cells.Where(c=>c.Coordinates.Distance(processedCell.Coordinates)<=neighborhoodDistance && c != processedCell).ToList();
            var neighborsFromBoundary = _boundaryCells.BoundaryCells.Where(c => c.Coordinates.Distance(processedCell.Coordinates) <= neighborhoodDistance && c != processedCell).ToList();
            neighbors.AddRange(neighborsFromBoundary);
            _neighborhoods.Add(processedCell, neighbors);

        }
    }

}