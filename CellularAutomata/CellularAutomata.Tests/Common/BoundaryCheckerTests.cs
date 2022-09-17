using System;
using FluentAssertions;
using WPFUserInterface.Common;
using WPFUserInterface.Domain;
using WPFUserInterface.Domain.BoundaryConditions;
using Xunit;

namespace CellularAutomata.Tests.Common;

public class BoundaryCheckerTests
{
    private int maxX = 100; 
    private int maxY = 99;


    [Fact]
    public void CheckBoundary_ShouldReturnTopLeft_WhenCoordinatesAreXZeroYZero()
    {
        var coordinates = new Coordinates(0, 0);

        TwoDimensionBoundaries boundary = BoundaryChecker.CheckBoundary(coordinates, maxX, maxY);

        boundary.Should().Be(TwoDimensionBoundaries.TopLeft);
    }

    [Fact]
    public void CheckBoundary_ShouldReturnTopRight_WhenCoordinatesAreXMaxYZero()
    {
        var coordinates = new Coordinates(100, 0);

        TwoDimensionBoundaries boundary = BoundaryChecker.CheckBoundary(coordinates, maxX, maxY);

        boundary.Should().Be(TwoDimensionBoundaries.TopRight);
    }

    [Fact]
    public void CheckBoundary_ShouldReturnBottomLeft_WhenCoordinatesAreXZeroYMax()
    {
        var coordinates = new Coordinates(0, 99);

        TwoDimensionBoundaries boundary = BoundaryChecker.CheckBoundary(coordinates, maxX, maxY);

        boundary.Should().Be(TwoDimensionBoundaries.BottomLeft);
    }

    [Fact]
    public void CheckBoundary_ShouldReturnBottomRight_WhenCoordinatesAreXMaxYMax()
    {
        var coordinates = new Coordinates(100, 99);

        TwoDimensionBoundaries boundary = BoundaryChecker.CheckBoundary(coordinates, maxX, maxY);

        boundary.Should().Be(TwoDimensionBoundaries.BottomRight);
    }

    [Theory]
    [InlineData(1,0)]
    [InlineData(28,0)]
    [InlineData(50,0)]
    [InlineData(73,0)]
    [InlineData(99,0)]
    public void CheckBoundary_ShouldReturnTop_WhenCoordinatesAreXNotZeroAndNotMaxYZero(int x, int y)
    {
        var coordinates = new Coordinates(x,y);

        TwoDimensionBoundaries boundary = BoundaryChecker.CheckBoundary(coordinates, maxX, maxY);

        boundary.Should().Be(TwoDimensionBoundaries.Top);
    }

    [Theory]
    [InlineData(100, 1)]
    [InlineData(100, 23)]
    [InlineData(100, 50)]
    [InlineData(100, 66)]
    [InlineData(100, 98)]
    public void CheckBoundary_ShouldReturnRight_WhenCoordinatesAreXMaxYNotZeroAndNotMax(int x, int y)
    {
        var coordinates = new Coordinates(x, y);

        TwoDimensionBoundaries boundary = BoundaryChecker.CheckBoundary(coordinates, maxX, maxY);

        boundary.Should().Be(TwoDimensionBoundaries.Right);
    }

    [Theory]
    [InlineData(1, 99)]
    [InlineData(28, 99)]
    [InlineData(50, 99)]
    [InlineData(73, 99)]
    [InlineData(99, 99)]
    public void CheckBoundary_ShouldReturnBottom_WhenCoordinatesAreXNotZeroAndNotMaxYMax(int x, int y)
    {
        var coordinates = new Coordinates(x, y);

        TwoDimensionBoundaries boundary = BoundaryChecker.CheckBoundary(coordinates, maxX, maxY);

        boundary.Should().Be(TwoDimensionBoundaries.Bottom);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(0, 23)]
    [InlineData(0, 50)]
    [InlineData(0, 66)]
    [InlineData(0, 98)]
    public void CheckBoundary_ShouldReturnLeft_WhenCoordinatesAreXZeroYNotZeroAndNotMax(int x, int y)
    {
        var coordinates = new Coordinates(x, y);

        TwoDimensionBoundaries boundary = BoundaryChecker.CheckBoundary(coordinates, maxX, maxY);

        boundary.Should().Be(TwoDimensionBoundaries.Left);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 23)]
    [InlineData(10, 73)]
    [InlineData(1, 98)]
    [InlineData(50, 50)]
    [InlineData(65, 98)]
    [InlineData(99, 23)]
    [InlineData(99, 98)]
    public void CheckBoundary_ShouldReturnNotOnBoundary_WhenCoordinatesAreXNotZeroAndNotMaxYNotZeroAndNotMax(int x, int y)
    {
        var coordinates = new Coordinates(x, y);

        TwoDimensionBoundaries boundary = BoundaryChecker.CheckBoundary(coordinates, maxX, maxY);

        boundary.Should().Be(TwoDimensionBoundaries.NotOnBoundary);
    }

    [Theory]
    [InlineData(-1, -1)]
    [InlineData(-1, 0)]
    [InlineData(-1, 3)]
    [InlineData(-1, 99)]
    [InlineData(34, -1)]
    [InlineData(79, -1)]
    [InlineData(100, -1)]
    [InlineData(34, 100)]
    [InlineData(50, 100)]
    [InlineData(75, 100)]
    [InlineData(101, 54)]
    [InlineData(101, 23)]
    [InlineData(101, 100)]
    public void CheckBoundary_ShouldThrowException_WhenCoordinatesAreXLessThanZeroAndGreaterThanMaxYLessThanZeroAndGreaterThanMax(int x, int y)
    {
        var coordinates = new Coordinates(x, y);
        void MethodToTest() => BoundaryChecker.CheckBoundary(coordinates, maxX, maxY);

        var exceptionWasThrown = Record.Exception(MethodToTest);

        exceptionWasThrown.Should().BeOfType<ArgumentOutOfRangeException>();
    }

}