using System;
using System.Collections;
using System.Collections.Generic;
using WPFUserInterface.Domain.Boundaries;

namespace WPFUserInterface.Domain.BoundaryConditions;

public class BoundaryCellsFactory
{
    public static IBoundary? Create(BoundaryConditionsTypes conditionsTypes, IEnumerable<SimpleCell> cells, int width, int height)
    {
        switch (conditionsTypes)
        {
            case BoundaryConditionsTypes.Constant:
                return new ConstantBoundary(width, height, false);
            case BoundaryConditionsTypes.Mirroring:
                break;
            case BoundaryConditionsTypes.Continuous:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(conditionsTypes), conditionsTypes, null);
        }

        return null;
    }
} 