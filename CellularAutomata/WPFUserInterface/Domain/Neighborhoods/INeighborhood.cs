using System.Collections.Generic;

namespace WPFUserInterface.Domain.Neighborhoods;

public interface INeighborhood
{
    IEnumerable<BooleanCell>? GetNeighbors(BooleanCell cell);
}