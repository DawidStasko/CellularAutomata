using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using WPFUserInterface.Domain.BoundaryConditions;
using WPFUserInterface.Domain.Neighborhoods;
using WPFUserInterface.Domain;
using Xunit;
using NSubstitute;

namespace CellularAutomata.Tests.Domain.Neighborhoods;


public class VonNeumannNeighborhoodTests
{
    private VonNeumannNeighborhood _sut;

    [Fact]
    public void VonNeumannNeighborhood_ShouldThrowArgumentException_WhenCellsCollectionIsEmpty()
    {
        IEnumerable<ICell> cells = new List<ICell>();
        Exception? thrownException = null;

        try
        {
            _sut = new VonNeumannNeighborhood(cells, BoundaryConditionsTypes.Constant);
        }
        catch (Exception e)
        {
            thrownException = e;
        }

        thrownException.Should().NotBeNull().And.BeOfType<ArgumentException>();
    }

    [Theory]
    [InlineData(3, 1)]
    [InlineData(3, 3)]
    [InlineData(3, 6)]
    [InlineData(7, 3)]
    public void VonNeumannNeighborhood_ShouldHaveFourNeighborsFromBoard_WhenNotOnBoundary(int x, int y)
    {
        IEnumerable<ICell> board = PrepareRectangleBoard();
        ICell cellToTest = board.Single(c => c.Coordinates == new Coordinates(x, y));
        _sut = new VonNeumannNeighborhood(board, BoundaryConditionsTypes.Constant);

        IEnumerable<ICell> neighbors = _sut.GetNeighbors(cellToTest).ToList();

        neighbors.Should().HaveCount(4);
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x + 0, y - 1));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x - 1, y + 0));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x + 1, y + 0));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x - 0, y + 1));

        foreach (ICell processedNeighbor in neighbors)
        {
            board.Should().Contain(processedNeighbor);
        }
    }

    [Theory]
    [InlineData(3, 0)]
    [InlineData(0, 3)]
    [InlineData(3, 9)]
    [InlineData(9, 3)]
    public void VonNeumannNeighborhood_ShouldHaveThreeNeighborsFromBoardOneFromBoundary_WhenOnBoundaryButNotInCorner(int x, int y)
    {
        IEnumerable<ICell> board = PrepareRectangleBoard();
        ICell cellToTest = board.Single(c => c.Coordinates == new Coordinates(x, y));
        _sut = new VonNeumannNeighborhood(board, BoundaryConditionsTypes.Constant);

        IList<ICell> neighbors = _sut.GetNeighbors(cellToTest).ToList();

        neighbors.Should().HaveCount(4);
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x + 0, y - 1));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x - 1, y + 0));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x + 1, y + 0));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x - 0, y + 1));
        neighbors.Count(c => board.Contains(c)).Should().Be(3);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 9)]
    [InlineData(9, 0)]
    [InlineData(9, 9)]
    public void VonNeumannNeighborhood_ShouldHaveTwoNeighborsFromBoardTwoFromBoundary_WhenOnCorner(int x, int y)
    {
        IEnumerable<ICell> board = PrepareRectangleBoard();
        ICell cellToTest = board.Single(c => c.Coordinates == new Coordinates(x, y));
        _sut = new VonNeumannNeighborhood(board, BoundaryConditionsTypes.Constant);

        IList<ICell> neighbors = _sut.GetNeighbors(cellToTest).ToList();

        neighbors.Should().HaveCount(4);
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x + 0, y - 1));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x - 1, y + 0));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x + 1, y + 0));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x - 0, y + 1));
        neighbors.Count(c => board.Contains(c)).Should().Be(2);

    }

    [Fact]
    public void GetNeighbors_ShouldReturnNull_WhenCellIsNotOnBoard()
    {
        IEnumerable<ICell> board = PrepareRectangleBoard();
        ICell cellOutsideOfBoard = new BooleanCell(30, 30);
        _sut = new VonNeumannNeighborhood(board, BoundaryConditionsTypes.Constant);

        IEnumerable<ICell>? neighbors = _sut.GetNeighbors(cellOutsideOfBoard);

        neighbors.Should().BeNull();
    }

    [Fact]
    public void VonNeumannNeighborhood_ShouldThrowNullArgumentException_WhenCellsCollectionIsNull()
    {
        IEnumerable<ICell> board = null;
        Exception thrownException = null;

        try
        {
            _sut = new VonNeumannNeighborhood(board, BoundaryConditionsTypes.Constant);
        }
        catch (Exception e)
        {
            thrownException = e;
        }

        thrownException.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    private IEnumerable<ICell> PrepareRectangleBoard()
    {
        List<ICell> cells = new List<ICell>();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                ICell? cellSubstitute = Substitute.For<ICell>();
                cellSubstitute.Coordinates.Returns(new Coordinates(i, j));
                cells.Add(cellSubstitute);
            }
        }

        return cells;
    }
}