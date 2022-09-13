using System.Collections.Generic;

namespace WPFUserInterface.Domain.Neighborhoods;

public interface INeighborhood
{
    IEnumerable<SimpleCell> GetNeighbors(SimpleCell cell);
}