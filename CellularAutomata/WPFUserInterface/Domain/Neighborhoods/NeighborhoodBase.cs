using System.Collections.Generic;
using System.Linq;
using WPFUserInterface.Domain.Boundaries;

namespace WPFUserInterface.Domain.Neighborhoods;

public abstract class NeighborhoodBase:INeighborhood
{
    protected Dictionary<BooleanCell, IEnumerable<BooleanCell>> _neighborhoods = new Dictionary<BooleanCell, IEnumerable<BooleanCell>>();
    protected IBoundary _boundaryCells;
    public virtual IEnumerable<BooleanCell>? GetNeighbors(BooleanCell cell)
    {
        _neighborhoods.TryGetValue(cell, out IEnumerable<BooleanCell>? neighbors);
        return neighbors;
    }
}