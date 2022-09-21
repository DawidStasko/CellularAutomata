using System;
using System.Collections;
using System.Collections.Generic;
using WPFUserInterface.Domain.BoundaryConditions;

namespace WPFUserInterface.Domain.Neighborhoods;

public class NeighborhoodsFactory
{
    public static INeighborhood Create(IEnumerable<SimpleCell> cells, BoundaryConditionsTypes boundaryConditions,
        NeighborhoodType neighborhoodType)
    {
        switch (neighborhoodType)
        {
            case NeighborhoodType.VonNeumann:
                return new VonNeumannNeighborhood(cells, boundaryConditions);
            case NeighborhoodType.Moore:
                return new MooreNeighborhood(cells, boundaryConditions);
            default:
                throw new ArgumentOutOfRangeException(nameof(neighborhoodType), neighborhoodType, null);
        }
    }
}