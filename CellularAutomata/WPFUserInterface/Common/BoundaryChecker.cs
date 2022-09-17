using System;
using WPFUserInterface.Domain;
using WPFUserInterface.Domain.BoundaryConditions;

namespace WPFUserInterface.Common;

public static class BoundaryChecker
{
    public static TwoDimensionBoundaries CheckBoundary(Coordinates coordinates, int maxXCoordinate, int maxYCoordinate)
    {
        var x = coordinates.X;
        var y = coordinates.Y;
        if (x < 0 || y < 0 || x > maxXCoordinate || y > maxYCoordinate)
            throw new ArgumentOutOfRangeException();
        if (x == 0 && y == 0)
            return TwoDimensionBoundaries.TopLeft;
        if (x == maxXCoordinate && y == 0)
            return TwoDimensionBoundaries.TopRight; 
        if (x == 0 && y == maxYCoordinate)
            return TwoDimensionBoundaries.BottomLeft;
        if (x == maxXCoordinate && y == maxYCoordinate)
            return TwoDimensionBoundaries.BottomRight;
        if (x == 0)
            return TwoDimensionBoundaries.Left; 
        if (x == maxXCoordinate) 
            return TwoDimensionBoundaries.Right;
        if (y == 0)
            return TwoDimensionBoundaries.Top; 
        if (y == maxYCoordinate)
            return TwoDimensionBoundaries.Bottom;
        return TwoDimensionBoundaries.NotOnBoundary;
    }
}