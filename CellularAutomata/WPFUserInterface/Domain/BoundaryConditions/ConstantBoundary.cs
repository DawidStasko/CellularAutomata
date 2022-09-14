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
        var cells = new List<SimpleCell>();
        for (int i = 0; i <= maxX; i++)
        {
            var topCell = new SimpleCell( i, -1){State = value};
            cells.Add(topCell);
            var bottomCell = new SimpleCell(i, maxY+1) { State = value };
            cells.Add(bottomCell);
        }

        for (int i = 0; i <= maxY; i++)
        {
            var leftCell = new SimpleCell(-1, i) { State = value };
            cells.Add(leftCell);
            var rightCell = new SimpleCell(maxX+1, i) { State = value };
            cells.Add(rightCell);
        }

        //cells.Add(new SimpleCell(-1, -1) { State = value });
        //cells.Add(new SimpleCell(maxX + 1, -1) { State = value });
        //cells.Add(new SimpleCell(-1, maxY+1) { State = value });
        //cells.Add(new SimpleCell(maxX + 1, maxY + 1) { State = value });

        BoundaryCells = cells;
    }
}