using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Media.Media3D;
using FluentAssertions;
using WPFUserInterface.Domain;
using WPFUserInterface.Domain.BoundaryConditions;
using WPFUserInterface.Domain.Neighborhoods;
using Xunit;

namespace CellularAutomata.Tests.Domain;

public class RectangleBoardTests
{
    private RectangleBoard _sut;

    public RectangleBoardTests()
    {
        BoardData data = new BoardData()
        {
            Height = 3,
            Width = 3,
            BoundaryConditionType = BoundaryConditionsTypes.Constant,
            NeighborhoodType = NeighborhoodType.Moore
        };
        _sut = new RectangleBoard(data);
    }

    [Theory]
    [InlineData(3, 3, 9)]
    [InlineData(3, 4, 12)]
    public void RectangleBoard_ShouldCreateCellsInGridWithAmountEqualsToWidthMultiplyByHeight_OnCreation(int width, int height, int cellsAmount)
    {
        BoardData data = new BoardData()
        {
            Height = height,
            Width = width,
            BoundaryConditionType = BoundaryConditionsTypes.Constant,
            NeighborhoodType = NeighborhoodType.VonNeumann
        };

        _sut = new RectangleBoard(data);

        _sut.Cells.Count().Should().Be(cellsAmount);
    }
    
    #region Tests for cases when cell state should be set to true

