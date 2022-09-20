using System;
using FluentAssertions;
using WPFUserInterface.Domain;
using Xunit;

namespace CellularAutomata.Tests.Domain;

public class CoordinatesTests
{
    private Coordinates _sut;

    [Theory]
    [InlineData(1, 2, 3, 4, 2.828427)]
    [InlineData(-3, -2, 4, 5, 9.899494)]
    [InlineData(4, 5, -3, -2, 9.899494)]
    [InlineData(-2, 2, 1, 5, 4.242640)]
    [InlineData(1, 5, -2, 2, 4.242640)]
    [InlineData(0, 0, 3, 4, 5.000000)]
    [InlineData(3, 4, 0, 0, 5.000000)]
    public void Distance_ShouldReturnDistance_BetweenThisCoordinatesAndGivenCoordinatesInArguments(
        int x1, int y1, int x2, int y2, double distance)
    {
        _sut = new Coordinates(x1, y1);
        var secondCoords = new Coordinates(x2, y2);

        double calculatedDistance = _sut.Distance(secondCoords);
        double difference = Math.Abs(calculatedDistance - distance);

        difference.Should().BeLessOrEqualTo(0.0001);
    }

    [Fact]
    public void Equality_ShouldBeTrue_WhenCoordinatesHaveTheSameValues()
    {
        _sut = new Coordinates(3, 3); 
        var secondCoordinates=new Coordinates(3, 3);

        var fromEqualMethod = _sut.Equals(secondCoordinates);
        var fromEqualOperator = _sut == secondCoordinates;
        var fromNotEqualOperator = _sut != secondCoordinates;

        fromEqualMethod.Should().Be(true);
        fromEqualOperator.Should().Be(true);
        fromNotEqualOperator.Should().Be(false);
    }
    
    [Theory]
    [InlineData(1,2,3,4)]
    [InlineData(1,1,1,2)]
    [InlineData(-1,1,1,-1)]
    public void Equality_ShouldBeFalse_WhenCoordinatesAreDifferent(int x1, int y1, int x2, int y2)
    {
        _sut = new Coordinates(x1, y1);
        var secondCoordinates = new Coordinates(x2, y2);

        var fromEqualMethod = _sut.Equals(secondCoordinates);
        var fromEqualOperator = _sut == secondCoordinates;
        var fromNotEqualOperator = _sut != secondCoordinates;

        fromEqualMethod.Should().Be(false);
        fromEqualOperator.Should().Be(false);
        fromNotEqualOperator.Should().Be(true);
    }

    [Fact]
    public void Equality_ShouldBeFalse_WhenComparingToNull()
    {
        _sut = new Coordinates(3, 3);
        Coordinates? secondCoordinates = null;

        var fromEqualMethod = _sut.Equals(secondCoordinates);
        var fromEqualOperator = _sut == secondCoordinates;
        var fromNotEqualOperator = _sut != secondCoordinates;

        fromEqualMethod.Should().Be(false);
        fromEqualOperator.Should().Be(false);
        fromNotEqualOperator.Should().Be(true);
    }

}