using System.Collections;
using System.Collections.Generic;
using Archero.UnitSystem;
using UnityEngine;

namespace Archero.UnitSystem
{
    public interface IUnitFactory
    {
        Unit CreateUnit(string id, IReadOnlyCollection<UnitStat> stats);
    }
}
