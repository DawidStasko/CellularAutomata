using FluentAssertions;
using WPFUserInterface.Domain;
using Xunit;

namespace CellularAutomata.Tests.Domain;

public class SimpleCellTests
{
    private SimpleCell _sut; 

    [Fact]
    public void ChangeState_ShouldChangeStateToOpposite_WhenItIsCalled()
    {
        _sut = new SimpleCell(1, 1){State = false}; 

        _sut.ChangeState();
        var afterFirstChange = _sut.State;
        _sut.ChangeState();
        var afterSecondChange = _sut.State;

        afterFirstChange.Should().Be(true);
        afterSecondChange.Should().Be(false);
    }
}