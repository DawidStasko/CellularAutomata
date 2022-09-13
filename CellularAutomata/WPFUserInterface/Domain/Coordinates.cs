using System;
using System.Drawing;
using Accessibility;

namespace WPFUserInterface.Domain;

public readonly struct Coordinates
{
    public int X { get; }
    
    public int Y { get; }

    public Coordinates(int x, int y)
    {
            X = x;
            Y = y;
    }

    public double Distance(Coordinates coords)
    {
        return Math.Sqrt(Math.Pow(X - coords.X, 2) + Math.Pow(Y - coords.Y, 2));
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not Coordinates)
            return false;
        Coordinates coords = (Coordinates)obj;
        return X == coords.X && Y == coords.Y;
    }

    public static bool operator ==(Coordinates coordinates1, Coordinates coordinates2)
    {
        return coordinates1.Equals(coordinates2);
    }

    public static bool operator !=(Coordinates coordinates1, Coordinates coordinates2)
    {
        return !(coordinates1 == coordinates2);
    }

    public override int GetHashCode()
    {
        return new Point(X, Y).GetHashCode();
    }
}