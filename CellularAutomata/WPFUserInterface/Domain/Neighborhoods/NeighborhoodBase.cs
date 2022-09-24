using System.Collections.Generic;
using System.Linq;
using WPFUserInterface.Domain.Boundaries;

namespace WPFUserInterface.Domain.Neighborhoods;

public abstract class NeighborhoodBase:INeighborhood
{
    protected Dictionary<ICell, IEnumerable<ICell>> _neighborhoods = new Dictionary<ICell, IEnumerable<ICell>>();
    protected IBoundary _boundaryCells;
    public virtual IEnumerable<ICell>? GetNeighbors(ICell cell)
    {
        _neighborhoods.TryGetValue(cell, out IEnumerable<ICell>? neighbors);
        return neighbors;
    }
}