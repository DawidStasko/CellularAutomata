using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Windows;

namespace WPFUserInterface.Domain.Boundaries;

public class ConstantBoundary:IBoundary
{
    public IEnumerable<SimpleCell> BoundaryCells { get; }

    public ConstantBoundary(int maxX, int maxY, bool value)
    {
        var point = new Point(Double.MinValue, Double.MinValue);
        var cells = new List<SimpleCell>();
        for (int i = 0; i <= maxX; i++)
        {
            var topCell = new SimpleCell(point,  i, -1){State = value};
            cells.Add(topCell);
            var bottomCell = new SimpleCell(point,  i, maxY+1) { State = value };
            cells.Add(bottomCell);
        }

        for (int i = 0; i <= maxY; i++)
        {
            var leftCell = new SimpleCell(point,  -1, i) { State = value };
            cells.Add(leftCell);
            var rightCell = new SimpleCell(point,  maxX+1, i) { State = value };
            cells.Add(rightCell);
        }

        cells.Add(new SimpleCell(point,  -1, -1) { State = value });
        cells.Add(new SimpleCell(point,  maxX + 1, -1) { State = value });
        cells.Add(new SimpleCell(point,  -1, maxY+1) { State = value });
        cells.Add(new SimpleCell(point, maxX + 1, maxY + 1) { State = value });

        BoundaryCells = cells;
    }
}