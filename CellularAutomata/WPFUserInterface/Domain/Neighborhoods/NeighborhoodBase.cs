using System.Collections.Generic;
using System.Linq;
using WPFUserInterface.Domain.Boundaries;

namespace WPFUserInterface.Domain.Neighborhoods;

public abstract class NeighborhoodBase:INeighborhood
{
    protected Dictionary<SimpleCell, IEnumerable<SimpleCell>> _neighborhoods = new Dictionary<SimpleCell, IEnumerable<SimpleCell>>();
    protected IBoundary _boundaryCells;
    public virtual IEnumerable<SimpleCell> GetNeighbors(SimpleCell cell)
    {
        _neighborhoods.TryGetValue(cell, out IEnumerable<SimpleCell>? neighbors);
        return neighbors ?? Enumerable.Empty<SimpleCell>();
    }
}