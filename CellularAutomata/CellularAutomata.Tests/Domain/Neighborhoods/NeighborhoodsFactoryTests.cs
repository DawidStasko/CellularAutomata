using System.Collections.Generic;
using FluentAssertions;
using WPFUserInterface.Domain;
using WPFUserInterface.Domain.BoundaryConditions;
using WPFUserInterface.Domain.Neighborhoods;
using Xunit;

namespace CellularAutomata.Tests.Domain.Neighborhoods;

public class NeighborhoodsFactoryTests
{
    [Fact]
    public void Create_ShouldReturnMooreNeighborhood_WhenThisTypeIsChosen()
    {
        IEnumerable<SimpleCell> cells = PrepareRectangleBoard();
        BoundaryConditionsTypes boundaryConditions = BoundaryConditionsTypes.Constant;
        NeighborhoodType neighborhoodType = NeighborhoodType.Moore;

        INeighborhood neighborhood = NeighborhoodsFactory.Create(cells, boundaryConditions, neighborhoodType);

        neighborhood.Should().BeOfType<MooreNeighborhood>(); 
    }

    [Fact]
    public void Create_ShouldReturnVonNeumannNeighborhood_WhenThisTypeIsChosen()
    {
        IEnumerable<SimpleCell> cells = PrepareRectangleBoard();
        BoundaryConditionsTypes boundaryConditions = BoundaryConditionsTypes.Constant;
        NeighborhoodType neighborhoodType = NeighborhoodType.VonNeumann;

        INeighborhood neighborhood = NeighborhoodsFactory.Create(cells, boundaryConditions, neighborhoodType);

        neighborhood.Should().BeOfType<VonNeumannNeighborhood>();
    }

    private IEnumerable<SimpleCell> PrepareRectangleBoard()
    {
        var cells = new List<SimpleCell>();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                cells.Add(new SimpleCell(i, j));
            }
        }

        return cells;
    }
}