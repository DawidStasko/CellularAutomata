using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using System.Windows.Media.Media3D;
using WPFUserInterface.ViewModels;
using Xunit;

namespace CellularAutomata.Tests.ViewModel;

public class BoardVMTests
{
    private BoardVM _sut;

    [Theory]
    [InlineData(3,3,9)]
    [InlineData(2, 4, 8)]
    [InlineData(5, 3, 15)]
    public async Task DrawBoardCommand_ShouldDrawRectangleBoard_WhenItIsCalled(int width, int height, int cellsAmount)
    {
        _sut = new BoardVM();
        _sut.BoardHeight = height;
        _sut.BoardWidth = width;

        _sut.DrawBoardCommand.Execute(null);

        _sut.Cells.Count.Should().Be(cellsAmount);
    }

    [Fact]
    public void CalculateNextGenerationCommand_ShouldCallCalculateNextGenerationMethodOnRectangleBoard_WhenItIsCalled()
    {
        _sut = new BoardVM();
        _sut.BoardHeight = 3;
        _sut.BoardWidth = 3;
        _sut.DrawBoardCommand.Execute(null);
        double[] cellsValues = _sut.Cells.Select(c => c.Position.X).Distinct().ToArray();
        cellsValues.Length.Should().Be(3);
        var min = cellsValues.Min();
        var max = cellsValues.Max();
        var middle = cellsValues.Single(c => c != min && c != max);
        var cellsToChangeState = _sut.Cells.Where(c => Math.Abs(c.Position.X - middle) < 0.0001);
        foreach (var cell in _sut.Cells)
        {
            cell.State.Should().Be(false);
        }
        foreach (var cellVm in cellsToChangeState)
        {
            cellVm.ChangeStateCommand.Execute(null);
        }


        _sut.CalculateNextGenerationCommand.Execute(null);

        foreach (var cell in _sut.Cells)
        {
            if(Math.Abs(cell.Position.Y - middle) < 0.0001)
            {
                cell.State.Should().Be(true);
                continue;
            }
            cell.State.Should().Be(false);
        }

    }

}