using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using WPFUserInterface.Domain;
using WPFUserInterface.Domain.BoundaryConditions;
using WPFUserInterface.Domain.Neighborhoods;
using Xunit;

namespace CellularAutomata.Tests.Domain.Neighborhoods;

public class MooreNeighborhoodTests
{
    private MooreNeighborhood _sut;

    [Theory]
    [InlineData(3, 1)]
    [InlineData(3, 3)]
    [InlineData(3, 6)]
    [InlineData(7, 3)]
    public void MooreNeighborhood_ShouldHaveEightNeighborsFromBoard_WhenNotOnBoundary(int x, int y)
    {
        var board = PrepareRectangleBoard();
        var cellToTest = board.Single(c => c.Coordinates == new Coordinates(x, y));
        _sut = new MooreNeighborhood(board, BoundaryConditionsTypes.Constant);

        var neighbors = _sut.GetNeighbors(cellToTest).ToList();

        neighbors.Should().HaveCount(8);
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x - 1, y - 1));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x + 0, y - 1));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x + 1, y - 1));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x - 1, y + 0));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x + 1, y + 0));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x - 1, y + 1));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x - 0, y + 1));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x + 1, y + 1));

        foreach (var processedNeighbor in neighbors)
        {
            board.Should().Contain(processedNeighbor);
        }
    }

    [Theory]
    [InlineData(3, 0)]
    [InlineData(0, 3)]
    [InlineData(3, 9)]
    [InlineData(9, 3)]
     public void MooreNeighborhood_ShouldHaveFiveNeighborsFromBoardThreeFromBoundary_WhenOnBoundaryButNotInCorner(int x, int y)
    {
        var board = PrepareRectangleBoard();
        var cellToTest = board.Single(c => c.Coordinates == new Coordinates(x, y));
        _sut = new MooreNeighborhood(board, BoundaryConditionsTypes.Constant);
        
        var neighbors = _sut.GetNeighbors(cellToTest).ToList();

        neighbors.Should().HaveCount(8);
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x - 1, y - 1));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x + 0, y - 1));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x + 1, y - 1));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x - 1, y + 0));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x + 1, y + 0));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x - 1, y + 1));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x - 0, y + 1));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x + 1, y + 1));
        neighbors.Count(c => board.Contains(c)).Should().Be(5);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 9)]
    [InlineData(9, 0)]
    [InlineData(9, 9)]
    public void MooreNeighborhood_ShouldHaveThreeNeighborsFromBoardFiveFromBoundary_WhenOnCorner(int x, int y)
    {
        var board = PrepareRectangleBoard();
        var cellToTest = board.Single(c => c.Coordinates == new Coordinates(x, y));
        _sut = new MooreNeighborhood(board, BoundaryConditionsTypes.Constant);

        var neighbors = _sut.GetNeighbors(cellToTest).ToList();

        neighbors.Should().HaveCount(8);
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x - 1, y - 1));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x + 0, y - 1));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x + 1, y - 1));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x - 1, y + 0));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x + 1, y + 0));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x - 1, y + 1));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x - 0, y + 1));
        neighbors.Should().Contain(c => c.Coordinates == new Coordinates(x + 1, y + 1));
        neighbors.Count(c => board.Contains(c)).Should().Be(3);

    }

    [Fact]
    public void MooreNeighborhood_ShouldReturnNull_WhenCellIsNotOnBoard()
    {
        var board = PrepareRectangleBoard();
        var cellOutsideOfBoard = new BooleanCell(30, 30);
        _sut = new MooreNeighborhood(board, BoundaryConditionsTypes.Constant);

        var neighbors = _sut.GetNeighbors(cellOutsideOfBoard);

        neighbors.Should().BeNull();
    }


    private IEnumerable<BooleanCell> PrepareRectangleBoard()
    {
        var cells = new List<BooleanCell>();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                cells.Add(new BooleanCell(i,j));
            }
        }

        return cells;
    }
}