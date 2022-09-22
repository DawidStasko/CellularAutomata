using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Windows;

namespace WPFUserInterface.Domain.Boundaries;

/// <summary>
/// Class responsible for cells creation with constant values which lay as neighbors for most outside cells.
/// </summary>
public class ConstantBoundary:IBoundary
{
    public IEnumerable<BooleanCell> BoundaryCells { get; }
    /// <summary>
    /// Class responsible for cells creation with constant values which lay as neighbors for most outside cells.
    /// </summary>
    /// <param name="maxX">The highest index which cell can have horizontally.</param>
    /// <param name="maxY">The highest index which cell can have vertically.</param>
    /// <param name="value">The value which will be assigned to boundary cells.</param>
    public ConstantBoundary(int maxX, int maxY, bool value)
    {
        var cells = new List<BooleanCell>();
        for (int i = 0; i <= maxX; i++)
        {
            var topCell = new BooleanCell( i, -1){State = value};
            cells.Add(topCell);
            var bottomCell = new BooleanCell(i, maxY+1) { State = value };
            cells.Add(bottomCell);
        }

        for (int i = 0; i <= maxY; i++)
        {
            var leftCell = new BooleanCell(-1, i) { State = value };
            cells.Add(leftCell);
            var rightCell = new BooleanCell(maxX+1, i) { State = value };
            cells.Add(rightCell);
        }

        cells.Add(new BooleanCell(-1, -1) { State = value });
        cells.Add(new BooleanCell(maxX + 1, -1) { State = value });
        cells.Add(new BooleanCell(-1, maxY+1) { State = value });
        cells.Add(new BooleanCell(maxX + 1, maxY + 1) { State = value });

        BoundaryCells = cells;
    }
}