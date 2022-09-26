using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using WPFUserInterface.Domain;
using WPFUserInterface.Domain.Boundaries;
using WPFUserInterface.Domain.BoundaryConditions;
using Xunit;

namespace CellularAutomata.Tests.Domain.BoundaryConditions;

public class BoundaryCellsFactoryTests
{

    [Fact]
    public void Create_ShouldReturnConstantBoundary_WhenConstantBoundaryEnumIsInArgumentList()
    {
        IBoundary? _sut =
            BoundaryCellsFactory.Create(BoundaryConditionsTypes.Constant, 
                Enumerable.Empty<ICell>(), 10, 10);

        _sut.Should().NotBeNull();
        _sut.Should().BeOfType<ConstantBoundary>();
    }

    [Fact]
    public void Create_ShouldThrowArgumentNullException_WhenCellsEnumerableIsNull()
    {
        BoundaryConditionsTypes conditions = BoundaryConditionsTypes.Constant;
        IEnumerable<ICell> cells = null;
        int width = 5;
        int height = 5;
        Exception thrownException = new Exception();

        try
        {
            IBoundary? _sut = BoundaryCellsFactory.Create(conditions, cells, width, height);
        }
        catch (Exception e)
        {
            thrownException = e;
        }

        thrownException.Should().NotBeNull();
        thrownException.Should().BeOfType<ArgumentNullException>();
    }

    [Theory]
    [InlineData(0,3)]
    [InlineData(-3,3)]
    [InlineData(3, 0)]
    [InlineData(3, -3)]
    public void Create_ShouldThrowArgumentOutOfRangeException_WhenWidthOrHeightIsEqualOrLessThanZero(int width, int height)
    {
        BoundaryConditionsTypes conditions = BoundaryConditionsTypes.Constant;
        IEnumerable<ICell> cells = Enumerable.Empty<ICell>();
        Exception thrownException = new Exception();

        try
        {
            IBoundary? _sut = BoundaryCellsFactory.Create(conditions, cells, width, height);
        }
        catch (Exception e)
        {
            thrownException = e;
        }

        thrownException.Should().NotBeNull();
        thrownException.Should().BeOfType<ArgumentException>();

    }

}