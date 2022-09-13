using System.Collections.Generic;
using System.Linq;
using WPFUserInterface.Common;
using WPFUserInterface.Domain.Boundaries;
using WPFUserInterface.Domain.BoundaryConditions;

namespace WPFUserInterface.Domain.Neighborhoods;

public class MooreNeighborhood:NeighborhoodBase
{
    public MooreNeighborhood(IEnumerable<SimpleCell> cells, BoundaryConditions.BoundaryConditions conditions)
    {
        var maxWidth = cells.Select(c => c.Coordinates.X).Max();
        var maxHeight = cells.Select(c => c.Coordinates.Y).Max();

        if (conditions == BoundaryConditions.BoundaryConditions.Constant)
        {
            _boundaryCells = new ConstantBoundary(maxWidth, maxHeight, false);
        }


        foreach (var processedCell in cells)
        {
            TwoDimensionBoundaries boundaryCell =
                BoundaryChecker.CheckBoundary(processedCell.Coordinates, maxWidth, maxHeight);

            double neighborhoodDistance = (new Coordinates(0, 0)).Distance(new Coordinates(1, 1));
            var neighbors = cells.Where(c => c.Coordinates.Distance(processedCell.Coordinates) <= neighborhoodDistance && c != processedCell).ToList();
            var tmp = (_boundaryCells.BoundaryCells.Where(c => c.Coordinates.Distance(processedCell.Coordinates) <= neighborhoodDistance).ToList());
            neighbors.AddRange(tmp);
            _neighborhoods.Add(processedCell, neighbors);
        }
        
    }
}