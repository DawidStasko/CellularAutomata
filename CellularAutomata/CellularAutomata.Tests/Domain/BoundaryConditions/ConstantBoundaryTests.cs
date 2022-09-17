using System.Xml.Serialization;
using FluentAssertions;
using WPFUserInterface.Domain.Boundaries;
using Xunit;

namespace CellularAutomata.Tests.Domain.BoundaryConditions;

public class ConstantBoundaryTests
{
    private ConstantBoundary _sut;

    [Theory]
    [InlineData(3,3,16)]
    [InlineData(4, 7, 26)]
    [InlineData(17, 9, 52)]
    public void ConstantBoundary_BoundaryCellsCollectionShouldHaveSizeOfDoubleHeightDoubleWidthPlus4_WhenCreated(int x, int y, int collectionSize)
    {
        _sut = new ConstantBoundary(x, y, false);

        _sut.BoundaryCells.Should().HaveCount(collectionSize);
    }
}