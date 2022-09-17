using System.Collections.Generic;
using System.Linq;
using WPFUserInterface.Common;
using WPFUserInterface.Domain.BoundaryConditions;

namespace WPFUserInterface.Domain.Neighborhoods;

public class VonNeumannNeighborhood:NeighborhoodBase
{
    public VonNeumannNeighborhood(IEnumerable<SimpleCell> cells, BoundaryConditions.BoundaryConditionsTypes conditionsTypes)
    {
        var maxWidth = cells.Select(c => c.Coordinates.X).Max();
        var maxHeight = cells.Select(c => c.Coordinates.Y).Max();

        foreach (var processedCell in cells)
        {
            IList<SimpleCell> neighbors = new List<SimpleCell>();

            TwoDimensionBoundaries boundaryCell =
                BoundaryChecker.CheckBoundary(processedCell.Coordinates, maxWidth, maxHeight); 


            if (conditionsTypes == BoundaryConditionsTypes.Constant)
            {
                double neighborhoodDistance = (new Coordinates(0,0)).Distance(new Coordinates(1,0));
                neighbors = cells.Where(c=>c.Coordinates.Distance(processedCell.Coordinates)<=neighborhoodDistance && c != processedCell).ToList();
            }

            _neighborhoods.Add(processedCell, neighbors);

        }
    }

}