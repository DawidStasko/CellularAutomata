﻿using System.Collections;
using System.Collections.Generic;

namespace WPFUserInterface.Domain.Boundaries;

public interface IBoundary
{
    IEnumerable<BooleanCell> BoundaryCells { get; }
}