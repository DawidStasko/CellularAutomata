using System;
using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using WPFUserInterface.Domain;
using WPFUserInterface.Domain.BoundaryConditions;
using WPFUserInterface.Domain.Neighborhoods;
using Xunit;

namespace CellularAutomata.Tests.Domain.Neighborhoods;

public class NeighborhoodsFactoryTests
{
    [Fact]
    public void Create_ShouldThrowArgumentNullException_WhenCellsCollectionIsNull()
    {
        IEnumerable<ICell> cells = null;
        BoundaryConditionsTypes boundaryConditions = BoundaryConditionsTypes.Constant;
        NeighborhoodType neighborhoodType = NeighborhoodType.Moore;
        Exception? thrownException = null;

        try
        {
            var _sut = NeighborhoodsFactory.Create(cells, boundaryConditions, neighborhoodType);
        }
        catch (Exception e)
        {
            thrownException = e;
        }

        thrownException.Should().NotBeNull();
        thrownException.Should().BeOfType<ArgumentNullException>();
    }


    [Fact]
    public void Create_ShouldReturnMooreNeighborhood_WhenThisTypeIsChosen()
    {
        IEnumerable<ICell> cells = PrepareRectangleBoard();
        BoundaryConditionsTypes boundaryConditions = BoundaryConditionsTypes.Constant;
        NeighborhoodType neighborhoodType = NeighborhoodType.Moore;

        INeighborhood neighborhood = NeighborhoodsFactory.Create(cells, boundaryConditions, neighborhoodType);

        neighborhood.Should().BeOfType<MooreNeighborhood>(); 
    }

    [Fact]
    public void Create_ShouldReturnVonNeumannNeighborhood_WhenThisTypeIsChosen()
    {
        IEnumerable<ICell> cells = PrepareRectangleBoard();
        BoundaryConditionsTypes boundaryConditions = BoundaryConditionsTypes.Constant;
        NeighborhoodType neighborhoodType = NeighborhoodType.VonNeumann;

        INeighborhood neighborhood = NeighborhoodsFactory.Create(cells, boundaryConditions, neighborhoodType);

        neighborhood.Should().BeOfType<VonNeumannNeighborhood>();
    }

    private IEnumerable<ICell> PrepareRectangleBoard()
    {
        var cells = new List<ICell>();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                var cellSubstitute = Substitute.For<ICell>();
                cellSubstitute.Coordinates.Returns(new Coordinates(i, j));
                cells.Add(cellSubstitute);
            }
        }

        return cells;
    }
}