    [Fact]
    public void CalculateNextGeneration_ShouldSetCellStateToBeTrue_WhenAliveCellHasTwoAliveNeighbors()
    {
        //Arrange
        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 1)).ChangeState();
        

        //Act
        _sut.CalculateNextGeneration();

        //Assert
        foreach (var cell in _sut.Cells)
        {
            if (cell.Coordinates.X==1)
            {
                cell.State.Should().BeTrue();
                continue;
            }

            cell.State.Should().BeFalse();
        }

    }

    [Fact]
    public void CalculateNextGeneration_ShouldSetCellStateToBeTrue_WhenAliveCellHasThreeAliveNeighbors()
    {
        //Arrange
        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 0)).ChangeState();


        //Act
        _sut.CalculateNextGeneration();

        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 0)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 0)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 0)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 1)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 1)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 2)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 2)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 2)).State.Should().BeFalse();
    }

    [Fact]
    public void CalculateNextGeneration_ShouldSetCellStateToBeTrue_WhenDeadCellHasThreeLiveNeighbors()
    {
        //Arrange
        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 2)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 1)).ChangeState();

        //Act
        _sut.CalculateNextGeneration();

        //Assert
        foreach (var cell in _sut.Cells)
        {
            if (cell.Coordinates == new Coordinates(1, 1))
            {
                cell.State.Should().BeTrue();
                continue;
            }

            cell.State.Should().BeFalse();
        }
    }

    #endregion

    #region Tests for cases when cell should be set to false.

    [Fact]
    public void CalculateNextGeneration_ShouldSetCellStateToBeFalse_WhenAliveCellHasZeroAliveNeighbors()
    {
        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 1)).ChangeState();

        _sut.CalculateNextGeneration();

        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
    }

    [Fact]
    public void CalculateNextGeneration_ShouldSetCellStateToBeFalse_WhenAliveCellHasOneAliveNeighbors()
    {
        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 1)).ChangeState();

        _sut.CalculateNextGeneration();

        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
    }

    [Fact]
    public void CalculateNextGeneration_ShouldSetCellStateToBeFalse_WhenAliveCellHasFourAliveNeighbors()
    {
        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 2)).ChangeState();

        _sut.CalculateNextGeneration();

        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 0)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 0)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 0)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 1)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 2)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 2)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 2)).State.Should().BeTrue();
    }

    [Fact]
    public void CalculateNextGeneration_ShouldSetCellStateToBeFalse_WhenAliveCellHasFiveAliveNeighbors()
    {
        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 2)).ChangeState();

        _sut.CalculateNextGeneration();

        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 0)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 0)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 0)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 1)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 2)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 2)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 2)).State.Should().BeTrue();
    }

    [Fact]
    public void CalculateNextGeneration_ShouldSetCellStateToBeFalse_WhenAliveCellHasSixAliveNeighbors()
    {
        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 1)).ChangeState(); 
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 2)).ChangeState();
        

        _sut.CalculateNextGeneration();

        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 0)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 0)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 0)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 2)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 2)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 2)).State.Should().BeTrue();
    }

    [Fact]
    public void CalculateNextGeneration_ShouldSetCellStateToBeFalse_WhenAliveCellHasSevenAliveNeighbors()
    {
        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 2)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 2)).ChangeState();

        _sut.CalculateNextGeneration();

        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 0)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 0)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 0)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 2)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 2)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 2)).State.Should().BeTrue();
    }

    [Fact]
    public void CalculateNextGeneration_ShouldSetCellStateToBeFalse_WhenAliveCellHasEightAliveNeighbors()
    {
        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 2)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 2)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 2)).ChangeState();

        _sut.CalculateNextGeneration();

        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 0)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 0)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 0)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 2)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 2)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 2)).State.Should().BeTrue();
    }


    [Fact]
    public void CalculateNextGeneration_ShouldSetCellStateToBeFalse_WhenDeadCellHasZeroAliveNeighbors()
    {
        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }

        _sut.CalculateNextGeneration();

        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
    }

    [Fact]
    public void CalculateNextGeneration_ShouldSetCellStateToBeFalse_WhenDeadCellHasOneAliveNeighbors()
    {
        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).ChangeState();

        _sut.CalculateNextGeneration();

        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
    }

    [Fact]
    public void CalculateNextGeneration_ShouldSetCellStateToBeFalse_WhenDeadCellHasTwoAliveNeighbors()
    {
        //Arrange
        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }

        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 0)).ChangeState();


        //Act
        _sut.CalculateNextGeneration();

        //Assert
        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
    }

    [Fact]
    public void CalculateNextGeneration_ShouldSetCellStateToBeFalse_WhenDeadCellHasFourAliveNeighbors()
    {
        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 2)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 2)).ChangeState();

        _sut.CalculateNextGeneration();

        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
    }

    [Fact]
    public void CalculateNextGeneration_ShouldSetCellStateToBeFalse_WhenDeadCellHasFiveAliveNeighbors()
    {
        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 2)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 2)).ChangeState();

        _sut.CalculateNextGeneration();

        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 0)).State.Should().BeFalse(); 
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 0)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 0)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 1)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 2)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 2)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 2)).State.Should().BeFalse();
    }

    [Fact]
    public void CalculateNextGeneration_ShouldSetCellStateToBeFalse_WhenDeadCellHasSixAliveNeighbors()
    {
        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 2)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 2)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 2)).ChangeState();

        _sut.CalculateNextGeneration();

        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 0)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 0)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 0)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 2)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 2)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 2)).State.Should().BeFalse();
    }

    [Fact]
    public void CalculateNextGeneration_ShouldSetCellStateToBeFalse_WhenDeadCellHasSevenAliveNeighbors()
    {
        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 2)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 2)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 2)).ChangeState();

        _sut.CalculateNextGeneration();

        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 0)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 0)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 0)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 2)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 2)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 2)).State.Should().BeFalse();
    }

    [Fact]
    public void CalculateNextGeneration_ShouldSetCellStateToBeFalse_WhenDeadCellHasEightAliveNeighbors()
    {
        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().BeFalse();
        }
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 0)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 1)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 2)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 2)).ChangeState();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 2)).ChangeState();

        _sut.CalculateNextGeneration();

        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 0)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 0)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 0)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 1)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(0, 2)).State.Should().BeTrue();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(1, 2)).State.Should().BeFalse();
        _sut.Cells.First(c => c.Coordinates == new Coordinates(2, 2)).State.Should().BeTrue();
    }

    #endregion

}