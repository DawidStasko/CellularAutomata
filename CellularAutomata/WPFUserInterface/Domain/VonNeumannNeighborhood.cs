using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WPFUserInterface.Domain;

public class VonNeumannNeighborhood:INeighborhood
{
    private Dictionary<SimpleCell, IEnumerable<SimpleCell>?> _neighborhoods = new Dictionary<SimpleCell, IEnumerable<SimpleCell>?>();

    public VonNeumannNeighborhood(IEnumerable<SimpleCell> cells, BoundaryBehaviour behaviour)
    {
        var minWidth = cells.Select(c=>c.Position.X).Min();
        var minHeight = cells.Select(c=>c.Position.Y).Min();

        var maxWidth = cells.Select(c => c.Position.X).Max();
        var maxHeight = cells.Select(c => c.Position.Y).Max();

        foreach (var processedCell in cells)
        {
            IList<SimpleCell> neighbors = new List<SimpleCell>();

            Point position = processedCell.Position;

            TwoDimensionBoundaries boundaryCell = CheckBoundary(position, minWidth, maxWidth, minHeight, maxHeight);

            _ = behaviour switch
            {
                BoundaryBehaviour.Zero => neighbors = cells.Where(c => (Math.Abs(c.HorizontalNo - processedCell.HorizontalNo) <= 1)&& (Math.Abs(c.VerticalNo - processedCell.VerticalNo) <= 1) && c!=processedCell).ToList(),
                _ => neighbors = new List<SimpleCell>()
            };

            _neighborhoods.Add(processedCell, neighbors);

        }
    }
    
    //TODO Test it carefully 
    private TwoDimensionBoundaries CheckBoundary(
        Point position,
        double minWidth, 
        double maxWidth, 
        double minHeight, 
        double maxHeight)
    {
        //Corners
        if (Math.Abs(position.X - minWidth) < 0.0001 && Math.Abs(position.Y - minHeight) < 0.0001)
            return TwoDimensionBoundaries.TopLeft;
        if (Math.Abs(position.X - minWidth) < 0.0001 && Math.Abs(position.Y - maxHeight) < 0.0001)
            return TwoDimensionBoundaries.BottomLeft;
        if (Math.Abs(position.X - maxWidth) < 0.0001 && Math.Abs(position.Y - minHeight) < 0.0001)
            return TwoDimensionBoundaries.TopRight;
        if (Math.Abs(position.X - maxWidth) < 0.0001 && Math.Abs(position.Y - maxHeight) < 0.0001)
            return TwoDimensionBoundaries.BottomRight;

        //Sides
        if (Math.Abs(position.X - minWidth) < 0.0001)
            return TwoDimensionBoundaries.Left;
        if (Math.Abs(position.X - maxWidth) < 0.0001)
            return TwoDimensionBoundaries.Right;
        if (Math.Abs(position.Y - minHeight) < 0.0001)
            return TwoDimensionBoundaries.Top;
        if (Math.Abs(position.Y - maxHeight) < 0.0001)
            return TwoDimensionBoundaries.Bottom;

        //InsideCells
        return TwoDimensionBoundaries.NotOnBoundary;

    }


    public IEnumerable<SimpleCell> GetNeighbors(SimpleCell cell)
    {
        _neighborhoods.TryGetValue(cell, out IEnumerable<SimpleCell>? neighbors); 
        return neighbors??Enumerable.Empty<SimpleCell>();
        }
}