using System.Collections.Generic;

namespace WPFUserInterface.Domain;

public interface INeighborhood
{
    IEnumerable<SimpleCell> GetNeighbors(SimpleCell cell);
}