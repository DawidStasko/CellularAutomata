using WPFUserInterface.Domain;

namespace WPFUserInterface.Common;

public static class BoundaryChecker
{
    public static TwoDimensionBoundaries CheckBoundary(Coordinates coordinates, int maxXCoordinate, int maxYCoordinate)
    {
        if (coordinates.X == 0 && coordinates.Y == 0)
            return TwoDimensionBoundaries.TopLeft;
        if (coordinates.X == maxXCoordinate && coordinates.Y == 0)
            return TwoDimensionBoundaries.TopRight; 
        if (coordinates.X == 0 && coordinates.Y == maxYCoordinate)
            return TwoDimensionBoundaries.BottomLeft;
        if (coordinates.X == maxXCoordinate && coordinates.Y == maxYCoordinate)
            return TwoDimensionBoundaries.BottomRight;
        if (coordinates.X == 0)
            return TwoDimensionBoundaries.Left; 
        if (coordinates.X == maxXCoordinate) 
            return TwoDimensionBoundaries.Right;
        if (coordinates.Y == 0)
            return TwoDimensionBoundaries.Top; 
        if (coordinates.Y == maxYCoordinate)
            return TwoDimensionBoundaries.Bottom;
        return TwoDimensionBoundaries.NotOnBoundary;
    }
}