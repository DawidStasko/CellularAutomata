using System.Collections;
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
                Enumerable.Empty<SimpleCell>(), 10, 10);
        
        _sut.Should().NotBeNull();
        _sut.Should().BeOfType<ConstantBoundary>();
    }
}