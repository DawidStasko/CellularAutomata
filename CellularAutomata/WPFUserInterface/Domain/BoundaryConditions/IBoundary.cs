using System.Collections;
using System.Collections.Generic;

namespace WPFUserInterface.Domain.Boundaries;

public interface IBoundary
{
    IEnumerable<ICell> BoundaryCells { get; }
}