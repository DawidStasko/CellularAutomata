using System;
using System.Collections;
using System.Collections.Generic;
using WPFUserInterface.Domain.Boundaries;

namespace WPFUserInterface.Domain.BoundaryConditions;

public class BoundaryCellsFactory
{
    /// <summary>
    /// Factory method to create boundary cells. 
    /// </summary>
    /// <param name="conditionsTypes">Condition type which will be created.</param>
    /// <param name="cells">Board cells.</param>
    /// <param name="width">The highest index which cell can have horizontally.</param>
    /// <param name="height">The highest index which cell can have vertically.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static IBoundary? Create(BoundaryConditionsTypes conditionsTypes, IEnumerable<ICell> cells, int width, int height)
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