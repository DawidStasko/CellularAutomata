using FluentAssertions;
using WPFUserInterface.Domain;
using Xunit;

namespace CellularAutomata.Tests.Domain;

public class SimpleCellTests
{
    private BooleanCell _sut; 

    [Fact]
    public void ChangeState_ShouldChangeStateToOpposite_WhenItIsCalled()
    {
        _sut = new BooleanCell(1, 1){State = false}; 

        _sut.ChangeState();
        var afterFirstChange = _sut.State;
        _sut.ChangeState();
        var afterSecondChange = _sut.State;

        afterFirstChange.Should().Be(true);
        afterSecondChange.Should().Be(false);
    }
}