using WPFUserInterface.Domain.Neighborhoods;
using WPFUserInterface.Domain.BoundaryConditions;
namespace WPFUserInterface.Domain;

public class BoardData
{
    public int Width { get; set; }
    public int Height { get; set; }
    public NeighborhoodType NeighborhoodType { get; set; }
    public BoundaryConditionsTypes BoundaryConditionType { get; set; }
}