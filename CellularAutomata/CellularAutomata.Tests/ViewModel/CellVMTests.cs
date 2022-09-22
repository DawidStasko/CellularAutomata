using FluentAssertions;
using WPFUserInterface.Domain;
using WPFUserInterface.ViewModels;
using Xunit;

namespace CellularAutomata.Tests.ViewModel;

public class CellVMTests
{
    private CellVM _sut;

    [Fact]
    public void ChangeState_ShouldCallChangeStateOnSimpleCell_WhenMethodIsCalled()
    {
        var cell = new BooleanCell();
        _sut = new CellVM(cell);
        _sut.State.Should().BeFalse();
        cell.State.Should().BeFalse();

        _sut.ChangeStateCommand.Execute(null);

        _sut.State.Should().BeTrue();
        cell.State.Should().BeTrue();
    }

    [Fact]
    public void OnPropertyChanged_ShouldBeCalled_WhenCellStateIsChanged()
    {
        var cell = new BooleanCell();
        _sut = new CellVM(cell);
        bool propertyHasChanged=false;
        _sut.PropertyChanged += (o, i) =>
        {
            if(i.PropertyName == nameof(CellVM.State))
            {
                propertyHasChanged = true;
            }
        };

        cell.State = false;

        propertyHasChanged.Should().BeTrue();

    }
}