using System.ComponentModel;
using FluentAssertions;
using NSubstitute;
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
        var cell = Substitute.For<ICell>();
        _sut = new CellVM(cell);

        _sut.ChangeStateCommand.Execute(null);

        cell.Received().ChangeState();
    }

    [Fact]
    public void OnPropertyChanged_ShouldBeCalled_WhenCellStateIsChanged()
    {
        var cell = Substitute.For<ICell>();
        _sut = new CellVM(cell);
        bool propertyHasChanged=false;
        _sut.PropertyChanged += (o, i) =>
        {
            if(i.PropertyName == nameof(CellVM.State))
            {
                propertyHasChanged = true;
            }
        };
        
        cell.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(cell, new PropertyChangedEventArgs(nameof(ICell.State)));
        
        propertyHasChanged.Should().BeTrue();

    }
}