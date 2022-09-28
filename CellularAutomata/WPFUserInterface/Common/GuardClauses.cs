using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Ardalis.GuardClauses;

namespace WPFUserInterface.Common;

public static class GuardClauses
{
    public static IEnumerable<T> EmptyCollection<T>(this IGuardClause clause, IEnumerable<T> collection, string paramName ="" )
    {
        if (!collection.Any())
        {
            throw new ArgumentException("Collection must have at least one element.");
        }

        return collection;
    }

}