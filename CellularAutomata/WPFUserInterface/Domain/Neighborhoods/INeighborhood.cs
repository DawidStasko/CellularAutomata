using System.Collections.Generic;

namespace WPFUserInterface.Domain.Neighborhoods;

public interface INeighborhood
{
    IEnumerable<ICell>? GetNeighbors(ICell cell);
}