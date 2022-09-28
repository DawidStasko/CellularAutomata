using System;
using System.Collections;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using WPFUserInterface.Domain.BoundaryConditions;

namespace WPFUserInterface.Domain.Neighborhoods;

public class NeighborhoodsFactory
{
    public static INeighborhood Create(IEnumerable<ICell> cells, BoundaryConditionsTypes boundaryConditions,
        NeighborhoodType neighborhoodType)
    {
        Guard.Against.Null(cells, nameof(cells));

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